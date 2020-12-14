using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagerFinal
{
    public class DbUtil
    {
        private static DbUtil sInstance;

        public static DbUtil getInstance()
        {
            if (sInstance == null)
            {
                sInstance = new DbUtil();
            }
            return sInstance;
        }

        private DbUtil() { }

        public List<Contact> getAll()
        {
            List<Contact> listContacts = new List<Contact>();
            var query = "select * from Contact";
            var con = new SqlConnection(@"data source=localhost\SQLEXPRESS;database = CONTACTS;Trusted_Connection=True");
            SqlCommand cm = new SqlCommand(query, con);
            con.Open();
            SqlDataReader sdr = cm.ExecuteReader();
            while (sdr.Read())
            {
                Contact tempContact = new Contact(
                    (int)sdr["Id"],
                    (string)sdr["Firstname"],
                    (string)sdr["Lastname"],
                    (string)sdr["Phonenumber"],
                    (string)sdr["Username"],
                    (string)sdr["Email"],
                    (string)sdr["Place"],
                    (string)sdr["State"]);

                listContacts.Add(tempContact);
            }
            con.Close();
            return listContacts;
        }

        public Contact getOneById(int id)
        {
            Contact tempContact = new Contact();
            var query = "select * from ContContactacts WHERE Contact.Id = @id";
            var con = new SqlConnection(@"data source=localhost\SQLEXPRESS;database = CONTACTS;Trusted_Connection=True");
            SqlCommand cm = new SqlCommand(query, con);
            cm.Parameters.AddWithValue("@id", id);
            con.Open();
            SqlDataReader sdr = cm.ExecuteReader();
            while (sdr.Read())
            {
                tempContact = new Contact(
                    (int)sdr["Id"],
                    (string)sdr["Firstname"],
                    (string)sdr["Lastname"],
                    (string)sdr["Phonenumber"],
                    (string)sdr["Username"],
                    (string)sdr["Email"],
                    (string)sdr["Place"],
                    (string)sdr["State"]);
            }
            con.Close();
            return tempContact;
        }

        public void createOne(string fn, string ln, string pn, string usern, string email,string place, string state)
        {
            var query = "INSERT INTO Contact" +
                "(Firstname, Lastname, Phonenumber, Username, Email, Place, State) " +
                "VALUES(@fn, @ln, @pn, @usern, @email, @place, @state)";
            var con = new SqlConnection(@"data source=localhost\SQLEXPRESS;database = CONTACTS;Trusted_Connection=True");
            SqlCommand cm = new SqlCommand(query, con);
            cm.Parameters.AddWithValue("@fn", fn);
            cm.Parameters.AddWithValue("@ln", ln);
            cm.Parameters.AddWithValue("@pn", pn);
            cm.Parameters.AddWithValue("@usern", usern);
            cm.Parameters.AddWithValue("@email", email);
            cm.Parameters.AddWithValue("@place", place);
            cm.Parameters.AddWithValue("@state", state);

            try
            {
                con.Open();
                cm.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                e.ToString();
            }
            finally
            {
                con.Close();
            } 
        }

        public void updateOne(int id, string fn, string ln, string pn, string usern, string email, string place, string state)
        {
            var query = "UPDATE Contact SET Firstname = @fn, Lastname = @ln, Phonenumber = @pn, Username = @usern, Email = @email, Place = @place, State = @state  WHERE Id = @id";
            var con = new SqlConnection(@"data source=localhost\SQLEXPRESS;database = CONTACTS;Trusted_Connection=True");
            SqlCommand cm = new SqlCommand(query, con);
            cm.Parameters.AddWithValue("@id", id);
            cm.Parameters.AddWithValue("@fn", fn);
            cm.Parameters.AddWithValue("@ln", ln);
            cm.Parameters.AddWithValue("@pn", pn);
            cm.Parameters.AddWithValue("@usern", usern);
            cm.Parameters.AddWithValue("@email", email);
            cm.Parameters.AddWithValue("@place", place);
            cm.Parameters.AddWithValue("@state", state);

            try
            {
                con.Open();
                cm.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                e.ToString();
            }
            finally
            {
                con.Close();
            }
        }

        public void deleteOne(int id)
        {
            var query = "DELETE FROM Contact WHERE Id = @id";
            var con = new SqlConnection(@"data source=localhost\SQLEXPRESS;database = CONTACTS;Trusted_Connection=True");
            SqlCommand cm = new SqlCommand(query, con);
            cm.Parameters.AddWithValue("@id", id);
            try
            {
                con.Open();
                cm.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                e.ToString();
            }
            finally
            {
                con.Close();
            }
        }
    }
}
