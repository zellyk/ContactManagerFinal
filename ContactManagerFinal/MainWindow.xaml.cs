using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ContactManagerFinal
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var lv_contacts = (ListView)this.FindName("lv_contacts");
            DbUtil dbList = DbUtil.getInstance();

            List<Contact> listContacts = new List<Contact>(dbList.getAll());
            foreach (Contact c in listContacts)
            {
                lv_contacts.Items.Add(c);
            }

        }

        private void lv_contacts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var txt_id = (TextBox)this.FindName("txt_id");
            var txt_firstname = (TextBox)this.FindName("txt_firstname");
            var txt_lastname = (TextBox)this.FindName("txt_lastname");
            var txt_phonenumber = (TextBox)this.FindName("txt_phonenumber");
            var txt_username = (TextBox)this.FindName("txt_username");
            var txt_email = (TextBox)this.FindName("txt_email");
            var txt_place = (TextBox)this.FindName("txt_place");
            var txt_state = (TextBox)this.FindName("txt_state");

            Contact selectedContact = (Contact)lv_contacts.SelectedItem;
            txt_id.Text = selectedContact.id.ToString();
            txt_firstname.Text = selectedContact.firstname;
            txt_lastname.Text = selectedContact.lastname;
            txt_phonenumber.Text = selectedContact.phonenumber;
            txt_username.Text = selectedContact.username;
            txt_email.Text = selectedContact.email;
            txt_place.Text = selectedContact.place;
            txt_state.Text = selectedContact.state;
        }

        private void add(object sender, RoutedEventArgs e)
        {
            var txt_firstname = (TextBox)this.FindName("txt_firstname");
            var txt_lastname = (TextBox)this.FindName("txt_lastname");
            var txt_phonenumber = (TextBox)this.FindName("txt_phonenumber");
            var txt_username = (TextBox)this.FindName("txt_username");
            var txt_email = (TextBox)this.FindName("txt_email");
            var txt_place = (TextBox)this.FindName("txt_place");
            var txt_state = (TextBox)this.FindName("txt_state");

            DbUtil dbList = DbUtil.getInstance();
            var lv_contacts = (ListView)this.FindName("lv_contacts");
            dbList.createOne(txt_firstname.Text, txt_lastname.Text, txt_phonenumber.Text, txt_username.Text, txt_email.Text, txt_place.Text, txt_state.Text);

        }

        private void delete(object sender, RoutedEventArgs e)
        {
            var txt_id = (TextBox)this.FindName("txt_id");
            
            DbUtil dbList = DbUtil.getInstance();
            int id = Convert.ToInt32(txt_id.Text);
            dbList.deleteOne(id);
        }


        private void edit(object sender, RoutedEventArgs e)
        {
            var txt_id = (TextBox)this.FindName("txt_id");
            var txt_firstname = (TextBox)this.FindName("txt_firstname");
            var txt_lastname = (TextBox)this.FindName("txt_lastname");
            var txt_phonenumber = (TextBox)this.FindName("txt_phonenumber");
            var txt_username = (TextBox)this.FindName("txt_username");
            var txt_email = (TextBox)this.FindName("txt_email");
            var txt_place = (TextBox)this.FindName("txt_place");
            var txt_state = (TextBox)this.FindName("txt_state");

            DbUtil dbList = DbUtil.getInstance();

            int id = Convert.ToInt32(txt_id.Text);

            dbList.updateOne(id, txt_firstname.Text, txt_lastname.Text, txt_phonenumber.Text, txt_username.Text, txt_email.Text, txt_place.Text, txt_state.Text);
        }

        private void importCsv(object sender, RoutedEventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Csv files (*.csv)|*.csv|All files(*.*)|*.*";
            List<Contact> fileContact = new List<Contact>();

            if (opf.ShowDialog() == true)
            {
                string[] fileContents = File.ReadAllLines(opf.FileName);

                foreach (string s in fileContents)
                {
                    string[] items = s.Split(',');
                    Contact contact = new Contact(items[0], items[1], items[2], items[3], items[4], items[5], items[6]);
                    fileContact.Add(contact);
                }
            }

            DbUtil dbList = DbUtil.getInstance();
            foreach (Contact contact in fileContact)
            {
                dbList.createOne(contact.firstname, contact.lastname, contact.phonenumber, contact.username, contact.email, contact.place, contact.state);
            }
            var label = (Label)this.FindName("label_importexport");
            label.Content = "Added to database, Refresh window";
        }

        private void exportCsv(object sender, RoutedEventArgs e)
        {
            DbUtil dbList = DbUtil.getInstance();
            List<Contact> contacts = dbList.getAll();
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Csv files (*.csv)|*.csv|All files(*.*)|*.*";

            if (sfd.ShowDialog() == true)
            {
                using (StreamWriter sw = File.CreateText(@"C:\Users\Olivier\Documents\ExportedContacts.csv"))
                {
                    foreach (Contact contact in contacts)
                    {
                        sw.WriteLine(contact.ToString());
                    }
                }
            }
            var label = (Label)this.FindName("label_importexport");
            label.Content = "Exported contacts to ExportedContacts.csv";
        }




    }
}
