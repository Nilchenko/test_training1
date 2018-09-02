using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {

        public ContactData (string firstname, string lastname)
        {
            FirstName = firstname;
            LastName = lastname;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }

        public string HomePhone { get; set; }
        public string MobilePhone { get; set; }
        public string WorkPhone { get; set; }

        public string EMail { get; set; }
        public string EMail2 { get; set; }
        public string EMail3 { get; set; }

        public bool Equals(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }
            return FirstName == other.FirstName && LastName == other.LastName;
        }

        public override string ToString()
        {
            return "name = " + FirstName + ", surname = " + LastName;
        }

        public int CompareTo(ContactData other)
        {
            if (object.ReferenceEquals(other, null))
            {
                return 1;
            }

            if (LastName.CompareTo(other.LastName) == 0)
            {
                return FirstName.CompareTo(other.FirstName);
            }
            return LastName.CompareTo(other.LastName);
        }

        public override int GetHashCode()
        {
            return FirstName.GetHashCode() & LastName.GetHashCode();
        }

    }
}
