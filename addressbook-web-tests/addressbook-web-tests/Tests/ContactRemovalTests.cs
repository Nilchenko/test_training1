using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactRemovalTests : ContactTestBase
    {
        public ContactData defaultData = new ContactData("default1stName", "default2ndName");



        [Test]
        public void ContactRemovalTest()
        {
            if (!app.Contact.ContactExist())
            {
                app.Contact.AddContact(defaultData);
            }

            List<ContactData> oldContacts = ContactData.GetAll();
            ContactData toBeRemoved = oldContacts[0];

            app.Contact.Remove(toBeRemoved);

            List<ContactData> newContacts = ContactData.GetAll();

            oldContacts.RemoveAt(0);
            Assert.AreEqual(oldContacts, newContacts);

            foreach (ContactData contact in newContacts)
            {
                //проверка на то, что ид удаленного элемента не совпадает со всеми остальными
                Assert.AreNotEqual(contact.Id, toBeRemoved.Id);
            }

        }
    }
}
