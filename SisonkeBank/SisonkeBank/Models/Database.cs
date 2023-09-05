using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace SisonkeBank.Models
{
    [Table("users")]
    public class User
    {
        [PrimaryKey]
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string MobileNumber { get; set; }
        public string Gender { get; set; }
    }

    [Table("accounts")]
    public class Account
    {
        [PrimaryKey, AutoIncrement]
        public int AccountNumber { get; set; }
        public string AccountType { get; set; }
        public decimal AccountBalance { get; set; }
        public string UserEmail { get; set; }
    }
}
