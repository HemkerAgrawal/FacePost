using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Firestore;
using Firebase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Firebase.Auth;

namespace FacePost.Helpers
{
    //internal class AppDataHelper
    public static class AppDataHelper
    {
        static ISharedPreferences preferences = Application.Context.GetSharedPreferences("userinfo", FileCreationMode.Private);
        static ISharedPreferencesEditor editor;
        public static FirebaseFirestore GetFirestore()
        {
            var app = FirebaseApp.InitializeApp(Application.Context);
            FirebaseFirestore database;
            if (app == null)
            {
                var options = new FirebaseOptions.Builder()
             /*    .SetProjectId("facepostapp-b75e0")
                 .SetApplicationId("facepostapp-b75e0")//same as project id
                 .SetApiKey("AIzaSyBq7FyIb_WiAtwcIIQKbdNSzm2W-FsG3MI")
                 // .SetDatabaseUrl("facepostapp-9661c")
                 .SetDatabaseUrl("https://facepostApp.firebaseio.com") //databaseURL: "https://DATABASE_NAME.firebaseio.com",
                 .SetStorageBucket("facepostapp-b75e0.appspot.com")
                 .Build();
             */
             .SetProjectId("facepostapp-4a5c1")
                .SetApplicationId("facepostapp-4a5c1")//same as project id
                .SetApiKey("AIzaSyD7gQk8wjOcJ2ZzicN1lCAl3OQYcp_qmnI")
                // .SetDatabaseUrl("facepostapp-9661c")
                .SetDatabaseUrl("https://facepostApp.firebaseio.com") //databaseURL: "https://DATABASE_NAME.firebaseio.com",
                .SetStorageBucket("facepostapp-4a5c1.appspot.com")
                .Build();

                app = FirebaseApp.InitializeApp(Application.Context, options);
                database = FirebaseFirestore.GetInstance(app);

            }

            else
            {
                database = FirebaseFirestore.GetInstance(app);
            }

            return database;
        }


        public static FirebaseAuth GetFirebaseAuth()
        {
            var app = FirebaseApp.InitializeApp(Application.Context);
            FirebaseAuth mAuth;

            if (app == null)
            {
                var options = new FirebaseOptions.Builder()
                  /* .SetProjectId("facepostapp-b75e0")
                .SetApplicationId("facepostapp-b75e0")//same as project id
                .SetApiKey("AIzaSyBq7FyIb_WiAtwcIIQKbdNSzm2W-FsG3MI")
                // .SetDatabaseUrl("facepostapp-9661c")
                .SetDatabaseUrl("https://facepostApp.firebaseio.com") //databaseURL: "https://DATABASE_NAME.firebaseio.com",
                .SetStorageBucket("facepostapp-b75e0.appspot.com")
                    .Build();
                */
                
                .SetProjectId("facepostapp-4a5c1")
                .SetApplicationId("facepostapp-4a5c1")//same as project id
                .SetApiKey("AIzaSyD7gQk8wjOcJ2ZzicN1lCAl3OQYcp_qmnI")
                // .SetDatabaseUrl("facepostapp-9661c")
                .SetDatabaseUrl("https://facepostApp.firebaseio.com") //databaseURL: "https://DATABASE_NAME.firebaseio.com",
                .SetStorageBucket("facepostapp-4a5c1.appspot.com")
                .Build();

  //              app = FirebaseApp.InitializeApp(Application.Context, options);
//                database = FirebaseFirestore.GetInstance(app);

                app = FirebaseApp.InitializeApp(Application.Context, options);
                mAuth = FirebaseAuth.Instance;
            }
            else
            {
                mAuth = FirebaseAuth.Instance;
            }
            return mAuth;
        }

        public static void SaveFullName(string fullname)
        {
            editor = preferences.Edit();
            editor.PutString("fullname", fullname);
            editor.Apply();
        }
        public static string GetFullName()
        {
            string fullname = "";
            fullname = preferences.GetString("fullname", "");
            return fullname;
        }
    }
}