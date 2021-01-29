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
	public class DateTimeToForegroundConverter : IMultiValueConverter
	{

		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			DateTime date = (DateTime)values[0];
			int monthNumber = (int)values[1] + 1;

			if (CultureInfo.CurrentCulture.Calendar.GetMonth(date) != monthNumber)
			{
				return new SolidColorBrush(Colors.Gray);
			}
			return new SolidColorBrush(Colors.White);
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
