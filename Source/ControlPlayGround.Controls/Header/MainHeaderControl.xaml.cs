using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace ControlPlayGround.Controls.Header
{
	/// <summary>
	/// Interaction logic for MainHeaderControl.xaml
	/// </summary>
	public partial class MainHeaderControl : UserControl
	{
		public static readonly DependencyProperty AboutWindowProperty = DependencyProperty.Register("AboutWindow", typeof(Window), typeof(MainHeaderControl));
		public static readonly DependencyProperty MenuProperty = DependencyProperty.Register("Menu", typeof(Menu), typeof(MainHeaderControl));

		public Menu Menu
		{
			get { return (Menu)GetValue(MenuProperty); }
			set { SetValue(MenuProperty, value); }
		}

		public Window AboutWindow
		{
			get { return (Window)GetValue(AboutWindowProperty); }
			set { SetValue(AboutWindowProperty, value); }
		}

		public MainHeaderControl()
		{
			InitializeComponent();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			if (AboutWindow != null)
			{
				AboutWindow.Owner = Application.Current.MainWindow;
				AboutWindow.ShowDialog();
			}
		}
	}
}
