using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupModificationTests : GroupTestBase
    {
        GroupData defaultData = new GroupData("defaultName");

        [Test]
        public void GroupModificationTest()
        {
            GroupData newData = new GroupData("_ModifyGroupTest1");
            newData.Header = null;
            newData.Footer = null;

            app.Navigator.OpenGroupsPage();

            if (!app.Groups.GroupExist())
            {
                app.Groups.Create(defaultData);
            }

            List<GroupData> oldGroups = GroupData.GetAll();
            GroupData toBeModificated = oldGroups[0]; //копирование 0-го элемента

            app.Groups.Modify(toBeModificated, newData);

            //Проверка на сравнение количества элементов
            Assert.AreEqual(oldGroups.Count, app.Groups.GetGroupCount());

            List<GroupData> newGroups = GroupData.GetAll();
            toBeModificated.Name = newData.Name;
            //oldGroups.Sort();
            //newGroups.Sort();
            //Assert.AreEqual(oldGroups, newGroups);

            foreach (GroupData group in newGroups)
            {
                if (group.Id == toBeModificated.Id)
                {
                    Assert.AreEqual(newData.Name, group.Name);
                }
            }
        }
    }
}
