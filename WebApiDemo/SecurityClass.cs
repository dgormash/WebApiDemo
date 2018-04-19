using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EmployeeDataAccess;

namespace WebApiDemo
{
    public class SecurityClass
    {
        static readonly SQLiteWorker DBaseWorker = new SQLiteWorker();
        public static bool Login(string username, string password)
        {
           return DBaseWorker.FindeUser(username, password);
        }
    }
}