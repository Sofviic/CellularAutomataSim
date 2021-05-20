using System.Drawing;

namespace CASim {
	public static class ExtendedFunctions {
		public static Size Mul(this Size a, Size b) => new Size(a.Width * b.Width, a.Height * b.Height);
	}
}
