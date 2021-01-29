using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ControlPlayGround.Themes.Styles
{
	public partial class MainWindowStyle : ResourceDictionary
	{
		private void ExitButton_Click(object sender, RoutedEventArgs e)
		{
			Application.Current.Shutdown();
		}
		private void MaximizeButton_Click(object sender, RoutedEventArgs e)
		{
			if (Application.Current.MainWindow.WindowState == WindowState.Normal)
			{
				Application.Current.MainWindow.WindowState = WindowState.Maximized;
			}
			else
			{
				Application.Current.MainWindow.WindowState = WindowState.Normal;
			}
		}
		private void MinimizeButton_Click(object sender, RoutedEventArgs e)
		{
			Application.Current.MainWindow.WindowState = WindowState.Minimized;
		}
	}
}
