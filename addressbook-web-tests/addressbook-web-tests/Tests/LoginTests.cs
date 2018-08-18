using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace WebAddressbookTests
{
    [TestFixture]
    public class LoginTests : TestBase
    {

        [Test]
        public void LoginWithValidCredentials()
        {
            app.Auth.Logout();
            AccountData accountData = new AccountData("admin", "secret");
            app.Auth.Login(accountData);
            Assert.IsTrue(app.Auth.IsLoggedIn(accountData));
        }

        [Test]
        public void LoginWithInvalidCredentials()
        {
            app.Auth.Logout();
            AccountData accountData = new AccountData("admin", "qwerty");
            app.Auth.Login(accountData);
            Assert.IsFalse(app.Auth.IsLoggedIn(accountData));
        }
    }
}
