/*
 PROLOGUE
=============================================================
Programmer: Zwelethu Jr. Nkosi
Description: This page is for transfering money between accounts.
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
    public partial class Transfer : ContentPage
    {
        public Transfer()
        {
            InitializeComponent();

            //assigns the user's email to the variable
            var userEmail = (string)Application.Current.Properties["User"];

            //connecting to the database
            using (var db = new SQLiteConnection(App.connectionString))
            {
                var savings = db.Table<Account>().FirstOrDefault(s => s.UserEmail == userEmail && s.AccountType == "Savings");
                var cheque = db.Table<Account>().FirstOrDefault(c => c.UserEmail == userEmail && c.AccountType == "Cheque");

                //fetches the balances of each account
                savingsBal.Text = $"R{savings.AccountBalance}";
                chequeBal.Text = $"R{cheque.AccountBalance}";

            }

            transferDirectionPicker.SelectedIndex = 0; // makes the default option the first option

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

        private void TransferBtn_Clicked(object sender, EventArgs e)
        {
            decimal transferAmount = decimal.Parse(amountEntry.Text); //transfer the the amount
            string userId = (string)Application.Current.Properties["User"];

            //connects to the database
            using (var db = new SQLiteConnection(App.connectionString))
            {
                var chequeAccount = db.Table<Account>().FirstOrDefault(a => a.UserEmail == userId && a.AccountType == "Cheque");
                var savingsAccount = db.Table<Account>().FirstOrDefault(a => a.UserEmail == userId && a.AccountType == "Savings");

                if (transferDirectionPicker.SelectedIndex == 0)
                {
                    if (savingsAccount.AccountBalance >= transferAmount)
                    {
                        savingsAccount.AccountBalance -= transferAmount;
                        chequeAccount.AccountBalance += transferAmount;
                        db.Update(chequeAccount);
                        db.Update(savingsAccount);

                        savingsBal.Text = $"R{savingsAccount.AccountBalance}";
                        chequeBal.Text = $"R{chequeAccount.AccountBalance}";

                        DisplayAlert("Transaction Successful", "The money has been transfered successful", "Okay");
                    }
                    else
                    {
                        DisplayAlert("Insuffecient funds", "you have insuffecient funds to make that transaction", "Back");
                    }

                }
                else
                {
                    if (chequeAccount.AccountBalance >= transferAmount)
                    {
                        chequeAccount.AccountBalance -= transferAmount;
                        savingsAccount.AccountBalance += transferAmount;
                        db.Update(chequeAccount);
                        db.Update(savingsAccount);

                        savingsBal.Text = $"R{savingsAccount.AccountBalance}";
                        chequeBal.Text = $"R{chequeAccount.AccountBalance}";

                        DisplayAlert("Transaction Successful", "The money has been transfered successful", "Okay");
                    }
                    else
                    {
                        DisplayAlert("Insuffecient funds", "you have insuffecient funds to make that transaction", "Back");
                    }
                }

            }

        }
    }
}