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
    public class ContactHelper : HelperBase
    {
        public ContactHelper(ApplicationManager manager) : base(manager)
        {
        }

        public ContactHelper AddContact(ContactData contact)
        {
            manager.Navigator.OpenHomePage();

            AddContactPage();
            FillContactForm(contact);
            SubmitAddContact();

            manager.Navigator.OpenHomePage();
            return this;
        }

        public ContactHelper Modify(int v, ContactData defaultData, ContactData newContactData)
        {
            manager.Navigator.OpenHomePage();

            InitContactModify(v);
            FillContactForm(newContactData);
            SubmitContactModify();

            manager.Navigator.OpenHomePage();
            return this;
        }

        public ContactHelper Removal(int v, ContactData defaultData)
        {
            manager.Navigator.OpenHomePage();

            SelectContact(v);
            RemoveContact();
            DriverAlert();

            manager.Navigator.OpenHomePage();
            return this;
        }



        public bool ContactExist()
        {
            return IsElementPresent(By.Name("selected[]"));
        }

        public ContactHelper AddContactPage()
        {
            driver.FindElement(By.LinkText("add new")).Click();
            return this;

        }

        public ContactHelper FillContactForm(ContactData contact)
        {
            Type(By.Name("firstname"), contact.FirstName);
            Type(By.Name("lastname"), contact.LastName);
            return this;
        }

        public ContactHelper SubmitAddContact()
        {
            driver.FindElement(By.Name("submit")).Click();
            contactCache = null;
            return this;
        }

        public ContactHelper InitContactModify(int index)
        {
            //driver.FindElement(By.XPath("(//img[@alt='Edit'])[" + (index + 1) + "]")).Click();
            driver.FindElements(By.Name("entry"))[index].
                FindElements(By.TagName("td"))[7].
                FindElement(By.TagName("a")).Click();
            return this;
        }

        public ContactHelper SubmitContactModify()
        {
            driver.FindElement(By.Name("update")).Click();
            contactCache = null;
            return this;
        }

        public ContactHelper SelectContact(int index)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + (index + 1) + "]")).Click();
            return this;
        }

        public ContactHelper RemoveContact()
        {
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            contactCache = null;
            return this;
        }

        public ContactHelper DriverAlert()
        {
            driver.SwitchTo().Alert().Accept();
            return this;
        }

        private List<ContactData> contactCache = null;

        public List<ContactData> GetContactList()
        {
            if (contactCache == null)
            {
                contactCache = new List<ContactData>();

                manager.Navigator.OpenHomePage();

                ICollection<IWebElement> elements = driver.FindElements(By.CssSelector("tr[name='entry']"));

                foreach (IWebElement element in elements)
                {
                    string firstName = element.FindElement(By.XPath("td[3]")).Text;
                    string lastName = element.FindElement(By.XPath("td[2]")).Text;
                    contactCache.Add(new ContactData(firstName, lastName));
                }

            }
            return new List<ContactData>(contactCache); //Возвращаем копию кэша
        }


        public ContactData GetContactInformationFromTable(int index)
        {
            throw new NotImplementedException();
        }

        public ContactData GetContactInformationFromEditForm(int index)
        {
            manager.Navigator.OpenHomePage();
            InitContactModify(index);

            string firstName = driver.FindElement(By.Name("firstname")).GetAttribute("value");
            string lastName = driver.FindElement(By.Name("lastname")).GetAttribute("value");
            string address = driver.FindElement(By.Name("address")).GetAttribute("value");

            string homePhone = driver.FindElement(By.Name("home")).GetAttribute("value");
            string mobilePhone = driver.FindElement(By.Name("mobile")).GetAttribute("value");
            string workPhone = driver.FindElement(By.Name("work")).GetAttribute("value");

            string eMail = driver.FindElement(By.Name("email")).GetAttribute("value");
            string eMail2 = driver.FindElement(By.Name("email2")).GetAttribute("value");
            string eMail3 = driver.FindElement(By.Name("email3")).GetAttribute("value");

            return new ContactData(firstName, lastName)
            {
                Address = address,
                HomePhone = homePhone,
                MobilePhone = mobilePhone,
                WorkPhone = workPhone,
                EMail = eMail,
                EMail2 = eMail2,
                EMail3 = eMail3
            };

        }
    }
}

