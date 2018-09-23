using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB.Mapping;

namespace WebAddressbookTests
{
    [Table(Name = "group_list")] //привязка к таблице БД
    public class GroupData : IEquatable<GroupData>, IComparable<GroupData>
    {

        public GroupData()
        {
        }

        public GroupData(string name)
        {
            Name = name;
        }

        [Column(Name = "group_name")] //привязка с столбцу
        //так поля создаются автоматически:
        public string Name { get; set; }

        [Column(Name = "group_header")]
        public string Header { get; set; }

        [Column(Name = "group_footer")]
        public string Footer { get; set; }

        [Column(Name = "group_id"), PrimaryKey, Identity]
        public string Id { get; set; }


        public bool Equals(GroupData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }
            return Name == other.Name;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override string ToString()
        {
            return "name = " + Name + "\nheader = " + Header + "\nfooter = " + Footer;
        }

        public int CompareTo(GroupData other)
        {
            if (object.ReferenceEquals(other, null))
            {
                return 1;
            }
            return Name.CompareTo(other.Name);
        }

        public static List<GroupData> GetAll()
        {
            //AddressBookDB db = new AddressBookDB(); //установка соединения

            using (AddressBookDB db = new AddressBookDB()) //устанавливается соединение и после выполения кода автоматически закрывается
            {
                return (from g in db.Groups select g).ToList(); //возвращает список из бд

            }
            //db.Close();
        }

        public List<ContactData> GetContacts()
        {
            using (AddressBookDB db = new AddressBookDB()) //устанавливается соединение и после выполения кода автоматически закрывается
            {
                return (from c in db.Contacts
                        from gcr in db.GCR.Where(p => p.GroupId == Id && p.ContactId == c.Id)
                                 select c).Distinct().ToList(); //возвращает список из бд

            }

        }
    }
}
