using System;
using System.Drawing;
using System.Linq;

namespace CASim {
	public static class CA {
		public static int[,] FillCA(this int[,] grid, Func<int, int, bool> f) {
			var (x, y) = grid.Size();
			for(int j = 0; j < y; ++j)
				for(int i = 0; i < x; ++i)
					if(f(i, j))
						grid[i, j] = 1;
					else
						grid[i, j] = 0;
			return grid;
		}

		public static int[,] UpdateCA(this int[,] grid, Func<int[,], bool> f) {
			var (x, y) = grid.Size();
			int[,] res = (int[,])grid.Clone();
			int[,][,] convGrid = grid.ConvoluteGrid();
			for(int j = 0; j < y; ++j)
				for(int i = 0; i < x; ++i)
					res[i, j] = f(convGrid[i, j]) ? 1 : 0;
			return res;
		}

		public static Bitmap Draw(this int[,] grid, Size pixelPerCell) {
			var (x, y) = grid.Size();
			Pen pen = new Pen(Color.LemonChiffon, 1f);
			Bitmap img = new Bitmap(x * pixelPerCell.Width, y * pixelPerCell.Height);
			using(Graphics g = Graphics.FromImage(img)) {
				for(int j = 0; j < y; ++j)
					for(int i = 0; i < x; ++i)
						if(grid[i, j] == 1)
							g.FillRectangle(pen.Brush, pixelPerCell.Width * i, pixelPerCell.Height * j, pixelPerCell.Width, pixelPerCell.Height);
			}
			return img;
		}

		public static Bitmap Draw(this int[,] grid, Size pixelPerCell, int[,] oldGrid, Bitmap oldImg) {
			var (x, y) = grid.Size();
			Pen pen = new Pen(Color.LemonChiffon, 1f);
			Pen clr = new Pen(Color.Black, 1f);
			using(Graphics g = Graphics.FromImage(oldImg)) {
				for(int j = 0; j < y; ++j)
					for(int i = 0; i < x; ++i)
						if(grid[i, j] != oldGrid[i, j])
							g.FillRectangle(grid[i, j] == 1 ? pen.Brush : clr.Brush, pixelPerCell.Width * i, pixelPerCell.Height * j, pixelPerCell.Width, pixelPerCell.Height);
			}
			return oldImg;
		}

		public static Func<int[,], bool> conway = g => g.Flatten().Sum() == 3 || g.Flatten().Sum() == 4 && g[1, 1] == 1;

		public static (int, int) Size<T>(this T[,] grid) => (grid.GetLength(0), grid.GetLength(1));
	}
}
