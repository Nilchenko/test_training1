using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace WebAddressbookTests
{
    [TestFixture]
    public class RemoveContactFromGroupTests : AuthTestBase
    {
        GroupData defaultGroupData = new GroupData("defaultName");
        ContactData defaultContactData = new ContactData("default1stName", "default2ndName");


        [Test]
        public void RemoveContactFromGroupTest()
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
            if (contacts.Count == 0)
            {
                app.Contact.AddContact(defaultContactData);
                contacts = ContactData.GetAll();
            }


            GroupData group = groups[0]; //выбор группы
            List<ContactData> oldList = group.GetContacts();

            //добавление контакта в группу, если группа пустая
            if(oldList.Count == 0)
            {
                ContactData contactToAdd = contacts[0];
                app.Contact.AddContactToGroup(contactToAdd, group);
                oldList = group.GetContacts();
            }
            ContactData contact = oldList[0]; 
                        
            app.Contact.RemoveContactFromGroup(contact, group);

            List<ContactData> newList = group.GetContacts();
            oldList.RemoveAt(0);
            oldList.Sort();
            newList.Sort();
            Assert.AreEqual(oldList, newList);

        }

    }
}
