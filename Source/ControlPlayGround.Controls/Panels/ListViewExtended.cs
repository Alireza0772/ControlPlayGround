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
	public class ListViewExtended : ListView
	{
		private Point dragStart;
		private Point dragEnd;

		private int indexerCounter = 0;
		public bool IsDragSelectionActive { get; set; }
		public int StartSelectionIndex { get; set; }
		public int PreviousSelectionIndex { get; set; }
		public List<ListViewItemExtended> Containers { get; set; } = new List<ListViewItemExtended>();


		protected override DependencyObject GetContainerForItemOverride()
		{
			var container = new ListViewItemExtended(indexerCounter++, this);
			Containers.Add(container);
			return container;
		}
		protected override void OnMouseUp(MouseButtonEventArgs e)
		{
			IsDragSelectionActive = false;
			base.OnMouseUp(e);
		}
	}
	public class ListViewItemExtended : ListViewItem
	{
		private int index;
		private ListViewExtended view;

		public ListViewItemExtended(int index, ListViewExtended view)
		{
			this.index = index;
			this.view = view;
		}

		protected override void OnMouseDown(MouseButtonEventArgs e)
		{
			
			view.IsDragSelectionActive = true;
			view.StartSelectionIndex = index;
			view.PreviousSelectionIndex = index;
			if (e.RightButton == MouseButtonState.Pressed)
			{
				e.Handled = true;
			}
			else
			{
				base.OnMouseDown(e);
			}
		}
		protected override void OnMouseUp(MouseButtonEventArgs e)
		{
			view.IsDragSelectionActive = false;
			base.OnMouseUp(e);
		}
		protected override void OnMouseEnter(MouseEventArgs e)
		{
			if (view.IsDragSelectionActive == true)
			{
				view.PreviousSelectionIndex = index;
				for (int i = Math.Min(view.StartSelectionIndex, index); i <= Math.Max(view.StartSelectionIndex, index); i++)
				{
					if (Mouse.LeftButton == MouseButtonState.Pressed)
					{
						view.Containers[i].IsSelected = true;
					}
					else if(Mouse.RightButton == MouseButtonState.Pressed)
					{
						view.Containers[i].IsSelected = false;
					}
				}
			}
			else
			{
				if (Mouse.LeftButton == MouseButtonState.Pressed ||
					Mouse.RightButton == MouseButtonState.Pressed)
				{
					view.IsDragSelectionActive = true;
					view.StartSelectionIndex = index;
					view.PreviousSelectionIndex = index;
				}
			}
			base.OnMouseEnter(e);
		}
	}
}
