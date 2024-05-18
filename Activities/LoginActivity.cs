using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Support.V4.App;
using Android.Support.V7.App;
using Android.Support.Design.Widget;
using FacePost.EventListeners;
using Firebase.Auth;
using FacePost.Helpers;
using FacePost.Fragments;
//using Google.Android.Material.Tabs.AppCompat.App;
//using AndroidX.AppCompat.App;

//using Android.Support.V7.App;

namespace FacePost.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]
 //   [Activity(Label = "LoginActivity")]
    public class LoginActivity : AppCompatActivity//Activity
    {
        EditText emailText, passwordText;
        Button loginButton;
        FirebaseAuth mAuth;
        TaskCompletionListeners taskCompletionListeners = new TaskCompletionListeners();
        ProgressDialogueFragment progressDialogue;
        TextView clickToRegister;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.login);  

            emailText = FindViewById<EditText>(Resource.Id.emailText);
            passwordText = FindViewById<EditText>(Resource.Id.passwordText);
            loginButton = (Button)FindViewById(Resource.Id.loginButton);
            clickToRegister = (TextView)FindViewById(Resource.Id.clickToRegister);
            clickToRegister.Click += clickToRegister_Click;
            loginButton.Click += LoginButton_Click;
            mAuth = AppDataHelper.GetFirebaseAuth();

            // Create your application here
        }

        private void clickToRegister_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(RegistrationActivity));
            Finish();
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            string email, password;
            email = emailText.Text;
            password = passwordText.Text;
            if (!email.Contains("@"))
            {
                Toast.MakeText(this, "Please provide a valid email address", ToastLength.Short).Show();
                return;
            }
            else if (password.Length < 8)
            {
                Toast.MakeText(this, "Please provide a valid password", ToastLength.Short).Show();
                return;
            }
            ShowProgressDialogue("Verifying you... ");
            mAuth.SignInWithEmailAndPassword(email, password).AddOnSuccessListener(taskCompletionListeners)
                .AddOnFailureListener(taskCompletionListeners);

            taskCompletionListeners.Sucess += (success, args) =>
            {
                CloseProgressDialogue();
              //  Toast.MakeText(this, "Login Succesfull : " , ToastLength.Short).Show();

                StartActivity(typeof(MainActivity));
                Finish();
            };

            taskCompletionListeners.Failure += (success, args) =>
            {
                CloseProgressDialogue();
                Toast.MakeText(this, "Login failed : ", ToastLength.Short).Show();
//                Toast.MakeText(this, "Login Failed : " + args.Cause, ToastLength.Long).Show();
            };

        }
        void ShowProgressDialogue(string status)
        {
            progressDialogue = new ProgressDialogueFragment(status);
            var trans = SupportFragmentManager.BeginTransaction();
            progressDialogue.Cancelable = false;
            progressDialogue.Show(trans, "Progress");
        }
        void CloseProgressDialogue()
        {
            if (progressDialogue != null)
            {
                progressDialogue.Dismiss();
                progressDialogue = null;
            }
        }
    }
}