using Busines.IDAO;
using Common.DotNetBean;
using Common.DotNetCode;
using Common.DotNetData;
using Common.DotNetEncrypt;
using Common.DotNetJson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Busines.DAL
{
    public class RM_UserInfo_Dal : RM_UserInfo_IDAO
    {
        public DataTable Load_StaffOrganizeList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT Organization_ID,Organization_Name,ParentId,'0' AS isUser FROM Base_Organization UNION ALL SELECT U.User_ID AS Organization_ID ,U.User_Code+'|'+U.User_Name AS User_Name,S.Organization_ID,'1' AS isUser FROM Base_UserInfo U RIGHT JOIN Base_StaffOrganize S ON U.User_ID = S.User_ID");
            return DataFactory.SqlDataBase().GetDataTableBySQL(strSql);
        }

        public DataTable GetOrganizeList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT departmentID,departmentName FROM base_department ORDER BY parentId ASC");
            return DataFactory.SqlDataBase().GetDataTableBySQL(strSql);
        }

        public DataTable GetUserListByDepID(string departmentID,string searchKey)
        {
            StringBuilder strSql = new StringBuilder();
            if (string.IsNullOrEmpty(searchKey))
            {
                strSql.Append("SELECT loginId,userName,telePhoneNO FROM base_login WHERE departmentID=@departmentID");
            }
            else
            {
                strSql.AppendFormat("SELECT loginId,userName,telePhoneNO FROM base_login WHERE departmentID=@departmentID and (userName like '%{0}%' or telePhoneNO like '%{0}%')", searchKey);
            }
            return DataFactory.SqlDataBase().GetDataTableBySQL(strSql, new SqlParam[] { new SqlParam("@departmentID", departmentID) });
        }

        public DataTable GetUserInfoPage(StringBuilder SqlWhere, IList<SqlParam> IList_param, int pageIndex, int pageSize, ref int count)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT U.User_ID,U.User_Code,U.User_Name,U.User_Account,U.User_Sex,U.Title,U.DeleteMark,U.User_Remark,U.CreateDate from Base_UserInfo U LEFT JOIN Base_StaffOrganize S ON U.User_ID = S.User_ID where U.DeleteMark !=0");
            strSql.Append(SqlWhere);
            strSql.Append("GROUP BY U.User_ID,U.User_Code,U.User_Name,U.User_Account,U.User_Sex,U.Title,U.DeleteMark,U.User_Remark,U.CreateDate");
            return DataFactory.SqlDataBase().GetPageList(strSql.ToString(), IList_param.ToArray<SqlParam>(), "CreateDate", "Desc", pageIndex, pageSize, ref count);
        }

        public DataTable UserLogin(string name, string pwd)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Select loginId,loginName,loginPassword,departmentID,userName from base_login where ");
            strSql.Append("loginName=@loginName ");
            strSql.Append("and loginPassword=@loginPassword ");
            SqlParam[] para = new SqlParam[]
			{
				new SqlParam("@loginName", name),
				new SqlParam("@loginPassword", pwd)
			};
            return DataFactory.SqlDataBase().GetDataTableBySQL(strSql, para);
        }

        public string UserLoginKeys(string name, string password, string DeviceId)
        {
            DataTable dt = UserLogin(name, password);
            string keys = string.Empty;

            if(DataTableHelper.IsExistRows(dt))
            {
                string username = dt.Rows[0]["User_Account"].ToString();
                string userpwd = dt.Rows[0]["User_Pwd"].ToString();

                keys = Common.DotNetEncrypt.DESEncrypt.Encrypt(username + "|" + userpwd + "|" + DeviceId+DateTime.Now.ToString());
                //keys=
            }
            else
            {

             
               keys = "false";

            }

            return keys;
        }

        public DataTable GetUserInfoInfo(StringBuilder SqlWhere, IList<SqlParam> IList_param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * from Base_UserInfo where DeleteMark !=0");
            strSql.Append(SqlWhere);
            return DataFactory.SqlDataBase().GetDataTableBySQL(strSql, IList_param.ToArray<SqlParam>());
        }

        public DataTable InitUserRight(string User_ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT Menu_Id FROM Base_UserRight WHERE User_ID = @User_ID");
            SqlParam[] para = new SqlParam[]
			{
				new SqlParam("@User_ID", User_ID)
			};
            return DataFactory.SqlDataBase().GetDataTableBySQL(strSql, para);
        }

        public DataTable InitUserInfoUserGroup(string User_ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT UserGroup_ID FROM Base_UserInfoUserGroup WHERE User_ID = @User_ID");
            SqlParam[] para = new SqlParam[]
			{
				new SqlParam("@User_ID", User_ID)
			};
            return DataFactory.SqlDataBase().GetDataTableBySQL(strSql, para);
        }

        public DataTable InitUserRole(string User_ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT Roles_ID FROM Base_UserRole WHERE User_ID = @User_ID");
            SqlParam[] para = new SqlParam[]
			{
				new SqlParam("@User_ID", User_ID)
			};
            return DataFactory.SqlDataBase().GetDataTableBySQL(strSql, para);
        }

        public DataTable InitStaffOrganize(string User_ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT Organization_ID FROM Base_StaffOrganize WHERE User_ID = @User_ID");
            SqlParam[] para = new SqlParam[]
			{
				new SqlParam("@User_ID", User_ID)
			};
            return DataFactory.SqlDataBase().GetDataTableBySQL(strSql, para);
        }

        public void SysLoginLog(string SYS_USER_ACCOUNT, string SYS_LOGINLOG_STATUS, string OWNER_address)
        {
            Hashtable ht = new Hashtable();
            ht["SYS_LOGINLOG_ID"] = CommonHelper.GetGuid;
            ht["User_Account"] = SYS_USER_ACCOUNT;
            ht["SYS_LOGINLOG_IP"] = RequestHelper.GetIP();
            ht["OWNER_address"] = OWNER_address;
            ht["SYS_LOGINLOG_STATUS"] = SYS_LOGINLOG_STATUS;
            DataFactory.SqlDataBase().InsertByHashtable("Base_SysLoginlog", ht);
        }

        public DataTable GetSysLoginLogPage(StringBuilder SqlWhere, IList<SqlParam> IList_param, int pageIndex, int pageSize, ref int count)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * from Base_SysLoginlog where 1=1");
            strSql.Append(SqlWhere);
            return DataFactory.SqlDataBase().GetPageList(strSql.ToString(), IList_param.ToArray<SqlParam>(), "SYS_LOGINLOG_TIME", "Desc", pageIndex, pageSize, ref count);
        }

        public DataTable GetLogin_Info(ref int count)
        {
            DateTime now = DateTime.Now;
            DateTime d = new DateTime(now.Year, now.Month, 1);
            DateTime d2 = d.AddMonths(1).AddDays(-1.0);
            string UserAccount = RequestSession.GetSessionUser().UserAccount.ToString();
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSqlCount = new StringBuilder();
            strSql.Append("Select top 2 SYS_LOGINLOG_IP,Sys_LoginLog_Time from Base_SysLoginlog where User_Account = @User_Account");
            strSql.Append(" and Sys_LoginLog_Time >= @BeginBuilTime");
            strSql.Append(" and Sys_LoginLog_Time <= @endBuilTime ORDER BY Sys_LoginLog_Time DESC ");
            strSqlCount.Append("Select count(1) from Base_SysLoginlog where User_Account = @User_Account");
            strSqlCount.Append(" and Sys_LoginLog_Time >= @BeginBuilTime");
            strSqlCount.Append(" and Sys_LoginLog_Time <= @endBuilTime");
            SqlParam[] para = new SqlParam[]
			{
				new SqlParam("@User_Account", UserAccount),
				new SqlParam("@BeginBuilTime", d),
				new SqlParam("@endBuilTime", d2)
			};
            count = Convert.ToInt32(DataFactory.SqlDataBase().GetObjectValue(strSqlCount, para));
            return DataFactory.SqlDataBase().GetDataTableBySQL(strSql, para);
        }

        public DataTable InitUserGroupList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * from Base_UserGroup WHERE DeleteMark = 1 ORDER BY SortCode ASC");
            return DataFactory.SqlDataBase().GetDataTableBySQL(strSql);
        }

        public DataTable InitUserGroupParentId()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT UserGroup_ID,\r\n                            UserGroup_Name+' - '+CASE ParentId WHEN '0' THEN '父节' ELSE  '子节' END AS UserGroup_Name\r\n                            FROM Base_UserGroup WHERE DeleteMark = 1 ORDER BY SortCode ASC");
            return DataFactory.SqlDataBase().GetDataTableBySQL(strSql);
        }

        public DataTable Load_UserInfoUserGroupList(string UserGroup_ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT UserInfoUserGroup_ID,U.User_Name+'|'+U.User_Code AS User_Name,U.User_Account,U.User_Sex,U.Title,U.DeleteMark,U.User_Remark\r\n                            FROM Base_UserInfo U RIGHT JOIN Base_UserInfoUserGroup G ON G.User_ID = U.User_ID \r\n                            WHERE G.UserGroup_ID = @UserGroup_ID");
            SqlParam[] para = new SqlParam[]
			{
				new SqlParam("@UserGroup_ID", UserGroup_ID)
			};
            return DataFactory.SqlDataBase().GetDataTableBySQL(strSql, para);
        }

        public DataTable InitUserGroupRight(string UserGroup_ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT Menu_Id FROM Base_UserGroupRight WHERE UserGroup_ID = @UserGroup_ID");
            SqlParam[] para = new SqlParam[]
			{
				new SqlParam("@UserGroup_ID", UserGroup_ID)
			};
            return DataFactory.SqlDataBase().GetDataTableBySQL(strSql, para);
        }

        public bool AddUserGroupMenber(string[] User_ID, string UserGroup_ID)
        {
            bool result;
            try
            {
                StringBuilder[] sqls = new StringBuilder[User_ID.Length];
                object[] objs = new object[User_ID.Length];
                int index = 0;
                for (int i = 0; i < User_ID.Length; i++)
                {
                    string item = User_ID[i];
                    if (item.Length > 0)
                    {
                        StringBuilder sbadd = new StringBuilder();
                        sbadd.Append("Insert into Base_UserInfoUserGroup(");
                        sbadd.Append("UserInfoUserGroup_ID,User_ID,UserGroup_ID,CreateUserId,CreateUserName");
                        sbadd.Append(")Values(");
                        sbadd.Append("@UserInfoUserGroup_ID,@User_ID,@UserGroup_ID,@CreateUserId,@CreateUserName)");
                        SqlParam[] parmAdd = new SqlParam[]
						{
							new SqlParam("@UserInfoUserGroup_ID", CommonHelper.GetGuid),
							new SqlParam("@User_ID", item),
							new SqlParam("@UserGroup_ID", UserGroup_ID),
							new SqlParam("@CreateUserId", RequestSession.GetSessionUser().UserId),
							new SqlParam("@CreateUserName", RequestSession.GetSessionUser().UserName)
						};
                        sqls[index] = sbadd;
                        objs[index] = parmAdd;
                        index++;
                    }
                }
                result = (DataFactory.SqlDataBase().BatchExecuteBySql(sqls, objs) >= 0);
            }
            catch
            {
                result = false;
            }
            return result;
        }

        public bool Add_UserGroupAllotAuthority(string[] pkVal, string UserGroup_ID)
        {
            bool result;
            try
            {
                StringBuilder[] sqls = new StringBuilder[pkVal.Length + 1];
                object[] objs = new object[pkVal.Length + 1];
                StringBuilder sbDelete = new StringBuilder();
                sbDelete.Append("Delete From Base_UserGroupRight Where UserGroup_ID =@UserGroup_ID");
                SqlParam[] parm = new SqlParam[]
				{
					new SqlParam("@UserGroup_ID", UserGroup_ID)
				};
                sqls[0] = sbDelete;
                objs[0] = parm;
                int index = 1;
                for (int i = 0; i < pkVal.Length; i++)
                {
                    string item = pkVal[i];
                    if (item.Length > 0)
                    {
                        StringBuilder sbadd = new StringBuilder();
                        sbadd.Append("Insert into Base_UserGroupRight(");
                        sbadd.Append("UserGroupRight_ID,UserGroup_ID,Menu_Id,CreateUserId,CreateUserName");
                        sbadd.Append(")Values(");
                        sbadd.Append("@UserGroupRight_ID,@UserGroup_ID,@Menu_Id,@CreateUserId,@CreateUserName)");
                        SqlParam[] parmAdd = new SqlParam[]
						{
							new SqlParam("@UserGroupRight_ID", CommonHelper.GetGuid),
							new SqlParam("@UserGroup_ID", UserGroup_ID),
							new SqlParam("@Menu_Id", item),
							new SqlParam("@CreateUserId", RequestSession.GetSessionUser().UserId),
							new SqlParam("@CreateUserName", RequestSession.GetSessionUser().UserName)
						};
                        sqls[index] = sbadd;
                        objs[index] = parmAdd;
                        index++;
                    }
                }
                result = (DataFactory.SqlDataBase().BatchExecuteBySql(sqls, objs) >= 0);
            }
            catch
            {
                result = false;
            }
            return result;
        }

        public DataTable GetAreaTongji(string _AreaId, string _SearchKey)
        {
            Hashtable ht = new Hashtable();
            ht["AreaID"] = _AreaId;
            ht["SearchKey"] = _SearchKey;
            return DataFactory.SqlDataBase().GetDataTableProc("PR_App_TJ_Month", ht);
        }
        public DataTable GetMonthTJ()
        {
           return GetAreaTongji(null,null);
        }
        public DataTable GetArea()
        {
            return DataFactory.SqlDataBase().GetDataTable("base_area");
        }

        public DataTable GetUserGPS(int Min)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@" SELECT UserTrack.Longitude,UserTrack.Latitude,UserTrack.LoginID, V_LOGIN.userName,V_LOGIN.departmentID,V_LOGIN.departmentName,V_LOGIN.telePhoneNO FROM UserTrack LEFT JOIN V_LOGIN ON UserTrack.LoginID=V_LOGIN.loginId  WHERE TrackID IN
 (select max(TrackID) AS TrackID from UserTrack where datediff(minute,CreateDateTime,getdate())<@Min and LoginID<>'' group by LoginID)");
            SqlParam[] para = new SqlParam[]
			{
				new SqlParam("@Min", Min)
			};
            return DataFactory.SqlDataBase().GetDataTableBySQL(strSql, para);
        }
        public DataTable GetUserGPS()
        {
            return  GetUserGPS(40);
        }

        public DataTable GetCBUserByAreaID(string AreaID)
        {
            if (!string.IsNullOrEmpty(AreaID))
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(@"SELECT DISTINCT LOGINID,USERNAME FROM V_METERREADING WHERE areaId=@areaId");
                SqlParam[] para = new SqlParam[]
			{
				new SqlParam("@areaId", AreaID)
			};
                return DataFactory.SqlDataBase().GetDataTableBySQL(strSql, para);
            }
            else
            {
                return null;
            }
        }

        public DataTable GetReadingNOByUser(string LoginID)
        {
            if (!string.IsNullOrEmpty(LoginID))
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(@"SELECT DISTINCT meterReadingID,meterReadingNO FROM V_METERREADING WHERE LOGINID=@LoginID");
                SqlParam[] para = new SqlParam[]
			{
				new SqlParam("@LoginID", LoginID)
			};
                return DataFactory.SqlDataBase().GetDataTableBySQL(strSql, para);
            }
            else
            {
                return null;
            }
        }

        public DataTable GetMeterStat(string _SearchWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(@"SELECT DATEPART(YEAR,GETDATE()) AS Tyear,DATEPART(Month,GETDATE()) AS TMonth,COUNT(1) AS UTotal,count(distinct loginid) as UUserNum,
SUM(case when checkState='1' AND totalNumber>0 then 1 else 0 END) AS UOver,
0 AS UNever,--最好用总户数减去已抄数量
SUM(case when totalNumber=0 then 1 else 0 END) AS U0,
0 AS Urate,
SUM(case chargeState when '3' then 1 else 0 END) AS FNum,
SUM(case chargeState when '3' then totalNumber else 0 END) AS Fwater,
SUM(case chargeState when '3' then totalCharge else 0 END) AS FFee
from readMeterRecord where WATERMETERNUMBERCHANGESTATE='0' {0}  and 
DATEDIFF(MONTH,readMeterRecordYearAndMonth,GETDATE())=0", _SearchWhere);
            return DataFactory.SqlDataBase().GetDataTableBySQL(strSql);
        }

        public DataTable GetRecordInfoSearch(string BeginDate,string EndDate,string SearchKey)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(@"SELECT TOP 8 * FROM readMeterRecord WHERE WATERMETERNUMBERCHANGESTATE='0' and 
readMeterRecordYearAndMonth BETWEEN '{0} 00:00:00' AND '{1} 00:00:00'
AND (waterUserId like '%{2}%' or waterUserName like '%{2}%') ", BeginDate, EndDate, SearchKey);
            return DataFactory.SqlDataBase().GetDataTableBySQL(strSql);
        }
      
    }
}