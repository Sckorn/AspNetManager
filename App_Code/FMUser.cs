using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for FMUser
/// </summary>
/// 
namespace FootballManager
{
    public class FMUser
    {
        public Guid UserID = Guid.Empty;

        public string UserName = "";
        public string Password = "";

        public string Email = "";
        public DateTime CreationDate = DateTime.Now;
        public DateTime LastLogInDate = DateTime.MinValue;
        public DateTime LastPasswordChangeTime = DateTime.MinValue;
        public string PasswordQuestion = "";
        public string PasswordAnswer = "";

        public bool IsApproved = false;
    }
}