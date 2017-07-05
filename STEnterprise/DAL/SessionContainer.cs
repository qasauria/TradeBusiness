using System;

namespace STEnterprise.DAL
{
    [Serializable()]
    public class SessionContainer
    {
        public SessionContainer()
        {
            SessionUtility.STSessionContainer = this;
        }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public int UserRole { get; set; }
        public object OBJ_DOC_CLASS { get; set; }
        public object OBJ_Menu_CLASS { get; set; }
        public object OBJ_RPTDOC { get; set; }
        public string ErrorMsg { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public DateTime SystemDate { get; set; }
        public bool IsAuthenticatedMenu { get; set; }
        public string UniqNumber { get; set; }
    }
}