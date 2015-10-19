
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
using Android.Graphics;
using Android.Media;
using Android.Gestures;
using Android.Views.Animations;
using Java.Lang;
using System.Collections;

namespace DropboxApp
{
	[Activity (Label = "FileActivity")]			
	public class FileActivity : Activity
	{
		
		private ImageButton moreButton;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);



			SetContentView (Resource.Layout.ListLayout);
			InfoAdapter adapter = new InfoAdapter (this, buildList ());
			ListView listview = FindViewById<ListView> (Resource.Id.listView);
			listview.Adapter = adapter;
			View buttonLayout = FindViewById (Resource.Id.plusLayout);
			View uploadLayout = FindViewById (Resource.Id.upLayout);
			var buttonPlus = buttonLayout.FindViewById (Resource.Id.plusButton);
			LinearLayout plusLayout = buttonLayout.FindViewById<LinearLayout> (Resource.Id.plusLayout);
			RelativeLayout upLayout = uploadLayout.FindViewById<RelativeLayout> (Resource.Id.uploadLayoutRelative);
			RelativeLayout totalLayout = FindViewById<RelativeLayout> (Resource.Id.linearList2);
			var rotateRight = AnimationUtils.LoadAnimation (this, Resource.Animation.rotate_centre);
			var rotateLeft = AnimationUtils.LoadAnimation (this, Resource.Animation.rotate_left);
			totalLayout.Click += delegate {	
				totalLayout.Visibility = ViewStates.Gone;	
				plusLayout.Visibility = ViewStates.Visible;
				buttonPlus.StartAnimation (rotateLeft);
				rotateLeft.AnimationEnd += delegate {
					buttonPlus.Rotation = 0;
				};
			};
			buttonPlus.Click += delegate {				
				buttonPlus.StartAnimation (rotateRight);
				rotateRight.AnimationEnd += delegate {
					buttonPlus.Rotation = 45;
					plusLayout.Visibility = ViewStates.Gone;
					totalLayout.Visibility = ViewStates.Visible;
				};
			};

		}

		public List<Info> buildList ()
		{
			return new List<Info> () {
				new Info () { Name = "Camera", ColorId = 1, Image = Resource.Drawable.ic_folder },
				new Info () { Name = "My Work", ColorId = 2, Image = Resource.Drawable.ic_pencil },
				new Info () { Name = "Office", ColorId = 2, Image = Resource.Drawable.ic_folder },
				new Info () { Name = "Screenshot.jpg", ColorId = 3, Image = Resource.Drawable.ic_magnify },
				new Info () { Name = "final.pdf", ColorId = 1, Image = Resource.Drawable.ic_star },
				new Info () { Name = "Password.txt", ColorId = 2, Image = Resource.Drawable.ic_folder },
				new Info () { Name = "Admin", ColorId = 3, Image = Resource.Drawable.ic_upload }
			};
		}
	}
}

