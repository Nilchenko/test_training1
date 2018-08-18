using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;


namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : AuthTestBase
    {
       [Test]
        public void ContactCreationTest()
        {
            ContactData contact = new ContactData("test1stName", "2ndName");

            app.Contact.AddContact(contact);
        }

        [Test]
        public void EmptyContactCreationTest()
        {
            ContactData contact = new ContactData("", "");

            app.Contact.AddContact(contact);
        }
    }
}
