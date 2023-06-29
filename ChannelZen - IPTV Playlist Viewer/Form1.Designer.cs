namespace ChannelZen
{
	partial class Form1
	{
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.ListBox channelListBox;
		private System.Windows.Forms.Label lblChannelName;
		private System.Windows.Forms.Label lblGroup;
		private System.Windows.Forms.PictureBox pictureEditLogo;
		private System.Windows.Forms.ProgressBar progressBar;
		private System.Windows.Forms.Label loadingLabel;
		private System.Windows.Forms.Button watchButton;

		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			channelListBox = new System.Windows.Forms.ListBox();
			lblChannelName = new System.Windows.Forms.Label();
			lblGroup = new System.Windows.Forms.Label();
			pictureEditLogo = new System.Windows.Forms.PictureBox();
			progressBar = new System.Windows.Forms.ProgressBar();
			loadingLabel = new System.Windows.Forms.Label();
			watchButton = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)pictureEditLogo).BeginInit();
			SuspendLayout();
			// 
			// channelListBox
			// 
			channelListBox.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			channelListBox.FormattingEnabled = true;
			channelListBox.ItemHeight = 17;
			channelListBox.Location = new System.Drawing.Point(14, 14);
			channelListBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			channelListBox.Name = "channelListBox";
			channelListBox.Size = new System.Drawing.Size(291, 531);
			channelListBox.TabIndex = 0;
			channelListBox.SelectedIndexChanged += ChannelListBox_SelectedIndexChanged;
			// 
			// lblChannelName
			// 
			lblChannelName.AutoSize = true;
			lblChannelName.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
			lblChannelName.ForeColor = System.Drawing.Color.FromArgb(255, 128, 128);
			lblChannelName.Location = new System.Drawing.Point(313, 14);
			lblChannelName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			lblChannelName.Name = "lblChannelName";
			lblChannelName.Size = new System.Drawing.Size(79, 25);
			lblChannelName.TabIndex = 1;
			lblChannelName.Text = "Canal: ?";
			// 
			// lblGroup
			// 
			lblGroup.AutoSize = true;
			lblGroup.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			lblGroup.ForeColor = System.Drawing.Color.FromArgb(255, 192, 192);
			lblGroup.Location = new System.Drawing.Point(313, 39);
			lblGroup.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			lblGroup.Name = "lblGroup";
			lblGroup.Size = new System.Drawing.Size(61, 21);
			lblGroup.TabIndex = 2;
			lblGroup.Text = "Grupo: ";
			// 
			// pictureEditLogo
			// 
			pictureEditLogo.Location = new System.Drawing.Point(316, 63);
			pictureEditLogo.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			pictureEditLogo.Name = "pictureEditLogo";
			pictureEditLogo.Size = new System.Drawing.Size(426, 346);
			pictureEditLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			pictureEditLogo.TabIndex = 3;
			pictureEditLogo.TabStop = false;
			// 
			// progressBar
			// 
			progressBar.ForeColor = System.Drawing.Color.IndianRed;
			progressBar.Location = new System.Drawing.Point(313, 415);
			progressBar.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			progressBar.Name = "progressBar";
			progressBar.Size = new System.Drawing.Size(429, 27);
			progressBar.TabIndex = 4;
			progressBar.Visible = false;
			// 
			// loadingLabel
			// 
			loadingLabel.AutoSize = true;
			loadingLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
			loadingLabel.ForeColor = System.Drawing.Color.FromArgb(255, 128, 128);
			loadingLabel.Location = new System.Drawing.Point(313, 445);
			loadingLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			loadingLabel.Name = "loadingLabel";
			loadingLabel.Size = new System.Drawing.Size(111, 21);
			loadingLabel.TabIndex = 5;
			loadingLabel.Text = "Carregando...";
			loadingLabel.Visible = false;
			// 
			// watchButton
			// 
			watchButton.BackColor = System.Drawing.Color.FromArgb(255, 128, 128);
			watchButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			watchButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
			watchButton.ForeColor = System.Drawing.Color.WhiteSmoke;
			watchButton.Location = new System.Drawing.Point(313, 488);
			watchButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			watchButton.Name = "watchButton";
			watchButton.Size = new System.Drawing.Size(429, 48);
			watchButton.TabIndex = 6;
			watchButton.Text = "Assistir";
			watchButton.UseVisualStyleBackColor = false;
			watchButton.Click += watchButton_Click;
			// 
			// Form1
			// 
			AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.FromArgb(244, 244, 244);
			ClientSize = new System.Drawing.Size(761, 561);
			Controls.Add(watchButton);
			Controls.Add(loadingLabel);
			Controls.Add(progressBar);
			Controls.Add(pictureEditLogo);
			Controls.Add(lblGroup);
			Controls.Add(lblChannelName);
			Controls.Add(channelListBox);
			FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			Name = "Form1";
			StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			Text = "ChannelZen - IPTV Playlist Viewer";
			Load += Form1_Load;
			((System.ComponentModel.ISupportInitialize)pictureEditLogo).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}