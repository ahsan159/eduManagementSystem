namespace persons
{
    public enum genderType
    {
        Male,
        Female,
        Other
    }
    public class person
    {
        private string lastName;
        private string firstName;
        private string midName;
        private DateTime dob;
        private DateTime added;
        private int age;
        private string address;
        private string city;
        private string[] contact = new string[3];
        private string name;
        private genderType gender;
        private string picture;
        public person(string fName, string lName, string mName)
        {
            lastName = lName;
            firstName = fName;
            midName = mName;
            added = DateTime.Now;
            name = string.Format("{0} {1} {2}",
            arg0: firstName,
            arg1: midName,
            arg2: lastName);
            gender = genderType.Other;
            address = string.Empty;
            //contact = new string[3];
            //contact[0] = null;
            dob = DateTime.MinValue;
            //gender = ;
            city = string.Empty;
        }

        public person(string fName, string lName, string mName, genderType _gender, string _address, string _city, string _phone, DateTime _DateofBirth, string _picture)
        {
            lastName = lName;
            firstName = fName;
            midName = mName;
            added = DateTime.Now;
            name = string.Format("{0} {1} {2}",
            arg0: firstName,
            arg1: midName,
            arg2: lastName);
            address = _address;
            //contact = new string[3];
            contact[0] = _phone;
            dob = _DateofBirth;
            gender = _gender;
            city = _city;
            age = dob.Year - DateTime.Now.Year;// this age calculation needs improvement
            picture = _picture;
        }
        public string getName()
        {
            return name;
        }
        public void setAddress(string Address)
        {
            address = Address;
        }
        public string getAddress()
        {
            return address;
        }
        public void addContact(string _contact)
        {
            if (contact[0].Length == 0)
            {
                contact[0] = _contact;
            }
            else if (contact[1].Length == 0)
            {

                contact[1] = _contact;
            }
        }
        public void removeContact(string _contact)
        {
            if (contact[0].Equals(_contact))
            {
                contact[0] = contact[1];
                contact[1] = string.Empty;
            }
            else if (contact[1].Equals(_contact))
            {
                contact[1] = string.Empty;
            }
        }
    }
}