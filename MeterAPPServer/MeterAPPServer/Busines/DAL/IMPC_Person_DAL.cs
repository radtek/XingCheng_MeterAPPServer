using Busines.IDAO;
using Common.DotNetBean;
using Common.DotNetCode;
using Common.DotNetConfig;
using Common.DotNetData;
using Common.DotNetEamil;
using Common.DotNetJson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.SessionState;

namespace Busines.DAL
{
    public class IMPC_Person_DAL : IMPC_Person_IDAO, IRequiresSessionState
    {
        public DataTable PersonLogin(string name, string password, string loginName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM View_loginName where ");
            strSql.Append("loginName=@loginName ");
            strSql.Append("AND name=@name ");
            strSql.Append("AND password=@password ");
            //strSql.Append("AND islock=0");
            SqlParam[] para = new SqlParam[]
			{
				new SqlParam("@loginName", loginName),
				new SqlParam("@name", name),
                new SqlParam("@password", password)
			};
            return DataFactory.SqlDataBase().GetDataTableBySQL(strSql, para);
        }
        public int validname(string loginName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT TOP 1 *  FROM View_loginName WHERE ");
            strSql.Append("loginName=@loginName ");
            SqlParam[] para = new SqlParam[]
			{
				new SqlParam("@loginName", loginName),
			};
            DataTable dt = DataFactory.SqlDataBase().GetDataTableBySQL(strSql, para);
            if (DataTableHelper.IsExistRows(dt))
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public int validmail(string email)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT TOP 1 *  FROM View_loginName WHERE ");
            strSql.Append("email=@email ");
            SqlParam[] para = new SqlParam[]
			{
				new SqlParam("@email", email),
			};
            DataTable dt = DataFactory.SqlDataBase().GetDataTableBySQL(strSql, para);
            if (DataTableHelper.IsExistRows(dt))
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public int validmobile(string mobile)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT TOP 1 *  FROM View_loginName WHERE ");
            strSql.Append("mobile=@mobile ");
            SqlParam[] para = new SqlParam[]
			{
				new SqlParam("@mobile", mobile),
			};
            DataTable dt = DataFactory.SqlDataBase().GetDataTableBySQL(strSql, para);
            if (DataTableHelper.IsExistRows(dt))
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public int validemailcode(string email, string emailcode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT *  FROM View_MailSend WHERE ");
            strSql.Append("mail=@email ");
            strSql.Append("AND dcode=@emailcode ");
            SqlParam[] para = new SqlParam[]
			{
                new SqlParam("@email", email),
				new SqlParam("@emailcode", emailcode),
			};
            DataTable dt = DataFactory.SqlDataBase().GetDataTableBySQL(strSql, para);
            if (DataTableHelper.IsExistRows(dt))
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        //政治面貌
        public DataTable getpolity(string value)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT VALUE,NAME FROM polity ORDER BY VALUE ASC");
            DataTable dt = DataFactory.SqlDataBase().GetDataTableBySQL(strSql);
            if (!string.IsNullOrEmpty(value))
            {
                if (DataTableHelper.IsExistRows(dt))
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["VALUE"].ToString() == value)
                        {
                            dt.Rows.RemoveAt(i);
                        }
                    }
                }
            }
            return dt;
        }
        public DataTable getNation(string value)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM Nation ORDER BY VALUE ASC");
            DataTable dt = DataFactory.SqlDataBase().GetDataTableBySQL(strSql);
            if (!string.IsNullOrEmpty(value))
            {
                if (DataTableHelper.IsExistRows(dt))
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["value"].ToString() == value)
                        {
                            dt.Rows.RemoveAt(i);
                        }
                    }
                }
            }
            return dt;
        }
        public DataTable getmarriage(string value)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM marriage ORDER BY VALUE ASC");
            DataTable dt = DataFactory.SqlDataBase().GetDataTableBySQL(strSql);
            if (!string.IsNullOrEmpty(value))
            {
                if (DataTableHelper.IsExistRows(dt))
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["value"].ToString() == value)
                        {
                            dt.Rows.RemoveAt(i);
                        }
                    }
                }
            }
            return dt;
        }
        public DataTable getgradtype(string value)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM gradtype ORDER BY VALUE ASC");
            DataTable dt = DataFactory.SqlDataBase().GetDataTableBySQL(strSql);
            if (!string.IsNullOrEmpty(value))
            {
                if (DataTableHelper.IsExistRows(dt))
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["value"].ToString() == value)
                        {
                            dt.Rows.RemoveAt(i);
                        }
                    }
                }
            }
            return dt;
        }
        public DataTable geteduType(string value)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM eduType ORDER BY VALUE ASC");
            DataTable dt = DataFactory.SqlDataBase().GetDataTableBySQL(strSql);
            if (!string.IsNullOrEmpty(value))
            {
                if (DataTableHelper.IsExistRows(dt))
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["value"].ToString() == value)
                        {
                            dt.Rows.RemoveAt(i);
                        }
                    }
                }
            }
            return dt;
        }
        public DataTable geteduBack(string value)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM eduBack ORDER BY VALUE ASC");
            DataTable dt = DataFactory.SqlDataBase().GetDataTableBySQL(strSql);
            if (!string.IsNullOrEmpty(value))
            {
                if (DataTableHelper.IsExistRows(dt))
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["value"].ToString() == value)
                        {
                            dt.Rows.RemoveAt(i);
                        }
                    }
                }
            }
            return dt;
        }
        public DataTable getedubatch(string value)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM edubatch ORDER BY VALUE ASC");
            DataTable dt = DataFactory.SqlDataBase().GetDataTableBySQL(strSql);
            if (!string.IsNullOrEmpty(value))
            {
                if (DataTableHelper.IsExistRows(dt))
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["value"].ToString() == value)
                        {
                            dt.Rows.RemoveAt(i);
                        }
                    }
                }
            }
            return dt;
        }
        public DataTable getgradeType(string value)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM gradeType ORDER BY VALUE ASC");
            DataTable dt = DataFactory.SqlDataBase().GetDataTableBySQL(strSql);
            if (!string.IsNullOrEmpty(value))
            {
                if (DataTableHelper.IsExistRows(dt))
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["value"].ToString() == value)
                        {
                            dt.Rows.RemoveAt(i);
                        }
                    }
                }
            }
            return dt;
        }
        public int overstep(string userID)
        {
            int step = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT TOP 1 *  FROM View_Step WHERE ");
            strSql.Append("User_ID=@User_ID ");
            SqlParam[] para = new SqlParam[]
			{
				new SqlParam("@User_ID", userID),
			};
            DataTable dt = DataFactory.SqlDataBase().GetDataTableBySQL(strSql, para);
            if (DataTableHelper.IsExistRows(dt))
            {
                if (dt.Rows[0]["step1"].ToString() == "1")
                {
                    step = 1;
                    if (dt.Rows[0]["step2"].ToString() == "1")
                    {
                        step = 2;
                        if (dt.Rows[0]["step3"].ToString() == "1")
                        {
                            step = 3;
                        }
                    }
                }


            }
            return step;
        }
        public int overstep()
        {
            string userID = RequestSession.GetSessionUser().UserId.ToString();
            return overstep(userID);
        }
        public Hashtable allstep(string userID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT TOP 1 *  FROM View_Step WHERE ");
            strSql.Append("User_ID=@User_ID ");
            SqlParam[] para = new SqlParam[]
			{
				new SqlParam("@User_ID", userID),
			};
            DataTable dt = DataFactory.SqlDataBase().GetDataTableBySQL(strSql, para);
            if (DataTableHelper.IsExistRows(dt))
            {
                return DataTableHelper.DataRowToHashTable(dt.Rows[0]);
            }
            else
            {
                return null;
            }
        }
        public Hashtable allstep()
        {
            return allstep(RequestSession.GetSessionUser().UserId.ToString());
        }
        public DataTable getCity(string cityID)
        {
            string parentid = cityID;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT CityID,CityName  FROM area_data ");
            if (string.IsNullOrEmpty(parentid))
            {
                parentid = "1";
            }

            strSql.Append("WHERE ParentID=@parentid ");
            SqlParam[] para = new SqlParam[]
			{
				new SqlParam("@parentid",int.Parse(parentid)),
			};
            strSql.Append(" ORDER BY CITYID ASC");
            DataTable dt = DataFactory.SqlDataBase().GetDataTableBySQL(strSql, para);

            if (DataTableHelper.IsExistRows(dt))
            {
                return dt;
            }
            else
            {
                return null;
            }

        }
        public DataTable GetCompanyListPage(StringBuilder SqlWhere, IList<SqlParam> IList_param, int pageIndex, int pageSize, ref int count)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT *  ");
            strSql.Append("FROM Company ");
            //strSql.Append("WHERE DeleteMark=0   ");
            strSql.Append(SqlWhere);
            return DataFactory.SqlDataBase().GetPageList(strSql.ToString(), IList_param.ToArray<SqlParam>(), "CreateDate", "Asc", pageIndex, pageSize, ref count);
        }
        public bool IsFreshs(string specID, string UserID)
        {
            StringBuilder struser = new StringBuilder();
            struser.AppendFormat("SELECT gradDate,gradtype,birthday FROM Personinfo WHERE USER_ID='{0}'", UserID);

            DataTable dtuser = DataFactory.SqlDataBase().GetDataTableBySQL(struser);

            if (DataTableHelper.IsExistRows(dtuser))
            {
                string UsergradDate = dtuser.Rows[0][0].ToString();
                string UsergradType = dtuser.Rows[0][1].ToString();
                string Userbirthday = dtuser.Rows[0][2].ToString();

                StringBuilder strsql = new StringBuilder();

                //用户服务、输电线路检修 两个专业的报名时间设置为 2014年~2015年（相应的时间，顺延2015年的）
                if (UsergradType == "1")
                {
                    //SELECT * FROM Specialty WHERE '1988-01-01'>=USERAGE AND '2014-06-07' BETWEEN StartIndate AND overIndate AND ID='{2}'
                    //strsql.AppendFormat("SELECT COUNT(*) FROM SelectBetween WHERE '{0}' BETWEEN BeginDate AND EndDate AND IsUse=1", UsergradDate);
                    strsql.AppendFormat("SELECT COUNT(*) FROM Specialty WHERE '{0}'>=USERAGE AND '{1}' BETWEEN StartIndate AND overIndate AND ID='{2}'", Userbirthday, UsergradDate, specID);
                }
                else
                {
                    //SELECT * FROM Specialty WHERE '1988-01-01'>=USERAGE AND '2014-06-07' BETWEEN StartOverseaDate AND endOverseaDate AND ID='{2}'
                   // strsql.AppendFormat("SELECT COUNT(*) FROM SelectBetween WHERE '{0}' BETWEEN BeginDate0 AND EndDate0 AND IsUse=1", UsergradDate);
                    strsql.AppendFormat("SELECT COUNT(*) FROM Specialty WHERE '{0}'>=USERAGE AND '{1}' BETWEEN StartOverseaDate AND endOverseaDate AND ID='{2}'", Userbirthday, UsergradDate, specID);
                }

                DataTable dt = DataFactory.SqlDataBase().GetDataTableBySQL(strsql);
                if (int.Parse(dt.Rows[0][0].ToString()) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            else
            {
                return false;
            }

        }
        public bool IsFreshs(string specID)
        {
           return IsFreshs(specID, RequestSession.GetSessionUser().UserId.ToString());   

        }
        public bool IsFresh(string UserID)
        {
            StringBuilder struser = new StringBuilder();
            struser.AppendFormat("SELECT gradDate,gradtype FROM Personinfo WHERE birthday>='1988-01-01' AND USER_ID='{0}'", UserID);

            DataTable dtuser = DataFactory.SqlDataBase().GetDataTableBySQL(struser);

            if (DataTableHelper.IsExistRows(dtuser))
            {
                string UsergradDate = dtuser.Rows[0][0].ToString();
                string UsergradType = dtuser.Rows[0][1].ToString();

                StringBuilder strsql = new StringBuilder();

                //用户服务、输电线路检修 两个专业的报名时间设置为 2014年~2015年（相应的时间，顺延2015年的）
                if (UsergradType == "1")
                {
                    strsql.AppendFormat("SELECT COUNT(*) FROM SelectBetween WHERE '{0}' BETWEEN BeginDate AND EndDate AND IsUse=1", UsergradDate);
                }
                else
                {
                    strsql.AppendFormat("SELECT COUNT(*) FROM SelectBetween WHERE '{0}' BETWEEN BeginDate0 AND EndDate0 AND IsUse=1", UsergradDate);
                }

                DataTable dt = DataFactory.SqlDataBase().GetDataTableBySQL(strsql);
                if (int.Parse(dt.Rows[0][0].ToString()) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                
            }
            else
            {
                return false;
            }
            
        }
        public bool IsFresh()
        {
            return IsFresh(RequestSession.GetSessionUser().UserId.ToString());
        }
        public int ApplySpec(string specID)
        {
            if (RequestSession.GetSessionUser() == null)
            {
                return 0;
            }
            if (string.IsNullOrEmpty(RequestSession.GetSessionUser().UserId.ToString()))
            {
                return 0;
            }
            if (string.IsNullOrEmpty(specID))
            {
                return 0;
            }
            if (!ValidateUtil.IsGuid(specID))
            {
                return 0;
            }
            if (DataFactory.SqlDataBase().IsExist("Specialty", "ID", specID) == 0)
            {
                return 0;
            }
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE Personinfo SET specID=@SPECID,applyDate=GETDATE() WHERE User_ID=@User_ID");
            SqlParam[] para = new SqlParam[]
            {
                new SqlParam("@SPECID", specID),
                new SqlParam("@User_ID", RequestSession.GetSessionUser().UserId.ToString())
            };
            int num = DataFactory.SqlDataBase().ExecuteBySql(strSql, para);
            return num;
        }
        public DataTable GetApply(string UserID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM View_SpecApply WHERE User_ID = @User_ID");

            SqlParam[] param = new SqlParam[]
            {
                new SqlParam("@User_ID",UserID)
            };
            return DataFactory.SqlDataBase().GetDataTableBySQL(strSql, param);
        }
        public DataTable GetApply()
        {
            return GetApply(RequestSession.GetSessionUser().UserId.ToString());
        }
        //论文等级
        public DataTable getPaperLevel(string value)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT  LevelValue AS VALUE,LevelName AS NAME FROM PaperLevel ORDER BY LevelValue ASC");
            DataTable dt = DataFactory.SqlDataBase().GetDataTableBySQL(strSql);
            if (!string.IsNullOrEmpty(value))
            {
                if (DataTableHelper.IsExistRows(dt))
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["VALUE"].ToString() == value)
                        {
                            dt.Rows.RemoveAt(i);
                        }
                    }
                }
            }
            return dt;
        }
        //科研项目等级
        public DataTable getResearchLevel(string value)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT LevelValue AS VALUE,LevelName AS NAME FROM ResearchLevel ORDER BY LevelValue ASC");
            DataTable dt = DataFactory.SqlDataBase().GetDataTableBySQL(strSql);
            if (!string.IsNullOrEmpty(value))
            {
                if (DataTableHelper.IsExistRows(dt))
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["VALUE"].ToString() == value)
                        {
                            dt.Rows.RemoveAt(i);
                        }
                    }
                }
            }
            return dt;
        }
        //证件类型
        public DataTable getAttachmentType(string value)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT AttaType AS VALUE,AttaName AS NAME FROM AttachmentType ORDER BY AttaType ASC");
            DataTable dt = DataFactory.SqlDataBase().GetDataTableBySQL(strSql);
            if (!string.IsNullOrEmpty(value))
            {
                if (DataTableHelper.IsExistRows(dt))
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["VALUE"].ToString() == value)
                        {
                            dt.Rows.RemoveAt(i);
                        }
                    }
                }
            }
            return dt;
        }
        //家庭称谓
        public DataTable getFamilyCall(string value)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT CallValue AS VALUE,CallName AS NAME FROM FamilyCall ORDER BY CallValue ASC");
            DataTable dt = DataFactory.SqlDataBase().GetDataTableBySQL(strSql);
            if (!string.IsNullOrEmpty(value))
            {
                if (DataTableHelper.IsExistRows(dt))
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["VALUE"].ToString() == value)
                        {
                            dt.Rows.RemoveAt(i);
                        }
                    }
                }
            }
            return dt;
        }
        //外语类型
        public DataTable getLanguageName(string value)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT LangValue AS VALUE,LangName AS NAME FROM LanguageName ORDER BY LangValue ASC");
            DataTable dt = DataFactory.SqlDataBase().GetDataTableBySQL(strSql);
            if (!string.IsNullOrEmpty(value))
            {
                if (DataTableHelper.IsExistRows(dt))
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["VALUE"].ToString() == value)
                        {
                            dt.Rows.RemoveAt(i);
                        }
                    }
                }
            }
            return dt;
        }
        //外语水平等级
        public DataTable getLanguageLevel(string value)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT LevelValue AS VALUE,LevelName AS NAME FROM LanguageLevel ORDER BY LevelValue ASC");
            DataTable dt = DataFactory.SqlDataBase().GetDataTableBySQL(strSql);
            if (!string.IsNullOrEmpty(value))
            {
                if (DataTableHelper.IsExistRows(dt))
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["VALUE"].ToString() == value)
                        {
                            dt.Rows.RemoveAt(i);
                        }
                    }
                }
            }
            return dt;
        }
        //工作类型
        public DataTable getWorkType(string value)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT TypeValue AS VALUE,TypeName AS NAME FROM WorkType ORDER BY TypeValue ASC");
            DataTable dt = DataFactory.SqlDataBase().GetDataTableBySQL(strSql);
            if (!string.IsNullOrEmpty(value))
            {
                if (DataTableHelper.IsExistRows(dt))
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["VALUE"].ToString() == value)
                        {
                            dt.Rows.RemoveAt(i);
                        }
                    }
                }
            }
            return dt;
        }
        //学位
        public DataTable getDegree(string value)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT DegreeValue AS VALUE,DegreeName AS NAME FROM Degree ORDER BY DegreeValue ASC");
            DataTable dt = DataFactory.SqlDataBase().GetDataTableBySQL(strSql);
            if (!string.IsNullOrEmpty(value))
            {
                if (DataTableHelper.IsExistRows(dt))
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["VALUE"].ToString() == value)
                        {
                            dt.Rows.RemoveAt(i);
                        }
                    }
                }
            }
            return dt;
        }
        //院校所在地
        public DataTable getSchool_Area(string value)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT AreaCode AS VALUE,AreaName AS NAME FROM School_Area ORDER BY AreaCode ASC");
            DataTable dt = DataFactory.SqlDataBase().GetDataTableBySQL(strSql);
            if (!string.IsNullOrEmpty(value))
            {
                if (DataTableHelper.IsExistRows(dt))
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["VALUE"].ToString() == value)
                        {
                            dt.Rows.RemoveAt(i);
                        }
                    }
                }
            }
            return dt;
        }
        //院校搜索
        public DataTable getSchool_Info(string searchstr, string SchoolArea)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT ID AS SchoolCode,SchoolName FROM School_Info ");
            strSql.Append(" WHERE SchoolArea=@SchoolArea ");
            strSql.Append("AND SchoolName LIKE '%'+@SchoolName+'%' ");
            strSql.Append("ORDER BY ID ASC");
            SqlParam[] param = new SqlParam[]
            {
                new SqlParam("@SchoolArea",SchoolArea),
                new SqlParam("@SchoolName",searchstr)
            };

            return DataFactory.SqlDataBase().GetDataTableBySQL(strSql, param);
        }
        public DataTable getEducation(string EduID, string UserID)
        {
            StringBuilder strSql = new StringBuilder();
            //SELECT *,(SELECT name FROM EduType WHERE value=E.eduType) AS eduTypeName,
            //(SELECT name FROM EduBack WHERE value=E.eduBack) AS eduBackName,
            //(SELECT name FROM Edubatch WHERE value=E.edubatch) AS edubatchName,
            //(SELECT DegreeName FROM Degree WHERE DegreeValue=E.degree) AS degreeName,
            //(SELECT name FROM GradeType WHERE value=E.gradeType) AS gradeTypeName
            // FROM Education E
            strSql.Append("SELECT *,(SELECT name FROM EduType WHERE value=E.eduType) AS eduTypeName,");
            strSql.Append("(SELECT name FROM EduBack WHERE value=E.eduBack) AS eduBackName,");
            strSql.Append("(SELECT name FROM Edubatch WHERE value=E.edubatch) AS edubatchName,");
            strSql.Append("(SELECT DegreeName FROM Degree WHERE DegreeValue=E.degree) AS degreeName,");
            strSql.Append("(SELECT name FROM GradeType WHERE value=E.gradeType) AS gradeTypeName ");
            strSql.Append("FROM Education E ");
            strSql.AppendFormat("WHERE User_ID='{0}' ", UserID);
            if (string.IsNullOrEmpty(EduID))
            {
                strSql.Append("ORDER BY EndDate DESC");
                return DataFactory.SqlDataBase().GetDataTableBySQL(strSql, null);
            }
            else
            {
                strSql.Append("AND EduID=@EduID ORDER BY EndDate DESC");
                SqlParam[] param = new SqlParam[]
            {
                new SqlParam("@EduID",EduID)
            };
                return DataFactory.SqlDataBase().GetDataTableBySQL(strSql, param);
            }
        }
        public DataTable getEducation(string EduID)
        {
            return getEducation(EduID, RequestSession.GetSessionUser().UserId.ToString());
        }
        public DataTable getFirstEducation(string UserID, string EduID)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("SELECT *,(SELECT name FROM EduType WHERE value=E.eduType) AS eduTypeName,");
            strSql.Append("(SELECT name FROM EduBack WHERE value=E.eduBack) AS eduBackName,");
            strSql.Append("(SELECT name FROM Edubatch WHERE value=E.edubatch) AS edubatchName,");
            strSql.Append("(SELECT DegreeName FROM Degree WHERE DegreeValue=E.degree) AS degreeName,");
            strSql.Append("(SELECT name FROM GradeType WHERE value=E.gradeType) AS gradeTypeName ");
            strSql.Append("FROM Education E ");
            strSql.AppendFormat("WHERE User_ID='{0}' AND isfirst=1 ", UserID);

            if (string.IsNullOrEmpty(EduID))
            {
                strSql.Append("ORDER BY EndDate DESC");
                return DataFactory.SqlDataBase().GetDataTableBySQL(strSql);
            }
            else
            {
                strSql.Append("AND EduID NOT IN (@EduID) ORDER BY EndDate DESC");
                SqlParam[] param = new SqlParam[]
            {
                new SqlParam("@EduID",EduID)
            };
                return DataFactory.SqlDataBase().GetDataTableBySQL(strSql, param);
            }
        }
        public DataTable getFirstEducation(string EduID)
        {
            return getFirstEducation(RequestSession.GetSessionUser().UserId.ToString(), EduID);
        }
        public DataTable getFirstEducation()
        {
            return getFirstEducation(RequestSession.GetSessionUser().UserId.ToString(), "");
        }
        public DataTable getEducation()
        {
            return getEducation("");
        }
        public DataTable GetFamily(string UserID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM View_Step4 WHERE User_ID = @User_ID");

            SqlParam[] param = new SqlParam[]
            {
                new SqlParam("@User_ID",UserID)
            };
            return DataFactory.SqlDataBase().GetDataTableBySQL(strSql, param);
        }
        public DataTable GetFamily()
        {
            return GetFamily(RequestSession.GetSessionUser().UserId.ToString());
        }
        public void ChangeStep(string UserID, string steps, int state)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("UPDATE Personinfo SET {0}=@state WHERE USER_ID=@UserID", steps);
            SqlParam[] param = new SqlParam[]
            {
                new SqlParam("@state",state),
                new SqlParam("@UserID",UserID)
            };
            DataFactory.SqlDataBase().GetDataTableBySQL(strSql, param);
        }
        public void ChangeStepT(string steps)
        {
            ChangeStep(RequestSession.GetSessionUser().UserId.ToString(), steps, 1);
        }
        public void ChangeStepF(string steps)
        {
            ChangeStep(RequestSession.GetSessionUser().UserId.ToString(), steps, 0);
        }
        public DataTable GetWork(string UserID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT WorkID,User_ID,CompName,Job,BeginDate,EndDate,WorkType,WorkPlace,WorkDescription,(SELECT TypeName FROM WorkType WHERE TypeValue=W.WorkType) AS TypeName FROM WORK W WHERE W.User_ID=@User_ID");

            SqlParam[] param = new SqlParam[]
            {
                new SqlParam("@User_ID",UserID)
            };
            return DataFactory.SqlDataBase().GetDataTableBySQL(strSql, param);
        }
        public DataTable GetWork()
        {
            return GetWork(RequestSession.GetSessionUser().UserId.ToString());
        }
        public int WorkDel(string ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("DELETE FROM Work WHERE WorkID=@WorkID AND User_ID=@User_ID");
            SqlParam[] para = new SqlParam[]
            {
                new SqlParam("@WorkID", ID),
                new SqlParam("@User_ID", RequestSession.GetSessionUser().UserId.ToString())
            };
            int num = DataFactory.SqlDataBase().ExecuteBySql(strSql, para);
            StringBuilder strstep = new StringBuilder();
            strstep.Append("SELECT COUNT(*) FROM Work WHERE User_ID=@User_ID");
            SqlParam[] para2 = new SqlParam[]
            {
                new SqlParam("@User_ID", RequestSession.GetSessionUser().UserId.ToString())
            };
            int count = int.Parse(DataFactory.SqlDataBase().GetDataTableBySQL(strstep, para2).Rows[0][0].ToString());
            if (count > 0)
            {
                ChangeStepT("step5");
            }
            else
            {
                ChangeStepF("step5");
            }

            return num;
        }
        public DataTable getAwardLevel(string value)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT LevelValue AS VALUE,LevelName AS NAME FROM AwardLevel ORDER BY LevelValue ASC");
            DataTable dt = DataFactory.SqlDataBase().GetDataTableBySQL(strSql);
            if (!string.IsNullOrEmpty(value))
            {
                if (DataTableHelper.IsExistRows(dt))
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["VALUE"].ToString() == value)
                        {
                            dt.Rows.RemoveAt(i);
                        }
                    }
                }
            }
            return dt;
        }
        public DataTable GetAward(string UserID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT AwardID,User_ID,AwardName,AwardLevel,GetDate,AwardCompany,AwardNO,AwardUrl,Memo,(SELECT LevelName FROM AwardLevel WHERE LevelValue=A.AwardLevel) AS LevelName FROM Award A WHERE User_ID=@User_ID");

            SqlParam[] param = new SqlParam[]
            {
                new SqlParam("@User_ID",UserID)
            };
            return DataFactory.SqlDataBase().GetDataTableBySQL(strSql, param);
        }
        public DataTable GetAward()
        {
            return GetAward(RequestSession.GetSessionUser().UserId.ToString());
        }
        public int AwardDel(string ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("DELETE FROM Award WHERE AwardID=@AwardID AND User_ID=@User_ID");
            SqlParam[] para = new SqlParam[]
            {
                new SqlParam("@AwardID", ID),
                new SqlParam("@User_ID", RequestSession.GetSessionUser().UserId.ToString())
            };
            int num = DataFactory.SqlDataBase().ExecuteBySql(strSql, para);

            StringBuilder strstep = new StringBuilder();
            strstep.Append("SELECT COUNT(*) FROM  Award WHERE User_ID=@User_ID");
            SqlParam[] para2 = new SqlParam[]
            {
                new SqlParam("@User_ID", RequestSession.GetSessionUser().UserId.ToString())
            };
            int count = int.Parse(DataFactory.SqlDataBase().GetDataTableBySQL(strstep, para2).Rows[0][0].ToString());
            if (count > 0)
            {
                ChangeStepT("step7");
            }
            else
            {
                ChangeStepF("step7");
            }

            return num;
        }
        public DataTable GetPaper(string UserID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT PaperID,User_ID,PaperName,PaperLevel,Pubdate,Press,Period,AuthorOrder,PaperIndex,(SELECT LevelName FROM PaperLevel WHERE LevelValue=P.PaperLevel) AS LevelName FROM Paper P WHERE User_ID=@User_ID");

            SqlParam[] param = new SqlParam[]
            {
                new SqlParam("@User_ID",UserID)
            };
            return DataFactory.SqlDataBase().GetDataTableBySQL(strSql, param);
        }
        public DataTable GetPaper()
        {
            return GetPaper(RequestSession.GetSessionUser().UserId.ToString());
        }
        public DataTable GetResearch(string UserID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT ReseID,User_ID,ReseName,ResearchLevel,BeginDate,EndDate,ResaCompany,ReseNO,ReseJob,CreateDate,(SELECT LevelName FROM ResearchLevel WHERE LevelValue=R.ResearchLevel) AS LevelName FROM Research R WHERE User_ID=@User_ID");

            SqlParam[] param = new SqlParam[]
            {
                new SqlParam("@User_ID",UserID)
            };
            return DataFactory.SqlDataBase().GetDataTableBySQL(strSql, param);
        }
        public DataTable GetResearch()
        {
            return GetResearch(RequestSession.GetSessionUser().UserId.ToString());
        }
        public DataTable GetAttachment(string UserID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT AttaID,User_ID,AttachmentType,AttaSize,AttaPath,CreateDate,(SELECT AttaName FROM AttachmentType WHERE AttaType=A.AttachmentType) AS AttaName FROM Attachment A WHERE User_ID=@User_ID");

            SqlParam[] param = new SqlParam[]
            {
                new SqlParam("@User_ID",UserID)
            };
            return DataFactory.SqlDataBase().GetDataTableBySQL(strSql, param);
        }
        public DataTable GetAttachment()
        {
            return GetAttachment(RequestSession.GetSessionUser().UserId.ToString());
        }
        public string GetUploadPath()
        {
            return ConfigHelper.GetAppSettings("UploadFilePath");
        }
        public string GetUserUploadPath()
        {
            return ConfigHelper.GetAppSettings("UploadFilePath") + RequestSession.GetSessionUser().UserAccount.ToString() + "\\";
        }
        public string GetWebPath()
        {
            return ConfigHelper.GetAppSettings("WebFilePath");
        }
        public string GetUserWebPath()
        {
            return ConfigHelper.GetAppSettings("WebFilePath") + RequestSession.GetSessionUser().UserAccount.ToString() + "/";
        }
        public bool IsUserCheck()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM View_UserCheck WHERE User_ID=@User_ID");
            SqlParam[] para = new SqlParam[]
            {
                new SqlParam("@User_ID", RequestSession.GetSessionUser().UserId.ToString())
            };
            DataTable dt = DataFactory.SqlDataBase().GetDataTableBySQL(strSql, para);
            if (DataTableHelper.IsExistRows(dt))
            {
                if (dt.Rows[0]["IsCheck"].ToString().Equals("1") || dt.Rows[0]["islock"].ToString().Equals("1"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public DataTable GetNewsListPage(StringBuilder SqlWhere, IList<SqlParam> IList_param, int pageIndex, int pageSize, ref int count)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT *  ");
            strSql.Append("FROM News ");
            strSql.Append("WHERE istop=0   ");
            strSql.Append(SqlWhere);
            return DataFactory.SqlDataBase().GetPageList(strSql.ToString(), IList_param.ToArray<SqlParam>(), "pubdate", "DESC", pageIndex, pageSize, ref count);
        }
        public int PaperDel(string ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("DELETE FROM Paper WHERE PaperID=@PaperID AND User_ID=@User_ID");
            SqlParam[] para = new SqlParam[]
            {
                new SqlParam("@PaperID", ID),
                new SqlParam("@User_ID", RequestSession.GetSessionUser().UserId.ToString())
            };
            int num = DataFactory.SqlDataBase().ExecuteBySql(strSql, para);

            StringBuilder strstep = new StringBuilder();
            strstep.Append("SELECT COUNT(*) FROM Paper WHERE User_ID=@User_ID");
            SqlParam[] para2 = new SqlParam[]
            {
                new SqlParam("@User_ID", RequestSession.GetSessionUser().UserId.ToString())
            };
            int count = int.Parse(DataFactory.SqlDataBase().GetDataTableBySQL(strstep, para2).Rows[0][0].ToString());
            if (count > 0)
            {
                ChangeStepT("step6");
            }
            else
            {
                ChangeStepF("step6");
            }

            return num;
        }
        public int ResearchDel(string ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("DELETE FROM Research WHERE ReseID=@ReseID AND User_ID=@User_ID");
            SqlParam[] para = new SqlParam[]
            {
                new SqlParam("@ReseID", ID),
                new SqlParam("@User_ID", RequestSession.GetSessionUser().UserId.ToString())
            };
            int num = DataFactory.SqlDataBase().ExecuteBySql(strSql, para);

            StringBuilder strstep = new StringBuilder();
            strstep.Append("SELECT COUNT(*) FROM Research WHERE User_ID=@User_ID");
            SqlParam[] para2 = new SqlParam[]
            {
                new SqlParam("@User_ID", RequestSession.GetSessionUser().UserId.ToString())
            };
            int count = int.Parse(DataFactory.SqlDataBase().GetDataTableBySQL(strstep, para2).Rows[0][0].ToString());
            if (count > 0)
            {
                ChangeStepT("step6");
            }
            else
            {
                ChangeStepF("step6");
            }

            return num;
        }
        public int AttachmentDel(string ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("DELETE FROM Attachment WHERE AttaID=@AttaID AND User_ID=@User_ID");
            SqlParam[] para = new SqlParam[]
            {
                new SqlParam("@AttaID", ID),
                new SqlParam("@User_ID", RequestSession.GetSessionUser().UserId.ToString())
            };
            int num = DataFactory.SqlDataBase().ExecuteBySql(strSql, para);

            StringBuilder strstep = new StringBuilder();
            strstep.Append("SELECT COUNT(*) FROM Attachment WHERE User_ID=@User_ID");
            SqlParam[] para2 = new SqlParam[]
            {
                new SqlParam("@User_ID", RequestSession.GetSessionUser().UserId.ToString())
            };
            int count = int.Parse(DataFactory.SqlDataBase().GetDataTableBySQL(strstep, para2).Rows[0][0].ToString());
            if (count > 0)
            {
                ChangeStepT("step8");
            }
            else
            {
                ChangeStepF("step8");
            }
            return num;
        }
        public int FamilyDel(string ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("DELETE FROM Family WHERE ID=@ID AND User_ID=@User_ID");
            SqlParam[] para = new SqlParam[]
            {
                new SqlParam("@ID", ID),
                new SqlParam("@User_ID", RequestSession.GetSessionUser().UserId.ToString())
            };
            int num = DataFactory.SqlDataBase().ExecuteBySql(strSql, para);

            StringBuilder strstep = new StringBuilder();
            strstep.Append("SELECT COUNT(*) FROM Family WHERE User_ID=@User_ID");
            SqlParam[] para2 = new SqlParam[]
            {
                new SqlParam("@User_ID", RequestSession.GetSessionUser().UserId.ToString())
            };
            int count = int.Parse(DataFactory.SqlDataBase().GetDataTableBySQL(strstep, para2).Rows[0][0].ToString());
            if (count > 0)
            {
                ChangeStepT("step4");
            }
            else
            {
                ChangeStepF("step4");
            }
            return num;
        }
        public int EducationDel(string ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("DELETE FROM Education WHERE EduID=@EduID AND User_ID=@User_ID");
            SqlParam[] para = new SqlParam[]
            {
                new SqlParam("@EduID", ID),
                new SqlParam("@User_ID", RequestSession.GetSessionUser().UserId.ToString())
            };
            int num = DataFactory.SqlDataBase().ExecuteBySql(strSql, para);

            StringBuilder strstep = new StringBuilder();
            strstep.Append("SELECT COUNT(*) FROM Education WHERE User_ID=@User_ID AND isfirst=1");
            SqlParam[] para2 = new SqlParam[]
            {
                new SqlParam("@User_ID", RequestSession.GetSessionUser().UserId.ToString())
            };
            int count = int.Parse(DataFactory.SqlDataBase().GetDataTableBySQL(strstep, para2).Rows[0][0].ToString());
            if (count > 0)
            {
                ChangeStepT("step2");
            }
            else
            {
                ChangeStepF("step2");
            }
            return num;
        }
        public DataTable GetUserInfo(string UserID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT User_ID,name,loginName,email,mobile,");
            strSql.Append("(SELECT sexText FROM Sex WHERE sexvalue=P.sex) AS SEXNAME,");
            strSql.Append("birthday,headphoto,islock,experience,native,gradDate,height,weight,z00hrsyd,healthy,Address,zipcode,");
            strSql.Append("(SELECT name FROM gradtype WHERE value=P.gradtype) AS gradtypeNAME,");
            strSql.Append("(SELECT name FROM Nation WHERE value=P.Nation) AS NATIONNAME,");
            strSql.Append("(SELECT name FROM marriage WHERE value=P.marriage) AS marriageNAME,");
            strSql.Append("(SELECT CityName FROM Area_data WHERE CityID=P.z00hrsyd) AS z00hrsydNAME,");
            strSql.Append("(SELECT name FROM polity WHERE value=P.polity) AS polityNAME ");
            strSql.Append("FROM Personinfo P ");
            strSql.Append("WHERE P.User_ID=@User_ID");

            SqlParam[] param = new SqlParam[]
            {
                new SqlParam("@User_ID",UserID)
            };
            return DataFactory.SqlDataBase().GetDataTableBySQL(strSql, param);
        }
        public DataTable GetUserInfo()
        {
            return GetUserInfo(RequestSession.GetSessionUser().UserId.ToString());
        }
        public DataTable GetLanguage(string UserID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT ID,User_ID,(SELECT LangName FROM LanguageName WHERE LangValue=L.LanguageName) AS LangName,(SELECT LevelName FROM LanguageLevel WHERE LevelValue=L.LanguageLevel) AS LangLevel,GetDate,Certbody,CertNO,CertResults,CertSize,CertFilePath,Memo FROM Language L WHERE User_ID=@User_ID");

            SqlParam[] param = new SqlParam[]
            {
                new SqlParam("@User_ID",UserID)
            };
            return DataFactory.SqlDataBase().GetDataTableBySQL(strSql, param);
        }
        public DataTable GetLanguage()
        {
            return GetLanguage(RequestSession.GetSessionUser().UserId.ToString());
        }
        public DataTable GetComputerLevel(string UserID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT ID,User_ID,CertName,GetDate,CertBody,Grade,CertLevel,CertNO,CertSize,CertPath,Memo,Createdate  FROM ComputerLevel WHERE User_ID=@User_ID");

            SqlParam[] param = new SqlParam[]
            {
                new SqlParam("@User_ID",UserID)
            };
            return DataFactory.SqlDataBase().GetDataTableBySQL(strSql, param);
        }
        public DataTable GetComputerLevel()
        {
            return GetComputerLevel(RequestSession.GetSessionUser().UserId.ToString());
        }
        public DataTable GetCertOther(string UserID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT ID,User_ID,CertName,GetDate,CertBody,CertLevel,Grades,CertNO,CertSize,CertPath,Memo,Createdate  FROM CertOther WHERE User_ID=@User_ID");

            SqlParam[] param = new SqlParam[]
            {
                new SqlParam("@User_ID",UserID)
            };
            return DataFactory.SqlDataBase().GetDataTableBySQL(strSql, param);
        }
        public DataTable GetCertOther()
        {
            return GetCertOther(RequestSession.GetSessionUser().UserId.ToString());
        }
        public DataTable GetCompany()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT *  FROM Company WHERE DeleteMark=0 ORDER BY CreateDate ASC ");

            return DataFactory.SqlDataBase().GetDataTableBySQL(strSql);
        }
        public DataTable GetSpecialty(string CompyID, string stand)
        {

            if (!string.IsNullOrEmpty(stand))
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("SELECT *,convert(char(19),beginDate,120) AS STARTDATE,convert(char(19),enddate,120) AS OVERDATE,(SELECT companyName FROM Company WHERE companyID=S.CompanyID) AS companyName,(SELECT name FROM EduBack WHERE value=S.EduBack) AS EduBackName FROM Specialty S WHERE GETDATE() BETWEEN beginDate AND endDate AND EduBack >= @stand AND CompanyID=@CompyID");

                SqlParam[] param = new SqlParam[]
            {
                new SqlParam("@stand",stand),
                new SqlParam("@CompyID",CompyID)
            };
                return DataFactory.SqlDataBase().GetDataTableBySQL(strSql, param);
            }
            else
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("SELECT *,convert(char(19),beginDate,120) AS STARTDATE,convert(char(19),enddate,120) AS OVERDATE,(SELECT companyName FROM Company WHERE companyID=S.CompanyID) AS companyName,(SELECT name FROM EduBack WHERE value=S.EduBack) AS EduBackName FROM Specialty S WHERE CompanyID=@CompyID");

                SqlParam[] param = new SqlParam[]
            {
                new SqlParam("@CompyID",CompyID)
            };
                return DataFactory.SqlDataBase().GetDataTableBySQL(strSql, param);
            }

        }
        public DataTable GetSpecialty(string CompyID)
        {
            return this.GetSpecialty(CompyID, "");
        }
        public DataTable GetUserSpecialty(string CompyID)
        {
            DataTable dt = this.getFirstEducation();
            if (DataTableHelper.IsExistRows(dt))
            {
                string eduBack = dt.Rows[0]["eduBack"].ToString();
                return this.GetSpecialty(CompyID, eduBack);
            }
            else
            {
                return this.GetSpecialty(CompyID);
            }
            //StringBuilder strSql = new StringBuilder();
            //strSql.Append("SELECT *,convert(char(19),beginDate,120) AS STARTDATE,convert(char(19),enddate,120) AS OVERDATE,(SELECT companyName FROM Company WHERE companyID=S.CompanyID) AS companyName FROM Specialty S WHERE GETDATE() BETWEEN beginDate AND endDate AND specType <= @stand AND CompanyID=@CompyID");

            //SqlParam[] param = new SqlParam[]
            //{
            //    new SqlParam("@stand",stand),
            //    new SqlParam("@CompyID",CompyID)
            //};
            //return DataFactory.SqlDataBase().GetDataTableBySQL(strSql, param);
        }
        public bool IsSpecialtyNormal(string specID)
        {
            //SELECT * FROM Company C,Specialty S WHERE C.companyID=S.CompanyID AND C.DeleteMark=0 AND S.isDelete=0 AND GETDATE() BETWEEN BeginDate AND EndDate AND S.ID='1c7d3dea-2137-4d35-b148-e5969e863a15'
            StringBuilder strSql = new StringBuilder();
            // strSql.Append("SELECT C.companyID FROM Company C,Specialty S WHERE C.companyID=S.CompanyID AND C.DeleteMark=0 AND S.isDelete=0 AND GETDATE() BETWEEN BeginDate AND EndDate AND S.ID=@specID");
            strSql.Append("SELECT *,convert(char(19),beginDate,120) AS STARTDATE,convert(char(19),enddate,120) AS OVERDATE FROM Specialty S WHERE GETDATE() BETWEEN beginDate AND endDate AND EduBack >= @EduBack AND ID=@specID");

            string EduBack = this.getFirstEducation().Rows[0]["eduBack"].ToString();
            SqlParam[] param = new SqlParam[]
            {
                new SqlParam("@EduBack",EduBack),
                new SqlParam("@specID",specID)
            };
            DataTable dt = DataFactory.SqlDataBase().GetDataTableBySQL(strSql, param);
            return DataTableHelper.IsExistRows(dt);
        }
        public int LanguageDel(string ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("DELETE FROM Language WHERE ID=@ID AND User_ID=@User_ID");
            SqlParam[] para = new SqlParam[]
            {
                new SqlParam("@ID", ID),
                new SqlParam("@User_ID", RequestSession.GetSessionUser().UserId.ToString())
            };
            int num = DataFactory.SqlDataBase().ExecuteBySql(strSql, para);

            StringBuilder strstep = new StringBuilder();
            strstep.Append("SELECT COUNT(*) FROM Language WHERE User_ID=@User_ID");
            SqlParam[] para2 = new SqlParam[]
            {
                new SqlParam("@User_ID", RequestSession.GetSessionUser().UserId.ToString())
            };
            int count = int.Parse(DataFactory.SqlDataBase().GetDataTableBySQL(strstep, para2).Rows[0][0].ToString());
            if (count > 0)
            {
                ChangeStepT("step3");
            }
            else
            {
                ChangeStepF("step3");
            }
            return num;
        }
        public int ComputerDel(string ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("DELETE FROM ComputerLevel WHERE ID=@ID AND User_ID=@User_ID");
            SqlParam[] para = new SqlParam[]
            {
                new SqlParam("@ID", ID),
                new SqlParam("@User_ID", RequestSession.GetSessionUser().UserId.ToString())
            };
            int num = DataFactory.SqlDataBase().ExecuteBySql(strSql, para);

            StringBuilder strstep = new StringBuilder();
            strstep.Append("SELECT COUNT(*) FROM ComputerLevel WHERE User_ID=@User_ID");
            SqlParam[] para2 = new SqlParam[]
            {
                new SqlParam("@User_ID", RequestSession.GetSessionUser().UserId.ToString())
            };
            int count = int.Parse(DataFactory.SqlDataBase().GetDataTableBySQL(strstep, para2).Rows[0][0].ToString());
            if (count > 0)
            {
                ChangeStepT("step3");
            }
            else
            {
                ChangeStepF("step3");
            }
            return num;
        }
        public int CertOtherDel(string ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("DELETE FROM CertOther WHERE ID=@ID AND User_ID=@User_ID");
            SqlParam[] para = new SqlParam[]
            {
                new SqlParam("@ID", ID),
                new SqlParam("@User_ID", RequestSession.GetSessionUser().UserId.ToString())
            };
            int num = DataFactory.SqlDataBase().ExecuteBySql(strSql, para);

            StringBuilder strstep = new StringBuilder();
            strstep.Append("SELECT COUNT(*) FROM CertOther WHERE User_ID=@User_ID");
            SqlParam[] para2 = new SqlParam[]
            {
                new SqlParam("@User_ID", RequestSession.GetSessionUser().UserId.ToString())
            };
            int count = int.Parse(DataFactory.SqlDataBase().GetDataTableBySQL(strstep, para2).Rows[0][0].ToString());
            if (count > 0)
            {
                ChangeStepT("step3");
            }
            else
            {
                ChangeStepF("step3");
            }
            return num;
        }
        //注册用户列表
        public DataTable GetUserListPage(StringBuilder SqlWhere, IList<SqlParam> IList_param, int pageIndex, int pageSize, ref int count)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT *  ");
            strSql.Append("FROM View_UserList U");
            strSql.Append(SqlWhere);
            return DataFactory.SqlDataBase().GetPageList(strSql.ToString(), IList_param.ToArray<SqlParam>(), "createdate", "DESC", pageIndex, pageSize, ref count);
        }
        public DataTable GetUserID(StringBuilder SqlWhere, IList<SqlParam> IList_param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT USER_ID AS ID  FROM PERSONINFO ");
            strSql.Append(SqlWhere);

            DataTable dt = DataFactory.SqlDataBase().GetDataTableBySQL(strSql, IList_param.ToArray<SqlParam>());
            return dt;

        }
        public DataTable GetSpecialtyListPage(StringBuilder SqlWhere, IList<SqlParam> IList_param, int pageIndex, int pageSize, ref int count)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT *  ");
            strSql.Append("FROM View_Specialty ");
            strSql.Append(SqlWhere);
            return DataFactory.SqlDataBase().GetPageList(strSql.ToString(), IList_param.ToArray<SqlParam>(), "companyName", "DESC", pageIndex, pageSize, ref count);
        }
        public int ChangeStatus(string User_ID, string status)
        {
            StringBuilder strSql = new StringBuilder();
            User_ID = "'" + User_ID.Replace(",", "','") + "'";
            strSql.AppendFormat("UPDATE View_UserStatus SET status=@status,islock=1 WHERE User_ID IN ({0}) ", User_ID);
            SqlParam[] para = new SqlParam[]
			{
				new SqlParam("@status", status),
			};
            int num = DataFactory.SqlDataBase().ExecuteBySql(strSql, para);

            return num;
        }
        public int ChangeLock(string User_ID, string locks)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE View_UserStatus SET islock=@locks WHERE User_ID IN (@User_ID) ");
            SqlParam[] para = new SqlParam[]
			{
				new SqlParam("@locks", locks),
                new SqlParam("@User_ID", User_ID),
			};
            int num = DataFactory.SqlDataBase().ExecuteBySql(strSql, para);
            return num;
        }
        private bool ExistFlag(string sysUserID, string User_ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT COUNT(*) FROM UserFlag WHERE sysUserID=@sysUserID AND UserID=@User_ID");
            SqlParam[] para = new SqlParam[]
            {
                new SqlParam("@sysUserID", sysUserID),
                new SqlParam("@User_ID", User_ID)
            };
            int count = int.Parse(DataFactory.SqlDataBase().GetDataTableBySQL(strSql, para).Rows[0][0].ToString());

            return count > 0 ? true : false;
        }
        public int AddFlag(string sysUserID, string User_ID)
        {
            if (string.IsNullOrEmpty(sysUserID))
            {
                return 0;
            }
            if (string.IsNullOrEmpty(User_ID))
            {
                return 0;
            }
            string[] user = User_ID.Split(',');
            StringBuilder strSql = new StringBuilder();
            int num = 0;
            if (user.Length <= 1)
            {
                if (!ExistFlag(sysUserID, User_ID))
                {
                    strSql.Append("INSERT UserFlag (sysUserID,UserID) VALUES (@sysUserID,@UserID)");
                    SqlParam[] para = new SqlParam[]
            {
                new SqlParam("@sysUserID", sysUserID),
                new SqlParam("@UserID", User_ID)
            };
                    num = DataFactory.SqlDataBase().ExecuteBySql(strSql, para);
                }
            }
            else
            {
                strSql.Append("BEGIN\n");
                int j = 0;
                for (int i = 0; i < user.Length; i++)
                {
                    if (!ExistFlag(sysUserID, user[i]))
                    {
                        j = 1;
                        strSql.AppendFormat("INSERT UserFlag (sysUserID,UserID) VALUES ('{0}','{1}');", sysUserID, user[i]);
                    }
                }
                strSql.Append("END;");
                if (j == 0)
                {
                    num = 0;
                }
                else
                {
                    num = DataFactory.SqlDataBase().ExecuteBySql(strSql);

                }
            }
            return num;
        }
        public void UpdateMessage(string MsgID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("  UPDATE UserMessage SET ISRead=1,ReadDate=GETDATE() WHERE ID=@MsgID");
            SqlParam[] para = new SqlParam[]
            {
                new SqlParam("@MsgID", MsgID)
            };
            DataFactory.SqlDataBase().ExecuteBySql(strSql, para);
        }
        public int ReCheckUser(string UserID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE Personinfo SET IsCheck=0 WHERE USER_ID=@USER_ID");

            SqlParam[] param = new SqlParam[]
            {
                new SqlParam("@User_ID",UserID)
            };
            int num = DataFactory.SqlDataBase().ExecuteBySql(strSql, param);

            return num;
        }
        public DataTable GetFlagListPage(StringBuilder SqlWhere, IList<SqlParam> IList_param, int pageIndex, int pageSize, ref int count)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT *  ");
            strSql.Append("FROM View_UserFlag U");
            strSql.Append(SqlWhere);
            return DataFactory.SqlDataBase().GetPageList(strSql.ToString(), IList_param.ToArray<SqlParam>(), "createdate", "DESC", pageIndex, pageSize, ref count);
        }
        public int DeleteFlag(string sysUserID, string User_ID)
        {
            StringBuilder strSql = new StringBuilder();
            User_ID = "'" + User_ID.Replace(",", "','") + "'";
            strSql.AppendFormat("DELETE UserFlag WHERE sysUserID=@sysUserID AND UserID IN ({0}) ", User_ID);
            SqlParam[] para = new SqlParam[]
			{
				new SqlParam("@sysUserID", sysUserID),
			};
            int num = DataFactory.SqlDataBase().ExecuteBySql(strSql, para);

            return num;
        }
        public DataTable GetMessageListPage(StringBuilder SqlWhere, IList<SqlParam> IList_param, int pageIndex, int pageSize, ref int count)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT *  ");
            strSql.Append("FROM View_MessageList U");
            strSql.Append(SqlWhere);
            return DataFactory.SqlDataBase().GetPageList(strSql.ToString(), IList_param.ToArray<SqlParam>(), "CreateDate", "DESC", pageIndex, pageSize, ref count);
        }
        public DataTable GetUserMessageTempListPage(StringBuilder SqlWhere, IList<SqlParam> IList_param, int pageIndex, int pageSize, ref int count)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT *  ");
            strSql.Append("FROM View_UserMessageTemp U");
            strSql.Append(SqlWhere);
            return DataFactory.SqlDataBase().GetPageList(strSql.ToString(), IList_param.ToArray<SqlParam>(), "CreateDate", "DESC", pageIndex, pageSize, ref count);
        }
        public int UpdateSendMessage(string MsgID)
        {
            MsgID = "'" + MsgID.Replace(",", "','") + "'";
            StringBuilder strSql = new StringBuilder();
            strSql.Append("BEGIN\n");
            strSql.AppendFormat("INSERT INTO UserMessage (ID,User_ID,MsgType,MsgTitle,MsgSender,MsgContent,CreateDate) SELECT ID,User_ID,MsgType,MsgTitle,MsgSender,MsgContent,CreateDate FROM UserMessageTemp WHERE ID IN ({0});\n", MsgID);
            strSql.AppendFormat("DELETE UserMessageTemp WHERE ID IN ({0}) ;\n", MsgID);
            strSql.Append("END");
            SqlParam[] para = new SqlParam[]
            {
                new SqlParam("@MsgID", MsgID)
            };
            int num = DataFactory.SqlDataBase().ExecuteBySql(strSql, para)/2;

            //SendMail(MsgID);
            return num;
        }
        private void SendMail(string MsgID)
        {
            StringBuilder sb = new StringBuilder();
            DataTable dtMsg = new DataTable();
            sb.AppendFormat("SELECT * FROM View_MessageMailSend WHERE ID IN ({0})", MsgID);
            dtMsg = DataFactory.SqlDataBase().GetDataTableBySQL(sb);
            if (DataTableHelper.IsExistRows(dtMsg))
            {
                foreach (DataRow dr in dtMsg.Rows)
                {
                    string uName = dr["name"].ToString();
                    string SendDate = dr["SendDate"].ToString();
                    string mail = dr["email"].ToString();

                    string str = string.Format("{0}，你好：<br/<br/> 你在内蒙古电力（集团）有限责任公司招聘平台有新消息，请登录招聘平台查看！<br/<br/>---------------------------------------------------------------------------<br/<br/>[内蒙古电力（集团）有限责任公司]<br/>http://zhaopin.impc.com.cn<br/><br/>{1}", uName, SendDate);
                    string sendresult = SMTPManager.MailSending(mail, "审核通知-内蒙古电力（集团）有限责任公司", str, "");

                    StringBuilder updatesql = new StringBuilder();
                    updatesql.AppendFormat("UPDATE View_MessageMailSend SET MailStates='{0}' WHERE ID='{1}'", "已发送", dr["ID"].ToString());
                    DataFactory.SqlDataBase().ExecuteBySql(updatesql);
                }
            }
        }
        private string userName(string userID)
        {
            string uName = "";
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("SELECT name FROM Personinfo WHERE User_ID='{0}'", userID);
            DataTable dt = DataFactory.SqlDataBase().GetDataTableBySQL(strSql);
            if (DataTableHelper.IsExistRows(dt))
            {
                uName = dt.Rows[0][0].ToString();
            }
            return uName;
        }
        public int CreateSendMessage(string sysUserID, string MessageType, string MsgTitle, string MsgInfo, string User_ID)
        {
            if (string.IsNullOrEmpty(User_ID))
            {
                return 0;
            }
            if (string.IsNullOrEmpty(MsgInfo))
            {
                return 0;
            }
            string[] user = User_ID.Split(',');
            string ID = CommonHelper.GetGuid;

            StringBuilder strSql = new StringBuilder();
            int num = 0;
            if (user.Length <= 1)
            {
                strSql.Append("INSERT INTO UserMessageTemp ");
                strSql.Append("(ID,User_ID,MsgType,MsgTitle,MsgSender,MsgContent) ");
                strSql.Append("VALUES ");
                strSql.Append("(@ID,@User_ID,@MsgType,@MsgTitle,@MsgSender,@MsgContent);");
                MsgInfo = MsgInfo.Replace("{name}", userName(User_ID));
                SqlParam[] para = new SqlParam[]
            {
                new SqlParam("@ID",ID),
                new SqlParam("@User_ID", User_ID),
                new SqlParam("@MsgType", MessageType),
                new SqlParam("@MsgTitle", MsgTitle),
                new SqlParam("@MsgSender",sysUserID),
                new SqlParam("@MsgContent", MsgInfo)
            };
                num = DataFactory.SqlDataBase().ExecuteBySql(strSql, para);

            }
            else
            {
                strSql.Append("BEGIN\n");
                for (int i = 0; i < user.Length; i++)
                {
                    string sMsgInfo = MsgInfo;
                    string sID = CommonHelper.GetGuid;
                    sMsgInfo = MsgInfo.Replace("{name}", userName(user[i]));
                    strSql.Append("INSERT INTO UserMessageTemp ");
                    strSql.Append("(ID,User_ID,MsgType,MsgTitle,MsgSender,MsgContent) ");
                    strSql.Append("VALUES ");
                    strSql.AppendFormat("('{0}','{1}',{2},'{3}','{4}','{5}');\n", sID, user[i], int.Parse(MessageType), MsgTitle, sysUserID, sMsgInfo);
                }
                strSql.Append("END;");

                num = DataFactory.SqlDataBase().ExecuteBySql(strSql);

            }

            return num;
        }
        public DataTable GetFeedbackListPage(StringBuilder SqlWhere, IList<SqlParam> IList_param, int pageIndex, int pageSize, ref int count)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT *  ");
            strSql.Append("FROM View_Feedback U");
            strSql.Append(SqlWhere);
            return DataFactory.SqlDataBase().GetPageList(strSql.ToString(), IList_param.ToArray<SqlParam>(), "CreateDate", "DESC", pageIndex, pageSize, ref count);
        }
        public int ReadFeedback(string FeedID)
        {
            FeedID = "'" + FeedID.Replace(",", "','") + "'";
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("UPDATE Feedback SET ISRead=1 WHERE ID IN ({0})", FeedID);

            int num = DataFactory.SqlDataBase().ExecuteBySql(strSql);
            return num;
        }
        public bool ExistAttaIDCard(string User_ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT COUNT(*) FROM Attachment WHERE User_ID=@User_ID AND AttachmentType=1");
            SqlParam[] para = new SqlParam[]
            {
                new SqlParam("@User_ID", User_ID)
            };
            int count = int.Parse(DataFactory.SqlDataBase().GetDataTableBySQL(strSql, para).Rows[0][0].ToString());

            return count > 0 ? true : false;
        }

        //public string ExportExcelFile(string sqlstr, string tablename)
        //{
        //    string result = string.Empty;
        //    StringBuilder sb=new StringBuilder();
        //    DataTable dt = null;
        //    sb.Append(sqlstr);
        //    try
        //    {
        //        dt = DataFactory.SqlDataBase().GetDataTableBySQL(sb);
        //    }
        //    catch (Exception ex)
        //    {
        //        return "0";
        //    }

        //    if (DataTableHelper.IsExistRows(dt))
        //    {
        //        string FilePath;
         
        //        dt.TableName = tablename;
        //        if (dt.Rows.Count < 2000)
        //        {
        //            FilePath = SRExcel.CreateExcel(dt);
        //        }
        //        else
        //        {
        //            FilePath = SRExcel.DataTableToExcel(dt);
        //        }


        //        result = FilePath;

        //    }
        //    else
        //    {
        //        result = "1";
        //    }

        //    return result;
        //}


    }
}