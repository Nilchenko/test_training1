using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupRemovalTests : AuthTestBase
    {
        GroupData defaultData = new GroupData("defaultName");

        public void CheckGroupExistsAndRemove(GroupData defaultData)
        {
            app.Navigator.OpenGroupsPage();
            if (!app.Groups.GroupExist())
            {
                app.Groups.Create(defaultData);
            }

            List<GroupData> oldGroups = app.Groups.GetGroupList();

            app.Groups.Remove(0, defaultData);

            List<GroupData> newGroups = app.Groups.GetGroupList();

            oldGroups.RemoveAt(0);
            Assert.AreEqual(oldGroups, newGroups);

        }

        [Test]
        public void GroupRemovalTest()
        {
            CheckGroupExistsAndRemove(defaultData);
        }
    }
}
