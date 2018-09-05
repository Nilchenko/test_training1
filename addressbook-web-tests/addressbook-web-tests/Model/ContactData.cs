using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace WebAddressbookTests
{
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        private string allPhones;
        private string allEMails;
        public string allDetails;

        public ContactData (string firstname, string lastname)
        {
            FirstName = firstname;
            LastName = lastname;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Nickname { get; set; }
        public string Company { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }

        public string HomePhone { get; set; }
        public string MobilePhone { get; set; }
        public string WorkPhone { get; set; }
        public string AllPhones
        {
            get
            {
                if (allPhones != null)
                {
                    return allPhones;
                }
                else
                {
                    return CleanUpPhone(HomePhone) + CleanUpPhone(MobilePhone) + CleanUpPhone(WorkPhone).Trim();
                }
            }

            set
            {
                allPhones = value;
            }
        }

        public string EMail { get; set; }
        public string EMail2 { get; set; }
        public string EMail3 { get; set; }
        public string AllEMails
        {
            get
            {
                if (allEMails != null)
                {
                    return allEMails;
                }
                else
                {
                    return CleanUp(EMail) + CleanUp(EMail2) + CleanUp(EMail3).Trim();
                }
            }
            set
            {
                allEMails = value;
            }
        }

        public string AllDetails
        {
            get
            {
                if (allDetails != null)
                {
                    return Regex.Replace(allDetails, "(\r\n)+", "\r\n");
                    //return allDetails;
                }
                else
                {
                    return CleanUpSpace(FirstName) + CleanUpSpace(MiddleName) + CleanUp(LastName) +
                        CleanUp(Nickname) + CleanUp(Title) + CleanUp(Company) + CleanUp(Address) + 
                        "H: " + CleanUp(HomePhone) + "M: " + CleanUp(MobilePhone) + "W: " + CleanUp(WorkPhone) + 
                        CleanUp(EMail) + CleanUp(EMail2) + CleanUp(EMail3).Trim();
                }
            }
            set
            {
                allDetails = value;
            }
        }


        private string CleanUpPhone(string phone)
        {
            if (phone == null || phone == "")
            {
                return "";
            }
            //return phone.Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "") + "\r\n";
            return Regex.Replace(phone, "[- ()]", "") + "\r\n"; // (где, что, на что)
        }

        private string CleanUp(string text)
        {
            if (text == null || text == "")
            {
                return "";
            }
           
            return text + "\r\n"; 
        }

        private string CleanUpSpace(string text)
        {
            if (text == null || text == "")
            {
                return "";
            }

            return text + " ";
        }



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
