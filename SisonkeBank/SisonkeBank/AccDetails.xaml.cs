/*
 PROLOGUE
=============================================================
Programmer: Zwelethu Jr. Nkosi
Description: This page shows the account's information.
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
    public partial class AccDetails : ContentPage
    {
        public AccDetails()
        {
            InitializeComponent();


            //assigns the user email to a variable 
            var userEmail = (string)Application.Current.Properties["User"];
            var type = (string)Application.Current.Properties["AccType"];

            //connects to the database
            using (var db = new SQLiteConnection(App.connectionString))
            {
                var balance = db.Table<Account>().FirstOrDefault(i => i.UserEmail == userEmail && i.AccountType == type);

                //fetches the account information
                accBal.Text = $"R{balance.AccountBalance}";
                accType.Text = balance.AccountType;
                accNumber.Text = balance.AccountNumber.ToString();

            }

        }
    }
}