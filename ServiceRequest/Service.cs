using System.Text;
using System;
namespace ServiceRequest
{
    [Serializable]
    public class Service
    {
        //Members of the Service class
        //What qualities does a service have?
        //A name
        public string name { get; set; }
        //A date to be completed by -or- a date to be completed on
        public string date { get; set; }
        //The location for the service to be completed in
        public string location { get; set; }
        //Any details pertaining to the service
        public string details { get; set; }

        //Defauly constructor for our Service objects.
        public Service() { }

        //Initialized constructor for our Service objects. 
        public Service(string serviceName, string serviceDate, string serviceLocation, string serviceDetails)
        {
            name = serviceName;
            date = serviceDate;
            location = serviceLocation;
            details = serviceDetails;
        }

        //Methods of the Service class
    }

}