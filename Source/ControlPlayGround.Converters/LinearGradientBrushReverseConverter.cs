using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace ControlPlayGround.Converters
{
	[ValueConversion(typeof(LinearGradientBrush),typeof(LinearGradientBrush))]
	public class LinearGradientBrushReverseConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			LinearGradientBrush brush = value as LinearGradientBrush;
			LinearGradientBrush temp = new LinearGradientBrush(brush.GradientStops.Clone());
			temp.GradientStops[0].Offset = brush.GradientStops[1].Offset;
			temp.GradientStops[1].Offset = brush.GradientStops[0].Offset;
			return temp;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
