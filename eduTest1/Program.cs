// See https://aka.ms/new-console-template for more information
using System.Xml;
using System.Xml.Linq;
using persons;

Console.WriteLine("Hello, World!");
person p = new person("Muhammad","Ahsan","");
p.setGender(genderType.Male);
DateTime dt = new DateTime(1987,12,21);
p.setDOB(dt);
p.setEmail("mahsan159@live.com");
p.setAddress("254-A, Alhmad Colony, AIT");
p.setCity("Lahore");
Console.WriteLine(p.ToString());
Console.WriteLine(p.getDOB().ToString());
Console.WriteLine(p.JSON());
Console.WriteLine(p.xml().ToString());

diary d = new diary();
d.add(p);

person p2 = new person("Muhammad","Shahid","Ehsan");
p2.setGender(genderType.Male);
DateTime dt1 =new DateTime(1983,10,5);
p2.setDOB(dt1);
p2.setEmail("ehsan.shahid@gmail.com");
p2.setAddress("Muslim Town");
p2.setCity("Lahore");
p2.addContact("0321-5638140");
p2.addContact("042-99024248");
d.add(p2);
XElement ele = d.xml();
XDocument doc = new XDocument();
doc.Add(ele);
doc.Save("diary.xml");