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
using Newtonsoft.Json;
using DoYourJob;

namespace ServiceRequest
{
    [Activity(Label = "ServiceInfoActivity")]
    public class ServiceInfoActivity : Activity
    {
        Button setReminderButton;
        Button deleteServiceButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.ServiceInfo);
            // Create your application here
            //Pull our serviceCollection in from the Intent extras
            List<Service> serviceCollection = JsonConvert.DeserializeObject<List<Service>>(Intent.GetStringExtra("collection"));

            int index = Intent.GetIntExtra("index", -1);

            if (index < 0)
                return;

            FindViewById<TextView>(Resource.Id.nameTextDisplay).Text = serviceCollection[index].name;
            FindViewById<TextView>(Resource.Id.dateTextDisplay).Text = serviceCollection[index].date;
            FindViewById<TextView>(Resource.Id.locationTextDisplay).Text = serviceCollection[index].location;
            FindViewById<TextView>(Resource.Id.detailsTextDisplay).Text = serviceCollection[index].details;

            setReminderButton = FindViewById<Button>(Resource.Id.setReminderButton);
            deleteServiceButton = FindViewById<Button>(Resource.Id.deleteServiceButton);

            setReminderButton.Click += (sender, e) =>
            {
                Remind(DateTime.Parse(serviceCollection[index].date), serviceCollection[index].name, serviceCollection[index].details);
            };
            deleteServiceButton.Click += (sender, e) =>
            {
                serviceCollection.Remove(serviceCollection[index]);
                //FIXME: We should probably not be opening the DB connection multiple times, but for now this is what we will do
                //DBHelper dbHelper = new DBHelper();
                //dbHelper.OpenConn();
                //dbHelper.AddStaff("Dudes", JsonConvert.SerializeObject(serviceCollection));


                var mainActivity = new Intent(this, typeof(MainActivity));
                mainActivity.PutExtra("shortenedServiceList", JsonConvert.SerializeObject(serviceCollection));
                StartActivity(mainActivity);
            };
        }

        public void Remind(DateTime dateTime, string title, string message)
        {

            Intent alarmIntent = new Intent(this, typeof(AlarmReceiver));
            alarmIntent.PutExtra("message", message);
            alarmIntent.PutExtra("title", title);

            PendingIntent pendingIntent = PendingIntent.GetBroadcast(this, 0, alarmIntent, PendingIntentFlags.UpdateCurrent);
            AlarmManager alarmManager = (AlarmManager)GetSystemService(Context.AlarmService);

            //TODO: For demo set after 5 seconds.
            //TODO: Set alarm to go off at a selected time of a specific day.
            alarmManager.Set(AlarmType.ElapsedRealtime, SystemClock.ElapsedRealtime() + 5 * 1000, pendingIntent);

        }
    }
}