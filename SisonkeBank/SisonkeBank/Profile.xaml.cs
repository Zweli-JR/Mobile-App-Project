/*
 PROLOGUE
=============================================================
Programmer: Zwelethu Jr. Nkosi
Description: This page simply shows the user their profile information.
=============================================================
*/

using SisonkeBank.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SisonkeBank
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Profile : ContentPage
    {
        public Profile()
        {
            InitializeComponent();

            //assigns the users's email to the variable
            string userEmail = Application.Current.Properties["User"].ToString();

            //connects to the database 
            using (var db = new SQLiteConnection(App.connectionString))
            {
                var user = db.Table<User>().FirstOrDefault(i => i.Email == userEmail);

                fname.Text = user.FirstName;
                lname.Text = user.LastName;
                email.Text = user.Email;
                gender.Text = user.Gender;
            }

        }
    }
}