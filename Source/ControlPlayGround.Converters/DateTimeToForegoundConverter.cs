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
	public class DateTimeToBackgroundConverter : IMultiValueConverter
	{
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			DateTime context = (DateTime)values[0];
			DateTime selectedDate = (DateTime)values[1];
			if (context == selectedDate && selectedDate.Date != DateTime.Now.Date)
			{
				return new SolidColorBrush(Colors.LightGreen);
			}
			else if (context == selectedDate && selectedDate.Date == DateTime.Now.Date)
			{
				return new SolidColorBrush(Colors.Purple);
			}
			else if (context.Date == DateTime.Now.Date)
			{
				return new SolidColorBrush(Colors.LightPink);
			}
			return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF252525"));
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
