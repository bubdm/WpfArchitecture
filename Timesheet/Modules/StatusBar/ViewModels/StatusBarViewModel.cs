using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timesheet.Infrastructure.Events;

namespace StatusBar.ViewModels
{
    public class StatusBarViewModel : BindableBase
    {
        #region Properties

        private string _status = "Initial Status";

        IEventAggregator _statusEventAggregator;

        public string Status
        {
            get { return _status; }
            set { SetProperty<string>(ref _status, value); }
        }

        #endregion

        #region Constructors

        public StatusBarViewModel(IEventAggregator statusEventAggregator)
        {
            _statusEventAggregator = statusEventAggregator;
            _statusEventAggregator.GetEvent<StatusUpdatedEvent>().Subscribe(StatusUpdated);
        }

        #endregion

        #region Private Methods

        private void StatusUpdated(string status)
        {
            Status = status;
        }

        #endregion
    }
}
