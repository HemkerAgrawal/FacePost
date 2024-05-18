﻿using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using FacePost.Helpers;
using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using AndroidX.AppCompat.App;


namespace FacePost.Activities
{
    [Activity(Label = "@string/app_name", Icon = "@mipmap/ic_facepost", Theme = "@style/MyTheme.Splash", MainLauncher = true)]
   // [Activity(Label = "SplashActivity")]
    public class SplashActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
        }

        protected override void OnResume()
        {
            base.OnResume();

            FirebaseUser currentUser = AppDataHelper.GetFirebaseAuth().CurrentUser;
            if (currentUser != null)
            {
                StartActivity(typeof(MainActivity));
                Finish();
            }
            else
            {
                StartActivity(typeof(LoginActivity));
            }
        }
    }
}