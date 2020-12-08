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
            var query = "select * from People INNER JOIN Emails ON (People.Domainid = Emails.Domainid)";
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
                    (string)sdr["Name"],
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
            var query = "select * from People INNER JOIN Emails ON (People.Domainid = Emails.Domainid) WHERE People.Id = @id";
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
                    (string)sdr["Name"],
                    (string)sdr["Place"],
                    (string)sdr["State"]);
            }
            con.Close();
            return tempContact;
        }

        public void createOne(string fn, string ln, string pn, string usern, int domainId,string place, string state)
        {
            var query = "INSERT INTO People" +
                "(Firstname, Lastname, Phonenumber, Username, Domainid, Place, State) " +
                "VALUES(@fn, @ln, @pn, @usern, @domainid, @place, @state)";
            var con = new SqlConnection(@"data source=localhost\SQLEXPRESS;database = CONTACTS;Trusted_Connection=True");
            SqlCommand cm = new SqlCommand(query, con);
            cm.Parameters.AddWithValue("@fn", fn);
            cm.Parameters.AddWithValue("@ln", ln);
            cm.Parameters.AddWithValue("@pn", pn);
            cm.Parameters.AddWithValue("@usern", usern);
            cm.Parameters.AddWithValue("@domainid", domainId);
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

        public void updateOne(int id, string fn, string ln, string pn, string usern, int domainId, string place, string state)
        {
            var query = "UPDATE People SET Firstname = @fn, Lastname = @ln, Phonenumber = @pn, Username = @usern, Domainid = @domainid, Place = @place, State = @state  WHERE Id = @id";
            var con = new SqlConnection(@"data source=localhost\SQLEXPRESS;database = CONTACTS;Trusted_Connection=True");
            SqlCommand cm = new SqlCommand(query, con);
            cm.Parameters.AddWithValue("@id", id);
            cm.Parameters.AddWithValue("@fn", fn);
            cm.Parameters.AddWithValue("@ln", ln);
            cm.Parameters.AddWithValue("@pn", pn);
            cm.Parameters.AddWithValue("@usern", usern);
            cm.Parameters.AddWithValue("@domainid", domainId);
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
            var query = "DELETE FROM People WHERE Id = @id";
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
