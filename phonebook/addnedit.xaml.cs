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
using persons;

namespace phonebook
{
    /// <summary>
    /// Interaction logic for addnedit.xaml
    /// </summary>
    public partial class addnedit : Window
    {
        // public Dictionary<string,string> dict{get;set;}
        public List<Object> dict;
        public addnedit()
        {
            InitializeComponent();                     
            dict = new List<Object>(){
                new {Key="First Name",Value=""},
                new {Key="Mid Name",Value=""},
                new {Key="Last Name",Value=""},
                new {Key="Gender",Value=""},
                new {Key="Date of Birth",Value=""},
                new {Key="Address",Value=""},
                new {Key="City",Value=""},
                new {Key="Contact 1",Value=""},
                new {Key="Contact 2",Value=""}
            };            
        dataEntry.ItemsSource = dict;

        }
        private void addFunction(object sender, RoutedEventArgs e)
        { 
            // MessageBox.Show(dict["Address"]);
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
