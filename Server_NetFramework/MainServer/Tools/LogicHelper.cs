using System;
public static class LogicHelper
{
	public static class Gomuku
	{
		public const int row = 12;
		public const int column = 12;
		public const int cellSize = 120;
		public static Vector2 GetChessPos(int num)
		{
			return GetChessPos(num / column, num % column);
		}

		public static Vector2 GetChessPos(int x, int y)
		{
			float posx = (x - row / 2) * cellSize + 0.5f * cellSize;
			float posy = (y - column / 2) * cellSize + 0.5f * cellSize;
			return new Vector2(posx, posy);
		}
	}
}
