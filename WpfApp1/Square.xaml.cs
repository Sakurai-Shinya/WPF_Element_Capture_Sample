using System.Windows.Controls;

namespace WpfApp1
{
	/// <summary>
	/// Square.xaml の相互作用ロジック
	/// </summary>
	public partial class Square : UserControl
	{
		public Square()
		{
			InitializeComponent();
		}

		public string Text
		{
			get => this.TextBlock.Text;
			set => this.TextBlock.Text = value;
		}
	}
}
