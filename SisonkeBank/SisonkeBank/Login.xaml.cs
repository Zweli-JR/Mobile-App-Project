/*
 PROLOGUE
=============================================================
Programmer: Zwelethu Jr. Nkosi
Description: This page is for loging in.
=============================================================
*/

using SisonkeBank.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SisonkeBank
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        //assigns the email pattern to the variable
        string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

        public Login()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private void LoginTG_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SignUp());
        }

        private void BtnLogin_Clicked(object sender, EventArgs e)
        {
            //checks if validation passes
            if (IsPageValid())
            {
                //connecting to the database
                using (var db = new SQLiteConnection(App.connectionString))
                {
                    var user = db.Table<User>().FirstOrDefault(i => i.Email == txtEmail.Text);
                    if (user != null) //checks if the user's email exists 
                    {
                        if (user.Password == txtPsswd.Text)
                        {
                            Application.Current.Properties["User"] = txtEmail.Text;
                            Application.Current.SavePropertiesAsync();

                            Navigation.PushAsync(new MainPage());
                        }
                        else
                        {
                            DisplayAlert("Incorrect Password", "The password you entered is incorrect", "Okay");
                        }
                    }
                    else
                    {
                        DisplayAlert("Error", "Invalid email", "OK");
                    }
                }
            }
            else
            {
                DisplayAlert("Email error", "Please enter a valid email", "Back");
            }
        }

        private bool IsPageValid() //validation for the page
        {
            
            if (string.IsNullOrEmpty(txtEmail.Text) || !Regex.IsMatch(txtEmail.Text, emailPattern))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}