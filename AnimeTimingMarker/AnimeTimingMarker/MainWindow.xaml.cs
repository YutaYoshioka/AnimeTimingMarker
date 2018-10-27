using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AnimeTimingMarker
{
	/// <summary>
	/// MainWindow.xaml の相互作用ロジック
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		bool running = false;
		int frames = 0;
		Timer timer;
		bool keyDown_Space = false;
		bool keyDown_LeftCtrl = false;
		bool keyDown_RightCtrl = false;

		private void Window_KeyDown(object sender, KeyEventArgs e)
		{
			if (Keyboard.IsKeyDown(Key.Space))
			{
				keyDown_Space = true;
			}
			else
			{
				keyDown_Space = false;
			}

			if (Keyboard.IsKeyDown(Key.LeftCtrl))
			{
				keyDown_LeftCtrl = true;
			}
			else
			{
				keyDown_LeftCtrl = false;
			}

			if (Keyboard.IsKeyDown(Key.RightCtrl))
			{
				keyDown_RightCtrl = true;
			}
			else
			{
				keyDown_RightCtrl = false;
			}
		}

		private void StartButton_Click(object sender, RoutedEventArgs e)
		{
			if (!running)
			{
				var frameTime = (int)(1000 /  double.Parse(FrameRate.Text));
				timer = new Timer(Callback, null, 1000, frameTime);
			}
			running = true;
		}

		private void Callback(Object sender)
		{
			var dataItem = new DataItem { Frame = frames.ToString(), Space = "□", LCtrl = "□", RCtrl = "□" };
			if (keyDown_Space)
			{
				dataItem.Space = "■";
			}

			if (keyDown_LeftCtrl)
			{
				dataItem.LCtrl = "■";
			}

			if (keyDown_RightCtrl)
			{
				dataItem.RCtrl = "■";
			}

			Dispatcher.BeginInvoke((Action)(() => {
				Data.Items.Add(dataItem);
				frames++;
				if (Data.Items.Count > 0)
				{
					if (VisualTreeHelper.GetChild(Data, 0) is Decorator border)
					{
						if (border.Child is ScrollViewer scroll)
						{
							scroll.ScrollToEnd();
						}
					}
				}
			}));
		}
		
		private struct DataItem
		{
			public string Frame { get; set; }
			public string Space { get; set; }
			public string LCtrl { get; set; }
			public string RCtrl { get; set; }
		}

		private void Window_PreviewKeyUp(object sender, KeyEventArgs e)
		{
			if (Keyboard.IsKeyDown(Key.Space))
			{
				keyDown_Space = true;
			}
			else
			{
				keyDown_Space = false;
			}

			if (Keyboard.IsKeyDown(Key.LeftCtrl))
			{
				keyDown_LeftCtrl = true;
			}
			else
			{
				keyDown_LeftCtrl = false;
			}

			if (Keyboard.IsKeyDown(Key.RightCtrl))
			{
				keyDown_RightCtrl = true;
			}
			else
			{
				keyDown_RightCtrl = false;
			}
		}

		private void StopButton_Click(object sender, RoutedEventArgs e)
		{
			timer.Change(-1, -1);
		}
	}
}
