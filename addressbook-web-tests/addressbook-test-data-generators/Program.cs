using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Excel = Microsoft.Office.Interop.Excel; // задали синоним, все обращения должны начинаться префикса
using WebAddressbookTests;

namespace addressbook_test_data_generators
{
    class Program
    {
        static void Main(string[] args)
        {
            int count = Convert.ToInt32(args[0]);
            string fileName = args[1];
            string format = args[2];
            string dataType = args[3];

            List<GroupData> groups = new List<GroupData>();
            List<ContactData> contacts = new List<ContactData>();

            for (int i = 0; i < count; i++)
            {
                groups.Add(new GroupData(TestBase.GenerateRandomString(10))
                {
                    Header = TestBase.GenerateRandomString(10),
                    Footer = TestBase.GenerateRandomString(10)
                });

                contacts.Add(new ContactData(TestBase.GenerateRandomString(10), TestBase.GenerateRandomString(10))
                {
                    HomePhone = TestBase.GenerateRandomString(10),
                    EMail = TestBase.GenerateRandomString(10)
                });
            }

            if (dataType == "groups")
            {
                if (format == "xlsx")
                {
                    WriteGroupsToXlsxFile(groups, fileName);
                }

                else
                {
                    StreamWriter writer = new StreamWriter(fileName);

                    if (format == "csv")
                    {
                        WriteGroupsToCsvFile(groups, writer);
                    }
                    else if (format == "xml")
                    {
                        WriteGroupsToXmlFile(groups, writer);
                    }
                    else if (format == "json")
                    {
                        WriteGroupsToJsonFile(groups, writer);
                    }

                    else
                    {
                        System.Console.Out.Write("Unrecognized format " + format);
                    }

                    writer.Close();
                }
            }
            else if (dataType == "contacts")
            {
           
            }

        }


        static void WriteGroupsToXlsxFile(List<GroupData> groups, string fileName)
        {
            Excel.Application app = new Excel.Application(); //запускает эксель
            app.Visible = true;  //позволяет смотреть, что происходит в окне приложения Excel
            Excel.Workbook wb = app.Workbooks.Add(); //открыли документ
            Excel.Worksheet sheet = wb.ActiveSheet; //получили открытую по умолчанию страницу
            //sheet.Cells[1, 1] = "test";

            int row = 1;
            foreach (GroupData group in groups)
            {
                sheet.Cells[row, 1] = group.Name;
                sheet.Cells[row, 2] = group.Header;
                sheet.Cells[row, 3] = group.Footer;

                row++;
            }
            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
            File.Delete(fullPath);
            wb.SaveAs(fullPath);

            wb.Close();
            app.Visible = false;
            app.Quit();
        }

        static void WriteGroupsToCsvFile(List<GroupData> groups, StreamWriter writer)
        {
            foreach(GroupData group in groups)
            {
                writer.WriteLine(String.Format("${0},${1},${2}",
                    group.Name, group.Header, group.Footer));

            }
        }

        static void WriteGroupsToXmlFile(List<GroupData> groups, StreamWriter writer)
        {
            new XmlSerializer(typeof(List<GroupData>)).Serialize(writer, groups);
        }

        //нужно загрузить библиотеку json.net nuget
        static void WriteGroupsToJsonFile(List<GroupData> groups, StreamWriter writer)
        {
            writer.Write(JsonConvert.SerializeObject(groups, Newtonsoft.Json.Formatting.Indented));
        }

    }
}
