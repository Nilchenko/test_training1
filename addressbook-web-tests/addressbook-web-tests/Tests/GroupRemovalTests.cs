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

        //[SetUp]
        public void CheckGroupExistsAndRemove(GroupData defaultData)
        {
            app.Navigator.OpenGroupsPage();
            if (!app.Groups.GroupExist())
            {
                app.Groups.Create(defaultData);
            }
            app.Groups.Remove(1, defaultData);
        }

        [Test]
        public void GroupRemovalTest()
        {
            CheckGroupExistsAndRemove(defaultData);
        }
    }
}
