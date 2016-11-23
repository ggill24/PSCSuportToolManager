using System;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SSSMLoginBot
{
    class Program
    {
        static void Main(string[] args)
        {
            const string driverPath = @"C:\PscSupport\Drivers";
            const string driver = @"C:\PscSupport\Drivers\chromedriver.exe";
            const string propertyNumberPath = @"C:\PscSupport\SSM Support Bot\Property.txt";

            if (!File.Exists(driver) || (!File.Exists(propertyNumberPath))) { Console.WriteLine("Missing Chrome Driver Or No Property Defined."); Console.ReadKey(); return; }

            int propertyNumber = PropertyNumber(propertyNumberPath);
            string propertyLocation = propertyNumber < 10 ? "p000" + propertyNumber.ToString() + "@(Removed)" : "p00" + propertyNumber.ToString() + "@(Removed)";
            const string propPass = "(Removed)";

            try
            {
                using (ChromeDriver chromeDriver = new ChromeDriver(driverPath))
                {
                    chromeDriver.Navigate().GoToUrl("(url)");

                    var logInBox = chromeDriver.FindElement(By.Name("scemail"));
                    var pwdBox = chromeDriver.FindElement(By.Name("scpassword"));

                    if (logInBox == null || pwdBox == null) { Console.WriteLine("Could not find Login/Password field"); Console.ReadKey(); return; }
                    logInBox.SendKeys(propertyLocation);
                    pwdBox.SendKeys(propPass);

                    var loginButton = chromeDriver.FindElement(By.ClassName("rebutton"));

                    if (loginButton == null) { Console.WriteLine("Could not find Login button"); Console.ReadKey(); return; }

                    loginButton.Click();
                    Console.ReadKey();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error:" + ex.Message);
                Console.ReadKey();
            }
        }

        private static int PropertyNumber(string filePath)
        {
            int propNumber = 0;

            try
            {
                var file = File.Open(filePath, FileMode.Open);
                file.Close();

                using (StreamReader sr = new StreamReader(filePath))
                {
                    var line = sr.ReadLine();

                    if (!String.IsNullOrEmpty(line))
                    {
                        int number = 0;

                        if (int.TryParse(line, out number))
                        {
                            propNumber = number;
                        }
                        else
                        {
                            Console.WriteLine("Invalid Property Number");

                        }
                    }
                }
                
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            return propNumber;
        }
    }
}


