using System;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;

namespace cstest {
	class MainClass {
		[STAThread]
		public static void Main(string[] args) {
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Form1());
		}
	}
	public class Form1 : Form {
		//////////////////////////////////////////////////////////////////////////////////////////AUTO-GENERATED
		private System.ComponentModel.IContainer components = null;

		public Form1() => InitializeComponent();

		protected override void Dispose(bool disposing) {
			if(disposing && (components != null)) components.Dispose();
			base.Dispose(disposing);
		}

		public void InitializeComponent() {
			SuspendLayout();
			AutoScaleDimensions = new SizeF(6F, 13F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(800, 450);
			Name = "Form1";
			Text = "Form1";
			ResumeLayout(false);
			Init();
		}
		//////////////////////////////////////////////////////////////////////////////////////////CODE

		private void Draw(object sender, PaintEventArgs e) {
			Pen pen = new Pen(Color.LemonChiffon, 1f);
			for(int j = 0; j < gridSize.Height; ++j)
				for(int i = 0; i < gridSize.Width; ++i)
					if(ca[i, j] == 1)
						e.Graphics.FillRectangle(pen.Brush, cellSize.Width * i, cellSize.Height * j, cellSize.Width, cellSize.Height);
		}

		Size cellSize;
		Size gridSize;
		int[,] ca;

		Func<int[,], bool> conway;

		private void Init() {
			cellSize = new Size(10, 10);
			gridSize = new Size(30, 30);

			ClientSize = Mul(cellSize, gridSize);

			PictureBox pictureBox = new PictureBox {
				BackColor = Color.Black,
				ClientSize = ClientSize,
				Location = new Point(0, 0)
			};

			pictureBox.Paint += Draw;
			pictureBox.Click += Tick;
			Controls.Add(pictureBox);

			ca = new int[gridSize.Width, gridSize.Height];
			ca = FillCA(ca, (i, j) => j == 3 && i > 0 && i < 4 || j == 2 && i == 3 || j == 1 && i == 2);

			conway = g => Flatten(g).Sum() == 3 || (Flatten(g).Sum() == 4 && g[1, 1] == 1);
		}

		private void Tick(object sender, EventArgs e) {
			ca = UpdateCA(ca, conway);
			Refresh();
		}

		int[,] FillCA(int[,] grid, Func<int,int,bool> f){
			for(int j = 0; j < gridSize.Height; ++j)
				for(int i = 0; i < gridSize.Width; ++i)
					if(f(i, j))
						grid[i, j] = 1;
					else
						grid[i, j] = 0;
			return grid;
		}

		int[,] UpdateCA(int[,] grid, Func<int[,], bool> f) {
			int x = grid.GetLength(0);
			int y = grid.GetLength(1);
			int[,] res = (int[,])grid.Clone();
			int[,][,] convGrid = ConvoluteGrid(grid);
			for(int j = 0; j < y; ++j)
				for(int i = 0; i < x; ++i)
					res[i, j] = f(convGrid[i, j]) ? 1 : 0;
			return res;
		}

		T[,][,] ConvoluteGrid<T>(T[,] grid) {
			int x = grid.GetLength(0);
			int y = grid.GetLength(1);
			// ie, T[,][,] res = new T[x, y][3, 3];
			T[,][,] res = new T[x, y][,];
			for(int j = 0; j < y; ++j)
				for(int i = 0; i < x; ++i)
					res[i,j] = new T[3, 3];
			
			for(int j = 0; j < y; ++j)
				for(int i = 0; i < x; ++i){
					res[i, j] = new T[,]{
						{ grid[Mod(i - 1, x), Mod(j - 1, y)], grid[Mod(i - 0, x), Mod(j - 1, y)], grid[Mod(i + 1, x), Mod(j - 1, y)] },
						{ grid[Mod(i - 1, x), Mod(j - 0, y)], grid[Mod(i - 0, x), Mod(j - 0, y)], grid[Mod(i + 1, x), Mod(j - 0, y)] },
						{ grid[Mod(i - 1, x), Mod(j + 1, y)], grid[Mod(i - 0, x), Mod(j + 1, y)], grid[Mod(i + 1, x), Mod(j + 1, y)] }
					};
				}
			return res;
		}

		Size Mul(Size a, Size b) => new Size(a.Width * b.Width, a.Height * b.Height);
		int Mod(int a, int b) => ((a % b) + b) % b;

		// Pretty(kinda) Print
		string Print<T>(T[] arr) {
			int x = arr.Length;
			string res = "";
			for(int i = 0; i < x; ++i)
				res += arr[i] + " ";
			return res;
		}

		string Print<T>(T[,] grid) {
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

		string Print2<T>(T[,][,] grid) {
			int x = grid.GetLength(0);
			int y = grid.GetLength(1);
			string res = "";
			for(int j = 0; j < y; ++j)
				for(int i = 0; i < x; ++i)
					res += Print(grid[i, j]) + "\n\n\n";
			return res;
		}

		// Array Manipulation
		T[] Flatten<T>(T[,] arr) {
			int x = arr.GetLength(0);
			int y = arr.GetLength(1);
			T[] res = new T[x * y];
			for(int i = 0; i < x; ++i)
				for(int j = 0; j < y; ++j)
					res[i + j * x] = arr[i, j];
			return res;
		}
		T[] Drop<T>(T[] arr, int i) => arr.Take(i).Concat(arr.Skip(i + 1)).ToArray();
	}
}