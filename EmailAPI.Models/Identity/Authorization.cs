using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailAPI.Models.Identity
{
    public class Authorization
    {
        public enum Roles
        {
            Administrator,
            User
        }
        public const string default_username = "admin";
        public const string default_email = "ertugrulkaanozdemir@gmail.com";
        public const string default_firstname = "ertugrulkaanozdemir@gmail.com";
        public const string default_lastname = "ertugrulkaanozdemir@gmail.com";
        public const string default_password = "12345Aa!";
        public const Roles default_role = Roles.Administrator;
    }
}
