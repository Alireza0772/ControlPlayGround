using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ControlPlayGround.Controls.Panels
{
	public class PoppingStackPanel : StackPanel
	{
		double mouseDistance = 0;


		public double MaxScale
		{
			get { return (double)GetValue(MaxScaleProperty); }
			set { SetValue(MaxScaleProperty, value); }
		}

		// Using a DependencyProperty as the backing store for MaxScale.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty MaxScaleProperty =
			DependencyProperty.Register("MaxScale", typeof(double), typeof(PoppingStackPanel), new PropertyMetadata(1.0));



		protected override void OnMouseMove(MouseEventArgs e)
		{
			InvalidateMeasure();
			InvalidateArrange();
		}
		protected override Size ArrangeOverride(Size arrangeSize)
		{
			var size = base.ArrangeOverride(arrangeSize);
			foreach (FrameworkElement child in Children)
			{
				mouseDistance = double.PositiveInfinity;
				if (IsMouseOver)
				{
					var mousePoint = Mouse.GetPosition(child) - new Point(child.RenderSize.Width / 2, child.RenderSize.Height / 2);
					mouseDistance = Math.Pow(mousePoint.Y, 2);
				}

				var scaleFactor = 1 + MaxScale * Math.Exp(-0.005 * mouseDistance);
				SetZIndex(child, (int)(scaleFactor * 50));
				child.RenderTransformOrigin = new Point(0.5, 0.5);
				child.RenderTransform = new ScaleTransform(scaleFactor, scaleFactor);
			}
			return size;
		}

		protected override void OnLostFocus(RoutedEventArgs e)
		{
			base.OnLostFocus(e);
			mouseDistance = double.PositiveInfinity;
			InvalidateArrange();
		}
		protected override void OnMouseLeave(MouseEventArgs e)
		{
			base.OnMouseLeave(e);
			InvalidateArrange();
		}
	}
}

