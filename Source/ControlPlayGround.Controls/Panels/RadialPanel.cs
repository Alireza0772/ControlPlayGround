using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ControlPlayGround.Controls.Panels
{
	public enum Arrangment
	{
		Stacked,
		Uniform
	}
	public class RadialPanel : Panel
	{
		public static readonly DependencyProperty SliceAngleProperty = DependencyProperty.RegisterAttached("SliceAngle", typeof(double), typeof(RadialPanel), new PropertyMetadata(0.0));
		public static readonly DependencyProperty OuterRadiusProperty = DependencyProperty.Register("OuterRadius", typeof(double), typeof(RadialPanel), new PropertyMetadata(0.0, OuterRadiusPropertyChangedCallback));
		public static readonly DependencyProperty InnerRadiusProperty = DependencyProperty.Register("InnerRadius", typeof(double), typeof(RadialPanel), new PropertyMetadata(0.0, InnerRadiusPropertyChangedCallback));
		public static readonly DependencyProperty LastChildFillProperty = DependencyProperty.Register("LastChildFill", typeof(bool), typeof(RadialPanel), new PropertyMetadata(true));
		public static readonly DependencyProperty ItemArrangmentProperty = DependencyProperty.Register("ItemArrangment", typeof(Arrangment), typeof(RadialPanel), new PropertyMetadata(Arrangment.Stacked, ItemArrangmentPropertyChangedCallback));

		double r;


		public double OuterRadius
		{
			get { return (double)GetValue(OuterRadiusProperty); }
			set { SetValue(OuterRadiusProperty, value); }
		}
		public double InnerRadius
		{
			get { return (double)GetValue(InnerRadiusProperty); }
			set { SetValue(InnerRadiusProperty, value); }
		}
		public Arrangment ItemArrangment
		{
			get { return (Arrangment)GetValue(ItemArrangmentProperty); }
			set { SetValue(ItemArrangmentProperty, value); }
		}
		public bool LastChildFill
		{
			get { return (bool)GetValue(LastChildFillProperty); }
			set { SetValue(LastChildFillProperty, value); }
		}


		private static void OuterRadiusPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			(d as RadialPanel).r = (double)e.NewValue;
			(d as RadialPanel).InvalidateArrange();
		}
		private static void InnerRadiusPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			throw new NotImplementedException();
		}
		private static void ItemArrangmentPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			(d as RadialPanel).InvalidateMeasure();
		}
		public static double GetSliceAngle(DependencyObject obj)
		{
			return (double)obj.GetValue(SliceAngleProperty);
		}
		public static void SetSliceAngle(DependencyObject obj, double value)
		{
			obj.SetValue(SliceAngleProperty, value);
		}
		protected override Size MeasureOverride(Size constraint)
		{
			List<double> childrensWidths = new List<double>();
			double totalAngle = 0;
			if (r == 0)
			{
				r = Math.Min(constraint.Width, constraint.Height);
				foreach (UIElement child in InternalChildren)
				{
					child.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
					childrensWidths.Add(child.DesiredSize.Width);
					switch (ItemArrangment)
					{
						case Arrangment.Stacked:
							double theta;
							if (OuterRadius < child.DesiredSize.Width / 2)
							{
								theta = 180;
							}
							theta = 2 * Math.Asin(child.DesiredSize.Width / (2 * r)) * 180 / Math.PI;
							if ((double)child.GetValue(SliceAngleProperty) <= theta)
							{
								child.SetValue(SliceAngleProperty, theta);
								totalAngle += theta;
							}
							else
							{
								totalAngle += (double)child.GetValue(SliceAngleProperty);
							}
							break;
						case Arrangment.Uniform:
							child.SetValue(SliceAngleProperty, 2 * Math.PI / InternalChildren.Count);
							break;
						default:
							break;
					}
				}
				if (ItemArrangment == Arrangment.Uniform)
				{
					var max = childrensWidths.Max();
					OuterRadius = max / (2 * Math.Sin((double)InternalChildren[0].GetValue(SliceAngleProperty) / 2));
				}
			}
			if (VerticalAlignment == VerticalAlignment.Stretch && HorizontalAlignment == HorizontalAlignment.Stretch)
			{
				OuterRadius = Math.Min(constraint.Width/2, constraint.Height/2);
				return new Size(Math.Max(constraint.Width,2 * OuterRadius),Math.Max(constraint.Height,2 * OuterRadius));
			}
			if (VerticalAlignment == VerticalAlignment.Stretch)
			{
				OuterRadius = Math.Min(OuterRadius, constraint.Height);
				return new Size(Math.Max(constraint.Width, 2 * OuterRadius), Math.Max(constraint.Height, 2 * OuterRadius));
			}
			else if (HorizontalAlignment == HorizontalAlignment.Stretch)
			{
				OuterRadius = Math.Min(constraint.Width/2, OuterRadius);
				return new Size(Math.Max(constraint.Width, 2 * OuterRadius), Math.Max(constraint.Height, 2 * OuterRadius));
			}
			return new Size(2 * OuterRadius, 2 * OuterRadius);
		}

		protected override Size ArrangeOverride(Size finalSize)
		{
			// Calculate radius
			var radius = Math.Min(finalSize.Width / 2, finalSize.Height / 2);

			double count = InternalChildren.Count;

			// Center of the ellipse
			Point center = new Point(finalSize.Width / 2, finalSize.Height / 2);

			double angle = 0;

			for (int i = 0; i < count; i++)
			{
				UIElement child = InternalChildren[i];
				var theta = (double)child.GetValue(SliceAngleProperty) != 0.0 ? (double)child.GetValue(SliceAngleProperty) : 0;
				if (theta == Math.PI * 2) theta = 0;
				// Calculate position
				double x = center.X + OuterRadius * Math.Cos(theta / 2) * Math.Cos(angle + theta / 2) - child.DesiredSize.Width / 2;
				double y = center.Y + OuterRadius * Math.Cos(theta / 2) * Math.Sin(angle + theta / 2) - child.DesiredSize.Height / 2;

				child.RenderTransformOrigin = new Point(0.5, 0.5);
				child.RenderTransform = new RotateTransform((angle + theta / 2 + Math.PI / 2) * 180 / Math.PI);

				angle += theta;

				child.Arrange(new Rect(new Point(x, y), child.DesiredSize));
			}
			return new Size(2 * OuterRadius, 2 * OuterRadius);
		}
	}
}
