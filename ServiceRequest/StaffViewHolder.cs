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
using Android.Support.V7.Widget;
using DoYourJob;

namespace ServiceRequest
{
    public class StaffViewHolder:RecyclerView.ViewHolder
    {
        //public ImageView Image { get; private set; }
        //public TextView Caption { get; private set; }
        public TextView StaffName { get; private set; }
        //public TextView Description { get; private set; }
        public TextView Location { get; private set; }

        public StaffViewHolder(View itemView, Action<int> listener) : base(itemView)
        {
            // Locate and cache view references:
            //Image = itemView.FindViewById<ImageView>(Resource.Id.imageView);
            //Caption = itemView.FindViewById<TextView>(Resource.Id.textView);
            StaffName = itemView.FindViewById<TextView>(Resource.Id.staffNameView);
            //Location = itemView.FindViewById<TextView>(Resource.Id.staffLocationView);

            itemView.Click += (sender, e) => listener(Position);
        }
    }
}