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
using System.Xml;
using System.Xml.Linq;
using persons;

namespace phonebook
{
    /// <summary>
    /// Interaction logic for addnedit.xaml
    /// </summary>
    public partial class addnedit : Window
    {
        // public Dictionary<string,string> dict{get;set;}
        public bool closeStatus = false;
        public person personData = null;
        public List<myDictionary> dict;
        public class myDictionary
        {
            public string Key { get; set; }
            public string Value { get; set; }

            public myDictionary()
            {
                Key = string.Empty;
                Value = string.Empty;
            }
        }
        public addnedit()
        {
            InitializeComponent();
            dict = new List<myDictionary>(){
                new myDictionary{Key="First Name",Value=""},
                new myDictionary{Key="Mid Name",Value=""},
                new myDictionary{Key="Last Name",Value=""},
                new myDictionary{Key="Gender",Value=""},
                new myDictionary{Key="Date of Birth",Value=""},
                new myDictionary{Key="Address",Value=""},
                new myDictionary{Key="City",Value=""},
                new myDictionary{Key="Contact 1",Value=""},
                new myDictionary{Key="Contact 2",Value=""},
                new myDictionary{Key="Email",Value=""}
            };
            dataEntry.ItemsSource = dict;

        }
        public addnedit(person p)
        {
            InitializeComponent();

            dict = new List<myDictionary>(){
                new myDictionary{Key="First Name",Value=p.name.Split(' ')[0]},
                new myDictionary{Key="Mid Name",Value=p.name.Split(' ')[1]},
                new myDictionary{Key="Last Name",Value=p.name.Split(' ')[2]},
                new myDictionary{Key="Gender",Value=p.getGender().ToString()},
                new myDictionary{Key="Date of Birth",Value=p.getDOB().ToString("dd/MM/yyyy")},
                new myDictionary{Key="Address",Value=p.getAddress()},
                new myDictionary{Key="City",Value=p.getCity()},
                new myDictionary{Key="Contact 1",Value=p.getContact0()},
                new myDictionary{Key="Contact 2",Value=p.getContact1()},
                new myDictionary{Key="Email",Value=p.getEmail()}
            };
            dataEntry.ItemsSource = dict;
        }
        private void addFunction(object sender, RoutedEventArgs e)
        {
            // MessageBox.Show(dict["Address"]);                   
            genderType gt = genderType.Other;
            if (dict[3].Value.ToUpper().Equals("MALE") || dict[3].Value.ToUpper().Equals("M"))
            {
                gt = genderType.Male;
            }
            else if (dict[3].Value.ToUpper().Equals("FEMALE") || dict[3].Value.ToUpper().Equals("F"))
            {
                gt = genderType.Female;
            }
            DateTime dob = DateTime.Now;
            try
            {
                dob = DateTime.ParseExact(dict[4].Value, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                dob = DateTime.ParseExact(dict[4].Value, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
            }
            personData = new person(dict[0].Value, dict[2].Value, dict[1].Value, gt, dict[5].Value, dict[6].Value, dict[7].Value, dob, string.Empty);
            personData.setContact0(dict[7].Value);
            personData.setContact1(dict[8].Value);
            personData.setEmail(dict[9].Value);
            closeStatus = true;
            this.Close();
        }
        private void cancelFunction(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cellSelected(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
