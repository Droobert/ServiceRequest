using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Android.Support.V7.Widget;
using Newtonsoft.Json;
using DoYourJob;

namespace ServiceRequest
{
    [Activity(Label = "SelectStaffActivity")]
    public class SelectStaffActivity : Activity
    {
        Button addStaffButton;
        List<Staff> staffCollection;

        RecyclerView staffRecyclerView;
        RecyclerView.LayoutManager staffLayoutManager;
        StaffAdapter staffAdapter;
        //FIXME: The staffs are all displaying The Moon as their location in the SelectStaffActivity
        //FIXME: All of the staffs are displaying the same list of services in MainActivity. 
        //Its always the first list of services loaded.
        //FIXME: All of the staffs are failing to load their services from the DB in MainActivity, only the first one is loading successfully.

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //-----------POPULATE HOUSECOLLECTION FROM DB------
            //Create a dbHelper
            //DBHelper dbHelper = new DBHelper();
            HTTPHelper httpHelper = new HTTPHelper();
            //Open connection to DB
            //dbHelper.OpenConn();
            //Download list of staffs from DB
            //While loop to debug
            while (staffCollection == null)
                staffCollection = httpHelper.QueryStaffs();


            //-----------SET UP RECYCLERVIEW AND HELPERS------
            //Create our Staff Adapter
            staffAdapter = new StaffAdapter(staffCollection);
            //Set our view
            SetContentView(Resource.Layout.SelectStaff);
            //Get our RecyclerView layout:
            staffRecyclerView = FindViewById<RecyclerView>(Resource.Id.StaffRecyclerView);
            // Plug the adapter into the RecyclerView:
            staffRecyclerView.SetAdapter(staffAdapter);
            //Set up our layout manager
            staffLayoutManager = new LinearLayoutManager(this);
            //Give the recycler view to our layout manager
            staffRecyclerView.SetLayoutManager(staffLayoutManager);

            //-----------BUTTONS------------------------------
            //Attach the event for individual recyclerview items being clicked:
            staffAdapter.ItemClick += OnItemClick;
            //Find the button to select a staff
            addStaffButton = FindViewById<Button>(Resource.Id.AddStaffButton);
            //Hook up the EDP
            addStaffButton.Click += (sender, e) =>
            {
                //Bring up the Add Staff menu
                var intent = new Intent(this, typeof(AddStaffActivity));
                StartActivity(intent);
            };

            void OnItemClick(object sender, int position)
            {
                //When a staff is clicked, it should either be highlighted with a radio button syle,
                //or immediately chosen as the staff and we return back to main activity with currentUser
                //For now lets go with immediately chosen as currentUser
                //STYLE POINTS: I kinda want a radio buttons feel for this.
                var mainIntent = new Intent(this, typeof(MainActivity));
                mainIntent.PutExtra("currentUser", JsonConvert.SerializeObject(staffCollection[position]));
                // mainIntent.PutExtra("staffCollection", JsonConvert.SerializeObject(staffCollection));

                StartActivity(mainIntent);
            }
        }
    }
}