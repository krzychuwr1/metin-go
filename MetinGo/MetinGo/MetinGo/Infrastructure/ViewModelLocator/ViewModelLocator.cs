using System;
using System.Collections.Generic;
using System.Text;
using MetinGo.ViewModels;

namespace MetinGo.Infrastructure.ViewModelLocator
{
    public class ViewModelLocator
    {
		public static ItemsViewModel ItemsViewModel => new ItemsViewModel();
	}
}
