using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using New.Entity;
using New.RestUtility;

namespace New.Service
{
    public class UserService
    {
        //        public const string GetCustomerListUrl = "/resource/customer/list";
        //        public const string GetCustomerDetailUrl = "/resource/customer";
        //        public const string GetCustomerListByCompanyDtoIdListUrl = "/resource/customer/customerByCompanyId/";
        //
        //        public const string SaveListUrl = "/resource/customerCall/createProductRelations";
        //        public const string GetAlleadsListUrl = "/resource/leads/list";
        //
        //        public const string GetRelatedCustomerListUrl = "/resource/customer/customers/";
        //
        //        public const string GetCurrentCustomerCallByIdUrl = "/resource/customerCall/detail/";
        //
        //
        //        public const string GetOpportunityListUrl = "/resource/customer/opportunity";
        //        public const string GetHistoryListUrl = "/resource/customer/suggestedCustomerlist/";
        //        public const string GetHistoryListfroNewUrl = "/resource/customer/suggestedList/";
        //        public const string GetCompanyListfroIBUrl = "/resource/company/listNameLike/";
        //        public const string GetHistoryListfroIBUrl = "/resource/company/customerList/";
        //
        //
        //        public const string GetSalesListUrl = "/resource/employees/salesList";
        //        public const string GetProductListUrl = "/resource/product/list";
        //
        //
        //        public const string GetSourceCodeListUrl = "/resource/product/sourceCodeList";
        //
        //        public const string GetIndustryListUrl = "/resource/product/industryList";
        //
        //        public const string GetCompanyListUrl = "/resource/product/companyList";
        //        public const string GetCompanyDtoListUrl = "/resource/company/CompanyDtoList";
        //
        //
        //        public const string GetCompanyListNullUrl = "/resource/product/companyList/null";
        //
        //        public const string UpdateCustomCallUrl = "/resource/customerCall/update";
        //        public const string UpdateCustomerUrl = "/resource/customerCall/updatecustomer";
        //
        //        public const string CreateCustomCallUrl = "/resource/customerCall/insert";
        //        public const string CreateCustomerUrl = "/resource/customerCall/createcustomer";
        //
        //        public const string AssignSalesByAppIdAndRoleIdUrl = "/resource/assignment/assignToSales";
        //        public const string AssignSalesByEmployeeIdUrl = "/resource/assignment/reAssignToSales";
        //
        //        public const string GetProvinceListUrl = "/resource/product/provinceList";
        //        public const string GetCityListUrl = "/resource/product/cityList";
        //        public const string GetCityListbyNameUrl = "/resource/product/getCityListbyProvince/";
        //
        //        public const string GetAreaListUrl = "/resource/product/areaList";
        //
        //        public const string GetCustomercallListUrl = "/resource/customerCall";
        //
        //        public const string GetVoListUrl = "/resource/files/export";
        //
        //        public const string UpdateLeadsUrl = "/resource/leads/update";
        //
        //
        //        public const string GetAssignCallListUrl = "/resource/assignment/assignedCustomers/pendingCallCustomers";
        //        public const string GetTodayCallListUrl = "/resource/customerCall/CustomerByToday";
        //
        //
        //        public const string GetpendingCallBackListUrl = "/resource/assignment/assignedCustomers/pendingCallBackCustomers";
        //        public const string GetBigClientListUrl = "/resource/bigClient/listWithNull";
        //        public const string GetInsideSalesUrl = "/resource/employees/InsidesalesList";
        //
        //
        //        public const string GetTagsListUrl = "/resource/tags/list";
        //        public const string GetTagsUrl = "/resource/customer/customerListByTag/";
        //        public const string GetProjectUrl = "/resource/customer/customerListBySaleProjectAndCurrentEmployee/";
        //        public const string GetStarUrl = "/resource/customer/customerListByPriority/";
        //
        //
        //        public const string GetIndustryUrl = "/resource/customer/customerListByIndustry/";
        //        public const string GetRegionUrl = "/resource/customer/customerListByRegion/";
        //
        //        public const string GetSaleProjectListUrl = "/resource/saleProject/list";
        //        public const string GetAssignRuleListUrl = "/resource/assignment/assignRule/list";
        //        public const string GetIndustryNameUrl = "/resource/assignment/industry/list";
        //
        //
        //
        //        public const string GetAreaNameUrl = "/resource/assignment/region/list";
        //
        //        public const string GetBlockListUrl = "/resource/customer/customerListByCallStatus/";
        //
        //        public const string GetListByUrl = "/resource/leads/filteredList/";
        //        public const string GetListByFileNameUrl = "/resource/customer/filteredList/";
        //
        //
        //
        //        public const string GetLeadsByIdUrl = "/resource/leads/";
        //        public const string GetShowLeadsListUrl = "/resource/leads/listByCustomerId/";
        //        public const string GetShowAllCompanyLeadsListUrl = "/resource/leads/listAllCompanyByCustomerId/";
        //
        //        public const string GetLeadsGradeListUrl = "/resource/product/leadsGradeList";
        //        public const string GetOpportunityGradeListUrl = "/resource/product/opportunityList";
        //        public const string GetProductUrl = "/resource/product/";
        //        public const string GetListByNameUrl = "/resource/customer/";
        //
        //        public const string GetParentsCompanyUrl = "/resource/company/parentInfo/";
        //        public const string SaveCompanyUrl = "/resource/company/insert";
        //
        //        public const string CaseUrl = "/resource/product/";
        //        public const string SaveCustomerUrl = "/resource/customer/insert";
        //
        //        public const string SaveLeadsUrl = "/resource/leads/add";
        //        public const string GetLeadsListUrl = "/resource/leads/list";
        //        public const string GetShowCaseUrl = "/resource/case/listAllCompanyByCustomerId/";
        //
        //        public const string SaveCaseUrl = "/resource/case/add";
        //        public const string CaseListUrl = "/resource/case/list";
        //        public const string GetCurrentCompanyUrl = "/resource/account/accountWithAll/";
        //        public const string GetCompanyListbyCurrentUserUrl = "/resource/account/currentAccountList";
        //        public const string GetCurrentContactUrl = "/resource/customer/customerWithAll/";
        //        public const string GetSpcUrl = "/resource/call";
        //
        //        public const string GetSelectedCompanyUrl = "/resource/account/accountVO/";
        //
        //
        //        public const string GetSupplierListUrl = "/resource/product/supplierList";
        //        public const string GetCaseByIdUrl = "/resource/case/";
        //        public const string GetCommonSearchDtoListUrl = "/resource/common/commonSearchDtoList/";
        //        public const string GetCustomerListByCustomerIdUrl = "/resource/customer/";
        //        public const string GetCompanyDtoListByIdUrl = "/resource/company/queryById/";
        //
        //
        //
        //        public const string UpdateCompanyUrl = "/resource/company/update";
        //        public const string UpdateContactUrl = "/resource/customer/update";
        //        public const string UpdateSaleProjectCustomerUrl = "/resource/customer/updateCustomerAndSaleProject";
        //
        //        public const string UpdateCaseUrl = "/resource/case/update";
        //        public const string UpdateSaleStatusUrl = "/resource/employees/updateSalesStatus/";
        //        public const string UpdateSaleRelationUrl = "/resource/employees/updateSalesInside/";
        //
        //        public const string GetCheckedCustomerListUrl = "/resource/customer/filteredLikeList/";
        //        public const string GetCtiCustomerListUrl = "/resource/customer/customerListByPhone/";
        //
        //        public const string GetCaseByUrl = "/resource/case/filteredList/";
        //        public const string GetCasePagedByUrl = "/resource/case/filteredPagedList/";
        //        public const string GetCasePagedByObjListUrl = "/resource/case/filteredPagedListByObj";
        //        public const string GetCaseByObjListUrl = "/resource/case/filteredListByObj";
        //
        //        public const string GetCompanyShowDTOtUrl = "/resource/common/companyIdByObj";
        //
        //
        //        public const string GetCasesByCompanyDtoIdListUrl = "/resource/case/caseListByCompanyId/";
        //        public const string GetCaseByCustomerIdListUrl = "/resource/case/listByCustomerId/";
        //
        //        public const string GetLeadsByUrl = "/resource/leads/filteredList/";
        //        public const string GetCompanyPageListUrl = "/resource/account/pagedCurrentAccountList/";
        //        public const string GetLeadsPagedByUrl = "/resource/leads/filteredPagedList/";
        //
        //        public const string GetLeadsPagedByObjListUrl = "/resource/leads/filteredPagedListByObj";
        //        public const string GetLeadsByObjListUrl = "/resource/leads/filteredList/v3";
        //        public const string GetAccountListbyObjUrl = "/resource/account/companyPagedListByObj";
        //        public const string GetCustomerListbyObjUrl = "/resource/customer/customerPagedListByObj";
        //        public const string QuerySaleProjectCustomerbyBjbUrl = "/resource/call/customerPagedListByObj";
        //        public const string QuerySPCDtoUrl = "/resource/call/saleProjectCustomerListByObj";
        //
        //        public const string GetLeadsPagedListbySalesIdUrl = "/resource/leads/leadsListBySalesId/";
        //        public const string GetLeadsListbyCompanyIdUrl = "/resource/leads/leadsListByCompanyId/";
        //        public const string GetLeadsListbyCustomerIdUrl = "/resource/leads/listByCustomerId/";
        //        public const string GetLeadsListbySupplierIdUrl = "/resource/leads/leadsListBySupplierId/";
        //
        //        public const string GetCaseLeadsDetailDtosUrl = "/resource/common/caseLeadsDetailData";
        //        public const string GetCaseLeadsDetailDtoWithCompanyUrl = "/resource/common/caseLeadsDetailDataWithCompany";
        //        public const string GetCaseLeadsDetailDtoWithIndustryUrl = "/resource/common/caseLeadsDetailDataWithIndustry";
        //        public const string GetCaseLeadsDetailDtoOnlyCompanyUrl = "/resource/common/caseLeadsDetailDataOnlyIndustry";
        //
        //
        //        public const string GetLeadsPagedListbySourceCodeUrl = "/resource/leads/leadsListBySourceCode/";
        //        public const string GetLeadsPagedListbyInsiteSaleIdUrl = "/resource/leads/leadsListByInitSale/";
        //        public const string GetLeadsPagedListbyCompyanDtoIdUrl = "/resource/leads/leadsListByCompanyId/";
        //        public const string GetLeadsPagedListbyCustomerIdUrl = "/resource/leads/listByCustomerId/";
        //        public const string GetLeadsPagedListbySupplierIdUrl = "/resource/leads/leadsListBySupplierId/";
        //
        //
        //        public const string GetLeadsPagedListbySourceCodeNopagerUrl = "/resource/leads/allLeadsListBySourceCode/";
        //        public const string GetLeadsPagedListbyIndustryUrl = "/resource/leads/leadsListByIndustry/";
        //        public const string GetLeadsPagedListbyIndustryNopagerUrl = "/resource/leads/allLeadsListByIndustry/";
        //        public const string GetCompanyPageListbyIndustryUrl = "/resource/account/pagedCurrentAccountListByIndustry/";
        //        public const string GetCompanyPageListbyPriorityUrl = "/resource/account/pagedCurrentAccountListByPriority/";
        //        public const string getLeadsByInsiteSaleIdNopagerUrl = "/resource/leads/allLeadsListByInitSale/";
        public const string GetUserListUrl = "/user/list";


        public ObservableCollection<Entity.User> GetUserList(List<KeyValuePair<string, string>> conditions = null)
        {
            return RestHelper.Get<ObservableCollection<Entity.User>>(GetUserListUrl, conditions);
        }


        
    }
}
