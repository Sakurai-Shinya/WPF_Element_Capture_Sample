using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WpfApp1
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			//四角の作成
			var random = new Random();
			for (var i = 0; i < 100; i++)
			{
				this.RootStackPanel.Children.Add(new Square
				{
					Text = (i + 1).ToString(),
					Background = new SolidColorBrush(Color.FromRgb((byte)random.Next(0, 256), (byte)random.Next(0, 256), (byte)random.Next(0, 256))),
				});
			}

			//StackPanelをキャプチャ
			var bitmap = FrameworkElementToBitmapSource(this.RootStackPanel);

			//PNGで保存
			BitmapSourceToPngFile(bitmap, "capture.png");


			Debug.WriteLine("保存完了");
		}

		public static BitmapSource FrameworkElementToBitmapSource(FrameworkElement element)
		{
			element.UpdateLayout();
			var width = element.ActualWidth;
			var height = element.ActualHeight;
			var dv = new DrawingVisual();
			using (var dc = dv.RenderOpen())
			{
				dc.DrawRectangle(new BitmapCacheBrush(element), null, new Rect(0, 0, width, height));
			}
			var rtb = new RenderTargetBitmap((int)width, (int)height, 96d, 96d, PixelFormats.Pbgra32);
			rtb.Render(dv);
			return rtb;
		}

		public static void BitmapSourceToPngFile(BitmapSource bitmapSource, string pngFilePath)
		{
			using (var stream = new FileStream(pngFilePath, FileMode.Create, FileAccess.Write, FileShare.None))
			{
				var encoder = new PngBitmapEncoder();
				encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
				encoder.Save(stream);
			}
		}
	}
}
