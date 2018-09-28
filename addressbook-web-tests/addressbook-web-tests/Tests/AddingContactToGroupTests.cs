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
        ContactData defaultContactData = new ContactData("default1stName", "default2ndName");


        [Test]
        public void TestAddingContactToGroup()
        {
            List<GroupData> groups = GroupData.GetAll();
            //Создание группы, если групп нет
            if (groups.Count == 0)
            {
                app.Groups.Create(defaultGroupData);
                groups = GroupData.GetAll();
            }

            //Создание контакта, если контактов нет
            List<ContactData> contacts = ContactData.GetAll();
            if(contacts.Count == 0)
            {
                app.Contact.AddContact(defaultContactData);
                contacts = ContactData.GetAll();

            }

            GroupData group = groups[0];
            List<ContactData> oldList = group.GetContacts(); //список контактов, состоящих в группе

            //проверка на наличие контакта, не состоящего в группе и создание, если такого нет
            if (oldList.Count == contacts.Count)
            {
                app.Contact.AddContact(defaultContactData);
                contacts = ContactData.GetAll();
            }

            //ContactData contact = ContactData.GetAll().Except(oldList).First();
            ContactData contact = contacts.Except(oldList).First();

            app.Contact.AddContactToGroup(contact, group);

            List<ContactData> newList = group.GetContacts();
            oldList.Add(contact);
            oldList.Sort();
            newList.Sort();
            Assert.AreEqual(oldList, newList);

        }
    }
}
