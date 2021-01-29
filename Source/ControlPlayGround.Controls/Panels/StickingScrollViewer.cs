using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ControlPlayGround.Controls.Panels
{
	public class StickingScrollViewer : ScrollViewer
	{
		protected override void OnScrollChanged(ScrollChangedEventArgs e)
		{
			base.OnScrollChanged(e);
		}

	}
}
