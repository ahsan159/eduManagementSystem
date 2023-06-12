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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        diary phoneDiary = new diary();
        person cPerson = null;
        public MainWindow()
        {
            InitializeComponent();
            phoneDiary.loadXml("diary.xml");
            cPerson = phoneDiary.First();
            display();
            List<person> pList = phoneDiary.getList();
            phoneTable.ItemsSource = pList;
        }
        private void display()
        {
            nameLabel.Content = cPerson.getName();
            addressLabel.Content = cPerson.getAddress();
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
        { }
        private void deleteContact(object sender, RoutedEventArgs e)
        { }
        private void saveContact(object sender, RoutedEventArgs e)
        { }
        private void printContact(object sender, RoutedEventArgs e)        
        { }
        
    }
}