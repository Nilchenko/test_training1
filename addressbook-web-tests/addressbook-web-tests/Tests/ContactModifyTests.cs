using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactModifyTests : TestBase
    {
        [Test]
        public void ContactModifyTest()
        {
            ContactData newContactData = new ContactData("New1st", "New2nd");

            app.Contact.Modify(1, newContactData);
        }
    }
}
