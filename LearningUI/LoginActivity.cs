using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;

namespace LearningUI
{
    [Activity(Label = "LoginActivity", NoHistory = true, WindowSoftInputMode = SoftInput.StateHidden | SoftInput.AdjustResize)]
    public class LoginActivity : Activity
    {
        static readonly string TAG = "X:" + typeof(LoginActivity).Name;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Login);
            var loginBtn = FindViewById<Button>(Resource.Id.LoginButton);
            var emailInput = FindViewById<EditText>(Resource.Id.emailInput);
            var passwordInput = FindViewById<EditText>(Resource.Id.passwordInput);
            var errorLabel = FindViewById<TextView>(Resource.Id.errorLabel);
            loginBtn.Click += (sender, e) => {
                VerifyLogin(emailInput, passwordInput, errorLabel);
            };
            
        }

        private void VerifyLogin(EditText email, EditText password, TextView error)
        {
            if (email.Text == "admin" && password.Text == "admin")
            {
                Log.Debug(TAG, "Successfully logged in.");
                StartActivity(new Intent(Application.Context, typeof(MainActivity)));
            }
            else
            {
                Log.Debug(TAG, "Wrong email or password");
                error.Text = "Wrong email or password. Try again.";
                HideSoftKeyboard();
            }
        }

        private void HideSoftKeyboard()
        {
            var currentFocus = CurrentFocus;
            if (currentFocus != null)
            {
                InputMethodManager inputMethodManager = (InputMethodManager)GetSystemService(InputMethodService);
                inputMethodManager.HideSoftInputFromWindow(currentFocus.WindowToken, HideSoftInputFlags.None);
            }
        }
    }
}