using System.Linq;

namespace CASim {
	public static class ArrayManip {
		public static T[,][,] ConvoluteGrid<T>(this T[,] grid) {
			int x = grid.GetLength(0);
			int y = grid.GetLength(1);
			// ie, T[,][,] res = new T[x, y][3, 3];
			T[,][,] res = new T[x, y][,];
			for(int j = 0; j < y; ++j)
				for(int i = 0; i < x; ++i)
					res[i, j] = new T[3, 3];

			for(int j = 0; j < y; ++j)
				for(int i = 0; i < x; ++i) {
					res[i, j] = new T[,]{
						{ grid[Mod(i - 1, x), Mod(j - 1, y)], grid[Mod(i - 0, x), Mod(j - 1, y)], grid[Mod(i + 1, x), Mod(j - 1, y)] },
						{ grid[Mod(i - 1, x), Mod(j - 0, y)], grid[Mod(i - 0, x), Mod(j - 0, y)], grid[Mod(i + 1, x), Mod(j - 0, y)] },
						{ grid[Mod(i - 1, x), Mod(j + 1, y)], grid[Mod(i - 0, x), Mod(j + 1, y)], grid[Mod(i + 1, x), Mod(j + 1, y)] }
					};
				}
			return res;
		}

		public static T[] Flatten<T>(this T[,] arr) {
			int x = arr.GetLength(0);
			int y = arr.GetLength(1);
			T[] res = new T[x * y];
			for(int i = 0; i < x; ++i)
				for(int j = 0; j < y; ++j)
					res[i + j * x] = arr[i, j];
			return res;
		}
		public static T[] Drop<T>(this T[] arr, int i) => arr.Take(i).Concat(arr.Skip(i + 1)).ToArray();

		private static int Mod(int a, int b) => ((a % b) + b) % b;
	}
}
