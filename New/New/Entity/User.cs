

using New.Common;

namespace New.Entity
{
    public class User : NotificationObject
    {
        private long _id;
        public long id
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    RaisePropertyChanged("id");
                }
            }
        }

        private string _Name;
        public string Name
        {
            get { return _Name; }
            set
            {
                if (_Name != value)
                {
                    _Name = value;
                    RaisePropertyChanged("Name");
                }
            }
        }

        private string _Department;
        public string Department
        {
            get { return _Department; }
            set
            {
                if (_Department != value)
                {
                    _Department = value;
                    RaisePropertyChanged("Department");
                }
            }
        }

        private string _UserType;
        public string UserType
        {
            get { return _UserType; }
            set
            {
                if (_UserType != value)
                {
                    _UserType = value;
                    RaisePropertyChanged("UserType");
                }
            }
        }

        private int _SubscriptionId;
        public int SubscriptionId
        {
            get { return _SubscriptionId; }
            set
            {
                if (_SubscriptionId != value)
                {
                    _SubscriptionId = value;
                    RaisePropertyChanged("SubscriptionId");
                }
            }
        }

    }
}
