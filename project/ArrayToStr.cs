namespace CASim {
	public static class ArrayToStr {
		public static string Show<T>(T[] arr) {
			int x = arr.Length;
			string res = "";
			for(int i = 0; i < x; ++i)
				res += arr[i] + " ";
			return res;
		}

		public static string Show2D<T>(T[,] grid) {
			int x = grid.GetLength(0);
			int y = grid.GetLength(1);
			string res = "";
			for(int j = 0; j < y; ++j) {
				for(int i = 0; i < x; ++i)
					res += grid[i, j] + " ";
				res += "\n";
			}
			return res;
		}

		public static string Show2D2D<T>(T[,][,] grid) {
			int x = grid.GetLength(0);
			int y = grid.GetLength(1);
			string res = "";
			for(int j = 0; j < y; ++j)
				for(int i = 0; i < x; ++i)
					res += Show2D(grid[i, j]) + "\n\n\n";
			return res;
		}
	}
}
