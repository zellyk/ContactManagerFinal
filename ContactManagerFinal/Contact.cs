using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagerFinal
{
    public class Contact
    {
        public int id { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string phonenumber { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string place { get; set; }
        public string state { get; set; }
        public Contact() { }
        public Contact(int id,string firstname,string lastname,string phonenumber,string username,string email,string place,string state)
        {
            this.id = id;
            this.firstname = firstname;
            this.lastname = lastname;
            this.phonenumber = phonenumber;
            this.username = username;
            this.email = email;
            this.place = place;
            this.state = state;
        }

        public Contact( string firstname, string lastname, string phonenumber, string username, string email, string place, string state)
        {
            this.firstname = firstname;
            this.lastname = lastname;
            this.phonenumber = phonenumber;
            this.username = username;
            this.email = email;
            this.place = place;
            this.state = state;
        }

    }
}
