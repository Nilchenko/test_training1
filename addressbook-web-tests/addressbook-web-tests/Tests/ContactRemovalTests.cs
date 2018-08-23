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

        public void CheckContactExistsAndRemove(int v, ContactData defaultData)
        {
            if (!app.Contact.ContactExist())
            {
                app.Contact.AddContact(defaultData);
            }

            app.Contact.Removal(1, defaultData);

        }

        [Test]
        public void ContactRemovalTest()
        {
            CheckContactExistsAndRemove(1, defaultData);
        }
    }
}
