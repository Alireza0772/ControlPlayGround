using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Effects;

namespace ControlPlayGround.Controls.Panels
{
	public class SlideOutPanel : StackPanel
	{
		public bool IsExpanded
		{
			get { return (bool)GetValue(IsExpandedProperty); }
			set { SetValue(IsExpandedProperty, value); }
		}

		public static readonly DependencyProperty IsExpandedProperty =
			DependencyProperty.Register("IsExpanded", typeof(bool), typeof(SlideOutPanel), new PropertyMetadata(false,IsExpandedPropertyChangedCallback));

		private static void IsExpandedPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			(d as SlideOutPanel).InvalidateMeasure();
		}

		protected override Size MeasureOverride(Size availableSize)
		{
			var size = base.MeasureOverride(availableSize);
			if (IsExpanded)
			{
				return size;
			}
			else
			{
				return new Size(size.Width, 0);
			}
		}
		protected override void OnVisualChildrenChanged(DependencyObject visualAdded, DependencyObject visualRemoved)
		{
			visualAdded.SetValue(ZIndexProperty, int.MaxValue - Children.Count);
			base.OnVisualChildrenChanged(visualAdded, visualRemoved);
		}
	}
}
