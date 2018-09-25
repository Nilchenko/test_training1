using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace WebAddressbookTests
{
    public class GroupHelper : HelperBase
    {

        public GroupHelper(ApplicationManager manager) : base(manager)
        {
        }

         public GroupHelper Create(GroupData group)
        {
            manager.Navigator.OpenGroupsPage();

            InitGroupCreation();
            FillGroupForm(group);
            SubmitGroupCreation();
            ReturnToGroupsPage();
            return this;
        }


        //для модификации объекта по индексу, полученному через UI
        public GroupHelper Modify(int v, GroupData newGroupData)
        {
            manager.Navigator.OpenGroupsPage();

            SelectGroup(v);
            InitGroupModification();
            FillGroupForm(newGroupData);
            SubmitGroupModification();
            ReturnToGroupsPage();
            return this;
        }

        //для модификации объекта из списка, полученного из бд
        public GroupHelper Modify(GroupData group, GroupData newGroupData)
        {
            manager.Navigator.OpenGroupsPage();

            SelectGroup(group.Id);
            InitGroupModification();
            FillGroupForm(newGroupData);
            SubmitGroupModification();
            ReturnToGroupsPage();
            return this;
        }

        //для удаления объекта из списка, полученного из бд
        public GroupHelper Remove(GroupData group)
        {
            manager.Navigator.OpenGroupsPage();

            SelectGroup(group.Id);
            RemoveGroup();
            ReturnToGroupsPage();
            return this;
        }

        //для удаления объекта по индексу, полученному через UI
        public GroupHelper Remove(int v)
        {
            manager.Navigator.OpenGroupsPage();

            SelectGroup(v);
            RemoveGroup();
            ReturnToGroupsPage();
            return this;
        }


        public bool GroupExist()
        {
            return IsElementPresent(By.Name("selected[]"));
        }

        public GroupHelper InitGroupCreation()
        {
            driver.FindElement(By.Name("new")).Click();
            return this;
        }

        public GroupHelper FillGroupForm(GroupData group)
        {
            Type(By.Name("group_name"), group.Name);
            Type(By.Name("group_header"), group.Header);
            Type(By.Name("group_footer"), group.Footer);
            //driver.FindElement(By.Name("group_header")).Clear();
            return this;
        }


        public GroupHelper SubmitGroupCreation()
        {
            driver.FindElement(By.Name("submit")).Click();
            groupCache = null;
            return this;
        }


        public GroupHelper ReturnToGroupsPage()
        {
            driver.FindElement(By.LinkText("group page")).Click();
            return this;
        }

        //выбор группы через индекс объекта в UI
        public GroupHelper SelectGroup(int index)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + (index+1) + "]")).Click();
            return this;
        }

        //выбор группы через id объекта в бд
        public GroupHelper SelectGroup(string id)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]' and @value = '"+id+"'])")).Click();
            return this;
        }


        public GroupHelper RemoveGroup()
        {
            driver.FindElement(By.Name("delete")).Click();
            groupCache = null;
            return this;
        }

        public GroupHelper SubmitGroupModification()
        {
            driver.FindElement(By.Name("update")).Click();
            groupCache = null;
            return this;
        }

        public GroupHelper InitGroupModification()
        {
            driver.FindElement(By.Name("edit")).Click();
            return this;
        }

        private List<GroupData> groupCache = null;

        public List<GroupData> GetGroupList()
        {
            if (groupCache == null)
            {
                groupCache = new List<GroupData>();

                manager.Navigator.OpenGroupsPage();

                ICollection<IWebElement> elements = driver.FindElements(By.CssSelector("span.group"));

                foreach (IWebElement element in elements)
                {
                    groupCache.Add(new GroupData(null)
                    {
                        Id = element.FindElement(By.TagName("input")).GetAttribute("value")
                    });
                }

                string allGroupNames = driver.FindElement(By.CssSelector("div#content form")).Text;
                string [] parts = allGroupNames.Split('\n'); //Разрезает строку на несколько строк по разделителю "перевод строки"
                int shift = groupCache.Count - parts.Length;
                for (int i = 0; i < groupCache.Count; i++)
                {
                    if(i < shift)
                    {
                        groupCache[i].Name = "";
                    }
                    else
                    {
                        groupCache[i].Name = parts[i-shift].Trim();

                    }
                }
            }

            return new List<GroupData>(groupCache); //возвращаем копию кэша
        }

        public int GetGroupCount()
        {
            return driver.FindElements(By.CssSelector("span.group")).Count;
        }

    }
}
