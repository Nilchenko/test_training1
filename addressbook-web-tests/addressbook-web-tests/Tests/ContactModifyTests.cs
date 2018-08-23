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

            app.Contact.Modify(1, defaultData, newContactData);
        }

        [Test]
        public void ContactModifyTest()
        {
            ContactData newContactData = new ContactData("New1st", "New2nd");

            CheckContactExistAndModify(1, defaultData, newContactData);
        }
    }
}
