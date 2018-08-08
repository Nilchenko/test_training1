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
            navigationHelper.OpenHomePage();
            loginHelper.Login(new AccountData ("admin", "secret"));
            AddContactPage();
            ContactData contact = new ContactData("1stName", "2ndName");
            FillContactForm(contact);
            SubmitAddContact();
            loginHelper.Logout();
        }
    }
}
