using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfClient.ViewModels
{
    public class ViewModelLocator
    {
        private MainViewModel mainViewModel;
        public MainViewModel MainViewModel
        {
            get
            {
                if (this.mainViewModel == null)
                    this.mainViewModel = new MainViewModel();
                return this.mainViewModel;
            }
        }
    }
}
