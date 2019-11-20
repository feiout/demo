using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace New.Common
{
    public class ViewModelBase : NotificationObject
    {

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                if (_isBusy != value)
                {
                    _isBusy = value;
                    RaisePropertyChanged("IsBusy");
                }
            }
        }

        private int _gridViewPageSizeConfig;

        public int GridViewPageSizeConfig
        {
            get { return _gridViewPageSizeConfig; }
            set
            {
                if (_gridViewPageSizeConfig != value)
                {
                    _gridViewPageSizeConfig = value;
                    RaisePropertyChanged("GridViewPageSizeConfig");
                }
            }
        }
        public ViewModelBase()
        {

        }

        #region Async
        protected delegate void LoadStartAsync();

        protected delegate void LoadFinishAsync();

        protected void LoadAsync(LoadStartAsync startMethod, LoadFinishAsync finishMethod)
        {
            IsBusy = true;
            var loadData = startMethod;
            loadData.BeginInvoke(delegate (IAsyncResult ar)
            {
                var bindData = (LoadStartAsync)ar.AsyncState;
                bindData.EndInvoke(ar);
                if (finishMethod != null)
                    finishMethod();

                IsBusy = false;
            }, loadData);

            var dispather = Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background, startMethod);
            dispather.Completed += delegate
            {
                if (finishMethod != null)
                    finishMethod();

                IsBusy = false;
            };
        }

        protected void LoadAsync(LoadStartAsync startMethod)
        {
            IsBusy = true;
            Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background, new Action(
                delegate
                {
                    startMethod();
                    IsBusy = false;
                }));
        }

        #endregion
        #region ClosePopupsCommand
        //public RelayCommand ClosePopupsCommand { get; set; }

        //protected virtual void ClosePopupsCommandMethod(object param)
        //{
        //    if (HostControl != null)
        //    {
        //        HostControl.ClosePopup();
        //    }
        //}

        protected virtual bool CanClosePopupsCommandExecute(object obj)
        {
            return true;
        }

        //public RelayCommand ClosePopupsAlertCommand { get; set; }

        //protected virtual void ClosePopupsAlertCommandMethod(object param)
        //{
        //    RadWindow.Confirm("是否关闭窗口？", delegate (object o, WindowClosedEventArgs args)
        //    {
        //        if (args.DialogResult == true)
        //        {
        //            if (HostControl != null)
        //            {
        //                HostControl.ClosePopup();
        //            }
        //        }
        //    });
        //}

        protected virtual bool ClosePopupsAlertCommandCommandExecute(object obj)
        {
            return true;
        }

        #endregion
    }
}
