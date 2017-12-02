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
    public class ServiceViewHolder : RecyclerView.ViewHolder
    {
        //public ImageView Image { get; private set; }
        //public TextView Caption { get; private set; }
        public TextView Name { get; private set; }
        //public TextView Description { get; private set; }
        public TextView Date { get; private set; }

        public ServiceViewHolder(View itemView, Action<int> listener) : base(itemView)
        {
            // Locate and cache view references:
            //Image = itemView.FindViewById<ImageView>(Resource.Id.imageView);
            //Caption = itemView.FindViewById<TextView>(Resource.Id.textView);
            Name = itemView.FindViewById<TextView>(Resource.Id.serviceNameView);
            Date = itemView.FindViewById<TextView>(Resource.Id.serviceDateView);

            itemView.Click += (sender, e) => listener(Position);
        }
    }
}