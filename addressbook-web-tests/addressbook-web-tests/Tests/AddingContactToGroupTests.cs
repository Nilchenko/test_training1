using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class AddingContactToGroupTests : AuthTestBase
    {
        GroupData defaultGroupData = new GroupData("defaultName");
        public ContactData defaultContactData = new ContactData("default1stName", "default2ndName");


        [Test]
        public void TestAddingContactToGroup()
        {
            List<GroupData> groups = GroupData.GetAll();
            if (groups.Count == 0)
            {
                app.Groups.Create(defaultGroupData);
            }

            List<ContactData> contacts = ContactData.GetAll();
            if(contacts.Count == 0)
            {
                app.Contact.AddContact(defaultContactData);
            }

            int groupsNum = GroupData.GetAll().Count;
            for(int i = 0; i <= groupsNum; i++)
            {
                GroupData group = GroupData.GetAll()[i];
                List<ContactData> oldList = group.GetContacts(); //список контактов, состоящих в группе
                List<ContactData> nonGroupContacts = ContactData.GetAll().Except(oldList).ToList(); //список контактов, не состоящих в выбранной группе
                //если список не пустой, то выполняет тест, если пустой, то идем к следующей группе
                ContactData contact = ContactData.GetAll().Except(oldList).First();

                app.Contact.AddContactToGroup(contact, group);

                List<ContactData> newList = group.GetContacts();
                oldList.Add(contact);
                oldList.Sort();
                newList.Sort();
                Assert.AreEqual(oldList, newList);

            }
        }
    }
}
