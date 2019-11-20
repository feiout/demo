using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using New.Common;
using New.Entity;
using New.RestUtility;
using New.Service;


namespace New.ViewModels
{
    public class VmUser : ViewModelBase
    {
        private readonly UserService _userService = ServiceHelper<UserService>.CreateInterface();
        public List<KeyValuePair<string, string>> Conditions = new List<KeyValuePair<string, string>>();

        public VmUser()
        {
            QueryUserList();

        }

//        private string _passedNote;
//        public string passedNote
//        {
//            get { return _passedNote; }
//            set
//            {
//                if (_passedNote != value)
//                {
//                    _passedNote = value;
//                    RaisePropertyChanged("passedNote");
//                }
//            }
//        }
//
//        private int _totalPages;
//
//        public int totalPages
//        {
//            get { return _totalPages; }
//            set
//            {
//                if (_totalPages != value)
//                {
//                    _totalPages = value;
//                    RaisePropertyChanged("totalPages");
//                }
//            }
//        }
//
//        private GenericPagedList<Customer> _customerPageList;
//        public GenericPagedList<Customer> CustomerPageList
//        {
//            get { return _customerPageList; }
//            set
//            {
//                if (_customerPageList != value)
//                {
//                    _customerPageList = value;
//                    RaisePropertyChanged("CustomerPageList");
//                }
//            }
//        }


//        private Visibility _canEdit;
//        public Visibility CanEdit
//        {
//            get { return _canEdit; }
//            set
//            {
//                if (_canEdit != value)
//                {
//                    _canEdit = value;
//                    RaisePropertyChanged("CanEdit");
//                }
//            }
//        }


//        private Leads _TLeads;
//
//        public Leads TLeads
//        {
//            get { return _TLeads; }
//            set
//            {
//                if (_TLeads != value)
//                {
//                    _TLeads = value;
//                    RaisePropertyChanged("TLeads");
//                }
//            }
//        }



        private ObservableCollection<User> _userList;
        public ObservableCollection<User> UserList
        {
            get { return _userList; }
            set
            {
                if (_userList != value)
                {
                    _userList = value;
                    RaisePropertyChanged("UserList");
                }
            }
        }


        public void QueryUserList()
        {
            UserList = _userService.GetUserList();
        }


    }
}
