/*
 PROLOGUE
=============================================================
Programmer: Zwelethu Jr. Nkosi
Description: This page is the main page and gives the user access to the other pages and it shows the user their balances.
=============================================================
*/

using SisonkeBank.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Shapes;

namespace SisonkeBank
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            //checks if a user is logged in
            if (Application.Current.Properties.ContainsKey("User"))
            {
                //assigns the user's email to the variable
                var userEmail = (string)Application.Current.Properties["User"];

                //connecting to the database
                using (var db = new SQLiteConnection(App.connectionString))
                {
                    var user = db.Table<User>().FirstOrDefault(u => u.Email == userEmail); 
                    var savings = db.Table<Account>().FirstOrDefault(s => s.UserEmail == userEmail && s.AccountType == "Savings"); //fetches the user's savings account information
                    var cheque = db.Table<Account>().FirstOrDefault(c => c.UserEmail == userEmail && c.AccountType == "Cheque"); //fetches the user's cheque account information

                    name.Text = $"Welcome {user.FirstName} {user.LastName}";
                    savingsBal.Text = $"R{savings.AccountBalance}";
                    chequeBal.Text = $"R{cheque.AccountBalance}";

                }
            }
            else
            {
                //if user is not logged in they should be redirected to the login page
                Application.Current.MainPage = new Login();
            }
        }

        private void SignOut_Tapped(object sender, EventArgs e)
        {
            Application.Current.Properties["User"] = null; //deletes the user's session
            Application.Current.SavePropertiesAsync();
            Application.Current.MainPage = new NavigationPage(new Login());
        }

        private void Profile_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Profile());
        }

        private void Transfer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Transfer());
        }

        private void Savings_Tapped(object sender, EventArgs e)
        {
            Application.Current.Properties["AccType"] = "Savings";
            Application.Current.SavePropertiesAsync();
            Navigation.PushAsync(new AccDetails());
        }

        private void Cheque_Tapped(object sender, EventArgs e)
        {
            Application.Current.Properties["AccType"] = "Cheque";
            Application.Current.SavePropertiesAsync();
            Navigation.PushAsync(new AccDetails());
        }
    }
}
