using System;
using System.Collections.Generic;
using System.Globalization;
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
	/// Interaction logic for DateTimePickerControl.xaml
	/// </summary>
	public partial class DateTimePickerControl : UserControl
	{
		System.Globalization.Calendar currentCalendar = CultureInfo.CurrentCulture.Calendar;
		int monthNumber;
		int yearNumber;
		int offset;
		DateTime firstDayOfView;
		DateTime[] days = new DateTime[42];
		DateTime firstDayOfMonth;

		public DateTime SelectedDate
		{
			get { return (DateTime)GetValue(SelectedDateProperty); }
			set { SetValue(SelectedDateProperty, value); }
		}

		public static readonly DependencyProperty SelectedDateProperty =
			DependencyProperty.Register(nameof(SelectedDate), typeof(DateTime), typeof(DateTimePickerControl), new PropertyMetadata(DateTime.Now));


		public DateTimePickerControl()
		{
			InitializeComponent();

			monthNumber = currentCalendar.GetMonth(DateTime.Today);
			yearNumber = currentCalendar.GetYear(DateTime.Today);

			var months = CultureInfo.CurrentCulture.DateTimeFormat.MonthNames;
			Array.Resize<string>(ref months, 12);
			monthBox.ItemsSource = months;

			yearBox.Text = $"{currentCalendar.GetYear(SelectedDate)}";

			monthBox.SelectedIndex = monthNumber - 1;
			Update();
		}
		protected override void OnInitialized(EventArgs e)
		{

			base.OnInitialized(e);
			if (CultureInfo.CurrentCulture.TextInfo.IsRightToLeft)
			{
				daysPanel.FlowDirection = FlowDirection.RightToLeft;
				weekDays.FlowDirection = FlowDirection.RightToLeft;
			}
			string[] daysOfWeek = new string[7];
			for (int i = 0; i < daysOfWeek.Length; i++)
			{
				daysOfWeek[i] = CultureInfo.CurrentCulture.DateTimeFormat.ShortestDayNames[(i + (int)CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek) % 7];
			}

			weekDays.ItemsSource = daysOfWeek;
		}

		private void Update()
		{
			firstDayOfMonth = new DateTime(yearNumber, monthNumber, 1, currentCalendar);
			offset = ((int)currentCalendar.GetDayOfWeek(firstDayOfMonth) - (int)CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek + 7) % 7;
			firstDayOfView = firstDayOfMonth.AddDays(-1 * offset);
			days = new DateTime[42];
			for (int i = 0; i < 42; i++)
			{
				days[i] = firstDayOfView + TimeSpan.FromDays(i);
			}
			daysPanel.SetValue(ItemsControl.ItemsSourceProperty, days);
		}

		private void Next_Click(object sender, RoutedEventArgs e)
		{
			monthNumber++;
			if (monthNumber == 13)
			{
				yearNumber++;
				yearBox.Text = $"{yearNumber}";
				monthNumber = 1;
			}
			monthBox.SelectedIndex = monthNumber - 1;
			Update();
		}
		private void Previous_Click(object sender, RoutedEventArgs e)
		{
			monthNumber--;
			if (monthNumber == 0)
			{
				monthNumber = 12;
				yearNumber--;
				yearBox.Text = $"{yearNumber}";
			}
			monthBox.SelectedIndex = monthNumber - 1;
			Update();
		}

		private void Day_Click(object sender, RoutedEventArgs e)
		{
			SelectedDate = (DateTime)(e.Source as FrameworkElement).DataContext;
			if (currentCalendar.GetMonth(SelectedDate) != monthNumber)
			{
				monthNumber = currentCalendar.GetMonth(SelectedDate);
				yearNumber = currentCalendar.GetYear(SelectedDate);
				yearBox.Text = $"{yearNumber}";
				monthBox.SelectedIndex = monthNumber - 1;
				Update();
			}
		}

		private void monthBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			monthNumber = (sender as ComboBox).SelectedIndex + 1;
			Update();
		}


		private void yearBox_LostFocus(object sender, RoutedEventArgs e)
		{
			yearBox.Text = $"{yearNumber}";
		}

		private void yearBox_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Space || (e.Key >= Key.A && e.Key <= Key.Z))
			{
				e.Handled = true;
			}
			else if (yearBox.Text.Length == 0 && (e.Key == Key.NumPad0 || e.Key == Key.D0))
			{
				e.Handled = true;
			}
		}

		private void yearBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (int.TryParse(yearBox.Text, out yearNumber))
			{
				Update();
			}

		}
	}
}
