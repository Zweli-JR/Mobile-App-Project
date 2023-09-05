/*
 PROLOGUE
=============================================================
Programmer: Zwelethu Jr. Nkosi
Description: This page is for signing up a user.
=============================================================
*/

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using SisonkeBank.Models;
using System.Text.RegularExpressions;

namespace SisonkeBank
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUp : ContentPage
    {
        //assigns the email pattern to the variable
        string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

        public SignUp()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private void SignTG_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Login());
        }

        private void BtnSignUP_Clicked(object sender, EventArgs e)
        {

            if (IsPageValid() == "true") //checks if all the validators passed
            {
                try
                {
                    //connection to the database 
                    using (var db = new SQLiteConnection(App.connectionString))
                    {
                        //inserts all the data into the database
                        db.Insert(new Models.User
                        {
                            Email = txtEmail.Text,
                            FirstName = txtName.Text,
                            LastName = txtSurname.Text,
                            Password = txtPsswd.Text,
                            MobileNumber = txtNumber.Text,
                            Gender = txtGender.Text
                        });

                        db.Insert(new Models.Account
                        {
                            AccountType = "Savings",
                            AccountBalance = 100,
                            UserEmail = txtEmail.Text
                        });

                        db.Insert(new Models.Account
                        {
                            AccountType = "Cheque",
                            AccountBalance = 0,
                            UserEmail = txtEmail.Text
                        });

                    }
                    //creates a session for the user
                    Application.Current.Properties["User"] = txtEmail.Text;
                    Application.Current.SavePropertiesAsync();

                    Navigation.PushAsync(new MainPage());


                }
                catch (SQLiteException)
                {
                    DisplayAlert("Registered", "You already have an account", "Return");
                }
            }
            else if (IsPageValid() == "name")
            {
                DisplayAlert("Name Required", "Please enter your first name", "Return");
            }

            else if (IsPageValid() == "name1")
            {
                DisplayAlert("Invalid First Name", "Please enter a valid name", "Return");
            }

            else if (IsPageValid() == "lname")
            {
                DisplayAlert("Last Name Required", "Please enter your last name", "Return");
            }

            else if (IsPageValid() == "lname1")
            {
                DisplayAlert("Invalid Last Name", "Please enter a valid name", "Return");
            }

            else if (IsPageValid() == "email")
            {
                DisplayAlert("Email Required", "Please enter a valid email", "Return");
            }

            else if (IsPageValid() == "gender")
            {
                DisplayAlert("Gender Required", "Please enter your gender", "Return");
            }

            else if (IsPageValid() == "number")
            {
                DisplayAlert("Contact Number", "Please enter a valid contact number", "Return");
            }

            else if (IsPageValid() == "psswd")
            {
                DisplayAlert("Password Required", "Please create a new password, it must be at least 5 charecters long", "Return");
            }

            else
            {
                DisplayAlert("Password Too Short", "Password must be at least 5 charecters long", "Return");
            }

        }

        private string IsPageValid()
        {
            string isValid = "true";

            if (string.IsNullOrEmpty(txtGender.Text))
            {
                isValid = "gender";
            }

            try
            {
                if (txtPsswd.Text.Length < 5)
                {
                    isValid = "psswd1";
                }
            }
            catch (NullReferenceException)
            {
                isValid = "psswd";
            }

            if (string.IsNullOrEmpty(txtNumber.Text) || txtNumber.Text.Length != 10)
            {
                isValid = "number";
            }

            if (string.IsNullOrEmpty(txtEmail.Text) || !Regex.IsMatch(txtEmail.Text, emailPattern))
            {
                isValid = "email";
            }

            try
            {
                if (txtSurname.Text.Any(char.IsDigit))
                {
                    isValid = "lname1";
                }
            }
            catch (ArgumentNullException)
            {
                isValid = "lname";
            }

            try
            {
                if (txtName.Text.Any(char.IsDigit))
                {
                    isValid = "name1";
                }
            }
            catch (ArgumentNullException)
            {
                isValid = "name";
            }

            return isValid;
        }
    }
}