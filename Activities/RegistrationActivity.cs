using Android.App;
using Android.Content;
using Android.Gms.Tasks;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using FacePost.EventListeners;
using FacePost.Fragments;
using FacePost.Helpers;
using Firebase;
using Firebase.Auth;
using Firebase.Firestore;
using Java.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FacePost.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]

//    [Activity(Label = "RegistrationActivity")]
    public class RegistrationActivity : AppCompatActivity
    {
        Button registerButton;
        FirebaseFirestore database;
        EditText fullnameText, emailText, passwordText, confirmPasswordText;
        //string fullname, email, password, confirm;
        FirebaseAuth mAuth;
        string fullname, email, password, confirm;
        TaskCompletionListeners taskCompletionListeners = new TaskCompletionListeners();
        ProgressDialogueFragment progressDialogue;
        TextView clickToLogin;

        //  ProgressDialogueFragment progressDialog;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.register);
            /*fullnameText = (EditText)FindViewById(Resource.Id.fullNameRegText);
            emailText = (EditText)FindViewById(Resource.Id.emailRegText);
            passwordText = (EditText)FindViewById(Resource.Id.passwordRegText);
            confirmPasswordText = (EditText)FindViewById(Resource.Id.confirmPasswordRegText);*/


            fullnameText = FindViewById<EditText>(Resource.Id.fullnameText);
            emailText = FindViewById<EditText>(Resource.Id.emailText);
            passwordText = FindViewById<EditText>(Resource.Id.passwordText);
            confirmPasswordText = FindViewById<EditText>(Resource.Id.confirmPasswordText);
            clickToLogin = (TextView)FindViewById(Resource.Id.clickToLogin);
            clickToLogin.Click += clickToLogin_Click;
            registerButton = (Button) FindViewById(Resource.Id.registerButton);
            registerButton.Click += RegisterButton_Click;
            database = AppDataHelper.GetFirestore();
            mAuth = AppDataHelper.GetFirebaseAuth();
            // Create your application here
        }

        private void clickToLogin_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(LoginActivity));
            Finish();
        }

        private void RegisterButton_Click(object sender, EventArgs e)
        {
            
            fullname = fullnameText.Text;
            email = emailText.Text;
            password = passwordText.Text;
            confirm = confirmPasswordText.Text;

            if (fullname.Length < 4)
            {
                Toast.MakeText(this, "Please enter a valid name", ToastLength.Short).Show();
                return;
            }
            else if (!email.Contains("@"))
            {
                Toast.MakeText(this, "Please enter a valid email address", ToastLength.Short).Show();
                return;
            }
            else if (password.Length < 8)//password should be long... short password not accepted by firebase
            {
                Toast.MakeText(this, "Please enter a password upto 8 characters", ToastLength.Short).Show();
                return;
            }
            else if (password != confirm)
            {
                Toast.MakeText(this, "Password does not match, please make correction", ToastLength.Short).Show();
                return;
            }
            //Toast.MakeText(this, email, ToastLength.Short).Show();
            //Toast.MakeText(this, password, ToastLength.Short).Show();
            ShowProgressDialogue("Registering you....");
            mAuth.CreateUserWithEmailAndPassword(email, password).AddOnSuccessListener(this, taskCompletionListeners)
                .AddOnFailureListener(this, taskCompletionListeners);

            taskCompletionListeners.Sucess += (success, args) =>
            {
                HashMap userMap = new HashMap();
                userMap.Put("email", email);
                userMap.Put("fullname", fullname);

                DocumentReference userReference = database.Collection("users").Document(mAuth.CurrentUser.Uid);
                userReference.Set(userMap);
                CloseProgressDialogue();
                StartActivity(typeof(MainActivity));
                Finish();
            };

            // Registration Failure Callback
            taskCompletionListeners.Failure += (failure, args) =>
            {
                CloseProgressDialogue();
                Toast.MakeText(this, "Registartion Failed : " + args.Cause, ToastLength.Long).Show();
            };


            /*HashMap userMap = new HashMap();
            userMap.Put("email", "hemker@email.com");
            userMap.Put("name", "hemker");
            DocumentReference userReference = database.Collection("users").Document();
            userReference.Set(userMap);*/
            // above code used for testing of app connection wuth firebase
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



        /*  public static FirebaseAuth GetFirebaseAuth()
          {
              var app = FirebaseApp.InitializeApp(Application.Context);
              FirebaseAuth mAuth;

              if (app == null)
              {
                  var options = new FirebaseOptions.Builder()
                      .SetProjectId("facepostapp")
                      .SetApplicationId("facepostapp")
                      .SetApiKey("AIzaSyC6_pazwbpdsE3YfgtvKLJx3OOInrRD8SQ")
                      .SetDatabaseUrl("https://facepostapp.firebaseio.com")
                      .SetStorageBucket("facepostapp.appspot.com")
                      .Build();

                  app = FirebaseApp.InitializeApp(Application.Context, options);
                  mAuth = FirebaseAuth.Instance;
              }
              else
              {
                  mAuth = FirebaseAuth.Instance;
              }
              return mAuth;
          } */



        /*public void OnSuccess(Java.Lang.Object result)
        {
            HashMap userMap = new HashMap();
            userMap.Put("email", email);
            userMap.Put("name", fullname);
            DocumentReference userReference = database.Collection("users").Document(mAuth.CurrentUser.Uid);
            userReference.Set(userMap);
        }

        public void OnFailure(Java.Lang.Exception e)
        {
            Toast.MakeText(this, "Registration Failed" + e.Message, ToastLength.Short).Show();
        }*/
    }
}