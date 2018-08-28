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
            driver.FindElement(By.XPath("(//img[@alt='Edit'])[" + (index + 1) + "]")).Click();
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
    }
}

