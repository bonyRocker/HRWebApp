using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRWebApp.Utility
{
    public class A_AccountView
    {
        public long AccountId { get; set; }
        public string AccountName { get; set; }
        public string AccountType { get; set; }
        public string AccountCode { get; set; }
        public List<A_AccountView> ChildAccounts { get; set; }
    }
}