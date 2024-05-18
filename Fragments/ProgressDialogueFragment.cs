using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Xamarin.Forms.PlatformConfiguration;

namespace FacePost.Fragments
{
    public class ProgressDialogueFragment : Android.Support.V4.App.DialogFragment //Fragment
    {
        string status;
        public ProgressDialogueFragment(string _status) 
        {
            status = _status;
        }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            View view = inflater.Inflate(Resource.Layout.progress, container, false);
            TextView statusTextView = (TextView)view.FindViewById(Resource.Id.progressStatus);
            statusTextView.Text = status;
            return view;
            //return base.OnCreateView(inflater, container, savedInstanceState);
        }
    }
}