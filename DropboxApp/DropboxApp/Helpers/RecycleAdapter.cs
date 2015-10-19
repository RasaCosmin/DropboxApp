
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
using Android.Media;
using Android.Graphics;
using Org.Apache.Http.Protocol;

namespace DropboxApp
{
	public class RecyclerAdapter : RecyclerView.Adapter
	{
		private List<LabTestsUserOnlyResult> _Info;
		private RecyclerView _RecyclerView;

		public RecyclerAdapter (List<LabTestsUserOnlyResult> info, RecyclerView recyclerView)
		{
			_Info = info;
			_RecyclerView = recyclerView;
		}

		public class MyView : RecyclerView.ViewHolder
		{
			public View _MainView { get; set; }

			public TextView _Name { get; set; }

			public TextView _Value { get; set; }

			public ImageView _ButtonImage { get; set; }

			public MyView (View view) : base (view)
			{
				_MainView = view;
			}
		}

		public override RecyclerView.ViewHolder OnCreateViewHolder (ViewGroup parent, int viewType)
		{
			View row = LayoutInflater.From (parent.Context).Inflate (Resource.Layout.PhotoLayout, parent, false);
			TextView txtName = row.FindViewById<TextView> (Resource.Id.labtest_name);
			TextView txtValue = row.FindViewById<TextView> (Resource.Id.labtest_value);
			ImageView buttonRound = row.FindViewById<ImageView> (Resource.Id.roundButton);
			MyView view = new MyView (row) { _Name = txtName, _Value = txtValue, _ButtonImage = buttonRound };
			return view;
		}

		public override void OnBindViewHolder (RecyclerView.ViewHolder holder, int position)
		{
			var myHolder1 = holder as MyView;
			myHolder1._MainView.Click += mMainView_Click;
			myHolder1._Name.TextSize = 25;
			myHolder1._Name.Text = _Info [position].Name;
			myHolder1._Value.TextSize = 90;
			myHolder1._Value.Text = _Info [position].LatestLabTestResult.Value.ToString ();
			myHolder1._ButtonImage.Click += delegate {				
				
			};

		}

		void mMainView_Click (object sender, EventArgs e)
		{
			int position = _RecyclerView.GetChildPosition ((View)sender);
		}

		public override int ItemCount {
			get { return _Info.Count; }
		}

	}
}




