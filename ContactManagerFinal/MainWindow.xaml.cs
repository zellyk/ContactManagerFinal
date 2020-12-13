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
        SQLCommunications sql = SQLCommunications.Instance;

        public MainWindow()
        {
            InitializeComponent();

            DbUtil dbList = DbUtil.getInstance();

        }

        private void lv_contacts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DbUtil dbList = DbUtil.getInstance();

            List<Contact> listContacts = new List<Contact>(dbList.getAll());
            foreach (Contact c in listContacts)
            {

            }


        }


            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~


            private void ImportCSV_Click(object sender, RoutedEventArgs e)
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
                        Contact contact = new Contact(items[0], items[1], items[2], items[3]);
                        fileContact.Add(contact);
                    }
                }

                var data = SQLCommunications.Instance;
                foreach (Contact contact in fileContact)
                {
                    data.AddContacts(contact);
                }

                MessageBox.Show(opf.FileName, "Added to database", MessageBoxButton.OK);
            }

            private void ExportCSV_Click(object sender, RoutedEventArgs e)
            {
                List<Contact> contacts = sql.ReadContacts();
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Csv files (*.csv)|*.csv|All files(*.*)|*.*";


                if (sfd.ShowDialog() == true)
                {
                    using (StreamWriter sw = File.CreateText(@"D:\ExportContacts.csv"))
                    {
                        foreach (Contact contact in contacts)
                        {
                            sw.WriteLine(contact.ToString());
                        }
                    }
                }
            }





       
    }
}
