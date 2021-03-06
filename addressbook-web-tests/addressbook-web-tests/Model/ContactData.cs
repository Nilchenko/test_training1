﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using LinqToDB.Mapping;

namespace WebAddressbookTests
{
    [Table(Name = "addressbook")]
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        private string allPhones;
        private string allEMails;
        public string allDetails;

        public ContactData()
        {
        }

        public ContactData (string firstname, string lastname)
        {
            FirstName = firstname;
            LastName = lastname;
        }


        [Column(Name = "id"), PrimaryKey]
        public string Id { get; set; }
        
        [Column(Name = "firstname")]
        public string FirstName { get; set; }

        [Column(Name = "lastname")]
        public string LastName { get; set; }

        public string MiddleName { get; set; }
        public string AllNames
        {
            get
            {
                return Regex.Replace((FirstName + " " + MiddleName + " " + LastName), " +", " ").Trim();
            }
            set
            {
            }
        }

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
                    return NewLine(EMail) + NewLine(EMail2) + NewLine(EMail3).Trim();
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
                }
                else
                {
                    return $"{ NewLine(AllNames)}{ NewLine(Nickname)}{ NewLine(Title)}{ NewLine(Company)}{ NewLine(Address)}{ PhoneString("H", HomePhone)}{ PhoneString("M", MobilePhone)}{ PhoneString("W", WorkPhone)}{ NewLine(EMail)}{ NewLine(EMail2)}{ NewLine(EMail3)}".Trim();

                }
            }
            set
            {
                allDetails = value;
            }
        }

        [Column(Name = "deprecated")]
        public string Deprecated { get; set; }


        //методы приведения строк
        private string PhoneString (string prefix, string phone)
        {
            return (string.IsNullOrEmpty(phone)) ? "" : $"{prefix}: {phone}\r\n";
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

        private string NewLine(string text)
        {
            if (text == null || text == "")
            {
                return "";
            }
           
            return text + "\r\n"; 
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
            return FirstName == other.FirstName && LastName == other.LastName && Id == other.Id;
        }

        public override string ToString()
        {
            return $"name = {FirstName}\nmiddlename = {MiddleName}\nsurname = {LastName}\nid = {Id}";
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

        public static List<ContactData> GetAll()
        {
            using (AddressBookDB db = new AddressBookDB()) //устанавливается соединение и после выполения кода автоматически закрывается
            {
                return (from c in db.Contacts.Where(x => x.Deprecated == "0000-00-00 00:00:00") select c).ToList(); //возвращает список контактов, у которыйх в Deprecated указанное значение

            }
        }
    }
}
