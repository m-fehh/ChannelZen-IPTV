using System;
using System.Drawing;
using System.Windows.Forms;

namespace ChannelZen.Action
{
	public class LoadingScreen : UserControl
	{
		private Label lblLoading;
		private ProgressBar progressBar;

		public LoadingScreen()
		{
			InitializeComponents();
		}

		private void InitializeComponents()
		{
			lblLoading = new Label();
			progressBar = new ProgressBar();

			this.SuspendLayout();

			// lblLoading
			lblLoading.AutoSize = true;
			lblLoading.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
			lblLoading.Location = new Point(10, 10);
			lblLoading.Name = "lblLoading";
			lblLoading.Size = new Size(157, 21);
			lblLoading.TabIndex = 0;
			lblLoading.Text = "Loading... Please wait";

			// progressBar
			progressBar.Location = new Point(10, 40);
			progressBar.Name = "progressBar";
			progressBar.Size = new Size(200, 20);
			progressBar.TabIndex = 1;

			// LoadingScreen
			this.Controls.Add(lblLoading);
			this.Controls.Add(progressBar);
			this.Name = "LoadingScreen";
			this.Size = new Size(220, 70);
			this.ResumeLayout(false);
		}

		public void ShowLoadingScreen()
		{
			this.Visible = true;
			this.BringToFront();
		}

		public void HideLoadingScreen()
		{
			this.Visible = false;
		}

		public void UpdateProgress(int progress)
		{
			progressBar.Value = progress;
		}
	}
}