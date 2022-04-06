using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Getting Connection ...");
            string connString = "Data Source = OMERSARIER; Initial Catalog = WebApp; Integrated Security = True";

            //create instanace of database connection
            SqlConnection conn = new SqlConnection(connString);


            try
            {
                Console.WriteLine("Openning Connection ...");

                //open connection
                conn.Open();

                Console.WriteLine("Connection successful!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }


            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://www.beyazperde.com/diziler/tum/?page=297");
            IWebElement coki = driver.FindElement(By.CssSelector("#js-cookie-info > span"));
            coki.Click();
            for (int x = 297; x <= 400; x++)
            {
                for (int i = 1; i <= 18; i++)
                {
                    StringBuilder strBuilder = new StringBuilder();
                    strBuilder.Append("INSERT INTO [Table_Work] (Name, Owner, Type , About , Cast) values (@Name, @Owner, @Type, @About, @Cast) ");


                    if (i != 2 && i != 7 && i != 13)
                    {
                        string sqlQuery = strBuilder.ToString();
                        using (SqlCommand command = new SqlCommand(sqlQuery, conn)) //pass SQL query created above and connection
                        {
                           

                          



                            IWebElement element = driver.FindElement(By.CssSelector
                                ("#content-layout > section.section.section-wrap.gd-3-cols.gd-gap-20 > div.gd-col-middle > ul > li:nth-child(" + i + ") > div > div.meta > h2 > a"));
                            var name = element.Text;
                            Console.WriteLine(name);
                            command.Parameters.AddWithValue("@Name", name);
                            IWebElement element2 = driver.FindElement(By.CssSelector
                               ("#content-layout > section.section.section-wrap.gd-3-cols.gd-gap-20 > div.gd-col-middle > ul > li:nth-child(" + i + ") > div > div.meta > div > div.meta-body-item.meta-body-info"));
                            var açıklama = element2.Text;
                            Console.WriteLine(açıklama);
                            command.Parameters.AddWithValue("@About", açıklama);




                            ReadOnlyCollection<IWebElement> ownercnt = driver.FindElements(By.CssSelector("#content-layout > section.section.section-wrap.gd-3-cols.gd-gap-20 > div.gd-col-middle > ul > li:nth-child(" + i + ") > div > div.meta > div > div.meta-body-item.meta-body-direction > a"));

                            if (ownercnt.Count != 0)
                            {
                                IWebElement element3 = driver.FindElement(By.CssSelector
                                    ("#content-layout > section.section.section-wrap.gd-3-cols.gd-gap-20 > div.gd-col-middle > ul > li:nth-child(" + i + ") > div > div.meta > div > div.meta-body-item.meta-body-direction > a"));
                                var owner = element3.Text;
                                Console.WriteLine(owner);
                                command.Parameters.AddWithValue("@Owner", owner);
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@Owner", DBNull.Value);
                            }


                            ReadOnlyCollection<IWebElement> castcnt = driver.FindElements(By.CssSelector("#content-layout > section.section.section-wrap.gd-3-cols.gd-gap-20 > div.gd-col-middle > ul > li:nth-child(" + i + ") > div > div.meta > div > div.meta-body-item.meta-body-actor"));
                            if (castcnt.Count != 0)
                            {
                                IWebElement element4 = driver.FindElement(By.CssSelector
                        ("#content-layout > section.section.section-wrap.gd-3-cols.gd-gap-20 > div.gd-col-middle > ul > li:nth-child(" + i + ") > div > div.meta > div > div.meta-body-item.meta-body-actor"));
                                var cast = element4.Text;
                                Console.WriteLine(cast);
                                command.Parameters.AddWithValue("@Cast", cast);
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@Cast", DBNull.Value);
                            }
                            command.Parameters.AddWithValue("@Type","Dizi");
                            command.ExecuteNonQuery();
                            Console.WriteLine("Query Executed.");

                        }
                       



                    }

               



                }
                var urldegisken = "http://www.beyazperde.com/diziler/tum/?page=" + x;
                driver.Navigate().GoToUrl(urldegisken);

            }

        }
    }
}
