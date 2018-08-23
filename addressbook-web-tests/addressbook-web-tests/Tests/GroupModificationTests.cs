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
            app.Groups.Modify(v, defaultData, newData);
        }


        [Test]
        public void GroupModificationTest()
        {
            GroupData newData = new GroupData("ModifyGroupTest1");
            newData.Header = null;
            newData.Footer = null;

            CheckGroupExistsAndModify(1, defaultData, newData);
        }
    }
}
