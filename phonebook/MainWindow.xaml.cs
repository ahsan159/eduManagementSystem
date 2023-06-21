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
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
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
            if (updateBox.closeStatus)
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
            //XElement ele = cPerson.xml();
            XDocument doc = new XDocument();
            //doc.Add(ele);
            if (File.Exists("printContact.html"))
            {
                File.Delete("printContact.html");
            }
            XElement html = new XElement("html");
            XElement head = new XElement("head");
            XElement title = new XElement("title");
            title.Value = "Contact Preview";
            head.Add(title);
            XElement style = new XElement("style");
            style.Value = @"        .top {
            display: flex;
            margin-bottom: 10px;
            height: 200px;
            }
            .picture, .name {
                margin-right: 20px;                                
            }
            .picture {
                display: flex;
                justify-content: center;
            }
            .name {
                padding-top: 35px;
                display: block;
                width: 100%;
            }
            .middle {                
                display: flex;
                justify-content: left;
            }
            .headers{
                width: 10%;                
                margin-left: 20px;
            }
            .data
            {
                width: 80%;                
            }";
            head.Add(style);
            html.Add(head);

            XElement body = new XElement("body");

            XElement top = new XElement("div");
            top.Add(new XAttribute("class", "top"));

            XElement picdiv = new XElement("div");
            picdiv.Add(new XAttribute("class", "picture"));

            XElement img = new XElement("img");
            img.Add(new XAttribute("src", "personempty.png"));
            img.Add(new XAttribute("alt", "person picture"));
            picdiv.Add(img);

            XElement name = new XElement("div");
            name.Add(new XAttribute("class", "name"));
            XElement pname = new XElement("h1");
            pname.Value = cPerson.getName();
            name.Add(pname);
            XElement contact = new XElement("h3");
            contact.Value = cPerson.getContact();
            name.Add(contact);

            top.Add(picdiv);
            top.Add(name);

            body.Add(top);

            XElement middle = new XElement("div");
            middle.Add(new XAttribute("class","middle"));
            XElement headers = new XElement("div");
            headers.Add(new XAttribute("class", "headers"));

            XElement n1 = new XElement("h3");
            n1.Value = "Name";

            XElement n2 = new XElement("h3");
            n2.Value = "Gender";

            XElement n3 = new XElement("h3");
            n3.Value = "Address";

            XElement n4 = new XElement("h3");
            n4.Value = "City";

            XElement n5 = new XElement("h3");
            n5.Value = "Contact 0";

            XElement n6 = new XElement("h3");
            n6.Value = "Contact 1";

            XElement n7 = new XElement("h3");
            n7.Value = "Email";

            XElement n8 = new XElement("h3");
            n8.Value = "Date of Birth";

            // XElement n9 = new XElement("h3");
            // n9.Value = "Name";

            headers.Add(n1);
            headers.Add(n2);
            headers.Add(n3);
            headers.Add(n4);
            headers.Add(n5);
            headers.Add(n6);
            headers.Add(n7);
            headers.Add(n8);

            middle.Add(headers);

            XElement data = new XElement("div");
            data.Add(new XAttribute("class", "data"));

            XElement o1 = new XElement("h3");
            o1.Value = cPerson.getName() + " ";

            XElement o2 = new XElement("h3");
            o2.Value = cPerson.getGender().ToString() + " ";

            XElement o3 = new XElement("h3");
            o3.Value = cPerson.getAddress() + " ";

            XElement o4 = new XElement("h3");
            o4.Value = cPerson.getCity() + " ";

            XElement o5 = new XElement("h3");
            o5.Value = cPerson.getContact0() + " ";

            XElement o6 = new XElement("h3");
            o6.Value = cPerson.getContact1() + " ";

            XElement o7 = new XElement("h3");
            o7.Value = cPerson.getEmail() + " ";

            XElement o8 = new XElement("h3");
            o8.Value = cPerson.getDOB().ToString("dd/MM/yyyy") + " Age " + cPerson.getAge() + " ";

            // XElement n9 = new XElement("h3");
            // n9.Value = "Name";

            data.Add(o1);
            data.Add(o2);
            data.Add(o3);
            data.Add(o4);
            data.Add(o5);
            data.Add(o6);
            data.Add(o7);
            data.Add(o8);

            middle.Add(data);

            body.Add(middle);
            html.Add(body);

            //MessageBox.Show( html.ToString());
            File.WriteAllText("printContact.html",html.ToString());

            // FileStream htmlFile = File.Open("printContact.html",FileMode.Open);
            // FileStream pdfFile = File.Open("pdfContact.pdf",FileMode.Create);
            // ConverterProperties cvp = new ConverterProperties();
            // try {
            // HtmlConverter.ConvertToPdf(htmlFile,pdfFile,cvp);
            // }
            // catch(Exception ee)
            // {
            //     MessageBox.Show(ee.Message+Environment.NewLine+ee.StackTrace,ee.Source );
            // }
            // htmlFile.Close();
            // pdfFile.Close();
            // htmlFile.Dispose();
            // pdfFile.Dispose();
            
            

        }

    }
}