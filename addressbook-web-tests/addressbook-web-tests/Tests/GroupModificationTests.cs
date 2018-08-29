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

        [Test]
        public void GroupModificationTest()
        {
            GroupData newData = new GroupData("ModifyGroupTest1");
            newData.Header = null;
            newData.Footer = null;

            app.Navigator.OpenGroupsPage();

            if (!app.Groups.GroupExist())
            {
                app.Groups.Create(defaultData);
            }

            List<GroupData> oldGroups = app.Groups.GetGroupList();
            GroupData oldData = oldGroups[0]; //копирование 0-го элемента

            app.Groups.Modify(0, defaultData, newData);

            //Проверка на сравнение количества элементов
            Assert.AreEqual(oldGroups.Count, app.Groups.GetGroupCount());

            List<GroupData> newGroups = app.Groups.GetGroupList();
            oldGroups[0].Name = newData.Name;
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);

            foreach (GroupData group in newGroups)
            {
                if (group.Id == oldData.Id)
                {
                    Assert.AreEqual(newData.Name, group.Name);
                }
            }
        }
    }
}
