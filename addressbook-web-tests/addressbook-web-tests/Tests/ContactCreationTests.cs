using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using NUnit.Framework;


namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : AuthTestBase
    {
        public static IEnumerable<ContactData> RandomContactDataProvider()
        {
            List<ContactData> contacts = new List<ContactData>();
            for (int i = 0; i < 3; i++)
            {
                contacts.Add(new ContactData(GenerateRandomString(10), GenerateRandomString(10))
                {
                    MiddleName = GenerateRandomString(10),
                    HomePhone = GenerateRandomString(6),
                    EMail = GenerateRandomString(10)
                });
            }
            return contacts;
        }

       [Test]
        public void ContactCreationTest()
        {
            ContactData contact = new ContactData("1stName", "2ndNameTest");
            contact.EMail = "qwerty@qwerty.ru";

            List<ContactData> oldContacts = app.Contact.GetContactList();

            app.Contact.AddContact(contact);

            List<ContactData> newContacts = app.Contact.GetContactList();
            oldContacts.Add(contact);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }

        [Test, TestCaseSource("RandomContactDataProvider")]
        public void RandomContactCreationTest(ContactData contact)
        {
            //ContactData contact = new ContactData("1stName", "2ndNameTest");
            //contact.EMail = "qwerty@qwerty.ru";

            List<ContactData> oldContacts = app.Contact.GetContactList();

            app.Contact.AddContact(contact);

            List<ContactData> newContacts = app.Contact.GetContactList();
            oldContacts.Add(contact);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }

        [Test]
        public void EmptyContactCreationTest()
        {
            ContactData contact = new ContactData("", "");

            List<ContactData> oldContacts = app.Contact.GetContactList();

            app.Contact.AddContact(contact);

            List<ContactData> newContacts = app.Contact.GetContactList();
            oldContacts.Add(contact);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}
