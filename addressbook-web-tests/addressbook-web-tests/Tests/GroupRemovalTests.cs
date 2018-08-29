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

        [Test]
        public void GroupRemovalTest()
        {
            app.Navigator.OpenGroupsPage();
            if (!app.Groups.GroupExist())
            {
                app.Groups.Create(defaultData);
            }

            List<GroupData> oldGroups = app.Groups.GetGroupList();

            app.Groups.Remove(0, defaultData);

            //Проверка на сравнение количества элементов
            Assert.AreEqual(oldGroups.Count - 1, app.Groups.GetGroupCount());

            List<GroupData> newGroups = app.Groups.GetGroupList();

            GroupData toBeRemoved = oldGroups[0];

            oldGroups.RemoveAt(0);
            Assert.AreEqual(oldGroups, newGroups);

            foreach ( GroupData group in newGroups)
            {
                //проверка на то, что ид удаленного элемента не совпадает со всеми остальными
                Assert.AreNotEqual(group.Id, toBeRemoved.Id); 
            }

        }
    }
}
