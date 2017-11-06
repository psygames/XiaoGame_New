using System;
using System.Collections.Generic;
using System.Text;

namespace Coolfish.System
{
    public static class EnumerableExtend
    {
        public static bool Any<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
                throw new ArgumentException("source");
            var e = source.GetEnumerator();
            return e.MoveNext();
        }

        public static bool Any<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source == null)
                throw new ArgumentException("source");
            if (predicate == null)
                throw new ArgumentException("predicate");
            var element = source.GetEnumerator();
            while (element.MoveNext())
            {
                if (predicate(element.Current))
                    return true;
            }
            return false;
        }

        public static bool All<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source == null)
                throw new ArgumentException("source");
            if (predicate == null)
                throw new ArgumentException("predicate");
            var element = source.GetEnumerator();
            while (element.MoveNext())
            {
                if (!predicate(element.Current))
                    return false;
            }
            return true;
        }

        public static TSource First<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source == null)
                throw new ArgumentException("source");
            if (predicate == null)
                throw new ArgumentException("predicate");
            var element = source.GetEnumerator();
            while (element.MoveNext())
            {
                if (predicate(element.Current))
                    return element.Current;
            }
            return default(TSource);
        }

        public static TSource First<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
                throw new ArgumentException("source");
            IList<TSource> list = source as IList<TSource>;
            if (list != null)
            {
                if (list.Count > 0)
                    return list[0];
            }
            else
            {
                var e = source.GetEnumerator();
                if (e.MoveNext())
                    return e.Current;
            }
            return default(TSource);
        }


        public static TSource Last<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
                throw new ArgumentException("source");
            IList<TSource> list = source as IList<TSource>;
            if (list != null)
            {
                int count = list.Count;
                if (count > 0)
                    return list[count - 1];
            }
            else
            {
                var e = source.GetEnumerator();
                if (e.MoveNext())
                {
                    TSource result;

                    do
                    {
                        result = e.Current;
                    } while (e.MoveNext());

                    return result;

                }
            }
            return default(TSource);
        }

        public static TSource Last<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source == null)
                throw new ArgumentException("source");
            if (predicate == null)
                throw new ArgumentException("predicate");
            TSource result = default(TSource);
            var element = source.GetEnumerator();
            while (element.MoveNext())
            {
                if (predicate(element.Current))
                {
                    result = element.Current;
                }
            }
            return result;
        }

        public static int QuickFindIndex<TSource>(this List<TSource> source, Func<TSource, bool> predicate)
        {
            for (int i = 0; i < source.Count; i++)
            {
                if (predicate(source[i]))
                    return i;
            }
            return -1;
        }

        public static List<TSource> FindAllByPool<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate = null)
        {
            return ToListFromPool(source, predicate);
        }

        public static List<TSource> ToListFromPool<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate = null)
        {
            if (source == null)
                throw new ArgumentException("source");
            List<TSource> list = ListPool<TSource>.Get();
            var element = source.GetEnumerator();
            while (element.MoveNext())
            {
                if (predicate == null || predicate(element.Current))
                {
                    list.Add(element.Current);
                }
            }
            return list;
        }
        public static void ReleaseToPool<TSource>(this List<TSource> source)
        {
            if (source == null)
                throw new ArgumentException("source");
            ListPool<TSource>.Release(source);
        }

    }
}
