using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Provider;
using Android.Views;
using Android.Widget;

namespace DropboxApp
{
	public class InfoAdapter: BaseAdapter<Info>
	{
		List<Info> infoList;
		Activity activity;

		public InfoAdapter (Activity activity, List<Info> infoList) : base ()
		{
			this.activity = activity;
			this.infoList = infoList;
		}

		public override int Count {
			get { return infoList.Count; }
		}

		public override long GetItemId (int position)
		{
			return position;
		}

		public override Info this [int position] {   
			get { return infoList [position]; } 
		}

		public override View GetView (int position, View convertView, ViewGroup parent)
		{
			View view = convertView; 
			view = activity.LayoutInflater.Inflate (Resource.Layout.FileLayout, null);
			var name = view.FindViewById<TextView> (Resource.Id.infoText);
			name.Text = infoList [position].Name;
			var moreButton = view.FindViewById<ImageButton> (Resource.Id.moreButton);
			moreButton.Click += delegate {
				


			};
			return view;
		}


	}

	public class Info
	{
		public string Name { get; set; }
	}
}

