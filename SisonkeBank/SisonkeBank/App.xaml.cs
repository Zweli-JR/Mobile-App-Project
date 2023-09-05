/*
 PROLOGUE
=============================================================
Programmer: Zwelethu Jr. Nkosi
Description: This page is for creation of the database.
=============================================================
*/

using System;
using SQLite;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SisonkeBank.Models;

namespace SisonkeBank
{
    public partial class App : Application
    {
        public static string connectionString;

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage (new SignUp()); //makes the sign up page a navigation page
        }

        protected override void OnStart()
        {
            connectionString = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "sisonkeDB.db3");
            //connecting to the database
            using (var db = new SQLiteConnection(connectionString))
            {
                //creates the tables 
                db.CreateTable<User>();
                db.CreateTable<Account>();
            }
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
