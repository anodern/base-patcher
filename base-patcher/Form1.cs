using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace base安装器 {
	public partial class Form1 : Form {
		public Form1() {
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e) {
			GetExtensionFile("tar");
		}

		private void GetExtensionFile(string extension) {
			DirectoryInfo theFolder = new DirectoryInfo(Environment.CurrentDirectory);
			FileInfo[] fileInfo = theFolder.GetFiles();
			foreach (FileInfo NextFile in fileInfo) {
				if (NextFile.Extension == "."+ extension) {
					listBox1.Items.Add(NextFile.Name);
				}
			}
		}

		private void listBox1_SelectedIndexChanged(object sender, EventArgs e) {
			button1.Text = "安装" + listBox1.SelectedItem.ToString().Substring(0,listBox1.SelectedItem.ToString().Length-4);
			button1.Enabled = true;
		}
		
		Process process = new Process();
		private void button1_Click(object sender, EventArgs e) {
			process.StartInfo.FileName = Environment.CurrentDirectory+@"\nbs.exe";
			process.StartInfo.Arguments = @" x "+ @" -o.\temp " + listBox1.SelectedItem;
			process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
			process.Start();
			timer1.Enabled = true;
			panel1.Location=new Point(0,0);
			panel1.Visible = true;
		}
		
		private void timer1_Tick(object sender, EventArgs e) {
			if (process.HasExited) {
				panel1.Visible = false;
				timer2.Enabled = true;
				timer1.Enabled = false;
			}
		}

		private void timer2_Tick(object sender, EventArgs e) {
			process.StartInfo.Arguments = @" a base.scs .\temp\* -tzip";
			process.Start();
			timer3.Enabled = true;
			panel1.Location = new Point(0, 0);
			panel1.Visible = true;
			timer2.Enabled = false;
		}

		private void timer3_Tick(object sender, EventArgs e) {
			if (process.HasExited) {
				panel1.Visible = false;
				Directory.Delete(Environment.CurrentDirectory + @"\temp", true);
				timer3.Enabled = false;
			}
		}
	}
}
