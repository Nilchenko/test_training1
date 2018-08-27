using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupModificationTests : AuthTestBase
    {
        GroupData defaultData = new GroupData("defaultName");

        public void CheckGroupExistsAndModify(int v, GroupData defaultData, GroupData newData)
        {
            app.Navigator.OpenGroupsPage();
            if (!app.Groups.GroupExist())
            {
                app.Groups.Create(defaultData);
            }

            List<GroupData> oldGroups = app.Groups.GetGroupList();

            app.Groups.Modify(v, defaultData, newData);

            List<GroupData> newGroups = app.Groups.GetGroupList();
            oldGroups[0].Name = newData.Name;
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);

        }


        [Test]
        public void GroupModificationTest()
        {
            GroupData newData = new GroupData("ModifyGroupTest1");
            newData.Header = null;
            newData.Footer = null;

            CheckGroupExistsAndModify(0, defaultData, newData);
        }
    }
}
