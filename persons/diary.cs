using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
//using persons;


namespace persons
{
    public class diary
    {
        List<person> personList = new List<person>();
        public diary()
        {

        }
        public void add(person _p)
        {
            personList.Add(_p);
        }
        public void remove(person _p)
        {
            personList.Remove(_p);
        }
        public XElement xml()
        {
            XElement ele = new XElement("phonebook");
            foreach(person p in personList)
            {
                ele.Add(p.xml());
            }
            return ele;
        }
    }
}