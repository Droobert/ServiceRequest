using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using Android.Support.V7.Widget;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using Android.Database.Sqlite;
using DoYourJob;

namespace ServiceRequest
{
    [Activity(Label = "ServiceRequest", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        RecyclerView serviceRecyclerView;
        RecyclerView.LayoutManager serviceLayoutManager;
        ServiceAdapter serviceAdapter;

        public static List<Service> serviceCollection; //Replaced ServiceCollection type with List<Service>
        public static Staff currentUser;

        //IOHelper ioHelper;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            
            //-----------LOAD SERVICE LIST FROM DB-----------
            //Get the dbPath
            var dbPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            dbPath = Path.Combine(dbPath, "service-request.db3");

            SQLiteDatabase mydatabase = OpenOrCreateDatabase(dbPath, FileCreationMode.Private, null);
            //Create dbHelper
            DBHelper dbHelper = new DBHelper();
            //Open dbConnection
            dbHelper.OpenConn();
            //Create the table if it does not exist
            //dbHelper.DropStaffTable();
            dbHelper.CreateStaffTable();
            
            
            //check to see if Intent has an extra for my resources
            if (Intent.HasExtra("currentUser"))
            {
                currentUser = JsonConvert.DeserializeObject<Staff>(Intent.GetStringExtra("currentUser"));
                //Update DB by adding Staff to the table, replacing the previous version if it exists
                dbHelper.AddStaff(currentUser);
            }
            //else if(I don't have a staff)
            if(currentUser == null)
            {
                //go to the screen to select a staff...
                //start the SelectStaffActivity
                var intent = new Intent(this, typeof(SelectStaffActivity));
                StartActivity(intent);
            }

            //serviceCollection = JsonConvert.DeserializeObject<List<Service>>(currentUser.ServicesJson);
            if (currentUser != null)
                try
                {
                    serviceCollection = dbHelper.GetMyServices(currentUser.StaffName);
                }
                catch
                {
                    Toast.MakeText(this, "Failed to load services from DB.", ToastLength.Long).Show();
                    if (serviceCollection == null)
                        serviceCollection = new List<Service>();
                }

            //-----------UPDATE THE DB AFTER DELETING A CHORE-------
            //check Intent for an updated serviceCollection from DeleteServiceButton in ServiceInfoActivity
            if (Intent.HasExtra("shortenedServiceList"))
            {
                //DONE: The DB stuff should be done here instead of in the ServiceInfoActivity
                serviceCollection = JsonConvert.DeserializeObject<List<Service>>(Intent.GetStringExtra("shortenedServiceList"));
                dbHelper.AddStaff(currentUser.StaffName, JsonConvert.SerializeObject(serviceCollection));
            }


            //-----------ADD NEW CHORE TO FILE-------
            ////Add a new element from our AddServiceActivity to the file
            //if (Intent.HasExtra("NewService"))
            //{
            //    serviceCollection.Add(JsonConvert.DeserializeObject<Service>(Intent.GetStringExtra("NewService")));

            //    using (var streamWriter = new StreamWriter(filename, false))
            //    {
            //        streamWriter.Write(JsonConvert.SerializeObject(serviceCollection));
            //    }
            //    //ioHelper.WriteToJsonFile<List<Service>>(this, serviceCollection);
            //}

            //-----------ADD NEW CHORE TO DB-------
            if (Intent.HasExtra("NewService"))
            {
                serviceCollection.Add(JsonConvert.DeserializeObject<Service>(Intent.GetStringExtra("NewService")));
                //Update DB by adding Staff to the table, replacing the previous version if it exists
                dbHelper.AddStaff(currentUser.StaffName, JsonConvert.SerializeObject(serviceCollection));
            }

            //-----------SET UP RECYCLERVIEW AND HELPERS------
            // Instantiate the adapter and pass in its data source:
            serviceAdapter = new ServiceAdapter(serviceCollection);

            // Set our view from the "main" layout resource:
            SetContentView(Resource.Layout.Main);

            // Get our RecyclerView layout:
            serviceRecyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView);

            //Attach the event for individual items being clicked:
            serviceAdapter.ItemClick += OnItemClick;
            // Plug the adapter into the RecyclerView:
            serviceRecyclerView.SetAdapter(serviceAdapter);

            serviceLayoutManager = new LinearLayoutManager(this);
            serviceRecyclerView.SetLayoutManager(serviceLayoutManager);
            
            //-----------BUTTONS------------------------------
            Button addServiceButton = FindViewById<Button>(Resource.Id.AddServiceButton);
            Button selectStaffButton = FindViewById<Button>(Resource.Id.SelectStaffButton);

            selectStaffButton.Click += (sender, e) =>
            {
                //FIXME: We should probably pass the list of staffs or the dbConnection here instead of opening the dbConnection twice...
                //Bring up the Select Group menu
                var intent = new Intent(this, typeof(SelectStaffActivity));
                StartActivity(intent);
            };

            addServiceButton.Click += (sender, e) =>
            {
                //Bring up the Add Service menu
                var intent = new Intent(this, typeof(AddServiceActivity));
                StartActivity(intent);
            };

            void OnItemClick(object sender, int position)
            {
                var serviceInfoIntent = new Intent(this, typeof(ServiceInfoActivity));
                serviceInfoIntent.PutExtra("index", position);
                serviceInfoIntent.PutExtra("collection", JsonConvert.SerializeObject(serviceCollection));

                StartActivity(serviceInfoIntent);
            }
        }
    }
}

