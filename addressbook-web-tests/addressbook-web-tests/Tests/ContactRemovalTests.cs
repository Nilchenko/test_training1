using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactRemovalTests : AuthTestBase
    {
        public ContactData defaultData = new ContactData("default1stName", "default2ndName");



        [Test]
        public void ContactRemovalTest()
        {
            if (!app.Contact.ContactExist())
            {
                app.Contact.AddContact(defaultData);
            }

            List<ContactData> oldContacts = app.Contact.GetContactList();

            app.Contact.Removal(0, defaultData);

            List<ContactData> newContacts = app.Contact.GetContactList();

            oldContacts.RemoveAt(0);
            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}
