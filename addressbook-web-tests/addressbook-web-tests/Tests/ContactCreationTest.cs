using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;


namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : TestBase
    {
       [Test]
        public void ContactCreationTest()
        {
            app.Navigator.OpenHomePage();
            app.Auth.Login(new AccountData ("admin", "secret"));
            app.Contact.AddContactPage();
            ContactData contact = new ContactData("1stName", "2ndName");
            app.Contact.FillContactForm(contact);
            app.Contact.SubmitAddContact();
            app.Auth.Logout();
        }
    }
}
