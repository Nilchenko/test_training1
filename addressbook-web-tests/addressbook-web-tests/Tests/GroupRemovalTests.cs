using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupRemovalTests : AuthTestBase
    {
        GroupData defaultData = new GroupData("defaultName");

        [Test]
        public void GroupRemovalTest()
        {
            app.Groups.Remove(1, defaultData);
        }
    }
}
