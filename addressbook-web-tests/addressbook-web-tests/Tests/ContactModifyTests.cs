using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactModifyTests : ContactTestBase
    {
        public ContactData defaultData = new ContactData("default1stName", "default2ndName");


        [Test]
        public void ContactModifyTest()
        {
            if (!app.Contact.ContactExist())
            {
                app.Contact.AddContact(defaultData);
            }

            ContactData newContactData = new ContactData("New1st", "New2nd");

            List<ContactData> oldContacts = ContactData.GetAll();
            ContactData toBeModificated = oldContacts[0];

            app.Contact.Modify(toBeModificated, newContactData);


            //List<ContactData> newContacts = app.Contact.GetContactList();
            //oldContacts[0].FirstName = newContactData.FirstName;
            //oldContacts[0].LastName = newContactData.LastName;
            //oldContacts.Sort();
            //newContacts.Sort();
            //Assert.AreEqual(oldContacts, newContacts);

        }
    }
}
