using DevExpress.CodeParser;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Http;
using System.Runtime.Intrinsics.X86;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChannelZen
{
	public partial class Form1 : Form
	{
		private List<ChannelEntry> channels;
		private BackgroundWorker backgroundWorker;

		public Form1()
		{
			InitializeComponent();
			InitializeBackgroundWorker();
		}

		private void InitializeBackgroundWorker()
		{
			backgroundWorker = new BackgroundWorker();
			backgroundWorker.DoWork += BackgroundWorker_DoWork;
			backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			string filePath = @"C:\Users\marti\Downloads\Felipe\Iptv\iptv-naruto.m3u";

			if (!File.Exists(filePath))
			{
				MessageBox.Show("O arquivo M3U especificado não existe.");
				return;
			}

			StartLoading();

			backgroundWorker.RunWorkerAsync(filePath);
		}

		private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			string filePath = e.Argument as string;
			LoadChannelsFromFile(filePath);
		}

		private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if (channels.Count == 0)
			{
				MessageBox.Show("Nenhum canal encontrado no arquivo M3U.");
				StopLoading();
				return;
			}

			channelListBox.DataSource = channels;
			channelListBox.DisplayMember = "Name";

			StopLoading();
			EnableChannelSelection();
		}

		private void StartLoading()
		{
			channelListBox.Enabled = false;
			progressBar.Visible = true;
			loadingLabel.Visible = true;
		}

		private void StopLoading()
		{
			channelListBox.Enabled = true;
			progressBar.Visible = false;
			loadingLabel.Visible = false;
		}

		private void EnableChannelSelection()
		{
			channelListBox.SelectedIndexChanged += ChannelListBox_SelectedIndexChanged;
			watchButton.Enabled = true;
		}

		private void LoadChannelsFromFile(string filePath)
		{
			string m3uContent = File.ReadAllText(filePath);
			channels = ParseChannels(m3uContent);
		}

		private List<ChannelEntry> ParseChannels(string m3uContent)
		{
			List<ChannelEntry> parsedChannels = new List<ChannelEntry>();

			string[] lines = m3uContent.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
			ChannelEntry currentChannel = null;

			for (int i = 0; i < lines.Length - 1; i++)
			{
				string line = lines[i];

				if (line.StartsWith("EXTINF:-1") || line.StartsWith("#EXTINF:-1"))
				{
					currentChannel = new ChannelEntry();

					// Extrair o nome do canal
					int nameStartIndex = line.IndexOf(",") + 1;
					currentChannel.Name = line.Substring(nameStartIndex).Trim();
					currentChannel.Name = currentChannel.Name.Replace("NS", "Naruto Shippuden");

					// Extrair o grupo do canal
					Match groupMatch = Regex.Match(line, "group-title=\"([^\"]+)\"");
					if (groupMatch.Success)
					{
						currentChannel.Group = groupMatch.Groups[1].Value;
					}

					// Extrair a URL do logo do canal
					Match logoMatch = Regex.Match(line, "tvg-logo=\"([^\"]+)\"");
					if (logoMatch.Success)
					{
						currentChannel.LogoUrl = logoMatch.Groups[1].Value;
					}
				}
				else if (line.StartsWith("http"))
				{
					if (currentChannel != null)
					{
						currentChannel.StreamUrl = line;
						parsedChannels.Add(currentChannel);
						currentChannel = null;
					}
				}
			}

			return parsedChannels;
		}

		private void ShowChannelDetails(ChannelEntry channel)
		{
			lblChannelName.Text = "Canal: " + channel.Name;
			lblGroup.Text = "Grupo: " + channel.Group;

			if (!string.IsNullOrEmpty(channel.LogoUrl))
			{
				using (WebClient client = new WebClient())
				{
					try
					{
						byte[] imageData = client.DownloadData(channel.LogoUrl);
						using (MemoryStream stream = new MemoryStream(imageData))
						{
							pictureEditLogo.Image = Image.FromStream(stream);
						}
					}
					catch (Exception ex)
					{
						Debug.WriteLine("Erro ao baixar o logo do canal: " + ex.Message);
						pictureEditLogo.Image = null;
					}
				}
			}
			else
			{
				pictureEditLogo.Image = null;
			}
		}

		private void ChannelListBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			ChannelEntry selectedChannel = channelListBox.SelectedItem as ChannelEntry;
			if (selectedChannel != null)
			{
				ShowChannelDetails(selectedChannel);
			}
		}

		private async void watchButton_Click(object sender, EventArgs e)
		{
			ChannelEntry selectedChannel = channelListBox.SelectedItem as ChannelEntry;
			if (selectedChannel != null)
			{
				// Verificar a acessibilidade da URL
				bool isAccessible = await IsUrlAccessible(selectedChannel.StreamUrl);

				if (isAccessible)
				{
					// Iniciar o fluxo do episódio 1 para o canal selecionado
					StartEpisodeFlow(selectedChannel);
				}
				else
				{
					// Lidar com a URL inacessível (ignorar o certificado SSL)
					MessageBox.Show("A URL é considerada insegura, mas o vídeo será reproduzido.", "Aviso");
					StartEpisodeFlow(selectedChannel);
				}
			}
		}

		private async void StartEpisodeFlow(ChannelEntry channel)
		{
			if (channel.StreamUrl.EndsWith(".m3u8"))
			{
				string m3u8Content = await DownloadM3U8(channel.StreamUrl);
				if (!string.IsNullOrEmpty(m3u8Content))
				{
					string streamUrl = ExtractStreamUrlFromM3U8(m3u8Content);
					if (!string.IsNullOrEmpty(streamUrl))
					{
						try
						{
							Process.Start("cmd", "/c start " + streamUrl);
						}
						catch (Exception ex)
						{
							MessageBox.Show("Erro ao abrir a URL no navegador: " + ex.Message);
						}
					}
				}
			}
			else
			{
				try
				{
					Process.Start("cmd", "/c start " + channel.StreamUrl);
				}
				catch (Exception ex)
				{
					MessageBox.Show("Erro ao abrir a URL no navegador: " + ex.Message);
				}
			}
		}

		private async Task<bool> IsUrlAccessible(string url)
		{
			try
			{
				using (HttpClientHandler handler = new HttpClientHandler())
				{
					// Ignorar a verificação do certificado SSL
					handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;

					using (HttpClient client = new HttpClient(handler))
					{
						// Definir um tempo limite para a requisição
						client.Timeout = TimeSpan.FromSeconds(10);

						HttpResponseMessage response = await client.GetAsync(url);

						return response.IsSuccessStatusCode;
					}
				}
			}
			catch (Exception)
			{
				return false;
			}
		}

		private async Task<string> DownloadM3U8(string url)
		{
			try
			{
				using (HttpClientHandler handler = new HttpClientHandler())
				{
					// Ignorar a verificação do certificado SSL
					handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;

					using (HttpClient client = new HttpClient(handler))
					{
						// Definir um tempo limite para a requisição
						client.Timeout = TimeSpan.FromSeconds(10);

						HttpResponseMessage response = await client.GetAsync(url);

						if (response.IsSuccessStatusCode)
						{
							return await response.Content.ReadAsStringAsync();
						}
					}
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine("Erro ao baixar o arquivo M3U8: " + ex.Message);
			}

			return null;
		}

		private string ExtractStreamUrlFromM3U8(string m3u8Content)
		{
			string[] lines = m3u8Content.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);

			foreach (string line in lines)
			{
				if (!line.StartsWith("#"))
				{
					return line.Trim();
				}
			}

			return null;
		}
	}

	public class ChannelEntry
	{
		public string Name { get; set; }
		public string Group { get; set; }
		public string LogoUrl { get; set; }
		public string StreamUrl { get; set; }

		public override string ToString()
		{
			return Name;
		}
	}
}
