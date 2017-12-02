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

namespace ServiceRequest
{
    public class Staff
    {
        //[PrimaryKey, AutoIncrement]
        //public int ID { get; set; }

        [PrimaryKey]
        public string StaffName { get; set; }

        public string ServicesJson { get; set; }

        //public string Location { get; set; }

        public Staff() { }

        //Previously had location = "The Moon" as a default value for debugging
        public Staff(string staffName, string servicesJson)
        {
            StaffName = staffName;
            ServicesJson = servicesJson;
        }

        public override string ToString()
        {
            return string.Format("[Person: StaffName={0}, ServicesJson={1}]", StaffName, ServicesJson);
        }
    }
}