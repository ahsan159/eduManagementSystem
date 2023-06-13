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
        public class stringWrapper
        {
            public string value {get;set;}
        }
        List<stringWrapper> wrapper = new List<stringWrapper>();
        stringWrapper wfirstName = new stringWrapper(){value="firstName"};
            //wfirstName.value="firstName";
        public addnedit()
        {
            InitializeComponent();
            //dataEntry.ItemsSource = wrapper;                     
            wrapper.Add(wfirstName);
        }
        private void addFunction(object sender, RoutedEventArgs e)
        { }
        private void cancelFunction(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
