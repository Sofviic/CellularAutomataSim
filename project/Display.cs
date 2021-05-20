using System;
using System.Drawing;
using System.Windows.Forms;

namespace CASim {
	public class Display : Form {

		private System.ComponentModel.IContainer components = null;
		public Display() => InitializeComponent();
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

		private void Draw(object sender, PaintEventArgs e) {
			buf = (fbuf ? ca : ca2).Draw(cellSize, (fbuf ? ca2 : ca), buf);
			e.Graphics.DrawImage(buf, 0, 0);
		}

		Size cellSize;
		Size gridSize;
		int[,] ca;
		int[,] ca2;
		bool fbuf = true;
		Bitmap buf;

		private void Init() {
			cellSize = new Size(10, 10);
			gridSize = new Size(30, 30);

			ClientSize = gridSize.Mul(cellSize);

			PictureBox pictureBox = new PictureBox {
				BackColor = Color.Black,
				ClientSize = ClientSize,
				Location = new Point(0, 0)
			};

			pictureBox.Paint += Draw;
			pictureBox.Click += Tick;
			Controls.Add(pictureBox);

			ca = new int[gridSize.Width, gridSize.Height];
			ca = ca.FillCA((i, j) => j == 3 && i > 0 && i < 4 || j == 2 && i == 3 || j == 1 && i == 2);
			ca2 = (int[,])ca.Clone();
			buf = ca.Draw(cellSize);
		}

		private void Tick(object sender, EventArgs e) {
			if(fbuf)
				ca2 = ca.UpdateCA(CA.conway);
			else
				ca = ca2.UpdateCA(CA.conway);
			fbuf = !fbuf;
			Refresh();
		}
	}
}
