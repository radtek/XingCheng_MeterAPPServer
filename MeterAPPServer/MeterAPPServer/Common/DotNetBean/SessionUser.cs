namespace Common.DotNetBean
{
    public class SessionUser
    {
        public object UserId
        {
            get;
            set;
        }

        public object UserAccount
        {
            get;
            set;
        }

        public object UserPwd
        {
            get;
            set;
        }

        public object UserName
        {
            get;
            set;
        }

        public object AreaID
        {
            get;
            set;
        }

        public object OrganizationID
        {
            get;
            set;
        }
        public object Organization_Fax
        {
            get;
            set;
        }

        public SessionUser(object userId, object userAccount, object userPwd, object userName, object areaID)
        {
            this.UserId = userId;
            this.UserAccount = userAccount;
            this.UserName = userName;
            this.UserPwd = userPwd;
            this.AreaID = areaID;
            this.OrganizationID = OrganizationID;
        }

        public SessionUser()
        {
        }
    }
}