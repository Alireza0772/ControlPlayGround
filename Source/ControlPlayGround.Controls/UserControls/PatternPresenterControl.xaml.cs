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

namespace ControlPlayGround.Controls.UserControls
{
	/// <summary>
	/// Interaction logic for PatternPresenterControl.xaml
	/// </summary>
	public partial class PatternPresenterControl : UserControl
	{
		Point clickPoint;
		Point offsetPoint;

		public PatternPresenterControl()
		{
			InitializeComponent();
		}

		protected override void OnMouseDown(MouseButtonEventArgs e)
		{
			clickPoint = e.GetPosition(this);
			offsetPoint = new Point(brush.Viewport.X, brush.Viewport.Y);
		}
		protected override void OnMouseMove(MouseEventArgs e)
		{
			if (e.LeftButton == MouseButtonState.Pressed)
			{
				var newPoint = e.GetPosition(this);
				brush.Viewport = new Rect(offsetPoint.X + (newPoint.X - clickPoint.X), offsetPoint.Y + (newPoint.Y - clickPoint.Y), brush.Viewport.Width, brush.Viewport.Height);
			}
		}
		protected override void OnMouseWheel(MouseWheelEventArgs e)
		{
			if (e.Delta < 0)
			{
				brush.Viewport = new Rect(brush.Viewport.X, brush.Viewport.Y, brush.Viewport.Width / 2, brush.Viewport.Height / 2);
			}
			else if (e.Delta > 0)
			{
				brush.Viewport = new Rect(brush.Viewport.X, brush.Viewport.Y, brush.Viewport.Width * 2, brush.Viewport.Height * 2);
			}
		}
	}
}
