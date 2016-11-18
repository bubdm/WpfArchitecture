using System;
using System.Collections.Generic;
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
using Timesheet.Infrastructure.Prism.CustomCode;

namespace SpecialTabs.Views
{
    /// <summary>
    /// Interaction logic for SpecialTabAView.xaml
    /// </summary>
    public partial class SpecialTabAView : UserControl, ICreateRegionManagerScope
    {
        public SpecialTabAView()
        {
            InitializeComponent();
        }

        public bool CreateRegionManagerScope
        {
            get
            {
                return true;
            }
        }
    }
}
