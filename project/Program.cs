using System;
using System.Windows.Forms;
using System.Drawing;

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

		private void paint_something(object sender, PaintEventArgs e) {
			Pen pen = new Pen(Color.LemonChiffon, 1f);

			for(int j = 0; j < gridSize.Height; ++j)
				for(int i = 0; i < gridSize.Width; ++i)
					if((j + i) % 2 == 0)
						e.Graphics.FillRectangle(pen.Brush, cellSize.Width * i, cellSize.Height * j, cellSize.Width, cellSize.Height);
			
		}
		
		Size cellSize = new Size(10, 10);
		Size gridSize = new Size(30, 30);

		private void Init() {
			ClientSize = Mul(cellSize, gridSize);

			PictureBox pictureBox = new PictureBox {
				BackColor = Color.Black,
				ClientSize = ClientSize,
				Location = new Point(0, 0)
			};

			pictureBox.Paint += paint_something;
			Controls.Add(pictureBox);
		}

		Size Mul(Size a, Size b) => new Size(a.Width * b.Width, a.Height * b.Height);
	}
}