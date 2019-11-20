//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Windows;
//using New.Common;
//using New.Service;
//
//namespace Fighter.Anlar.ViewModel.CallManager
//{
//    public class VmCallManager : ViewModelBase
//    {
//        private readonly CallManagerService _callManagerService = ServiceHelper<UserService>.CreateInterface();
//        public List<KeyValuePair<string, string>> Conditions = new List<KeyValuePair<string, string>>();
//        public int CaseListCount = 0;
//         
//
//        public VmCallManager()
//        {
//            //            Loadcase();
//            //            LoadCustomers();
//            //            Loadleads();
//            //QueryCustomerList3();
//            //            CreateCommand = new RelayCommand(CreateCommandMethod);
//            //            QueryCustomerList1();
//            //            QueryCustomerList2();
//            //QueryCustomerList();
//            // CustomerList = new ObservableCollection<Customer>();
//            //            CustomerList.Clear();
//
//
//            //QueryCustomerList8();
//
//
//            saleProjectNullList = _callManagerService.GetSaleProjectNullList();   //不用缓存
//
//
//
//            RefreshCommand = new RelayCommand(RefreshCommandMethod);
//            RefreshCommand2 = new RelayCommand(RefreshCommand2Method);
//            //RefreshCommand3 = new RelayCommand(RefreshCommand3Method);
//            RefreshCommand4 = new RelayCommand(RefreshCommand4Method);
////            CanEdit = Visibility.Collapsed;
////            CanNew = Visibility.Collapsed;
//            ProductRelationList = new ObservableCollection<ProductRelation>();
//            orderList = new ObservableCollection<CustomerCallOrder>();
//            AddDoctorAdvicePlanCommand = new RelayCommand(AddDoctorAdvicePlanCommandMethod, CanAddDoctorAdvicePlanCommandExecute);
//            //            CaseStatusList = _callManagerService.GetCaseStatusList(null);
//
//            //初始化callist 的用户状态
//            
//            tstatusList = new ObservableCollection<SaleProjectCustomerStatus>();
//            NstatusList= new ObservableCollection<SaleProjectCustomerStatus>();
//            ArrayList Olist = new ArrayList();
//            Olist.Add("Not-Call");
//            Olist.Add("Pending");
//            Olist.Add("No-Vaule");
//            Olist.Add("No-Answer");
//            Olist.Add("No-Interesting");
//            Olist.Add("Value");
//            for (int i = 0; i < Olist.Count; i++)
//            {
//                SaleProjectCustomerStatus cpcs = new SaleProjectCustomerStatus();
//                cpcs.id = i.ToString();
//                cpcs.code = i.ToString();
//                cpcs.name = Olist[i].ToString();
//                NstatusList.Add(cpcs);
//            }
//            SaleProjectCustomerStatus tep = new SaleProjectCustomerStatus();
//            tep.name = "All";
//            tstatusList.Add(tep);
//            tstatusList.AddRange(NstatusList);
//
//
//            //初始化状态
//            NotCallStatusList = new ObservableCollection<SaleProjectCustomerStatusDetail>();
//            ArrayList NClist = new ArrayList();
//            NClist.Add("");
//            for (int i = 0; i < NClist.Count; i++)
//            {
//                SaleProjectCustomerStatusDetail cpcs = new SaleProjectCustomerStatusDetail();
//                cpcs.id = i.ToString();
//                cpcs.code = i.ToString();
//                cpcs.name = NClist[i].ToString();
//                NotCallStatusList.Add(cpcs);
//            }
//
//
//            ValueStatusList = new ObservableCollection<SaleProjectCustomerStatusDetail>();
//            ArrayList Vlist = new ArrayList();
//            Vlist.Add("");
//            for (int i = 0; i < Vlist.Count; i++)
//            {
//                SaleProjectCustomerStatusDetail cpcs = new SaleProjectCustomerStatusDetail();
//                cpcs.id = i.ToString();
//                cpcs.code = i.ToString();
//                cpcs.name = Vlist[i].ToString();
//                ValueStatusList.Add(cpcs);
//            }
//
//            PendingsSatusList = new ObservableCollection<SaleProjectCustomerStatusDetail>();
//            ArrayList PElist = new ArrayList();
//            PElist.Add("");
//            PElist.Add("用过Honeywell，接受跟进");
//            PElist.Add("没用过Honeywell，接受跟进");
//            PElist.Add("有关键联系人");
//            PElist.Add("有联系人");
//            for (int i = 0; i < PElist.Count; i++)
//            {
//                SaleProjectCustomerStatusDetail cpcs = new SaleProjectCustomerStatusDetail();
//                cpcs.id = i.ToString();
//                cpcs.code = i.ToString();
//                cpcs.name = PElist[i].ToString();
//                PendingsSatusList.Add(cpcs);
//            }
//
//            NoInterestingStatusList = new ObservableCollection<SaleProjectCustomerStatusDetail>();
//            ArrayList NIlist = new ArrayList();
//            NIlist.Add("");
//            NIlist.Add("有固定供应商");
//            NIlist.Add("已有固定销售联系");
//            NIlist.Add("对Honeywell品牌体验不好");
////            NIlist.Add("指定其他品牌");
//            for (int i = 0; i < NIlist.Count; i++)
//            {
//                SaleProjectCustomerStatusDetail cpcs = new SaleProjectCustomerStatusDetail();
//                cpcs.id = i.ToString();
//                cpcs.code = i.ToString();
//                cpcs.name = NIlist[i].ToString();
//                NoInterestingStatusList.Add(cpcs);
//            }
//
//            NoAnswerStatusList = new ObservableCollection<SaleProjectCustomerStatusDetail>();
//            ArrayList NAlist = new ArrayList();
//            NAlist.Add("");
//            NAlist.Add("无人接听");
//            NAlist.Add("三次以上无人接听");
//            for (int i = 0; i < NAlist.Count; i++)
//            {
//                SaleProjectCustomerStatusDetail cpcs = new SaleProjectCustomerStatusDetail();
//                cpcs.id = i.ToString();
//                cpcs.code = i.ToString();
//                cpcs.name = NAlist[i].ToString();
//                NoAnswerStatusList.Add(cpcs);
//            }
//
//            NoValueStatusList = new ObservableCollection<SaleProjectCustomerStatusDetail>();
//            ArrayList NVlist = new ArrayList();
//            NVlist.Add("");
//            NVlist.Add("找不到联系方式");
//            NVlist.Add("公司倒闭/公司不适合用Honeywell产品");
//            NVlist.Add("无独立采购权");
//            for (int i = 0; i < NVlist.Count; i++)
//            {
//                SaleProjectCustomerStatusDetail cpcs = new SaleProjectCustomerStatusDetail();
//                cpcs.id = i.ToString();
//                cpcs.code = i.ToString();
//                cpcs.name = NVlist[i].ToString();
//                NoValueStatusList.Add(cpcs);
//            }
//
//
//        }
//
//
//
//        #region Property
//
//        #region AddDoctorAdvicePlanCommand
//        public RelayCommand AddDoctorAdvicePlanCommand { get; set; }
//        /// <summary>
//        /// 增加药品
//        /// </summary>
//        /// <param name="param"></param>
//        private void AddDoctorAdvicePlanCommandMethod(object param)
//        {
//            //if (CurrentPatientRegistration == null)
//            //{
//            //    RadWindow.Alert("数据异常！");
//            //    return;
//            //}
//            //if (DoctorAdvicePlans == null)
//            //{
//            //    DoctorAdvicePlans = new ObservableCollection<DoctorAdvicePlan>();
//            //}
//            //if (RxCategoryId != ((int)EnumContants.PrescriptionType.HerbalPieces).ToString())
//            //{
//            //    DoctorAdvicePlan doctorAdvicePlan = AddDoctorAdvicePlanInfo();
//            //    DoctorAdvicePlans.Add(doctorAdvicePlan);
//            //}
//            //else
//            //{
//            //    if (DoctorAdvicePlanInfo == null)
//            //    {
//            //        DoctorAdvicePlanInfo = new DoctorAdvicePlan();
//            //        DoctorAdvicePlan doctorAdvicePlan = AddDoctorAdvicePlanInfo();
//            //        DoctorAdvicePlanInfo = doctorAdvicePlan;
//            //    }
//            //    if (DoctorAdvicePlanInfo.doctorAdvicePlanDetailList == null)
//            //    {
//            //        DoctorAdvicePlanInfo.doctorAdvicePlanDetailList = new ObservableCollection<DoctorAdvicePlanDetail>();
//            //    }
//            //    DoctorAdvicePlanDetail doctorAdvicePlanDetail = new DoctorAdvicePlanDetail();
//            //    doctorAdvicePlanDetail.issuedTime = DateTime.Now;
//            //    doctorAdvicePlanDetail.issuedDoctorId = Employee.id;
//            //    doctorAdvicePlanDetail.issuedDoctorName = Employee.name;
//            //    DoctorAdvicePlanInfo.doctorAdvicePlanDetailList.Add(doctorAdvicePlanDetail);
//            //}
//
//            orderList.Add(new CustomerCallOrder());
//        }
//        private bool CanAddDoctorAdvicePlanCommandExecute(object obj)
//        {
//            return true;
//        }
//        #endregion
//
//
//        private string _call;
//        public string call
//        {
//            get { return _call; }
//            set
//            {
//                if (_call != value)
//                {
//                    _call = value;
//                    RaisePropertyChanged("call");
//                }
//            }
//        }
//
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
//        private AxCrystalSoftPhone32.AxSoftPhone32 _cs;
//        public AxCrystalSoftPhone32.AxSoftPhone32 cs
//        {
//            get { return _cs; }
//            set
//            {
//                if (_cs != value)
//                {
//                    _cs = value;
//                    RaisePropertyChanged("cs");
//                }
//            }
//        }
//
//        private string _parms;
//        public string parms
//        {
//            get { return _parms; }
//            set
//            {
//                if (_parms != value)
//                {
//                    _parms = value;
//                    RaisePropertyChanged("parms");
//                }
//            }
//        }
//
//        private string _page;
//        public string page
//        {
//            get { return _page; }
//            set
//            {
//                if (_page != value)
//                {
//                    _page = value;
//                    RaisePropertyChanged("page");
//                }
//            }
//        }
//
//        private string _priority;
//        public string priority
//        {
//            get { return _priority; }
//            set
//            {
//                if (_priority != value)
//                {
//                    _priority = value;
//                    RaisePropertyChanged("priority");
//                }
//            }
//        }
//
//
//        private string _customerStatus;
//        public string customerStatus
//        {
//            get { return _customerStatus; }
//            set
//            {
//                if (_customerStatus != value)
//                {
//                    _customerStatus = value;
//                    RaisePropertyChanged("customerStatus");
//                }
//            }
//        }
//
//
//        private string _customerType;
//        public string customerType
//        {
//            get { return _customerType; }
//            set
//            {
//                if (_customerType != value)
//                {
//                    _customerType = value;
//                    RaisePropertyChanged("customerType");
//                }
//            }
//        }
//
//
//        private string _fileName;
//        public string fileName
//        {
//            get { return _fileName; }
//            set
//            {
//                if (_fileName != value)
//                {
//                    _fileName = value;
//                    RaisePropertyChanged("fileName");
//                }
//            }
//        }
//
//
//        private string _status;
//        public string status    
//        {
//            get { return _status; }
//            set
//            {
//                if (_status != value)
//                {
//                    _status = value;
//                    RaisePropertyChanged("status");
//                }
//            }
//        }
//
//
//        private string _leadsimportance;
//        public string Leadsimportance
//        {
//            get { return _leadsimportance; }
//            set
//            {
//                if (_leadsimportance != value)
//                {
//                    _leadsimportance = value;
//                    RaisePropertyChanged("Leadsimportance");
//                }
//            }
//        }
//
//
//        private string _leadscategory;
//        public string Leadscategory
//        {
//            get { return _leadscategory; }
//            set
//            {
//                if (_leadscategory != value)
//                {
//                    _leadscategory = value;
//                    RaisePropertyChanged("Leadscategory");
//                }
//            }
//        }
//
//        private string _leadsgrade;
//        public string Leadsgrade
//        {
//            get { return _leadsgrade; }
//            set
//            {
//                if (_leadsgrade != value)
//                {
//                    _leadsgrade = value;
//                    RaisePropertyChanged("Leadsgrade");
//                }
//            }
//        }
//
//        private string _Leadsreseller;
//        public string Leadsreseller
//        {
//            get { return _Leadsreseller; }
//            set
//            {
//                if (_Leadsreseller != value)
//                {
//                    _Leadsreseller = value;
//                    RaisePropertyChanged("Leadsreseller");
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
//
//        private GenericPagedList<Customer> _PageList;
//        public GenericPagedList<Customer> PageList
//        {
//            get { return _PageList; }
//            set
//            {
//                if (_PageList != value)
//                {
//                    _PageList = value;
//                    RaisePropertyChanged("PageList");
//                }
//            }
//        }
//
//
//        private GenericPagedList<CompanysForListVO> _companyPageList;
//        public GenericPagedList<CompanysForListVO> CompanyPageList
//        {
//            get { return _companyPageList; }
//            set
//            {
//                if (_companyPageList != value)
//                {
//                    _companyPageList = value;
//                    RaisePropertyChanged("CompanyPageList");
//                }
//            }
//        }
//
//        
//
//        private GenericPagedList<CasesForListVO> _casePageList;
//        public GenericPagedList<CasesForListVO> CasePageList
//        {
//            get { return _casePageList; }
//            set
//            {
//                if (_casePageList != value)
//                {
//                    _casePageList = value;
//                    RaisePropertyChanged("CasePageList");
//                }
//            }
//        }
//
//        private GenericPagedList<Case> _CasePageByDTO;
//        public GenericPagedList<Case> CasePageByDTO
//        {
//            get { return _CasePageByDTO; }
//            set
//            {
//                if (_CasePageByDTO != value)
//                {
//                    _CasePageByDTO = value;
//                    RaisePropertyChanged("CasePageByDTO");
//                }
//            }
//        }
//        
//
//        private GenericPagedList<LeadsForListVO> _leadsPageList;
//        public GenericPagedList<LeadsForListVO> LeadsPageList
//        {
//            get { return _leadsPageList; }
//            set
//            {
//                if (_leadsPageList != value)
//                {
//                    _leadsPageList = value;
//                    RaisePropertyChanged("LeadsPageList");
//                }
//            }
//        }
//
//
//        private string _currentFileId;
//        public string CurrentFileId
//        {
//            get { return _currentFileId; }
//            set
//            {
//                if (_currentFileId != value)
//                {
//                    _currentFileId = value;
//                    RaisePropertyChanged("CurrentFileId");
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
//        private int _accNos;
//
//        public int AccNos
//        {
//            get { return _accNos; }
//            set
//            {
//                if (_accNos != value)
//                {
//                    _accNos = value;
//                    RaisePropertyChanged("AccNos");
//                }
//            }
//        }
//
//        private int _conNos;
//
//        public int ConNos
//        {
//            get { return _conNos; }
//            set
//            {
//                if (_conNos != value)
//                {
//                    _conNos = value;
//                    RaisePropertyChanged("ConNos");
//                }
//            }
//        }
//
//        private int _leadNos;
//
//        public int LeadNos
//        {
//            get { return _leadNos; }
//            set
//            {
//                if (_leadNos != value)
//                {
//                    _leadNos = value;
//                    RaisePropertyChanged("LeadNos");
//                }
//            }
//        }
//
//        private int _caseNos;
//
//        public int CaseNos
//        {
//            get { return _caseNos; }
//            set
//            {
//                if (_caseNos != value)
//                {
//                    _caseNos = value;
//                    RaisePropertyChanged("CaseNos");
//                }
//            }
//        }
//
//
//
//
//        private long _totalElements;
//
//        public long totalElements
//        {
//            get { return _totalElements; }
//            set
//            {
//                if (_totalElements != value)
//                {
//                    _totalElements = value;
//                    RaisePropertyChanged("totalElements");
//                }
//            }
//        }
////
////        private int _totalPages;
////
////        public int totalPages
////        {
////            get { return _totalPages; }
////            set
////            {
////                if (_totalPages != value)
////                {
////                    _totalPages = value;
////                    RaisePropertyChanged("totalPages");
////                }
////            }
////        }
//
//
//        private string _executivesales;
//        public string executivesales
//        {
//            get { return _executivesales; }
//            set
//            {
//                if (_executivesales != value)
//                {
//                    _executivesales = value;
//                    RaisePropertyChanged("executivesales");
//                }
//            }
//        }
//        private string _company;
//        public string company
//        {
//            get { return _company; }
//            set
//            {
//                if (_company != value)
//                {
//                    _company = value;
//                    RaisePropertyChanged("company");
//                }
//            }
//        }
//        private string _dept;
//        public string dept
//        {
//            get { return _dept; }
//            set
//            {
//                if (_dept != value)
//                {
//                    _dept = value;
//                    RaisePropertyChanged("dept");
//                }
//            }
//        }
//        private string _lastName;
//        public string lastName
//        {
//            get { return _lastName; }
//            set
//            {
//                if (_lastName != value)
//                {
//                    _lastName = value;
//                    RaisePropertyChanged("lastName");
//                }
//            }
//        }
//        private string _firstName;
//        public string firstName
//        {
//            get { return _firstName; }
//            set
//            {
//                if (_firstName != value)
//                {
//                    _firstName = value;
//                    RaisePropertyChanged("firstName");
//                }
//            }
//        }
//        private string _workPhone;
//        public string workPhone
//        {
//            get { return _workPhone; }
//            set
//            {
//                if (_workPhone != value)
//                {
//                    _workPhone = value;
//                    RaisePropertyChanged("workPhone");
//                }
//            }
//        }
//        private string _cellPhone;
//        public string cellPhone
//        {
//            get { return _cellPhone; }
//            set
//            {
//                if (_cellPhone != value)
//                {
//                    _cellPhone = value;
//                    RaisePropertyChanged("cellPhone");
//                }
//            }
//        }
//
//        private string _productstring;
//        public string productstring
//        {
//            get { return _productstring; }
//            set
//            {
//                if (_productstring != value)
//                {
//                    _productstring = value;
//                    RaisePropertyChanged("productstring");
//                }
//            }
//        }
//
//
//        private string _leadsnewnote;
//        public string leadsnewnote
//        {
//            get { return _leadsnewnote; }
//            set
//            {
//                if (_leadsnewnote != value)
//                {
//                    _leadsnewnote = value;
//                    RaisePropertyChanged("leadsnewnote");
//                }
//            }
//        }
//
//        
//
//        private string _notes;
//        public string notes
//        {
//            get { return _notes; }
//            set
//            {
//                if (_notes != value)
//                {
//                    _notes = value;
//                    RaisePropertyChanged("notes");
//                }
//            }
//        }
//
//
//
//        private string _category;
//        public string category
//        {
//            get { return _category; }
//            set
//            {
//                if (_category != value)
//                {
//                    _category = value;
//                    RaisePropertyChanged("category");
//                }
//            }
//        }
//
//        private string _grade;
//        public string grade
//        {
//            get { return _grade; }
//            set
//            {
//                if (_grade != value)
//                {
//                    _grade = value;
//                    RaisePropertyChanged("grade");
//                }
//            }
//        }
//
//        private string _importance;
//        public string importance
//        {
//            get { return _importance; }
//            set
//            {
//                if (_importance != value)
//                {
//                    _importance = value;
//                    RaisePropertyChanged("importance");
//                }
//            }
//        }
//
//        private string _sourceCode;
//        public string sourceCode
//        {
//            get { return _sourceCode; }
//            set
//            {
//                if (_sourceCode != value)
//                {
//                    _sourceCode = value;
//                    RaisePropertyChanged("sourceCode");
//                }
//            }
//        }
//
//        private string _sales;
//        public string sales
//        {
//            get { return _sales; }
//            set
//            {
//                if (_sales != value)
//                {
//                    _sales = value;
//                    RaisePropertyChanged("sales");
//                }
//            }
//        }
//
//
//
//        private string _saleProject;
//        public string saleProject
//        {
//            get { return _saleProject; }
//            set
//            {
//                if (_saleProject != value)
//                {
//                    _saleProject = value;
//                    RaisePropertyChanged("saleProject");
//                }
//            }
//        }
//
//
//
//        private string _importedFile;
//        public string importedFile
//        {
//            get { return _importedFile; }
//            set
//            {
//                if (_importedFile != value)
//                {
//                    _importedFile = value;
//                    RaisePropertyChanged("importedFile");
//                }
//            }
//        }
//
//        private string _navigateType;
//        public string navigateType
//        {
//            get { return _navigateType; }
//            set
//            {
//                if (_navigateType != value)
//                {
//                    _navigateType = value;
//                    RaisePropertyChanged("navigateType");
//                }
//            }
//        }
//
//        private string _Cs;
//        public string Cs
//        {
//            get { return _Cs; }
//            set
//            {
//                if (_Cs != value)
//                {
//                    _Cs = value;
//                    RaisePropertyChanged("Cs");
//                }
//            }
//        }
//
//        private string _Ce;
//        public string Ce
//        {
//            get { return _Ce; }
//            set
//            {
//                if (_Ce != value)
//                {
//                    _Ce = value;
//                    RaisePropertyChanged("Ce");
//                }
//            }
//        }
//
//        private string _Ps;
//        public string Ps
//        {
//            get { return _Ps; }
//            set
//            {
//                if (_Ps != value)
//                {
//                    _Ps = value;
//                    RaisePropertyChanged("Ps");
//                }
//            }
//        }
//
//
//        private string _CallNO;
//        public string CallNO
//        {
//            get { return _CallNO; }
//            set
//            {
//                if (_CallNO != value)
//                {
//                    _CallNO = value;
//                    RaisePropertyChanged("CallNO");
//                }
//            }
//        }
//
//
//        private string _Pe;
//        public string Pe
//        {
//            get { return _Pe; }
//            set
//            {
//                if (_Pe != value)
//                {
//                    _Pe = value;
//                    RaisePropertyChanged("Pe");
//                }
//            }
//        }
//        private string _Cp;
//        public string Cp
//        {
//            get { return _Cp; }
//            set
//            {
//                if (_Cp != value)
//                {
//                    _Cp = value;
//                    RaisePropertyChanged("Cp");
//                }
//            }
//        }
//        private string _Tp;
//        public string Tp
//        {
//            get { return _Tp; }
//            set
//            {
//                if (_Tp != value)
//                {
//                    _Tp = value;
//                    RaisePropertyChanged("Tp");
//                }
//            }
//        }
//        private string _St;
//        public string St
//        {
//            get { return _St; }
//            set
//            {
//                if (_St != value)
//                {
//                    _St = value;
//                    RaisePropertyChanged("St");
//                }
//            }
//        }
//
//        private string _Si;
//        public string Si
//        {
//            get { return _Si; }
//            set
//            {
//                if (_Si != value)
//                {
//                    _Si = value;
//                    RaisePropertyChanged("Si");
//                }
//            }
//        }
//
//        private string _LS;
//        public string LS
//        {
//            get { return _LS; }
//            set
//            {
//                if (_LS != value)
//                {
//                    _LS = value;
//                    RaisePropertyChanged("LS");
//                }
//            }
//        }
//
//        private string _ProvinceName;
//        public string ProvinceName
//        {
//            get { return _ProvinceName; }
//            set
//            {
//                if (_ProvinceName != value)
//                {
//                    _ProvinceName = value;
//                    RaisePropertyChanged("ProvinceName");
//                }
//            }
//        }
//
//
//        private string _LeadsKey;
//        public string LeadsKey
//        {
//            get { return _LeadsKey; }
//            set
//            {
//                if (_LeadsKey != value)
//                {
//                    _LeadsKey = value;
//                    RaisePropertyChanged("LeadsKey");
//                }
//            }
//        }
//
//        private string _StatusString;
//        public string StatusString
//        {
//            get { return _StatusString; }
//            set
//            {
//                if (_StatusString != value)
//                {
//                    _StatusString = value;
//                    RaisePropertyChanged("StatusString");
//                }
//            }
//        }
//
//        private string _RelationString;
//        public string RelationString
//        {
//            get { return _RelationString; }
//            set
//            {
//                if (_RelationString != value)
//                {
//                    _RelationString = value;
//                    RaisePropertyChanged("RelationString");
//                }
//            }
//        }
//
//
//
//        private string _SaleId;
//        public string SaleId
//        {
//            get { return _SaleId; }
//            set
//            {
//                if (_SaleId != value)
//                {
//                    _SaleId = value;
//                    RaisePropertyChanged("SaleId");
//                }
//            }
//        }
//
//        private string _SalesIds;
//        public string SalesIds
//        {
//            get { return _SalesIds; }
//            set
//            {
//                if (_SalesIds != value)
//                {
//                    _SalesIds = value;
//                    RaisePropertyChanged("SalesIds");
//                }
//            }
//        }
//
//
//        private string _SalesNames;
//        public string SalesNames
//        {
//            get { return _SalesNames; }
//            set
//            {
//                if (_SalesNames != value)
//                {
//                    _SalesNames = value;
//                    RaisePropertyChanged("SalesNames");
//                }
//            }
//        }
//
//
//        private string _SalesIds2;
//        public string SalesIds2
//        {
//            get { return _SalesIds2; }
//            set
//            {
//                if (_SalesIds2 != value)
//                {
//                    _SalesIds2 = value;
//                    RaisePropertyChanged("SalesIds2");
//                }
//            }
//        }
//
//
//        private string _SalesNames2;
//        public string SalesNames2
//        {
//            get { return _SalesNames2; }
//            set
//            {
//                if (_SalesNames2 != value)
//                {
//                    _SalesNames2 = value;
//                    RaisePropertyChanged("SalesNames2");
//                }
//            }
//        }
//
//
//        private string _InsideIds;
//        public string InsideIds
//        {
//            get { return _InsideIds; }
//            set
//            {
//                if (_InsideIds != value)
//                {
//                    _InsideIds = value;
//                    RaisePropertyChanged("InsideIds");
//                }
//            }
//        }
//
//
//        private string _InsideNames;
//        public string InsideNames
//        {
//            get { return _InsideNames; }
//            set
//            {
//                if (_InsideNames != value)
//                {
//                    _InsideNames = value;
//                    RaisePropertyChanged("InsideNames");
//                }
//            }
//        }
//
//
//
//        private string _EmployeeStatus;
//        public string EmployeeStatus
//        {
//            get { return _EmployeeStatus; }
//            set
//            {
//                if (_EmployeeStatus != value)
//                {
//                    _EmployeeStatus = value;
//                    RaisePropertyChanged("EmployeeStatus");
//                }
//            }
//        }
//
//
//        private string _SCIds;
//        public string SCIds
//        {
//            get { return _SCIds; }
//            set
//            {
//                if (_SCIds != value)
//                {
//                    _SCIds = value;
//                    RaisePropertyChanged("SCIds");
//                }
//            }
//        }
//
//
//        private string _SCNames;
//        public string SCNames
//        {
//            get { return _SCNames; }
//            set
//            {
//                if (_SCNames != value)
//                {
//                    _SCNames = value;
//                    RaisePropertyChanged("SCNames");
//                }
//            }
//        }
//
//
//
//
//        private string _insiteSaleId;
//        public string insiteSaleId
//        {
//            get { return _insiteSaleId; }
//            set
//            {
//                if (_insiteSaleId != value)
//                {
//                    _insiteSaleId = value;
//                    RaisePropertyChanged("insiteSaleId");
//                }
//            }
//        }
//
//        private string _companyId;
//        public string CompanyId
//        {
//            get { return _companyId; }
//            set
//            {
//                if (_companyId != value)
//                {
//                    _companyId = value;
//                    RaisePropertyChanged("CompanyId");
//                }
//            }
//        }
//
//        private string _customerId;
//        public string CustomerId
//        {
//            get { return _customerId; }
//            set
//            {
//                if (_customerId != value)
//                {
//                    _customerId = value;
//                    RaisePropertyChanged("CustomerId");
//                }
//            }
//        }
//
//
//        private string _SourceCode;
//        public string SourceCode
//        {
//            get { return _SourceCode; }
//            set
//            {
//                if (_SourceCode != value)
//                {
//                    _SourceCode = value;
//                    RaisePropertyChanged("SourceCode");
//                }
//            }
//        }
//
//        private string _Industry;
//        public string Industry
//        {
//            get { return _Industry; }
//            set
//            {
//                if (_Industry != value)
//                {
//                    _Industry = value;
//                    RaisePropertyChanged("Industry");
//                }
//            }
//        }
//
//        private string _AccKey;
//        public string AccKey
//        {
//            get { return _AccKey; }
//            set
//            {
//                if (_AccKey != value)
//                {
//                    _AccKey = value;
//                    RaisePropertyChanged("AccKey");
//                }
//            }
//        }
//
//        private string _ComPriority;
//        public string ComPriority
//        {
//            get { return _ComPriority; }
//            set
//            {
//                if (_ComPriority != value)
//                {
//                    _ComPriority = value;
//                    RaisePropertyChanged("ComPriority");
//                }
//            }
//        }
//
//
//        private string _CaseKey;
//        public string CaseKey
//        {
//            get { return _CaseKey; }
//            set
//            {
//                if (_CaseKey != value)
//                {
//                    _CaseKey = value;
//                    RaisePropertyChanged("CaseKey");
//                }
//            }
//        }
//
//        private string _CustomerKey;
//        public string CustomerKey
//        {
//            get { return _CustomerKey; }
//            set
//            {
//                if (_CustomerKey != value)
//                {
//                    _CustomerKey = value;
//                    RaisePropertyChanged("CustomerKey");
//                }
//            }
//        }
//
//
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
//
//        private Visibility _canNew;
//        public Visibility CanNew
//        {
//            get { return _canNew; }
//            set
//            {
//                if (_canNew != value)
//                {
//                    _canNew = value;
//                    RaisePropertyChanged("CanNew");
//                }
//            }
//        }
//
//        private ObservableCollection<Customer> _customerList;
//        public ObservableCollection<Customer> CustomerList
//        {
//            get { return _customerList; }
//            set
//            {
//                if (_customerList != value)
//                {
//                    _customerList = value;
//                    RaisePropertyChanged("CustomerList");
//                }
//            }
//        }
//        
//
//        private ObservableCollection<Customer> _salesProjectCustomerList;
//        public ObservableCollection<Customer> SalesProjectCustomerList
//        {
//            get { return _salesProjectCustomerList; }
//            set
//            {
//                if (_salesProjectCustomerList != value)
//                {
//                    _salesProjectCustomerList = value;
//                    RaisePropertyChanged("SalesProjectCustomerList");
//                }
//            }
//        }
//
//        private ObservableCollection<Customer> _customers;
//        public ObservableCollection<Customer> Customers
//        {
//            get { return _customers; }
//            set
//            {
//                if (_customers != value)
//                {
//                    _customers = value;
//                    RaisePropertyChanged("Customers");
//                }
//            }
//        }
//
//
//        private ObservableCollection<Customer> _duplicateList;
//        public ObservableCollection<Customer> DuplicateList
//        {
//            get { return _duplicateList; }
//            set
//            {
//                if (_duplicateList != value)
//                {
//                    _duplicateList = value;
//                    RaisePropertyChanged("DuplicateList");
//                }
//            }
//        }
//
//        private ObservableCollection<Customer> _checkedCustomerList;
//        public ObservableCollection<Customer> CheckedCustomerList
//        {
//            get { return _checkedCustomerList; }
//            set
//            {
//                if (_checkedCustomerList != value)
//                {
//                    _checkedCustomerList = value;
//                    RaisePropertyChanged("CheckedCustomerList");
//                }
//            }
//        }
//
//        private ObservableCollection<Customer> _CtiCustomerList;
//        public ObservableCollection<Customer> CtiCustomerList
//        {
//            get { return _CtiCustomerList; }
//            set
//            {
//                if (_CtiCustomerList != value)
//                {
//                    _CtiCustomerList = value;
//                    RaisePropertyChanged("CtiCustomerList");
//                }
//            }
//        }
//
//
//
//        private string _CtiNO;
//        public string CtiNO
//        {
//            get { return _CtiNO; }
//            set
//            {
//                if (_CtiNO != value)
//                {
//                    _CtiNO = value;
//                    RaisePropertyChanged("CtiNO");
//                }
//            }
//        }
//
//        private string _CtiInNO;
//        public string CtiInNO
//        {
//            get { return _CtiInNO; }
//            set
//            {
//                if (_CtiInNO != value)
//                {
//                    _CtiInNO = value;
//                    RaisePropertyChanged("CtiInNO");
//                }
//            }
//        }
//
//        private string _CtiOutNO;
//        public string CtiOutNO
//        {
//            get { return _CtiOutNO; }
//            set
//            {
//                if (_CtiOutNO != value)
//                {
//                    _CtiOutNO = value;
//                    RaisePropertyChanged("CtiOutNO");
//                }
//            }
//        }
//
//
//        private string _CtiMobile;
//        public string CtiMobile
//        {
//            get { return _CtiMobile; }
//            set
//            {
//                if (_CtiMobile != value)
//                {
//                    _CtiMobile = value;
//                    RaisePropertyChanged("CtiMobile");
//                }
//            }
//        }
//
//        private string _CtiPhone;
//        public string CtiPhone
//        {
//            get { return _CtiPhone; }
//            set
//            {
//                if (_CtiPhone != value)
//                {
//                    _CtiPhone = value;
//                    RaisePropertyChanged("CtiPhone");
//                }
//            }
//        }
//
//        private Case _TCase;
//
//        public Case TCase
//        {
//            get { return _TCase; }
//            set
//            {
//                if (_TCase != value)
//                {
//                    _TCase = value;
//                    RaisePropertyChanged("TCase");
//                }
//            }
//        }
//
//        private Case _PassedCase;
//
//        public Case PassedCase
//        {
//            get { return _PassedCase; }
//            set
//            {
//                if (_PassedCase != value)
//                {
//                    _PassedCase = value;
//                    RaisePropertyChanged("PassedCase");
//                }
//            }
//        }
//
//        private CaseSearchDTO _CaseSearchDTO;
//
//        public CaseSearchDTO CaseSearchDTO
//        {
//            get { return _CaseSearchDTO; }
//            set
//            {
//                if (_CaseSearchDTO != value)
//                {
//                    _CaseSearchDTO = value;
//                    RaisePropertyChanged("CaseSearchDTO");
//                }
//            }
//        }
//
//        private LeadsSearchDTO _LeadsSearchDTO;
//
//        public LeadsSearchDTO LeadsSearchDTO
//        {
//            get { return _LeadsSearchDTO; }
//            set
//            {
//                if (_LeadsSearchDTO != value)
//                {
//                    _LeadsSearchDTO = value;
//                    RaisePropertyChanged("LeadsSearchDTO");
//                }
//            }
//        }
//
//        private CompanySearchDTO _CompanySearchDTO;
//
//        public CompanySearchDTO CompanySearchDTO
//        {
//            get { return _CompanySearchDTO; }
//            set
//            {
//                if (_CompanySearchDTO != value)
//                {
//                    _CompanySearchDTO = value;
//                    RaisePropertyChanged("CompanySearchDTO");
//                }
//            }
//        }
//
//        private CustomerSearchDTO _CustomerSearchDTO;
//
//        public CustomerSearchDTO CustomerSearchDTO
//        {
//            get { return _CustomerSearchDTO; }
//            set
//            {
//                if (_CustomerSearchDTO != value)
//                {
//                    _CustomerSearchDTO = value;
//                    RaisePropertyChanged("CustomerSearchDTO");
//                }
//            }
//        }
//
//
//        private SaleProjectCustomerSearchDTO _spcDto;
//
//        public SaleProjectCustomerSearchDTO spcDto
//        {
//            get { return _spcDto; }
//            set
//            {
//                if (_spcDto != value)
//                {
//                    _spcDto = value;
//                    RaisePropertyChanged("spcDto");
//                }
//            }
//        }
//
//        private ObservableCollection<SaleProjectCustomerVO> _spcList;
//
//        public ObservableCollection<SaleProjectCustomerVO> spcList
//        {
//            get { return _spcList; }
//            set
//            {
//                if (_spcList != value)
//                {
//                    _spcList = value;
//                    RaisePropertyChanged("spcList");
//                }
//            }
//        }
//
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
//
//
//
//        private ObservableCollection<CaseNote> _TcaseNotes;
//        public ObservableCollection<CaseNote> TcaseNotes
//        {
//            get { return _TcaseNotes; }
//            set
//            {
//                if (_TcaseNotes != value)
//                {
//                    _TcaseNotes = value;
//                    RaisePropertyChanged("TcaseNotes");
//                }
//            }
//        }
//
//
//        private ObservableCollection<Customer> _customerListInit;
//        public ObservableCollection<Customer> CustomerListInit
//        {
//            get { return _customerListInit; }
//            set
//            {
//                if (_customerListInit != value)
//                {
//                    _customerListInit = value;
//                    RaisePropertyChanged("CustomerListInit");
//                }
//            }
//        }
//
//        private ObservableCollection<Customer> _customerListPending;
//        public ObservableCollection<Customer> CustomerListPending
//        {
//            get { return _customerListPending; }
//            set
//            {
//                if (_customerListPending != value)
//                {
//                    _customerListPending = value;
//                    RaisePropertyChanged("CustomerListPending");
//                }
//            }
//        }
//
//        private ObservableCollection<Customer> _customerListLeads;
//        public ObservableCollection<Customer> CustomerListLeads
//        {
//            get { return _customerListLeads; }
//            set
//            {
//                if (_customerListLeads != value)
//                {
//                    _customerListLeads = value;
//                    RaisePropertyChanged("CustomerListLeads");
//                }
//            }
//        }
//
//        private ObservableCollection<Customer> _customerListInvalid;
//        public ObservableCollection<Customer> CustomerListInvalid
//        {
//            get { return _customerListInvalid; }
//            set
//            {
//                if (_customerListInvalid != value)
//                {
//                    _customerListInvalid = value;
//                    RaisePropertyChanged("CustomerListInvalid");
//                }
//            }
//        }
//
//        private ObservableCollection<Customer> _assignCallList;
//        public ObservableCollection<Customer> AssignCallList
//        {
//            get { return _assignCallList; }
//            set
//            {
//                if (_assignCallList != value)
//                {
//                    _assignCallList = value;
//                    RaisePropertyChanged("AssignCallList");
//                }
//            }
//        }
//
//        private ObservableCollection<Customer> _pendingCallBackList;
//        public ObservableCollection<Customer> pendingCallBackList
//        {
//            get { return _pendingCallBackList; }
//            set
//            {
//                if (_pendingCallBackList != value)
//                {
//                    _pendingCallBackList = value;
//                    RaisePropertyChanged("pendingCallBackList");
//                }
//            }
//        }
//
//
//        private ObservableCollection<Leads> _leadsList;
//        public ObservableCollection<Leads> LeadsList
//        {
//            get { return _leadsList; }
//            set
//            {
//                if (_leadsList != value)
//                {
//                    _leadsList = value;
//                    RaisePropertyChanged("LeadsList");
//                }
//            }
//        }
//
//        private ObservableCollection<Leads> _leadsSet;
//        public ObservableCollection<Leads> LeadsSet
//        {
//            get { return _leadsSet; }
//            set
//            {
//                if (_leadsSet != value)
//                {
//                    _leadsSet = value;
//                    RaisePropertyChanged("LeadsSet");
//                }
//            }
//        }
//
//        private ObservableCollection<LeadsForListVO> _leadss;
//        public ObservableCollection<LeadsForListVO> Leadss
//        {
//            get { return _leadss; }
//            set
//            {
//                if (_leadss != value)
//                {
//                    _leadss = value;
//                    RaisePropertyChanged("Leadss");
//                }
//            }
//        }
//        private ObservableCollection<Leads> _leadex;
//        public ObservableCollection<Leads> Leadex
//        {
//            get { return _leadex; }
//            set
//            {
//                if (_leadex != value)
//                {
//                    _leadex = value;
//                    RaisePropertyChanged("Leadex");
//                }
//            }
//        }
//
//        private ObservableCollection<LeadsVO> _leadsVOex;
//        public ObservableCollection<LeadsVO> leadsVOex
//        {
//            get { return _leadsVOex; }
//            set
//            {
//                if (_leadsVOex != value)
//                {
//                    _leadsVOex = value;
//                    RaisePropertyChanged("leadsVOex");
//                }
//            }
//        }
//
//
//
//        private ObservableCollection<LeadsVO> _leadsVOList;
//        public ObservableCollection<LeadsVO> LeadsVOList
//        {
//            get { return _leadsVOList; }
//            set
//            {
//                if (_leadsVOList != value)
//                {
//                    _leadsVOList = value;
//                    RaisePropertyChanged("LeadsVOList");
//                }
//            }
//        }
//
//        private ObservableCollection<CaseVO> _caseVOList;
//        public ObservableCollection<CaseVO> CaseVOList
//        {
//            get { return _caseVOList; }
//            set
//            {
//                if (_caseVOList != value)
//                {
//                    _caseVOList = value;
//                    RaisePropertyChanged("CaseVOList");
//                }
//            }
//        }
//
//
//
//        private ObservableCollection<CompanyVO> _AccountVOList;
//        public ObservableCollection<CompanyVO> AccountVOList
//        {
//            get { return _AccountVOList; }
//            set
//            {
//                if (_AccountVOList != value)
//                {
//                    _AccountVOList = value;
//                    RaisePropertyChanged("AccountVOList");
//                }
//            }
//        }
//
//        private ObservableCollection<CustomerVO> _CustomerVOList;
//        public ObservableCollection<CustomerVO> CustomerVOList
//        {
//            get { return _CustomerVOList; }
//            set
//            {
//                if (_CustomerVOList != value)
//                {
//                    _CustomerVOList = value;
//                    RaisePropertyChanged("CustomerVOList");
//                }
//            }
//        }
//
//
//        private ObservableCollection<Leads> _showleadsList;
//        public ObservableCollection<Leads> ShowLeadsList
//        {
//            get { return _showleadsList; }
//            set
//            {
//                if (_showleadsList != value)
//                {
//                    _showleadsList = value;
//                    RaisePropertyChanged("ShowLeadsList");
//                }
//            }
//        }
//
//
//        private Leads _selectLeads;
//
//        public Leads SelectLeads
//        {
//            get { return _selectLeads; }
//            set
//            {
//                if (_selectLeads != value)
//                {
//                    _selectLeads = value;
//                    RaisePropertyChanged("SelectLeads");
//                }
//            }
//        }
//
//        private Leads _tempLeads;
//
//        public Leads TempLeads
//        {
//            get { return _tempLeads; }
//            set
//            {
//                if (_tempLeads != value)
//                {
//                    _tempLeads = value;
//                    RaisePropertyChanged("TempLeads");
//                }
//            }
//        }
//
//        private ObservableCollection<Case> _caseList;
//        public ObservableCollection<Case> CaseList
//        {
//            get { return _caseList; }
//            set
//            {
//                if (_caseList != value)
//                {
//                    _caseList = value;
//                    RaisePropertyChanged("CaseList");
//                }
//            }
//        }
//        private ObservableCollection<CasesForListVO> _cases;
//        public ObservableCollection<CasesForListVO> cases
//        {
//            get { return _cases; }
//            set
//            {
//                if (_cases != value)
//                {
//                    _cases = value;
//                    RaisePropertyChanged("cases");
//                }
//            }
//        }
//        private ObservableCollection<CaseVO> _casesEX;
//        public ObservableCollection<CaseVO> casesEX
//        {
//            get { return _casesEX; }
//            set
//            {
//                if (_casesEX != value)
//                {
//                    _casesEX = value;
//                    RaisePropertyChanged("casesEX");
//                }
//            }
//        }
//        
//
//        private ObservableCollection<Case> _casesSet;
//        public ObservableCollection<Case> casesSet
//        {
//            get { return _casesSet; }
//            set
//            {
//                if (_casesSet != value)
//                {
//                    _casesSet = value;
//                    RaisePropertyChanged("casesSet");
//                }
//            }
//        }
//
//
//        private ObservableCollection<Case> _showCaseList;
//        public ObservableCollection<Case> ShowCaseList
//        {
//            get { return _showCaseList; }
//            set
//            {
//                if (_showCaseList != value)
//                {
//                    _showCaseList = value;
//                    RaisePropertyChanged("ShowCaseList");
//                }
//            }
//        }
//        
//
//        private Case _selectCase;
//
//        public Case SelectCase
//        {
//            get { return _selectCase; }
//            set
//            {
//                if (_selectCase != value)
//                {
//                    _selectCase = value;
//                    RaisePropertyChanged("SelectCase");
//                }
//            }
//        }
//
//        private Case _showCase;
//
//        public Case ShowCase
//        {
//            get { return _showCase; }
//            set
//            {
//                if (_showCase != value)
//                {
//                    _showCase = value;
//                    RaisePropertyChanged("ShowCase");
//                }
//            }
//        }
//
//
//        private ObservableCollection<Customer> _maintainList;
//        public ObservableCollection<Customer> MaintainList
//        {
//            get { return _maintainList; }
//            set
//            {
//                if (_maintainList != value)
//                {
//                    _maintainList = value;
//                    RaisePropertyChanged("MaintainList");
//                }
//            }
//        }
//
//
//        private ObservableCollection<ProductRelation> _productRelationList;
//        public ObservableCollection<ProductRelation> ProductRelationList
//        {
//            get { return _productRelationList; }
//            set
//            {
//                if (_productRelationList != value)
//                {
//                    _productRelationList = value;
//                    RaisePropertyChanged("ProductRelationList");
//                }
//            }
//        }
//
//
//        private ObservableCollection<Customer> _opportunityList;
//        public ObservableCollection<Customer> OpportunityList
//        {
//            get { return _opportunityList; }
//            set
//            {
//                if (_opportunityList != value)
//                {
//                    _opportunityList = value;
//                    RaisePropertyChanged("OpportunityList");
//                }
//            }
//        }
//
//        private ObservableCollection<Customer> _HistoryList;
//        public ObservableCollection<Customer> HistoryList
//        {
//            get { return _HistoryList; }
//            set
//            {
//                if (_HistoryList != value)
//                {
//                    _HistoryList = value;
//                    RaisePropertyChanged("HistoryList");
//                }
//            }
//        }
//
//        private ObservableCollection<Employee> _salesList;
//        public ObservableCollection<Employee> SalesList
//        {
//            get { return _salesList; }
//            set
//            {
//                if (_salesList != value)
//                {
//                    _salesList = value;
//                    RaisePropertyChanged("SalesList");
//                }
//            }
//        }
//
//
//        private ObservableCollection<Employee> _salesListA;
//        public ObservableCollection<Employee> SalesListA
//        {
//            get { return _salesListA; }
//            set
//            {
//                if (_salesListA != value)
//                {
//                    _salesListA = value;
//                    RaisePropertyChanged("SalesListA");
//                }
//            }
//        }
//
//
//        private ObservableCollection<Employee> _salesListB;
//        public ObservableCollection<Employee> SalesListB
//        {
//            get { return _salesListB; }
//            set
//            {
//                if (_salesListB != value)
//                {
//                    _salesListB = value;
//                    RaisePropertyChanged("SalesListB");
//                }
//            }
//        }
//
//
//        private ObservableCollection<Employee> _SalesListNull;
//        public ObservableCollection<Employee> SalesListNull
//        {
//            get { return _SalesListNull; }
//            set
//            {
//                if (_SalesListNull != value)
//                {
//                    _SalesListNull = value;
//                    RaisePropertyChanged("SalesListNull");
//                }
//            }
//        }
//
//        private ObservableCollection<Employee> _SalesListAA;
//        public ObservableCollection<Employee> SalesListAA
//        {
//            get { return _SalesListAA; }
//            set
//            {
//                if (_SalesListAA != value)
//                {
//                    _SalesListAA = value;
//                    RaisePropertyChanged("SalesListAA");
//                }
//            }
//        }
//
//
//        private ObservableCollection<Employee> _SalesListAB;
//        public ObservableCollection<Employee> SalesListAB
//        {
//            get { return _SalesListAB; }
//            set
//            {
//                if (_SalesListAB != value)
//                {
//                    _SalesListAB = value;
//                    RaisePropertyChanged("SalesListAB");
//                }
//            }
//        }
//
//        private ObservableCollection<SalesDto> _SalesDtoListNull;
//        public ObservableCollection<SalesDto> SalesDtoListNull
//        {
//            get { return _SalesDtoListNull; }
//            set
//            {
//                if (_SalesDtoListNull != value)
//                {
//                    _SalesDtoListNull = value;
//                    RaisePropertyChanged("SalesDtoListNull");
//                }
//            }
//        }
//
//        
//
//
//        private ObservableCollection<Employee> _SalesListAll;
//        public ObservableCollection<Employee> SalesListAll
//        {
//            get { return _SalesListAll; }
//            set
//            {
//                if (_SalesListAll != value)
//                {
//                    _SalesListAll = value;
//                    RaisePropertyChanged("SalesListAll");
//                }
//            }
//        }
//
//        private ObservableCollection<SourceCode> _sourceCodeList;
//        public ObservableCollection<SourceCode> SourceCodeList
//        {
//            get { return _sourceCodeList; }
//            set
//            {
//                if (_sourceCodeList != value)
//                {
//                    _sourceCodeList = value;
//                    RaisePropertyChanged("SourceCodeList");
//                }
//            }
//        }
//
//        private ObservableCollection<SourceCode> _sourceCodeAll;
//        public ObservableCollection<SourceCode> SourceCodeAll
//        {
//            get { return _sourceCodeAll; }
//            set
//            {
//                if (_sourceCodeAll != value)
//                {
//                    _sourceCodeAll = value;
//                    RaisePropertyChanged("SourceCodeAll");
//                }
//            }
//        }
//
//
//        private Employee _currentSales;
//        public Employee CurrentSales
//        {
//            get { return _currentSales; }
//            set
//            {
//                if (_currentSales != value)
//                {
//                    _currentSales = value;
//                    RaisePropertyChanged("CurrentSales");
//                }
//            }
//        }
//
//        private Employee _tempSales;
//        public Employee TempSales
//        {
//            get { return _tempSales; }
//            set
//            {
//                if (_tempSales != value)
//                {
//                    _tempSales = value;
//                    RaisePropertyChanged("TempSales");
//                }
//            }
//        }
//
//
//        private ObservableCollection<Product> _productList;
//        public ObservableCollection<Product> ProductList
//        {
//            get { return _productList; }
//            set
//            {
//                if (_productList != value)
//                {
//                    _productList = value;
//                    RaisePropertyChanged("ProductList");
//                }
//            }
//        }
//
//        private ObservableCollection<Industry> _industryList;
//
//        public ObservableCollection<Industry> IndustryList
//        {
//            get { return _industryList; }
//            set
//            {
//                if (_industryList != value)
//                {
//                    _industryList = value;
//                    RaisePropertyChanged("IndustryList");
//                }
//            }
//        }
//
//        private ObservableCollection<Company> _companyList;
//
//        public ObservableCollection<Company> CompanyList
//        {
//            get { return _companyList; }
//            set
//            {
//                if (_companyList != value)
//                {
//                    _companyList = value;
//                    RaisePropertyChanged("CompanyList");
//                }
//            }
//        }
//
//        private ObservableCollection<CompanysForListVO> _companys;
//
//        public ObservableCollection<CompanysForListVO> Companys
//        {
//            get { return _companys; }
//            set
//            {
//                if (_companys != value)
//                {
//                    _companys = value;
//                    RaisePropertyChanged("Companys");
//                }
//            }
//        }
//
//
//        private ObservableCollection<Company> _ShowCompanyList;
//
//        public ObservableCollection<Company> ShowCompanyList
//        {
//            get { return _ShowCompanyList; }
//            set
//            {
//                if (_ShowCompanyList != value)
//                {
//                    _ShowCompanyList = value;
//                    RaisePropertyChanged("ShowCompanyList");
//                }
//            }
//        }
//
//        private ObservableCollection<Company> _ParentCompanyList;
//
//        public ObservableCollection<Company> ParentCompanyList
//        {
//            get { return _ParentCompanyList; }
//            set
//            {
//                if (_ParentCompanyList != value)
//                {
//                    _ParentCompanyList = value;
//                    RaisePropertyChanged("ParentCompanyList");
//                }
//            }
//        }
//
//
//        private ObservableCollection<Company> _companyListNull;
//
//        public ObservableCollection<Company> CompanyListNull
//        {
//            get { return _companyListNull; }
//            set
//            {
//                if (_companyListNull != value)
//                {
//                    _companyListNull = value;
//                    RaisePropertyChanged("CompanyListNull");
//                }
//            }
//        }
//
//        private CaseLeadsDetailDTO _CaseLeadsDetailDtos;
//
//        public CaseLeadsDetailDTO CaseLeadsDetailDtos
//        {
//            get { return _CaseLeadsDetailDtos; }
//            set
//            {
//                if (_CaseLeadsDetailDtos != value)
//                {
//                    _CaseLeadsDetailDtos = value;
//                    RaisePropertyChanged("CaseLeadsDetailDtos");
//                }
//            }
//        }
//
//
//        private CaseLeadsDetailDTO _CompanyDtos;
//
//        public CaseLeadsDetailDTO CompanyDtos
//        {
//            get { return _CompanyDtos; }
//            set
//            {
//                if (_CompanyDtos != value)
//                {
//                    _CompanyDtos = value;
//                    RaisePropertyChanged("CompanyDtos");
//                }
//            }
//        }
//        private ObservableCollection<CompanyDto> _companyDtoList;
//
//        public ObservableCollection<CompanyDto> CompanyDtoList
//        {
//            get { return _companyDtoList; }
//            set
//            {
//                if (_companyDtoList != value)
//                {
//                    _companyDtoList = value;
//                    RaisePropertyChanged("CompanyDtoList");
//                }
//            }
//        }
//
//        private ObservableCollection<CommonSearchDTO> _CommonSearchDTOList;
//
//        public ObservableCollection<CommonSearchDTO> CommonSearchDTOList
//        {
//            get { return _CommonSearchDTOList; }
//            set
//            {
//                if (_CommonSearchDTOList != value)
//                {
//                    _CommonSearchDTOList = value;
//                    RaisePropertyChanged("CommonSearchDTOList");
//                }
//            }
//        }
//
//
//
//        private ObservableCollection<Supplier> _supplierListNull;
//
//
//
//        public ObservableCollection<Supplier> SupplierListNull
//        {
//            get { return _supplierListNull; }
//            set
//            {
//                if (_supplierListNull != value)
//                {
//                    _supplierListNull = value;
//                    RaisePropertyChanged("SupplierListNull");
//                }
//            }
//        }
//
//        private ObservableCollection<Supplier> _ShowSupplierList;
//
//        public ObservableCollection<Supplier> ShowSupplierList
//        {
//            get { return _ShowSupplierList; }
//            set
//            {
//                if (_ShowSupplierList != value)
//                {
//                    _ShowSupplierList = value;
//                    RaisePropertyChanged("ShowSupplierList");
//                }
//            }
//        }
//
//        private ObservableCollection<CaseStatus> _caseStatusList;
//
//        public ObservableCollection<CaseStatus> CaseStatusList
//        {
//            get { return _caseStatusList; }
//            set
//            {
//                if (_caseStatusList != value)
//                {
//                    _caseStatusList = value;
//                    RaisePropertyChanged("CaseStatusList");
//                }
//            }
//        }
//
//        private ObservableCollection<CaseStatus> _caseStatusListNull;
//
//        public ObservableCollection<CaseStatus> CaseStatusListNull
//        {
//            get { return _caseStatusListNull; }
//            set
//            {
//                if (_caseStatusListNull != value)
//                {
//                    _caseStatusListNull = value;
//                    RaisePropertyChanged("CaseStatusListNull");
//                }
//            }
//        }
//
//        private ObservableCollection<SaleProject> _saleProjectList;
//
//        public ObservableCollection<SaleProject> saleProjectList
//        {
//            get { return _saleProjectList; }
//            set
//            {
//                if (_saleProjectList != value)
//                {
//                    _saleProjectList = value;
//                    RaisePropertyChanged("saleProjectList");
//                }
//            }
//        }
//
//        private ObservableCollection<SaleProject> _saleProjectNullList;
//
//        public ObservableCollection<SaleProject> saleProjectNullList
//        {
//            get { return _saleProjectNullList; }
//            set
//            {
//                if (_saleProjectNullList != value)
//                {
//                    _saleProjectNullList = value;
//                    RaisePropertyChanged("saleProjectNullList");
//                }
//            }
//        }
//
//        
//
//        private ObservableCollection<CaseType> _caseType;
//
//        public ObservableCollection<CaseType> CaseType
//        {
//            get { return _caseType; }
//            set
//            {
//                if (_caseType != value)
//                {
//                    _caseType = value;
//                    RaisePropertyChanged("CaseType");
//                }
//            }
//        }
//
//        private ObservableCollection<CaseType> _caseTypeNull;
//
//        public ObservableCollection<CaseType> CaseTypeNull
//        {
//            get { return _caseTypeNull; }
//            set
//            {
//                if (_caseTypeNull != value)
//                {
//                    _caseTypeNull = value;
//                    RaisePropertyChanged("CaseTypeNull");
//                }
//            }
//        }
//
//        private ObservableCollection<CaseType> _caseTypeEmpty;
//
//        public ObservableCollection<CaseType> CaseTypeEmpty
//        {
//            get { return _caseTypeEmpty; }
//            set
//            {
//                if (_caseTypeEmpty != value)
//                {
//                    _caseTypeEmpty = value;
//                    RaisePropertyChanged("CaseTypeEmpty");
//                }
//            }
//        }
//        
//
//        private ObservableCollection<Tags> _tagsList;
//
//        public ObservableCollection<Tags> tagsList
//        {
//            get { return _tagsList; }
//            set
//            {
//                if (_tagsList != value)
//                {
//                    _tagsList = value;
//                    RaisePropertyChanged("tagsList");
//                }
//            }
//        }
//
//        private Tags _SelectedTags;
//        public Tags SelectedTags
//        {
//            get { return _SelectedTags; }
//            set
//            {
//                if (_SelectedTags != value)
//                {
//                    _SelectedTags = value;
//                    RaisePropertyChanged("SelectedTags");
//                }
//            }
//        }
//
//        private ObservableCollection<BigClient> _bigClientList;
//
//        public ObservableCollection<BigClient> BigClientList
//        {
//            get { return _bigClientList; }
//            set
//            {
//                if (_bigClientList != value)
//                {
//                    _bigClientList = value;
//                    RaisePropertyChanged("BigClientList");
//                }
//            }
//        }
//
//        private ObservableCollection<Employee> _InsideSales;
//
//        public ObservableCollection<Employee> InsideSales
//        {
//            get { return _InsideSales; }
//            set
//            {
//                if (_InsideSales != value)
//                {
//                    _InsideSales = value;
//                    RaisePropertyChanged("InsideSales");
//                }
//            }
//        }
//
//
//        private BigClient _SelectedBigClient;
//        public BigClient SelectedBigClient
//        {
//            get { return _SelectedBigClient; }
//            set
//            {
//                if (_SelectedBigClient != value)
//                {
//                    _SelectedBigClient = value;
//                    RaisePropertyChanged("SelectedBigClient");
//                }
//            }
//        }
//
//        private Employee _SelectedInsideSale;
//        public Employee SelectedInsideSale
//        {
//            get { return _SelectedInsideSale; }
//            set
//            {
//                if (_SelectedInsideSale != value)
//                {
//                    _SelectedInsideSale = value;
//                    RaisePropertyChanged("SelectedInsideSale");
//                }
//            }
//        }
//
//        private Employee _SelectedSale;
//        public Employee SelectedSale
//        {
//            get { return _SelectedSale; }
//            set
//            {
//                if (_SelectedSale != value)
//                {
//                    _SelectedSale = value;
//                    RaisePropertyChanged("SelectedSale");
//                }
//            }
//        }
//
//        private SaleProject _Selectedproject;
//        public SaleProject Selectedproject
//        {
//            get { return _Selectedproject; }
//            set
//            {
//                if (_Selectedproject != value)
//                {
//                    _Selectedproject = value;
//                    RaisePropertyChanged("Selectedproject");
//                }
//            }
//        }
//
//        private SaleProjectCustomerStatus _selectedTstatusList;
//
//        public SaleProjectCustomerStatus SelectedTstatusList
//        {
//            get { return _selectedTstatusList; }
//            set
//            {
//                if (_selectedTstatusList != value)
//                {
//                    _selectedTstatusList = value;
//                    RaisePropertyChanged("SelectedTstatusList");
//                }
//            }
//        }
//
//
//        private CaseType _SelectedCaseType;
//        public CaseType SelectedCaseType
//        {
//            get { return _SelectedCaseType; }
//            set
//            {
//                if (_SelectedCaseType != value)
//                {
//                    _SelectedCaseType = value;
//                    RaisePropertyChanged("SelectedCaseType");
//                }
//            }
//        }
//
//
//        private CaseStatus _SelectedCaseStatus;
//        public CaseStatus SelectedCaseStatus
//        {
//            get { return _SelectedCaseStatus; }
//            set
//            {
//                if (_SelectedCaseStatus != value)
//                {
//                    _SelectedCaseStatus = value;
//                    RaisePropertyChanged("SelectedCaseStatus");
//                }
//            }
//        }
//
//
//        private ObservableCollection<Province> _provinceList;
//
//        public ObservableCollection<Province> ProvinceList
//        {
//            get { return _provinceList; }
//            set
//            {
//                if (_provinceList != value)
//                {
//                    _provinceList = value;
//                    RaisePropertyChanged("ProvinceList");
//                }
//            }
//        }
//
//
//        private ObservableCollection<LeadsGrade> _leadsGradeList;
//
//        public ObservableCollection<LeadsGrade> LeadsGradeList
//        {
//            get { return _leadsGradeList; }
//            set
//            {
//                if (_leadsGradeList != value)
//                {
//                    _leadsGradeList = value;
//                    RaisePropertyChanged("LeadsGradeList");
//                }
//            }
//        }
//
//
//        private ObservableCollection<LeadsGrade> _statusList;
//
//        public ObservableCollection<LeadsGrade> StatusList
//        {
//            get { return _statusList; }
//            set
//            {
//                if (_statusList != value)
//                {
//                    _statusList = value;
//                    RaisePropertyChanged("StatusList");
//                }
//            }
//        }
//
//
//
//        private ObservableCollection<Supplier> _supplierList;
//
//        public ObservableCollection<Supplier> SupplierList
//        {
//            get { return _supplierList; }
//            set
//            {
//                if (_supplierList != value)
//                {
//                    _supplierList = value;
//                    RaisePropertyChanged("SupplierList");
//                }
//            }
//        }
//
//        private ObservableCollection<OpportunityGrade> _opportunityGradeList;
//
//        public ObservableCollection<OpportunityGrade> OpportunityGradeList
//        {
//            get { return _opportunityGradeList; }
//            set
//            {
//                if (_opportunityGradeList != value)
//                {
//                    _opportunityGradeList = value;
//                    RaisePropertyChanged("OpportunityGradeList");
//                }
//            }
//        }
//
//        private ObservableCollection<SaleProjectCustomerStatus> _tstatusList;
//
//        public ObservableCollection<SaleProjectCustomerStatus> tstatusList
//        {
//            get { return _tstatusList; }
//            set
//            {
//                if (_tstatusList != value)
//                {
//                    _tstatusList = value;
//                    RaisePropertyChanged("tstatusList");
//                }
//            }
//        }
//
//        private ObservableCollection<SaleProjectCustomerStatus> _NstatusList;
//
//        public ObservableCollection<SaleProjectCustomerStatus> NstatusList
//        {
//            get { return _NstatusList; }
//            set
//            {
//                if (_NstatusList != value)
//                {
//                    _NstatusList = value;
//                    RaisePropertyChanged("NstatusList");
//                }
//            }
//        }
//
//        private ObservableCollection<SaleProjectCustomerStatusDetail> _NotCallStatusList;
//
//        public ObservableCollection<SaleProjectCustomerStatusDetail> NotCallStatusList
//        {
//            get { return _NotCallStatusList; }
//            set
//            {
//                if (_NotCallStatusList != value)
//                {
//                    _NotCallStatusList = value;
//                    RaisePropertyChanged("NotCallStatusList");
//                }
//            }
//        }
//
//        private ObservableCollection<SaleProjectCustomerStatusDetail> _ValueStatusList;
//
//        public ObservableCollection<SaleProjectCustomerStatusDetail> ValueStatusList
//        {
//            get { return _ValueStatusList; }
//            set
//            {
//                if (_ValueStatusList != value)
//                {
//                    _ValueStatusList = value;
//                    RaisePropertyChanged("ValueStatusList");
//                }
//            }
//        }
//
//
//
//        private ObservableCollection<SaleProjectCustomerStatusDetail> _PendingStatusList;
//
//        public ObservableCollection<SaleProjectCustomerStatusDetail> PendingsSatusList
//        {
//            get { return _PendingStatusList; }
//            set
//            {
//                if (_PendingStatusList != value)
//                {
//                    _PendingStatusList = value;
//                    RaisePropertyChanged("PendingsSatusList");
//                }
//            }
//        }
//
//        private ObservableCollection<SaleProjectCustomerStatusDetail> _NoInterestingStatusList;
//
//        public ObservableCollection<SaleProjectCustomerStatusDetail> NoInterestingStatusList
//        {
//            get { return _NoInterestingStatusList; }
//            set
//            {
//                if (_NoInterestingStatusList != value)
//                {
//                    _NoInterestingStatusList = value;
//                    RaisePropertyChanged("NoInterestingStatusList");
//                }
//            }
//        }
//
//        private ObservableCollection<SaleProjectCustomerStatusDetail> _NoAnswerStatusList;
//
//        public ObservableCollection<SaleProjectCustomerStatusDetail> NoAnswerStatusList
//        {
//            get { return _NoAnswerStatusList; }
//            set
//            {
//                if (_NoAnswerStatusList != value)
//                {
//                    _NoAnswerStatusList = value;
//                    RaisePropertyChanged("NoAnswerStatusList");
//                }
//            }
//        }
//
//        private ObservableCollection<SaleProjectCustomerStatusDetail> _NoValueStatusList;
//
//        public ObservableCollection<SaleProjectCustomerStatusDetail> NoValueStatusList
//        {
//            get { return _NoValueStatusList; }
//            set
//            {
//                if (_NoValueStatusList != value)
//                {
//                    _NoValueStatusList = value;
//                    RaisePropertyChanged("NoValueStatusList");
//                }
//            }
//        }
//
//        private ObservableCollection<Category> _categoryList;
//
//        public ObservableCollection<Category> CategoryList
//        {
//            get { return _categoryList; }
//            set
//            {
//                if (_categoryList != value)
//                {
//                    _categoryList = value;
//                    RaisePropertyChanged("CategoryList");
//                }
//            }
//        }
//
//
//        private Province _selectProvince;
//
//        public Province SelectProvince
//        {
//            get { return _selectProvince; }
//            set
//            {
//                if (_selectProvince != value)
//                {
//                    _selectProvince = value;
//                    RaisePropertyChanged("SelectProvince");
//                }
//            }
//        }
//        
//
//        private ObservableCollection<City> _cityList;
//
//        public ObservableCollection<City> CityList
//        {
//            get { return _cityList; }
//            set
//            {
//                if (_cityList != value)
//                {
//                    _cityList = value;
//                    RaisePropertyChanged("CityList");
//                }
//            }
//        }
//
//
//        private City _selectCity;
//
//        public City SelectCity
//        {
//            get { return _selectCity; }
//            set
//            {
//                if (_selectCity != value)
//                {
//                    _selectCity = value;
//                    RaisePropertyChanged("SelectCity");
//                }
//            }
//        }
//
//
//        private ObservableCollection<Area> _areaList;
//
//        public ObservableCollection<Area> AreaList
//        {
//            get { return _areaList; }
//            set
//            {
//                if (_areaList != value)
//                {
//                    _areaList = value;
//                    RaisePropertyChanged("AreaList");
//                }
//            }
//        }
//
//
//        private Area _selectArea;
//
//        public Area SelectArea
//        {
//            get { return _selectArea; }
//            set
//            {
//                if (_selectArea != value)
//                {
//                    _selectArea = value;
//                    RaisePropertyChanged("SelectArea");
//                }
//            }
//        }
//
//
//        private Customer _inboundCallCustomer;
//        public Customer InboundCallCustomer//Collapsed  //Visible
//        {
//            get { return _inboundCallCustomer; }
//            set
//            {
//                if (_inboundCallCustomer != value)
//                {
//                    _inboundCallCustomer = value;
//                    RaisePropertyChanged("InboundCallCustomer");
//                }
//            }
//        }
//
//        private Customer _SelectedCustomer;
//        public Customer SelectedCustomer//Collapsed  //Visible
//        {
//            get { return _SelectedCustomer; }
//            set
//            {
//                if (_SelectedCustomer != value)
//                {
//                    _SelectedCustomer = value;
//                    RaisePropertyChanged("SelectedCustomer");
//                }
//            }
//        }
//
//        private Customer _AddedCustomer;
//        public Customer AddedCustomer//Collapsed  //Visible
//        {
//            get { return _AddedCustomer; }
//            set
//            {
//                if (_AddedCustomer != value)
//                {
//                    _AddedCustomer = value;
//                    RaisePropertyChanged("AddedCustomer");
//                }
//            }
//        }
//
//        private Customer _ShowedCustomer;
//        public Customer ShowedCustomer//Collapsed  //Visible
//        {
//            get { return _ShowedCustomer; }
//            set
//            {
//                if (_ShowedCustomer != value)
//                {
//                    _ShowedCustomer = value;
//                    RaisePropertyChanged("ShowedCustomer");
//                }
//            }
//        }
//
//
//        private ObservableCollection<Customer> _RelatedCustomer;
//        public ObservableCollection<Customer> RelatedCustomer//Collapsed  //Visible
//        {
//            get { return _RelatedCustomer; }
//            set
//            {
//                if (_RelatedCustomer != value)
//                {
//                    _RelatedCustomer = value;
//                    RaisePropertyChanged("RelatedCustomer");
//                }
//            }
//        }
//
//
//
//        private ObservableCollection<Company> _RelatedCompany;
//        public ObservableCollection<Company> RelatedCompany//Collapsed  //Visible
//        {
//            get { return _RelatedCompany; }
//            set
//            {
//                if (_RelatedCompany != value)
//                {
//                    _RelatedCompany = value;
//                    RaisePropertyChanged("RelatedCompany");
//                }
//            }
//        }
//
//
//        private Company _SelectedCompany;
//        public Company SelectedCompany
//        {
//            get { return _SelectedCompany; }
//            set
//            {
//                if (_SelectedCompany != value)
//                {
//                    _SelectedCompany = value;
//                    RaisePropertyChanged("SelectedCompany");
//                }
//            }
//        }
//
//
//        private Company _CurrentCompany;
//        public Company CurrentCompany
//        {
//            get { return _CurrentCompany; }
//            set
//            {
//                if (_CurrentCompany != value)
//                {
//                    _CurrentCompany = value;
//                    RaisePropertyChanged("CurrentCompany");
//                }
//            }
//        }
//
//        private CompanyPassDto _CurrentCompanyPassDto;
//        public CompanyPassDto CurrentCompanyPassDto
//        {
//            get { return _CurrentCompanyPassDto; }
//            set
//            {
//                if (_CurrentCompanyPassDto != value)
//                {
//                    _CurrentCompanyPassDto = value;
//                    RaisePropertyChanged("CurrentCompanyPassDto");
//                }
//            }
//        }
//
//        private CompanyShowDTO _CompanyShowDTO;
//        public CompanyShowDTO CompanyShowDTO
//        {
//            get { return _CompanyShowDTO; }
//            set
//            {
//                if (_CompanyShowDTO != value)
//                {
//                    _CompanyShowDTO = value;
//                    RaisePropertyChanged("CompanyShowDTO");
//                }
//            }
//        }
//
//
//        private Company _FilterCompany;
//        public Company FilterCompany
//        {
//            get { return _FilterCompany; }
//            set
//            {
//                if (_FilterCompany != value)
//                {
//                    _FilterCompany = value;
//                    RaisePropertyChanged("FilterCompany");
//                }
//            }
//        }
//
//        private CompanyDto _FilterCompanyDto;
//        public CompanyDto FilterCompanyDto
//        {
//            get { return _FilterCompanyDto; }
//            set
//            {
//                if (_FilterCompanyDto != value)
//                {
//                    _FilterCompanyDto = value;
//                    RaisePropertyChanged("FilterCompanyDto");
//                }
//            }
//        }
//
//        private CommonSearchDTO _CommonSearchDTO;
//        public CommonSearchDTO CommonSearchDTO
//        {
//            get { return _CommonSearchDTO; }
//            set
//            {
//                if (_CommonSearchDTO != value)
//                {
//                    _CommonSearchDTO = value;
//                    RaisePropertyChanged("CommonSearchDTO");
//                }
//            }
//        }
//
//
//
//        private CompanyDto _searchCompanyDto;
//        public CompanyDto SearchCompanyDto
//        {
//            get { return _searchCompanyDto; }
//            set
//            {
//                if (_searchCompanyDto != value)
//                {
//                    _searchCompanyDto = value;
//                    RaisePropertyChanged("SearchCompanyDto");
//                }
//            }
//        }
//
//
//
//        private Supplier _FilterSupplier;
//        public Supplier FilterSupplier
//        {
//            get { return _FilterSupplier; }
//            set
//            {
//                if (_FilterSupplier != value)
//                {
//                    _FilterSupplier = value;
//                    RaisePropertyChanged("FilterSupplier");
//                }
//            }
//        }
//
//        private Company _CheckedCompany;
//        public Company CheckedCompany
//        {
//            get { return _CheckedCompany; }
//            set
//            {
//                if (_CheckedCompany != value)
//                {
//                    _CheckedCompany = value;
//                    RaisePropertyChanged("CheckedCompany");
//                }
//            }
//        }
//
//
//
//        private Company _ParentsCompany;
//        public Company ParentsCompany
//        {
//            get { return _ParentsCompany; }
//            set
//            {
//                if (_ParentsCompany != value)
//                {
//                    _ParentsCompany = value;
//                    RaisePropertyChanged("ParentsCompany");
//                }
//            }
//        }
//
//
//        private CompanyDto _ParentsCompanyDto;
//        public CompanyDto ParentsCompanyDto
//        {
//            get { return _ParentsCompanyDto; }
//            set
//            {
//                if (_ParentsCompanyDto != value)
//                {
//                    _ParentsCompanyDto = value;
//                    RaisePropertyChanged("ParentsCompanyDto");
//                }
//            }
//        }
//
//
//
//        private Customer _SelectedList;
//        public Customer SelectedList
//        {
//            get { return _SelectedList; }
//            set
//            {
//                if (_SelectedList != value)
//                {
//                    _SelectedList = value;
//                    RaisePropertyChanged("SelectedList");
//                }
//            }
//        }
//
//        private Customer _SelectedQuery;
//        public Customer SelectedQuery//Collapsed  //Visible
//        {
//            get { return _SelectedQuery; }
//            set
//            {
//                if (_SelectedQuery != value)
//                {
//                    _SelectedQuery = value;
//                    RaisePropertyChanged("SelectedQuery");
//                }
//            }
//        }
//
//
//        private Customer _CurrentCustomer;
//        public Customer CurrentCustomer//Collapsed  //Visible
//        {
//            get { return _CurrentCustomer; }
//            set
//            {
//                if (_CurrentCustomer != value)
//                {
//                    _CurrentCustomer = value;
//                    RaisePropertyChanged("CurrentCustomer");
//                }
//            }
//        }
//
//
//        private SpcQueryDto _spcq;
//        public SpcQueryDto spcq//Collapsed  //Visible
//        {
//            get { return _spcq; }
//            set
//            {
//                if (_spcq != value)
//                {
//                    _spcq = value;
//                    RaisePropertyChanged("spcq");
//                }
//            }
//        }
//
//
//        private CustomerPassDto _CustomerPassDto;
//        public CustomerPassDto CustomerPassDto//Collapsed  //Visible
//        {
//            get { return _CustomerPassDto; }
//            set
//            {
//                if (_CustomerPassDto != value)
//                {
//                    _CustomerPassDto = value;
//                    RaisePropertyChanged("CustomerPassDto");
//                }
//            }
//        }
//
//
//
//        private Customer _QuickCustomer;
//        public Customer QuickCustomer//Collapsed  //Visible
//        {
//            get { return _QuickCustomer; }
//            set
//            {
//                if (_QuickCustomer != value)
//                {
//                    _QuickCustomer = value;
//                    RaisePropertyChanged("QuickCustomer");
//                }
//            }
//        }
//
//        private Customer _CheckedCustomer;
//        public Customer CheckedCustomer
//        {
//            get { return _CheckedCustomer; }
//            set
//            {
//                if (_CheckedCustomer != value)
//                {
//                    _CheckedCustomer = value;
//                    RaisePropertyChanged("CheckedCustomer");
//                }
//            }
//        }
//
//
//        private ObservableCollection<CustomerCall> _customercallList;
//        public ObservableCollection<CustomerCall> CustomercallList
//        {
//            get { return _customercallList; }
//            set
//            {
//                if (_customercallList != value)
//                {
//                    _customercallList = value;
//                    RaisePropertyChanged("CustomercallList");
//                }
//            }
//        }
//        private ObservableCollection<CustomerCall> _currentCustomercallList;
//        public ObservableCollection<CustomerCall> CurrentCustomercallList
//        {
//            get { return _currentCustomercallList; }
//            set
//            {
//                if (_currentCustomercallList != value)
//                {
//                    _currentCustomercallList = value;
//                    RaisePropertyChanged("CurrentCustomercallList");
//                }
//            }
//        }
//
//
//        private ObservableCollection<CustomerCall> _bigCustomercallList;
//        public ObservableCollection<CustomerCall> BigCustomercallList
//        {
//            get { return _bigCustomercallList; }
//            set
//            {
//                if (_bigCustomercallList != value)
//                {
//                    _bigCustomercallList = value;
//                    RaisePropertyChanged("BigCustomercallList");
//                }
//            }
//        }
//
//        private CustomerCall _currentCustomerCall;
//        public CustomerCall CurrentCustomerCall
//        {
//            get { return _currentCustomerCall; }
//            set
//            {
//                if (_currentCustomerCall != value)
//                {
//                    _currentCustomerCall = value;
//                    RaisePropertyChanged("CurrentCustomerCall");
//                }
//            }
//        }
//
//        private ObservableCollection<CustomerCallOrder> _orderList;
//        public ObservableCollection<CustomerCallOrder> orderList
//        {
//            get { return _orderList; }
//            set
//            {
//                if (_orderList != value)
//                {
//                    _orderList = value;
//                    RaisePropertyChanged("orderList");
//                }
//            }
//        }
//
//
//        private ObservableCollection<CustomerCallOrder> _TorderList;
//        public ObservableCollection<CustomerCallOrder> TorderList
//        {
//            get { return _TorderList; }
//            set
//            {
//                if (_TorderList != value)
//                {
//                    _TorderList = value;
//                    RaisePropertyChanged("TorderList");
//                }
//            }
//        }
//
//
//
//        private CustomerCallOrder _selectedOrder;
//        public CustomerCallOrder SelectedOrder
//        {
//            get { return _selectedOrder; }
//            set
//            {
//                if (_selectedOrder != value)
//                {
//                    _selectedOrder = value;
//                    RaisePropertyChanged("SelectedOrder");
//                }
//            }
//        }
//
//
//        private Product _selectedProduct;
//        public Product SelectedProduct
//        {
//            get { return _selectedProduct; }
//            set
//            {
//                if (_selectedProduct != value)
//                {
//                    _selectedProduct = value;
//                    RaisePropertyChanged("SelectedProduct");
//                }
//            }
//        }
//
//
//        private ObservableCollection<ExportRecordVO> _exportRecordVOList;
//        public ObservableCollection<ExportRecordVO> exportRecordVOList
//        {
//            get { return _exportRecordVOList; }
//            set
//            {
//                if (_exportRecordVOList != value)
//                {
//                    _exportRecordVOList = value;
//                    RaisePropertyChanged("exportRecordVOList");
//                }
//            }
//        }
//
//        #endregion
//
//        public RelayCommand RefreshCommand2 { get; set; }
//
//        private void RefreshCommand2Method(object obj)
//        {
//            Loadcase();
//        }
//
//        public RelayCommand RefreshCommand3 { get; set; }
//
//        private void RefreshCommand3Method(object obj)
//        {
////            MessageBox.Show(Nos.ToString());
////            QueryCustomerList(Nos);
//            LoadCustomers();
//        }
//
//        public RelayCommand RefreshCommand4 { get; set; }
//
//        private void RefreshCommand4Method(object obj)
//        {
//            AccKey = "1";
//            LoadCompany();
//        }
//
//
//
//
//        public RelayCommand RefreshCommand { get; set; }
//      
//        private void RefreshCommandMethod(object param)
//        {
//            Loadleads();
////            QueryCustomerList();
//        }
//        public void QueryCustomerList()
//        {
//            var conditions = new List<KeyValuePair<string, string>> { };
//            CustomerList = _callManagerService.GetCustomerList(conditions);
////            LeadsList = _callManagerService.GetLeadsList(conditions);
////            OpportunityList = _callManagerService.GetOpportunityList(conditions);
////            RewardList = _callManagerService.GetRewardList(conditions);
//
//        }
//
//
//        public void QueryCustomerList1()
//        {
//            var conditions = new List<KeyValuePair<string, string>> { };
//            CurrentSales = SalesList != null && SalesList.Count > 0 ? SalesList[0] : new Employee();
//            ProductList = _callManagerService.GetProductList(conditions);
//            IndustryList = _callManagerService.GetIndustryList(conditions);
//            CompanyList = _callManagerService.GetCompanyList(conditions);
//            ProvinceList = _callManagerService.GetProvinceList(conditions);
//            InsideSales = _callManagerService.GetInsideSales(conditions);
//            //SalesListNull = _callManagerService.GetSalesNullList(conditions);
//
//        }
//
//        public void QueryCustomerList2()
//        {
//            var conditions = new List<KeyValuePair<string, string>> { };
//            CompanyListNull = _callManagerService.GetCompanyNullList(conditions);
//            SupplierListNull = _callManagerService.GetSupplierNullList(conditions);
//        }
//
//        public void QueryCustomerList3()
//        {
//            var conditions = new List<KeyValuePair<string, string>> { };
//            SalesListAll = _callManagerService.GetSalesListAll(conditions);
//            SalesList = _callManagerService.GetSalesList(conditions);
//            SalesListA = _callManagerService.GetSalesList(conditions);
//            SalesListB = _callManagerService.GetSalesList(conditions);
//
//            CaseStatusListNull = _callManagerService.GetCaseStatusListNull(conditions);
//            SourceCodeAll=_callManagerService.GetSourceCodeAll(conditions);
//            InsideSales = _callManagerService.GetInsideSales(conditions);
//            CaseTypeNull = _callManagerService.GetCaseTypeAllList(conditions);
//            saleProjectNullList = _callManagerService.GetSaleProjectNullList();
//            ////快速查询
//            //CompanyDtoList = _callManagerService.GetCompanyDtoList(conditions);
//
//        }
//
//
//        public void QueryCustomerList8()
//        {
//            var conditions = new List<KeyValuePair<string, string>> { };
//            //快速查询
//            CompanyDtoList = _callManagerService.GetCompanyDtoList(conditions);
//
//        }
//
//
//        public void QueryStatus()
//        {
//            var conditions = new List<KeyValuePair<string, string>> { };
//            CaseStatusList = _callManagerService.GetCaseStatusList(conditions);
//            CaseTypeEmpty = _callManagerService.GetCaseTypeEmptyList(conditions);
//            saleProjectList = _callManagerService.GetSaleProjectList();
//        }
//
//        public void QueryCustomerList4()
//        {
//            var conditions = new List<KeyValuePair<string, string>> { };
//            ProductList = _callManagerService.GetProductList(conditions);
//
//        }
//
//        public void QueryCustomerList5()
//        {
//            var conditions = new List<KeyValuePair<string, string>> { };
//
//            IndustryList = _callManagerService.GetIndustryList(conditions);
//
//            CompanyList = _callManagerService.GetCompanyList(conditions);
//
//            ProvinceList = _callManagerService.GetProvinceList(conditions);
//
//            InsideSales = _callManagerService.GetInsideSales(conditions);
//        }
//
//        public void QueryCustomerList6()
//        {
//            //保存了之后查询一遍
//            var conditions = new List<KeyValuePair<string, string>> { };
//            CompanyList = _callManagerService.GetCompanyList(conditions);
//        }
//
//        public void QueryCustomerList7()
//        {
//            var conditions = new List<KeyValuePair<string, string>> { };
//            CompanyListNull = _callManagerService.GetCompanyNullList(conditions);
//            SupplierListNull = _callManagerService.GetSupplierNullList(conditions);
//
//        }
//
//        public void QueryCaseLeadsDetailDto(string id)
//        {
//            var conditions = new List<KeyValuePair<string, string>> { };
//            CaseLeadsDetailDtos = _callManagerService.GetCaseLeadsDetailDtosid(id,conditions);
//
//        }
//
//        public void QueryCaseLeadsDetailDto()
//        {
//            var conditions = new List<KeyValuePair<string, string>> { };
//            CaseLeadsDetailDtos = _callManagerService.GetCaseLeadsDetailDtosid(null, conditions);
//        }
//
//        public void QueryCaseLeadsDetailDtoWithIndustry()
//        {
//            var conditions = new List<KeyValuePair<string, string>> { };
//            CaseLeadsDetailDtos = _callManagerService.GetCaseLeadsDetailDtoWithIndustry(conditions);
//
//        }
//        public void QueryCaseLeadsDetailDtoWithCompany()
//        {
//            var conditions = new List<KeyValuePair<string, string>> { };
//            CaseLeadsDetailDtos = _callManagerService.GetCaseLeadsDetailDtoWithCompany(conditions);
//        }
//
//        public void QueryCaseLeadsDetailDtoOnlyCompany()
//        {
//            var conditions = new List<KeyValuePair<string, string>> { };
//            CompanyDtos = _callManagerService.GetCaseLeadsDetailDtoOnlyCompany(conditions);
//        }
//
//        public void QueryPagedCustomerList(string id, int pageNumber)
//        {
//            var conditions = new List<KeyValuePair<string, string>> { };
////            pageList = _importService.GetPagedCustomerSourceList(id, pageNumber, conditions);
////            CustomerList = pageList.customerSourceList;
////            totalElements = pageList.totalElements;
////            totalPages = pageList.totalPages;
//        }
//
//        public void SaveRefresh()
//        {
//            var conditions = new List<KeyValuePair<string, string>> { };
//
////            IndustryList = _callManagerService.GetIndustryList(conditions);
//
//            CompanyList = _callManagerService.GetCompanyList(conditions);
////
////            ProvinceList = _callManagerService.GetProvinceList(conditions);
////
////            InsideSales = _callManagerService.GetInsideSales(conditions);
//        }
//
//
//        public void QueryLeads()
//        {
//            category = "-1";
//            grade = "-1";
//            importance = "-1";
//            sourceCode = "-1";
//            QueryCustomerList3();
//
//            string key =sourceCode + "/" + category + "/" + importance + "/-1/-1/-1/-1/-1/-1/-1/-1/-1/" + grade;
//            var conditions = new List<KeyValuePair<string, string>> { };
//            LeadsList = _callManagerService.GetListBy(key, conditions);
//        }
//
//
//        public void LoadCustomers()
//        {
////            var conditions = new List<KeyValuePair<string, string>> { };
////            CustomerList = _callManagerService.GetCustomerList(conditions);
//
//            QueryCustomerList(ConNos);
//        }
//
//        public void Loadleads()
//        {
//            //改成传参数的loadleads
//            var conditions = new List<KeyValuePair<string, string>> { };
////            Leadss = _callManagerService.GetLeadsList(conditions);   //Messi blocked it on 20181203
//            MessageBox.Show("contact Hey, blocked on 20181203-VmCallManager.cs 3798 line");
//        }
//        public void Exportleads()
//        {
//            //改成传参数的loadleads
//            var conditions = new List<KeyValuePair<string, string>> { };
//            Leadex = _callManagerService.GetLeadsList(conditions);
//        }
//        
//
//        public void getleadss(string key)
//        {
//            var conditions = new List<KeyValuePair<string, string>> { };
////            Leadex = _callManagerService.GetLeadsBy(key, conditions);
//            leadsVOex= _callManagerService.GetLeadsBy(key, conditions);
//
//        }
//
//        public void getleadssbyDTO(LeadsSearchDTO leadsSearchDTO)
//        {
//            var result = _callManagerService.GetLeadsByObjList(leadsSearchDTO);
//            leadsVOex = result.LeadsVOLIst;
//        }
//
//
//
//        public void Loadcase()
//        {
//            //改成传参数的loadcase
////            var conditions = new List<KeyValuePair<string, string>> { };
////            cases = _callManagerService.GetCaseList(conditions);
//            MessageBox.Show("contact Hey, blocked on 20181203-VmCallManager.cs 3830 line");
//        }
////
////        public void getcases(string key)
////        {
////            var conditions = new List<KeyValuePair<string, string>> { };
////            cases = _callManagerService.GetCaseBy(key, conditions);
////        }
//
//
////        public void getcasesForAutoRefresh(string key)
////        {
////            var conditions = new List<KeyValuePair<string, string>> { };
////            ObservableCollection<Case>  caseList = _callManagerService.GetCaseBy(key, conditions);
////            if (null != caseList && caseList.Count > 0)
////            {
////                if (CaseListCount != caseList.Count)
////                {
////                    cases = caseList;
////                    CaseListCount = caseList.Count;
////                }
////                      
////            }
////        }
//
////        public void getcasespaged(string caseKey, int caseNos)
////        {
//////            var start2 = DateTime.Now;
////            var conditions = new List<KeyValuePair<string, string>> { };
////            CasePageList = _callManagerService.GetCasePagedList(caseKey, caseNos, conditions);
////            cases = CasePageList.PagedList;
////            totalElements = CasePageList.totalElements;
////            totalPages = CasePageList.totalPages;
//////            MessageBox.Show("load leads:" + (DateTime.Now - start2).Milliseconds);
////        }
//
//        public void getCasesByDTO(CaseSearchDTO caseSearchDto)
//        {
//            var result = _callManagerService.GetCaseByObjList(CaseSearchDTO);
//            casesEX = result.caseVOList;
//        }
//
//        public void getCasesPagedByDTO(CaseSearchDTO CaseSearchDTO)
//        {
//            var result = _callManagerService.GetCasePagedByObjList(CaseSearchDTO);
//            CasePageList = result.CasePageList;
//            cases = CasePageList.PagedList;
//            totalElements = CasePageList.totalElements;
//            totalPages = CasePageList.totalPages;
//        }
//
//        public void QueryCompanyShowDto(CompanyShowDTO vmCompanyShowDto)
//        {
//            CompanyShowDTO= _callManagerService.GetCompanyShowDTO(vmCompanyShowDto);
//            ProvinceName = CompanyShowDTO.CurrentCompany.province;
//        }
//
//        public void getCasesByCompanyDtoId(string companyId, int caseNos)
//        {
//            var conditions = new List<KeyValuePair<string, string>> { };
//            CasePageList = _callManagerService.GetCasesByCompanyDtoIdList(companyId, caseNos, conditions);
//            cases = CasePageList.PagedList;
//            totalElements = CasePageList.totalElements;
//            totalPages = CasePageList.totalPages;
////            MessageBox.Show("contact Hey, blocked on 20181203-VmCallManager.cs 3894 line");
//        }
//
//        public void getCaseByCustomerId(string companyId, int caseNos)
//        {
//            var conditions = new List<KeyValuePair<string, string>> { };
//            CasePageList = _callManagerService.GetCaseByCustomerIdList(companyId, caseNos, conditions);
//            cases = CasePageList.PagedList;
//            totalElements = CasePageList.totalElements;
//            totalPages = CasePageList.totalPages;
////            MessageBox.Show("contact Hey, blocked on 20181203-VmCallManager.cs 3904 line");
//        }
//
//
////        public void getLeadspaged(string leadsKey, int leadNos)
////        {
////
////            var conditions = new List<KeyValuePair<string, string>> { };
////            LeadsPageList = _callManagerService.GetLeadsPagedList(leadsKey, leadNos, conditions);
////            Leadss = LeadsPageList.PagedList;
////            totalElements = LeadsPageList.totalElements;
////            totalPages = LeadsPageList.totalPages;
////
////        }
//
//        public void getLeadsPagedByDTO(LeadsSearchDTO LeadsSearchDto)
//        {
//            var result = _callManagerService.GetLeadsPagedByObjList(LeadsSearchDto);
//            LeadsPageList = result.LeadsPageList;
//            Leadss = LeadsPageList.PagedList;
//            totalElements = LeadsPageList.totalElements;
//            totalPages = LeadsPageList.totalPages;
//        }
//
//
//        public void QueryAccountListbyObj(CompanySearchDTO CompanySearchDTO)
//        {
//            var result = _callManagerService.GetAccountListbyObj(CompanySearchDTO);
//            CompanyPageList = result.CompanyList;
//            Companys = CompanyPageList.PagedList;
//            totalElements = CompanyPageList.totalElements;
//            totalPages = CompanyPageList.totalPages;
//
//        }
//
//        public void QueryCustomerListbyObj(CustomerSearchDTO vmCustomerSearchDto)
//        {
//            if (vmCustomerSearchDto != null)
//            {
//                var result = _callManagerService.GetCustomerListbyObj(vmCustomerSearchDto);
//                CustomerPageList = result.CustomerList;
//                CustomerList = CustomerPageList.PagedList;
//                totalElements = CustomerPageList.totalElements;
//                totalPages = CustomerPageList.totalPages;
//            }
//
//        }
//
//
//        public void QuerySaleProjectCustomerbyBjb(CustomerSearchDTO vmCustomerSearchDto)
//        {
//            if (vmCustomerSearchDto != null)
//            {
//                var result = _callManagerService.QuerySaleProjectCustomerbyBjb(vmCustomerSearchDto);
//                CustomerPageList = result.CustomerList;
//                SalesProjectCustomerList = CustomerPageList.PagedList;
//                totalElements = CustomerPageList.totalElements;
//                totalPages = CustomerPageList.totalPages;
//            }
//
//        }
//
//        public void QuerySPCDto(SaleProjectCustomerSearchDTO vmSpcDto)
//        {
//            if (vmSpcDto != null)
//            {
//                var result = _callManagerService.QuerySPCDto(vmSpcDto);
//                spcList = result.spcList;
//            }
//        }
//
//        public void getLeadsBysaleIdNoPager(string id)
//        {
//
//            var conditions = new List<KeyValuePair<string, string>> { };
////            Leadex = _callManagerService.GetLeadsListbySalesId(id, conditions);
//            leadsVOex = _callManagerService.GetLeadsListbySalesId(id, conditions);
//        }
//
//        public void getLeadsByCompanyNopager(string id)
//        {
//            var conditions = new List<KeyValuePair<string, string>> { };
//            leadsVOex = _callManagerService.GetLeadsListbyCompanyId(id, conditions);
//        }
//
//
//        public void getLeadsByCustomerNopager(string id)
//        {
//            var conditions = new List<KeyValuePair<string, string>> { };
//            leadsVOex = _callManagerService.GetLeadsListbyCustomerId(id, conditions);
//        }
//
//
//        public void getLeadsBySupplierIdNoPager(string id)
//        {
//            var conditions = new List<KeyValuePair<string, string>> { };
//            leadsVOex = _callManagerService.GetLeadsListbySupplierId(id, conditions);
//
//        }
//
//
//
//        private void LoadCompany()
//        {
////            var conditions = new List<KeyValuePair<string, string>> { };
////            Companys = _callManagerService.GetCompanyListbyCurrentUser(conditions);
//
//            QueryAccountList(AccNos);
//        }
//
//        public void UpdateCommand(CustomerCall currentCustomerCall)
//        {
//            var result = _callManagerService.UpdateCustomCall(currentCustomerCall);
//            if (result)
//            {
//                RadWindow.Alert("Update Success！");
//                if (HostControl != null)
//                {
//                    QueryCustomerList();
//
////                    HostControl.ClosePopup();
//                }
//            }
//            else
//            {
//                RadWindow.Alert("Update Failed！");
//
//            }
//           
//            //QueryCustomerList();
//        }
//
//
//        public void UpdateCustomer(CustomerCall currentCustomerCall)
//        {
//            var result = _callManagerService.UpdateCustomer(currentCustomerCall);
//            if (result)
//            {
//                RadWindow.Alert("Update Success！");
//                if (HostControl != null)
//                {
//                    QueryCustomerList();
////                    HostControl.ClosePopup();
//                }
//            }
//            else
//            {
//                RadWindow.Alert("Update Failed！");
//
//            }
//            //QueryCustomerList();
//        }
//
//
//
//
//        public void CreateCommand(CustomerCall CurrentCustomer)
//        {
//            var result = _callManagerService.CreateCustomCall(CurrentCustomer);
//            if (result != null)
//            {
//                SaveRefresh();
//                RadWindow.Alert("Save Success!");
//                if (HostControl != null)
//                {
//
//                    HostControl.ClosePopup();
//                }
//            }
//        }
//
//        public void Createnew(CustomerCall CurrentCustomer)
//        {
//            var result = _callManagerService.CreateCustomer(CurrentCustomer);
//            if (result != null)
//            {
//                SaveRefresh();
//                RadWindow.Alert("Save Success!");
//                if (HostControl != null)
//                {
////                    HostControl.ClosePopup();
//                }
//            }
//        }
//
//
//        public void AssignSalesByAppIdAndRoleId(string empId, CustomerCall CurrentCustomer)
//        {
//            var result = _callManagerService.AssignSalesByAppIdAndRoleId(empId, CurrentCustomer);
//            if (result != null)
//            {
//                orderList = new ObservableCollection<CustomerCallOrder>();
//                RelatedCustomer = new ObservableCollection<Customer>();
//                orderList.Add(new CustomerCallOrder());
//                InboundCallCustomer = new Customer();
//                InboundCallCustomer.company = new Company();
//                InboundCallCustomer.currentLeads = new Leads();
//                InboundCallCustomer.currentLeads.currentNote = new LeadsNote();
//                InboundCallCustomer.currentLeads.products = new ObservableCollection<LeadsProduct>();
//
//                SaveRefresh();
//
//                RadWindow.Alert("Update Success!");
//                if (HostControl != null)
//                {
//                    HostControl.ClosePopup();
//                }
//            }
//        }
//
//
//        public void AssignSalesByEmployeeId(string leadsId, string empId, string executive)
//        {
//            var result = _callManagerService.AssignSalesByEmployeeId(leadsId, empId);
//            if (result != null)
//            {
//                RadWindow.Alert("Assign to "+executive+" success & copy data to clipboard!");
//                if (HostControl != null)
//                {
//                    HostControl.ClosePopup();
//                }
//                SelectedCustomer = null;
//            }
//        }
//
//
//
//        public void GetVoList(ObservableCollection<CustomerCall> customercallList)
//        {
//            exportRecordVOList = _callManagerService.GetVoList(customercallList);
//        }
//
//
//        public void SaveCommand(ObservableCollection<ProductRelation> productRelationList)
//        {
//
//            var result = _callManagerService.SaveProductRelation(ProductRelationList);
//            RadWindow.Alert(result != null ? "Saved！" : "Saved Failed！");
//            HostControl.ClosePopup();
//
//
//
//        }
//
//
//
//
//
//        public void QueryCustomerList(int pageNumber)
//        {
//            var conditions = new List<KeyValuePair<string, string>> { };
//            PageList = _callManagerService.GetCustomerPageList(pageNumber, conditions);
//            CustomerList = PageList.PagedList;
//            totalElements = PageList.totalElements;
//            totalPages = PageList.totalPages;
//        }
//
//        public void getCustomerListByCompanyDtoId(string companyId, int ConNos)
//        {
//            var conditions = new List<KeyValuePair<string, string>> { };
//            PageList = _callManagerService.GetCustomerListByCompanyDtoIdList(companyId, ConNos, conditions);
//            CustomerList = PageList.PagedList;
//            totalElements = PageList.totalElements;
//            totalPages = PageList.totalPages;
//
//
//        }
//
//        public void QueryAccountList(int pageNumber)
//        {
//
////            var conditions = new List<KeyValuePair<string, string>> { };
////            CompanyPageList = _callManagerService.GetCompanyPageList(pageNumber, conditions);
////            Companys = CompanyPageList.PagedList;
////            totalElements = CompanyPageList.totalElements;
////            totalPages = CompanyPageList.totalPages;
//            MessageBox.Show("Blocked by Hey, VM line 4180");
//
//        }
//
//
//
//
//        public void QueryImportedCustomerList(string key, int pageNumber)
//        {
//
//            var conditions = new List<KeyValuePair<string, string>> { };
//            PageList = _callManagerService.GetCustomerImportedList(key, pageNumber, conditions);
//            CustomerList = PageList.PagedList;
//            totalElements = PageList.totalElements;
//            totalPages = PageList.totalPages;
//        }
//
//
//        public void CreatenewCustomer(Customer selectedCustomer)
//        {
//            var result = _callManagerService.SaveCustomer(selectedCustomer);
//            RadWindow.Alert(result != null ? "Saved！" : "Saved Failed！");
////            QueryCustomerList6();
////            LoadCustomers();
////            QueryCustomerList(ConNos);
//            if (CustomerSearchDTO != null)
//            {
//                QueryCustomerListbyObj(CustomerSearchDTO);
//            }
//
//        }
//
//        public void CreatenewLeads(Leads selectLeads)
//        {
//            if (selectLeads.customer.status != 0)
//            {
//                selectLeads.customer.status = 0;
//            }
//            var result = _callManagerService.SaveLeads(selectLeads);
//            RadWindow.Alert(result != null ? "Saved！" : "Saved Failed！");
//            //var start2 = DateTime.Now;
//            //Loadleads();
//            //MessageBox.Show("load leads:" + (DateTime.Now - start2).Milliseconds);
//        }
//
//        public void CreatenewCase(Case selectCase)
//        {
//            if (selectCase.customer.status != 0)
//            {
//                selectCase.customer.status = 0;
//            }
//            var result = _callManagerService.SaveCase(selectCase);
//            RadWindow.Alert(result != null ? "Saved！" : "Saved Failed！");
////            QueryCustomerList6();
//            //改成将用户选择的条件传过来
////            Loadcase();
////            getcasespaged(CaseKey, CaseNos);
//        }
//
//
//        public void CreateLeads(Leads selectLeads)
//        {
//            var result = _callManagerService.CreateLeads(selectLeads);
//            RadWindow.Alert(result != null ? "Saved！" : "Saved Failed！");
//            //            HostControl.ClosePopup();
//
//        }
//
//        public void SaveCompany(Company selectedCompany)
//        {
//            var result = _callManagerService.SaveCompany(selectedCompany);
//            RadWindow.Alert(result != null ? "Saved！" : "Saved Failed！");
////            QueryCustomerList6();
//////            LoadCompany();
////            QueryAccountList(AccNos);
//        }
//
//        public void UpdateCompany(Company currentCompany)
//        {
//            var result = _callManagerService.UpdateCompany(currentCompany);
//            if (result)
//            {
//                RadWindow.Alert("Update Success！");
////                QueryCustomerList6();
//                LoadCompany();
//            }
//            else
//            {
//                RadWindow.Alert("Update Failed！");
//            }
//        }
//
//        public void UpdatedCompanyBeSide(Company currentCompany)
//        {
//            var result = _callManagerService.UpdateCompany(currentCompany);
//            if (result)
//            {
//                RadWindow.Alert("Update Success！");
//            }
//            else
//            {
//                RadWindow.Alert("Update Failed！");
//            }
//
//        }
//        //        public void UpdateCompany(Company currentCompany)
//        //        {
//        //            var result = _callManagerService.UpdateCompany(currentCompany);
//        //            RadWindow.Alert(result != null ? "Saved！" : "Saved Failed！");
//        //            QueryCustomerList6();
//        //
//        //        }
//
//
//        public void UpdateContact(Customer currentCustomer)
//        {
//            var result = _callManagerService.UpdateContact(currentCustomer);
//            if (result)
//            {
//                RadWindow.Alert("Update Success！");
////                QueryCustomerList6();
//            }
//            else
//            {
//                RadWindow.Alert("Update Failed！");
//            }
//        }
//
//        public void updateSaleProjectCustomer(Customer vmCurrentCustomer)
//        {
//
//            var result = _callManagerService.updateSaleProjectCustomer(vmCurrentCustomer);
//            if (result)
//            {
//                RadWindow.Alert("Update Success！");
//            }
//            else
//            {
//                RadWindow.Alert("Update Failed！");
//            }
//        }
//
//
//
//        public void UpdateLeads(Leads selectLeads)
//        {
//            var result = _callManagerService.UpdateLeads(selectLeads);
//
//            if (result)
//            {
//                RadWindow.Alert("Update Success！");
//                //Loadleads();
//            }
//            else
//            {
//                RadWindow.Alert("Update Failed！");
//            }
//
//        }
//
//
//        public void UpdateCase(Case selectCase)
//        {
//
//            var result = _callManagerService.UpdateCase(selectCase);
//            if (result)
//            {
//                RadWindow.Alert("Update Success！");
//            }
//            else
//            {
//                RadWindow.Alert("Update Failed！");
//            }
//        }
//
//
//        public void getLeadsBysaleId(string id, int leadNos)
//        {
//            var conditions = new List<KeyValuePair<string, string>> { };
//            LeadsPageList = _callManagerService.GetLeadsPagedListbySalesId(id, leadNos, conditions);
//            Leadss = LeadsPageList.PagedList;
//            totalElements = LeadsPageList.totalElements;
//            totalPages = LeadsPageList.totalPages;
//
////            MessageBox.Show("contact Hey, blocked on 20181203-VmCallManager.cs 4358 line");
//
//        }
//
//        public void getLeadsBySourceCode(string sourceCode, int leadNos)
//        {
//            var conditions = new List<KeyValuePair<string, string>> { };
//            LeadsPageList = _callManagerService.GetLeadsPagedListbySourceCode(sourceCode, leadNos, conditions);
//            Leadss = LeadsPageList.PagedList;
//            totalElements = LeadsPageList.totalElements;
//            totalPages = LeadsPageList.totalPages;
////            MessageBox.Show("contact Hey, blocked on 20181203-VmCallManager.cs 4370 line");
//        }
//
//        public void getLeadsByCompanyDtoId(string id, int leadNos)
//        {
//            var conditions = new List<KeyValuePair<string, string>> { };
//            LeadsPageList = _callManagerService.GetLeadsPagedListbyCompyanDtoId(id, leadNos, conditions);
//            Leadss = LeadsPageList.PagedList;
//            totalElements = LeadsPageList.totalElements;
//            totalPages = LeadsPageList.totalPages;
////            MessageBox.Show("contact Hey, blocked on 20181203-VmCallManager.cs 4380 line");
//        }
//
//
//        internal void getLeadsByCustomerId(string id, int leadNos)
//        {
//            var conditions = new List<KeyValuePair<string, string>> { };
//            LeadsPageList = _callManagerService.GetLeadsPagedListbyCustomerId(id, leadNos, conditions);
//            Leadss = LeadsPageList.PagedList;
//            totalElements = LeadsPageList.totalElements;
//            totalPages = LeadsPageList.totalPages;
////            MessageBox.Show("contact Hey, blocked on 20181203-VmCallManager.cs 4391 line");
//        }
//
//
//        public void getLeadsBySuplierId(string id, int leadNos)
//        {
////            var conditions = new List<KeyValuePair<string, string>> { };
////            LeadsPageList = _callManagerService.GetLeadsPagedListbySupplierId(id, leadNos, conditions);
////            Leadss = LeadsPageList.PagedList;
////            totalElements = LeadsPageList.totalElements;
////            totalPages = LeadsPageList.totalPages;
//            MessageBox.Show("contact Hey, blocked on 20181203-VmCallManager.cs 4402 line");
//        }
//
//        public void getLeadsBySourceCodeNoPager(string s)
//        {
//
//            var conditions = new List<KeyValuePair<string, string>> { };
////            Leadex = _callManagerService.GetLeadsListbySourceCode(s, conditions);
//            leadsVOex = _callManagerService.GetLeadsListbySourceCode(s, conditions);
//        }
//
//        public void getLeadsByIndustry(string industry, int leadNos)
//        {
////            var conditions = new List<KeyValuePair<string, string>> { };
////            LeadsPageList = _callManagerService.GetLeadsPagedListbyIndustry(industry, leadNos, conditions);
////            Leadss = LeadsPageList.PagedList;
////            totalElements = LeadsPageList.totalElements;
////            totalPages = LeadsPageList.totalPages;
//            MessageBox.Show("contact Hey, blocked on 20181203-VmCallManager.cs 4420 line");
//        }
//
//        public void getLeadsByIndustryNoPager(string industry)
//        {
//
//            var conditions = new List<KeyValuePair<string, string>> { };
////            Leadex = _callManagerService.GetLeadsListbyIndustry(industry, conditions);
//            leadsVOex = _callManagerService.GetLeadsListbyIndustry(industry, conditions);
//        }
//
//        public void getLeadsByInsiteSaleIdNoPager(string insiteSaleId)
//        {
//
//            var conditions = new List<KeyValuePair<string, string>> { };
////            Leadex = _callManagerService.getLeadsByInsiteSaleId(insiteSaleId, conditions);
//            leadsVOex = _callManagerService.getLeadsByInsiteSaleId(insiteSaleId, conditions);
//        }
//
//        public void UpdateSaleStatus(string statusString)
//        {
//            var result = _callManagerService.UpdateSaleStatus(statusString);
//            if (result)
//            {
//                RadWindow.Alert("Sales Status Update Success！");
//            }
//            else
//            {
//                RadWindow.Alert("Update Failed！");
//            }
//
//        }
//
//        public void UpdateSaleRelation(string relationString)
//        {
//            var result = _callManagerService.UpdateSaleRelation(relationString);
//            if (result)
//            {
//                RadWindow.Alert("Sales/Inside Relationship Update Success！");
//            }
//            else
//            {
//                RadWindow.Alert("Update Failed！");
//            }
//
//        }
//
//        public void getLeadsByInsiteSaleId(string insiteSaleId, int leadNos)
//        {
////            var conditions = new List<KeyValuePair<string, string>> { };
////            LeadsPageList = _callManagerService.GetLeadsPagedListbyInsiteSaleId(insiteSaleId, leadNos, conditions);
////            Leadss = LeadsPageList.PagedList;
////            totalElements = LeadsPageList.totalElements;
////            totalPages = LeadsPageList.totalPages;
//            MessageBox.Show("contact Hey, blocked on 20181203-VmCallManager.cs 4474 line");
//        }
//
//
//
//        public void getCompyanysByIndustry(string industry, int accNos)
//        {
//            var conditions = new List<KeyValuePair<string, string>> { };
//            CompanyPageList = _callManagerService.GetCompanyPageListByIndustry(industry, accNos, conditions);
//            Companys = CompanyPageList.PagedList;
//            totalElements = CompanyPageList.totalElements;
//            totalPages = CompanyPageList.totalPages;
////            MessageBox.Show("Blocked by Hey, VM line 4491");
//        }
//
//        public void getCompyanysByPriority(string comPriority, int accNos)
//        {
//            //            var conditions = new List<KeyValuePair<string, string>> { };
//            //            CompanyPageList = _callManagerService.GetCompanyPageListByPriority(comPriority, accNos, conditions);
//            //            Companys = CompanyPageList.PagedList;
//            //            totalElements = CompanyPageList.totalElements;
//            //            totalPages = CompanyPageList.totalPages;
//            MessageBox.Show("Blocked by Hey, VM line 4501");
//        }
//
//
//        public void QueryCommonSearchDTO(string type ,string txt)
//        {
//            var conditions = new List<KeyValuePair<string, string>> { };
//            CommonSearchDTOList= _callManagerService.GetCommonSearchDtoList(type,txt, conditions);
//        }
//
//        public void QuerySearchCompanyDto(string txt)
//        {
//            var conditions = new List<KeyValuePair<string, string>> { };
//            CompanyDtoList=_callManagerService.GetCompanyDtoListByText(txt,conditions);
//
//        }
//
//        public void GetCompanyListByCompanyDtoId(string companyId)
//        {
//            var conditions = new List<KeyValuePair<string, string>> { };
//            Companys = _callManagerService.GetCompanyDtoListById(companyId, conditions);
//        }
//
//
//        public void getCustomerbyCustomerId(string customerId)
//        {
//            var conditions = new List<KeyValuePair<string, string>> { };
//            CurrentCustomer = _callManagerService.GetCurrentContact(customerId, conditions);
//            CustomerList=new ObservableCollection<Customer>();
//            CustomerList.Add(CurrentCustomer);
//        }
//
//
//
//    }
//}
