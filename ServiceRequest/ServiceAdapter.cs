using System;
using System.Collections.Generic;
using Android.Views;
using Android.Support.V7.Widget;
using DoYourJob;

namespace ServiceRequest
{
    public class ServiceAdapter : RecyclerView.Adapter
    {
        //Include an event so our client can act when a user touches individual items
        public event EventHandler<int> ItemClick;
        //include the data source for our Adapter
        public List<Service> serviceCollection;
        //Constructor takes a List of Services
        public ServiceAdapter(List<Service> cCollection)
        {
           serviceCollection = cCollection;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            // Inflate the CardView for the photo:
            View itemView = LayoutInflater.From(parent.Context).
                        Inflate(Resource.Layout.ServiceCardView, parent, false);

            // Create a ViewHolder to hold view references inside the CardView:
            ServiceViewHolder vh = new ServiceViewHolder(itemView, OnClick);
            return vh;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            ServiceViewHolder vh = holder as ServiceViewHolder;

            // Load the Service Name from the container:
            vh.Name.Text = serviceCollection[position].name;
            // Load the Service Date from the container:
            vh.Date.Text = serviceCollection[position].date;

            // Load the photo image resource from the photo album:
            //   vh.Image.SetImageResource(mPhotoAlbum[position].PhotoID);
            // Load the photo caption from the photo album:
            //  vh.Caption.Text = mPhotoAlbum[position].Caption;
        }

        public override int ItemCount
        {
            get {
                if (serviceCollection != null)
                    return serviceCollection.Count;
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


//public class PhotoAlbumAdapter : RecyclerView.Adapter
//{
//    public PhotoAlbum mPhotoAlbum;

//    public PhotoAlbumAdapter(PhotoAlbum photoAlbum)
//    {
//        mPhotoAlbum = photoAlbum;
//    }
//    ...
//}