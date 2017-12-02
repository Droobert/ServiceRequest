using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using System.IO;
using Newtonsoft.Json;

namespace ServiceRequest
{
    class DBHelper
    {
        public SQLiteConnection dbConn;

        //public DBHelper()
        //{
        //    dbConn = new SQLiteConnection("foofoo");
        //}

        public void OpenConn()
        {
            var path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            path = Path.Combine(path, "do-your-job.db3");
            dbConn = new SQLiteConnection(path);
        }

        //FIXME: We should have a function to drop a table if its borked
        //FIXME: This should be a generic DropTable(Type T) function
        public void DropStaffTable()
        {
            dbConn.DropTable<Staff>();
        }

        //FIXME: This should be a generic CreateTable(Type T) function
        public void CreateStaffTable()
        {
            //Executes a "Create table if not exists" command on the DB
            dbConn.CreateTable<Staff>();
        }

        public void AddStaff(string name, string requests)
        {
            Staff staff = new Staff(name, requests);
            //if(db does not already have the staff in it...)
            dbConn.InsertOrReplace(staff);
        }

        public void AddStaff(Staff staff)
        {
            dbConn.InsertOrReplace(staff);
        }

        public List<Staff> QueryStaffs()
        {
            return dbConn.Query<Staff>("SELECT * FROM Staff");
        }

        public List<Service> GetMyServices(string staffName)
        {
            return JsonConvert.DeserializeObject<List<Service>>(dbConn.ExecuteScalar<string>("SELECT ServicesJson FROM Staff WHERE StaffName = ?", staffName));
        }
    }
}