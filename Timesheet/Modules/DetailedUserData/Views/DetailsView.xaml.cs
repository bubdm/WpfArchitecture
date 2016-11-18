using DetailedUserData.ViewModels;
using Prism.Common;
using Prism.Regions;
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
using Timesheet.Infrastructure.Models;

namespace DetailedUserData.Views
{
    /// <summary>
    /// Interaction logic for Details.xaml
    /// </summary>
    public partial class DetailsView : UserControl
    {
        public DetailsView()
        {
            InitializeComponent();

            RegionContext.GetObservableContext(this).PropertyChanged += (s, e) =>
            {
                var context = (ObservableObject<object>)s;
                var selectedMember = (TeamMember)context.Value;
                (DataContext as DetailsViewModel).Member = selectedMember;
            };
        }
    }
}
