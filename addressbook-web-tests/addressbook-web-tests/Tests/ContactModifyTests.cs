using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactModifyTests : AuthTestBase
    {
        public ContactData defaultData = new ContactData("default1stName", "default2ndName");

        public void CheckContactExistAndModify(int v, ContactData defaultData, ContactData newContactData)
        {
            if (!app.Contact.ContactExist())
            {
                app.Contact.AddContact(defaultData);
            }


            List<ContactData> oldContacts = app.Contact.GetContactList();

            app.Contact.Modify(v, defaultData, newContactData);


            List<ContactData> newContacts = app.Contact.GetContactList();
            oldContacts[v].FirstName = newContactData.FirstName;
            oldContacts[v].LastName = newContactData.LastName;
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);

        }

        [Test]
        public void ContactModifyTest()
        {
            ContactData newContactData = new ContactData("New1st", "New2nd");

            //List<ContactData> oldContacts = app.Contact.GetContactList();

            CheckContactExistAndModify(0, defaultData, newContactData);

            //List<ContactData> newContacts = app.Contact.GetContactList();
            //oldContacts[0].FirstName = newContactData.FirstName;
            //oldContacts[0].LastName = newContactData.LastName;
            //oldContacts.Sort();
            //newContacts.Sort();
            //Assert.AreEqual(oldContacts, newContacts);

        }
    }
}
