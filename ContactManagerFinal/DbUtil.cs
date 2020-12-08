using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagerFinal
{
    public sealed class DbUtil
    {
        DbUtil() { }

        static readonly DbUtil instance = new DbUtil();

        public static DbUtil Instance
        {
            get { return instance; }
        }

        public List<Contact> GetAll()
        {
            List<Contact> listContacts = new List<Contact>();
            var query = "SELECT * FROM People INNER JOIN Emails ON (People.Domainid = Emails.Domainid);";
            var con = new SqlConnection(@"data source=localhost\SQLEXPRESS;database = CONTACTS;Trusted_Connection=True");
            SqlCommand cm = new SqlCommand(query, con);
            SqlDataReader sdr = cm.ExecuteReader();
            while (sdr.Read())
            {
                Contact tempContact = new Contact(
                    (int)sdr["Id"],
                    (string)sdr["Firstname"],
                    (string)sdr["Lastname"],
                    (string)sdr["Phonenumber"],
                    (string)sdr["Username"],
                    (string)sdr["Name"],
                    (string)sdr["Place"],
                    (string)sdr["State"]);

                listContacts.Add(tempContact);
            }
            return listContacts;
        }
    }
}
