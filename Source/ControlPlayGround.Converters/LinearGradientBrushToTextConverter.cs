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
	[ValueConversion(typeof(LinearGradientBrush), typeof(string))]
	public class LinearGradientBrushToTextConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			LinearGradientBrush brush = value as LinearGradientBrush;
			return $"{brush.GradientStops[0].Color}\r\n{brush.GradientStops[1].Color}";
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
