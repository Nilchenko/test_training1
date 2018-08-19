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
            app.Contact.Removal(1, defaultData);
        }
    }
}
