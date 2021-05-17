using System;
using System.Windows.Forms;
using System.Drawing;

namespace cstest {
    class MainClass {
		[STAThread]
		public static void Main(string[] args) {
            Console.WriteLine("Hello World!");

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Form1 form = new Form1();
			Application.Run(form);
		}
    }
	public class Form1 : Form {
		private System.ComponentModel.IContainer components = null;

		public Form1() {
			InitializeComponent();
		}

		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void paint_something(object sender, PaintEventArgs e) {
			Pen pen = new Pen(Color.AliceBlue, 50f);

			e.Graphics.DrawLine(pen, 0, 0, 400, 400);
		}

		public void InitializeComponent() {
			Console.WriteLine("Init");
			this.SuspendLayout();
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);

			pictureBox = new PictureBox {
				BackColor = Color.Aqua,
				Size = new Size(256, 256),
				Location = new Point(0, 0)
			};

			pictureBox.Paint += paint_something;

			Controls.Add(pictureBox);
		}

		PictureBox pictureBox;
	}
}
