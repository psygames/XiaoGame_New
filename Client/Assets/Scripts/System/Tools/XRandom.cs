using System;

class XRandom
{
    private long seed;

    private const long multiplier = 0x5DEECE66DL;
    private const long addend = 0xBL;
    private const long mask = (1L << 48) - 1;

    private const double DOUBLE_UNIT = 1.0 / (1L << 53);

    const String BadBound = "bound must be positive";

    public static long nanoTime()
    {
        return DateTime.Now.Ticks * 100L; 
    }

    public XRandom() : this(seedUniquifier() ^ nanoTime())
    {

    }

    private static long seedUniquifier()
    {
        for (;;)
        {
            long current = _seedUniquifier;
            long next = current * 181783497276652981L;
            if (_seedUniquifier == current)
            {
                _seedUniquifier = next;
                return next;
            }
        }
    }

    private static long _seedUniquifier = 8682522807148012L;

    public XRandom(long seed)
    {
        if (GetType() == typeof(XRandom))
            this.seed = initialScramble(seed);
        else
        {
            this.seed = 0L;
            setSeed(seed);
        }
    }

    private static long initialScramble(long seed)
    {
        return (seed ^ multiplier) & mask;
    }

    public void setSeed(long seed)
    {
        lock (this)
        {
            this.seed = initialScramble(seed);
            haveNextNextGaussian = false;
        }
    }

    protected int next(int bits)
    {
        long oldseed, nextseed;
        bool isSeedEqual = false;
        do
        {
            oldseed = seed;
            nextseed = (oldseed * multiplier + addend) & mask;
            if (seed == oldseed)
            {
                seed = nextseed;
                isSeedEqual = true;
            }
        } while (!isSeedEqual);
        return (int)(move_fill_0(nextseed, 48 - bits));
    }

    public void nextBytes(byte[] bytes)
    {
        for (int i = 0, len = bytes.Length; i < len;)
            for (int rnd = nextInt(), n = Math.Min(len - i, 32
                    / 8); n-- > 0; rnd >>= 8)
                bytes[i++] = (byte)rnd;
    }

    public int nextInt()
    {
        return next(32);
    }

    public int nextInt(int bound)
    {
        if (bound <= 0)
            throw new ArgumentException(BadBound);

        int r = next(31);
        int m = bound - 1;
        if ((bound & m) == 0)
            r = (int)((bound * (long)r) >> 31);
        else
        {
            for (int u = r; u - (r = u % bound) + m < 0; u = next(31))
                ;
        }
        return r;
    }

    public long nextLong()
    {
        return ((long)(next(32)) << 32) + next(32);
    }

    public bool nextBoolean()
    {
        return next(1) != 0;
    }

    public float nextFloat()
    {
        return next(24) / ((float)(1 << 24));
    }

    public double nextDouble()
    {
        return (((long)(next(26)) << 27) + next(27)) * DOUBLE_UNIT;
    }

    private double nextNextGaussian;
    private bool haveNextNextGaussian = false;

    public double nextGaussian()
    {
        lock (this)
        {
            if (haveNextNextGaussian)
            {
                haveNextNextGaussian = false;
                return nextNextGaussian;
            }
            else
            {
                double v1, v2, s;
                do
                {
                    v1 = 2 * nextDouble() - 1;
                    v2 = 2 * nextDouble() - 1;
                    s = v1 * v1 + v2 * v2;
                } while (s >= 1 || s == 0);
                double multiplier = Math.Sqrt(-2 * Math.Log(s) / s);
                nextNextGaussian = v2 * multiplier;
                haveNextNextGaussian = true;
                return v1 * multiplier;
            }
        }
    }

    public static long move_fill_0(long value, int bits)
    {
        long mask = long.MaxValue;
        for (int i = 0; i < bits; i++)
        {
            value >>= 1;
            value &= mask;
        }
        return value;
    }
}
