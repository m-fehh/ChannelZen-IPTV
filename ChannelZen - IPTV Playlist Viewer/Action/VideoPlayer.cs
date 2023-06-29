using System;
using System.Windows.Forms;

namespace ChannelZen.Action
{
	public class VideoPlayer : UserControl
	{
		private System.Windows.Forms.Panel panel;
		private System.Windows.Forms.Label lblNoPlayer;
		private System.Windows.Forms.Timer timer;
		private bool isVideoPlaying;

		public VideoPlayer()
		{
			InitializeComponents();
		}

		private void InitializeComponents()
		{
			panel = new Panel();
			lblNoPlayer = new Label();
			timer = new Timer();

			panel.Dock = DockStyle.Fill;
			panel.BackColor = System.Drawing.Color.Black;

			lblNoPlayer.AutoSize = true;
			lblNoPlayer.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			lblNoPlayer.ForeColor = System.Drawing.Color.White;
			lblNoPlayer.Location = new System.Drawing.Point(10, 10);
			lblNoPlayer.Text = "No video player available";

			timer.Interval = 500;
			timer.Tick += Timer_Tick;

			this.Controls.Add(panel);
		}

		private void Timer_Tick(object sender, EventArgs e)
		{
			if (!isVideoPlaying)
			{
				panel.Controls.Add(lblNoPlayer);
				panel.Controls.SetChildIndex(lblNoPlayer, 0);
			}
			else
			{
				panel.Controls.Remove(lblNoPlayer);
			}
		}

		public void Play(string videoPath)
		{
			// Simulate video playing
			// Aqui você pode adicionar a lógica real para reprodução de vídeo usando uma biblioteca específica

			// Por enquanto, vamos apenas atualizar o estado de reprodução do vídeo
			isVideoPlaying = true;
			timer.Start();
		}

		public void Stop()
		{
			// Simulate video stop
			// Aqui você pode adicionar a lógica real para parar a reprodução do vídeo usando uma biblioteca específica

			// Por enquanto, vamos apenas atualizar o estado de reprodução do vídeo
			isVideoPlaying = false;
			timer.Stop();
		}
	}
}