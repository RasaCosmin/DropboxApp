using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Provider;
using Android.Views;
using Android.Widget;
using System.Resources;
using Android;
using Android.Text;
using Android.Util;
using System.Collections;
using Android.Renderscripts;
using Android.Bluetooth;
using System.Threading;
using Android.Graphics.Drawables;
using Android.Graphics;
using Android.Views.Animations;
using Android.Content.Res;


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
			var round = view.FindViewById<View> (Resource.Id.viewRound);
			var shape = (GradientDrawable)round.Background;
			var roundIcon = view.FindViewById<View> (Resource.Id.viewRound1);
			roundIcon.SetBackgroundResource (infoList [position].Image);
			var colorName = Android.Graphics.Color.Gray;
			switch (infoList [position].ColorId) {
			case 1:
				colorName = Android.Graphics.Color.DarkOrange;
				break;
			case 2:
				colorName = Android.Graphics.Color.Green;
				break;
			case 3:
				colorName = Android.Graphics.Color.Red;
				break;
			}
			//round.SetBackgroundResource (infoList [1].Image);
			shape.SetColor (colorName);
			name.Text = infoList [position].Name;
			var button = view.FindViewById<ImageButton> (Resource.Id.moreButton);
			var shadow = AnimationUtils.LoadAnimation (activity, Resource.Animation.shadow);
			var shadowMax = AnimationUtils.LoadAnimation (activity, Resource.Animation.shadowMax);
			button.Click += delegate {		
				//View footerLayout = activity.FindViewById (Resource.Id.layoutPop);
				//var relative = activity.FindViewById (Resource.Id.relativeLay);
				//var share = footerLayout.FindViewById<TextView> (Resource.Id.shareButton);
				//var layout = footerLayout.FindViewById (Resource.Id.linearLay);
				//layout.Visibility = ViewStates.Visible;				
				RelativeLayout popupLayout = activity.FindViewById<RelativeLayout> (Resource.Id.linearList);
				popupLayout.Visibility = ViewStates.Visible;
				popupLayout.StartAnimation (shadowMax);

				popupLayout.Click += delegate {
					popupLayout.StartAnimation (shadow);
					shadow.AnimationEnd += delegate {
						popupLayout.Visibility = ViewStates.Gone;
					};
				};
			};
			return view;
		}
	}


}

