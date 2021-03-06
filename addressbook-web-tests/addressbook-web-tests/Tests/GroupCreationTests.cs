﻿using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Excel = Microsoft.Office.Interop.Excel;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupCreationTests : GroupTestBase
    {
        public static IEnumerable<GroupData> RandomGroupDataProvider()
        {
            List<GroupData> groups = new List<GroupData>();
            for(int i=0; i < 3; i++)
            {
                groups.Add(new GroupData(GenerateRandomString(30))
                {
                    Header = GenerateRandomString(100),
                    Footer = GenerateRandomString(100)
                });
            }
            return groups;
        }

        public static IEnumerable<GroupData> GroupDataFromCsvFile()
        {
            List<GroupData> groups = new List<GroupData>();
            String[] lines = File.ReadAllLines(@"groups.csv");
            foreach (string l in lines)
            {
                string[] parts = l.Split(',');
                groups.Add(new GroupData(parts[0])
                {
                    Header = parts[1],
                    Footer = parts[2]              
                });
            }
            return groups;
        }

        public static IEnumerable<GroupData> GroupDataFromXmlFile()
        {
            return (List<GroupData>)                                //приведение типа
                new XmlSerializer(typeof(List<GroupData>))
                  .Deserialize(new StreamReader (@"groups.xml"));
        }

        public static IEnumerable<GroupData> GroupDataFromJsonFile()
        {
            return JsonConvert.DeserializeObject<List<GroupData>>(
                File.ReadAllText(@"groups.json"));
        }

        public static IEnumerable<GroupData> GroupDataFromXlsxFile()
        {
            List<GroupData> groups = new List<GroupData>();
            Excel.Application app = new Excel.Application();
            Excel.Workbook wb = app.Workbooks.Open(Path.Combine(Directory.GetCurrentDirectory(), @"groups.xlsx"));
            Excel.Worksheet sheet = wb.ActiveSheet;
            Excel.Range range =  sheet.UsedRange;// прямоугольник с данными
            for(int i = 1; i <=range.Rows.Count; i++)
            {
                groups.Add(new GroupData()
                {
                    Name = range.Cells[i, 1].Value,
                    Header = range.Cells[i, 2].Value,
                    Footer = range.Cells[i, 3].Value
                });
            }
            wb.Close();
            app.Visible = false;
            app.Quit();
            return groups;
        }



        [Test, TestCaseSource("GroupDataFromJsonFile")]
        public void GroupCreationTest(GroupData group)
        {
            List<GroupData> oldGroups = GroupData.GetAll();

            app.Groups.Create(group);

            //Проверка на сравнение количества элементов
            Assert.AreEqual(oldGroups.Count + 1, app.Groups.GetGroupCount());

            //проверка не нужна, т.к. есть проверка в GroupTestBase
            //List<GroupData> newGroups = GroupData.GetAll();
            //oldGroups.Add(group);
            //oldGroups.Sort();
            //newGroups.Sort();
            //Assert.AreEqual(oldGroups, newGroups);
        }


        [Test]
        public void BadNameGroupCreationTest()
        {
            GroupData group = new GroupData("f'f");
            group.Header = "";
            group.Footer = "";

            List<GroupData> oldGroups = GroupData.GetAll();

            app.Groups.Create(group);

            //Проверка на сравнение количества элементов
            Assert.AreEqual(oldGroups.Count + 1, app.Groups.GetGroupCount());
        }

        [Test]
        public void TestDBConnectivity()
        {

            GroupData group = GroupData.GetAll()[0];
            List<ContactData> oldList = group.GetContacts();
            //List<ContactData> contacts = ContactData.GetAll();
            List<ContactData> contacts = app.Contact.GetContactList();
            //ContactData contact = ContactData.GetAll().Except(oldList).First();

            int i = 1;
            Console.Out.WriteLine("Контакты в группе '" + group.Name + "':\n");

            foreach (ContactData contact in oldList)
            {
                Console.Out.WriteLine(i + ")" + contact.AllNames + "\nИД "+ contact.Id);
                i++;
            }

            i = 1;
            Console.Out.WriteLine("\n\nВсе контакты из приложения:\n");

            foreach (ContactData contact in contacts)
            {
                Console.Out.WriteLine(i + ")" + contact.AllNames + "\nИД " + contact.Id);
                i++;
            }

        }

    }
}
