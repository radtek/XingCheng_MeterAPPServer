using Common.DotNetCode;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Busines.IDAO
{
    public interface RM_UserInfo_IDAO
    {
        DataTable UserLogin(string name, string pwd);

        string UserLoginKeys(string name, string password, string DeviceId);

        DataTable GetUserInfoPage(StringBuilder SqlWhere, IList<SqlParam> IList_param, int pageIndex, int pageSize, ref int count);

        DataTable GetUserInfoInfo(StringBuilder SqlWhere, IList<SqlParam> IList_param);

        DataTable InitUserRight(string User_ID);

        DataTable InitUserInfoUserGroup(string User_ID);

        DataTable InitUserRole(string User_ID);

        DataTable InitStaffOrganize(string User_ID);

        void SysLoginLog(string SYS_USER_ACCOUNT, string SYS_LOGINLOG_STATUS, string OWNER_address);

        DataTable GetSysLoginLogPage(StringBuilder SqlWhere, IList<SqlParam> IList_param, int pageIndex, int pageSize, ref int count);

        DataTable GetLogin_Info(ref int count);

        DataTable Load_StaffOrganizeList();

        DataTable GetOrganizeList();
        DataTable GetUserListByDepID(string departmentID, string searchKey);

        DataTable InitUserGroupList();

        DataTable InitUserGroupParentId();

        DataTable Load_UserInfoUserGroupList(string UserGroup_ID);

        DataTable InitUserGroupRight(string UserGroup_ID);

        bool AddUserGroupMenber(string[] User_ID, string UserGroup_ID);

        bool Add_UserGroupAllotAuthority(string[] pkVal, string UserGroup_ID);
        DataTable GetAreaTongji(string _AreaId, string _SearchKey);
        DataTable GetMonthTJ();
        DataTable GetArea();
        DataTable GetUserGPS(int Min);
        DataTable GetUserGPS();

        DataTable GetCBUserByAreaID(string AreaID);
        DataTable GetReadingNOByUser(string LoginID);

        DataTable GetMeterStat(string _SearchWhere);
        DataTable GetRecordInfoSearch(string BeginDate, string EndDate, string SearchKey);
    }
}