using System;
using System.Collections.Generic;
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
using System.IO;
using System.Xml;
using System.Xml.Linq;
using persons;

namespace phonebook
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        diary phoneDiary = new diary();
        person cPerson = null;
        List<person> pList;
        public MainWindow()
        {
            InitializeComponent();
            phoneDiary.loadXml("diary.xml");
            cPerson = phoneDiary.First();
            display();
            pList = phoneDiary.getList();
            phoneTable.ItemsSource = pList;
            phoneTable.Columns[0].Width = 200;
            // phoneTable.Columns[0].Header = "Name";
            phoneTable.Columns[1].Width = 500;
            // phoneTable.Columns[1].Header = "Address";
            // phoneTable.Columns[3].Visibility= Visibility.Collapsed;
            // phoneTable.Columns[4].Visibility= Visibility.Collapsed;

        }
        private void indexRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }
        private void cellSelected(object sender, SelectionChangedEventArgs e)
        {
            string data = string.Empty;
            // List<DataGridCellInfo> cells = phoneTable.SelectedCells.ToList();
            // foreach(DataGridCellInfo c in cells)
            // {
            //     data = data + "," + c.Item.ToString();
            // }
            person cell = (person)phoneTable.SelectedItems[0];
            //MessageBox.Show(cell.getEmail());
            cPerson = cell;
            display();
        }

        private void display()
        {
            // top part in dock
            nameLabel.Content = cPerson.getName();
            cityLabel.Content = cPerson.getCity();
            string s = cPerson.getContact();
            string[] sSplit = s.Split(',', 2, StringSplitOptions.RemoveEmptyEntries);
            if (s.Equals(","))
            {
                contactLabel.Content = "";
                contact0Label.Content = "";
                contact1Label.Content = "";
            }
            else
            {
                contactLabel.Content = s;
                if (sSplit.Count() == 2)
                {
                    contact0Label.Content = sSplit[0];
                    contact1Label.Content = sSplit[1];
                }
                else
                {
                    contact0Label.Content = s;
                }
            }
            // middle part in dock
            addressLabel.Content = cPerson.getAddress();
            ageLabel.Content = cPerson.getAge();
            dobLabel.Content = cPerson.getDOB();
            emailLabel.Content = cPerson.getEmail();
        }
        private void firstContact(object sender, RoutedEventArgs e)
        {
            cPerson = phoneDiary.First();
            display();
        }
        private void lastContact(object sender, RoutedEventArgs e)
        {
            cPerson = phoneDiary.Last();
            display();
        }
        private void nextContact(object sender, RoutedEventArgs e)
        {
            cPerson = phoneDiary.Next();
            display();
        }
        private void prevContact(object sender, RoutedEventArgs e)
        {
            cPerson = phoneDiary.Previous();
            display();
        }
        private void addContact(object sender, RoutedEventArgs e)
        {
            addnedit ane = new addnedit();
            ane.ShowDialog();
            if (ane.closeStatus)
            {
                person p = ane.personData;
                phoneDiary.add(p);
                try
                {
                    if (File.Exists("diary.xml"))
                    {
                        File.Delete("diary.xml");
                    }
                    XDocument doc = new XDocument();
                    doc.Add(phoneDiary.xml());
                    doc.Save("diary.xml");
                    phoneDiary.clearAll();
                    phoneDiary.loadXml("diary.xml");
                    pList = phoneDiary.getList();
                    cPerson = phoneDiary.Last();
                    display();
                    try
                    {
                        phoneTable.Items.Refresh();
                    }
                    catch (Exception exp)
                    {
                        //MessageBox.Show(exp.Message, exp.Source);
                    }
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }                
            }            
        }
        private void deleteContact(object sender, RoutedEventArgs e)
        {
            phoneDiary.remove(cPerson);
            cPerson = phoneDiary.Next();
            display();
            if (File.Exists("diary.xml"))
            {
                File.Delete("diary.xml");
            }
            XDocument doc = new XDocument();
            doc.Add(phoneDiary.xml());
            doc.Save("diary.xml");
            phoneDiary.clearAll();
            phoneDiary.loadXml("diary.xml");
            pList = phoneDiary.getList();
            try
            {
                phoneTable.Items.Refresh();
            }
            catch (Exception exp)
            {
                //MessageBox.Show(exp.Message,exp.Source);
            }
            
        }
        private void saveContact(object sender, RoutedEventArgs e)
        {
            addnedit updateBox = new addnedit(cPerson);
            updateBox.ShowDialog();
            if(updateBox.closeStatus) 
            {
                phoneDiary.remove(cPerson);
                person pUpdated = updateBox.personData;
                phoneDiary.add(pUpdated);
                //MessageBox.Show("now");
                try
                {
                    if (File.Exists("diary.xml"))
                    {
                        File.Delete("diary.xml");
                    }
                    XDocument doc = new XDocument();
                    doc.Add(phoneDiary.xml());
                    doc.Save("diary.xml");
                    phoneDiary.clearAll();
                    phoneDiary.loadXml("diary.xml");
                    pList = phoneDiary.getList();
                    cPerson = phoneDiary.Last();
                    display();
                    try
                    {
                        phoneTable.Items.Refresh();
                    }
                    catch (Exception exp)
                    {
                        //MessageBox.Show(exp.Message, exp.Source);
                    }
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }       
            }

        }
        private void printContact(object sender, RoutedEventArgs e)
        { 
            XElement ele = cPerson.xml();
            XDocument doc = new XDocument();
            doc.Add(ele);
            if (File.Exists("printContact.xml"))
            {
                File.Delete("printContact.xml");
            }
            doc.Save("printContact.xml");
        }

    }
}