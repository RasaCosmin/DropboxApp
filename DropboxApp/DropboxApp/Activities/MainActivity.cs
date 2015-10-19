using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using Android.Text;
using Java.Util.Zip;
using Android.Gestures;

namespace DropboxApp
{
	[Activity (Label = "DropboxApp", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : TabActivity
	{
		private ImageButton overflowButton;
		private TextView textul;
		const int DATE_DIALOG_ID = 0;
		float lastX;
		float newX;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.Main);

			CreateTab (typeof(FileActivity), "files", "FILES");
			CreateTab (typeof(PhotoActivity), "photo", "PHOTOS");
			CreateTab (typeof(FavoritesActivity), "favorites", "FAVORITES");
			CreateTab (typeof(SettingsActivity), "settings", "SETTINGS");
			overflowButton = this.Window.FindViewById<ImageButton> (Resource.Id.allButton);
			textul = this.Window.FindViewById<TextView> (Resource.Id.textView1);
			var myView = FindViewById (Resource.Id.viewMain);
			overflowButton.Click += new EventHandler (OnOverflowClick);

				

		
		}

		private void CreateTab (Type activityType, string tag, string label)
		{
			var intent = new Intent (this, activityType);
			intent.AddFlags (ActivityFlags.NewTask);
			var spec = TabHost.NewTabSpec (tag);
			spec.SetIndicator (label);
			spec.SetContent (intent);
			TabHost.AddTab (spec);
		}

		public void OnOverflowClick (object sender, EventArgs args)
		{
			PopupMenu popupMenu = new PopupMenu (this, this.overflowButton);
			popupMenu.MenuInflater.Inflate (Resource.Menu.mainmenu, popupMenu.Menu);
			popupMenu.Show ();
		}

		public void switchTab (Boolean direction)
		{
			if (direction) {
				if (TabHost.CurrentTab == 0)
					TabHost.CurrentTab = TabHost.TabWidget.TabCount - 1;
				else
					TabHost.CurrentTab = TabHost.CurrentTab - 1;
			} else {
				if (TabHost.CurrentTab != TabHost.TabWidget.TabCount - 1)
					TabHost.CurrentTab = TabHost.CurrentTab + 1;
				else
					TabHost.CurrentTab = 0;
			}
		}

		public override bool OnTouchEvent (MotionEvent e)
		{	
			
			switch (e.Action) {
			case MotionEventActions.Down:
				{
					lastX = e.GetX ();
					break;
				}
			case MotionEventActions.Up:
			case MotionEventActions.Cancel:
				{
					float X = e.GetX ();
					if (lastX < X) {
						switchTab (false);
					}
					if (lastX > X) {
						switchTab (true);
					}
					break;
				}

			}
			return false;
		}

	}
}