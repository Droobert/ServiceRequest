using System;
using System.Collections.Generic;
using Android.Views;
using Android.Support.V7.Widget;
using DoYourJob;

namespace ServiceRequest
{
    class StaffAdapter : RecyclerView.Adapter
    {
        //Include an event so our client can act when a user touches individual items
        public event EventHandler<int> ItemClick;
        //include the data source for our Adapter
        public List<Staff> staffCollection;
        //Constructor takes a List of Services
        public StaffAdapter(List<Staff> hCollection)
        {
            staffCollection = hCollection;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            // Inflate the CardView for the photo:
            View itemView = LayoutInflater.From(parent.Context).
                        Inflate(Resource.Layout.StaffCardView, parent, false);

            // Create a ViewHolder to hold view references inside the CardView:
            StaffViewHolder vh = new StaffViewHolder(itemView, OnClick);
            return vh;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            StaffViewHolder vh = holder as StaffViewHolder;

            // Load the Staff Name from the container:
            vh.StaffName.Text = staffCollection[position].StaffName;
            // Load the Service Location from the container:
            //vh.Location.Text = staffCollection[position].Location;
        }

        public override int ItemCount
        {
            get
            {
                if (staffCollection != null)
                    return staffCollection.Count;
                else
                    return 0;
            }
        }

        private void OnClick(int position)
        {
            //if (ItemClick != null)
            //{
            ItemClick?.Invoke(this, position);
            //}
        }
    }
}