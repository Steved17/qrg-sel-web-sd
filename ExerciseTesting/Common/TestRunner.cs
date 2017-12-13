using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Threading;


namespace ExerciseTesting
{
    //[TestClass]
    public class TestRunner
    {
        private static IWebDriver DriverInit()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Manage().Window.Maximize();

            return driver;
        }

        private static void Navigate(IWebDriver driver, string desiredURL)
        {
            driver.Navigate().GoToUrl(desiredURL);

            Assert.AreEqual(desiredURL, driver.Url);
        }

        private static void SignUp(IWebDriver driver, string firstN, string lastN, string phoneN, string email, string password, string confirmpassword)
        {
            //6
            //string firstN = "STEVE";
            IWebElement firstName = driver.FindElement(By.Name("firstname"));
            firstName.SendKeys(firstN);

            //7
            IWebElement lastName = driver.FindElement(By.Name("lastname"));
            lastName.SendKeys(lastN);

            //8
            IWebElement phoneNumber = driver.FindElement(By.Name("phone"));
            phoneNumber.SendKeys(phoneN);

            //9
            IWebElement email9 = driver.FindElement(By.Name("email"));
            email9.SendKeys(email);

            //10
            IWebElement password10 = driver.FindElement(By.Name("password"));
            password10.SendKeys(password);

            //11
            IWebElement confirmpassword11 = driver.FindElement(By.Name("confirmpassword"));
            confirmpassword11.SendKeys(confirmpassword);

            //12
            IWebElement ele12 = driver.FindElement(By.CssSelector("#headersignupform > div:nth-child(9) > button"));
            ele12.Click();

            WebDriverWait wait1 = new WebDriverWait(driver, TimeSpan.FromSeconds(4));
            wait1.Until(ExpectedConditions.ElementToBeClickable(By.LinkText("STEVE")));

            IWebElement ele30 = driver.FindElement(By.LinkText("STEVE"));
            //Console.WriteLine(ele30.Displayed);
            Assert.IsTrue(ele30.Displayed);
        }

        private static void LogOut(IWebDriver driver)
        {
            IWebElement ele14 = driver.FindElement(By.LinkText("Logout"));
            ele14.Click();


            WebDriverWait wait1 = new WebDriverWait(driver, TimeSpan.FromSeconds(4));
            wait1.Until(ExpectedConditions.ElementToBeClickable(By.LinkText("MY ACCOUNT")));

            //Thread.Sleep(10000);

            IWebElement ele20 = driver.FindElement(By.LinkText("MY ACCOUNT"));
            Assert.IsTrue(ele20.Displayed);
        }

        //[TestMethod]
        public void main()
        {
            //1,2
            IWebDriver driver = DriverInit();

            //3
            Navigate(driver, "http://www.phptravels.net/");

            //4
            IWebElement ele4 = driver.FindElement(By.LinkText("MY ACCOUNT"));
            ele4.Click();

            //5
            IWebElement ele5 = driver.FindElement(By.LinkText("Sign Up"));
            ele5.Click();

            //6-12
            string firstN = "STEVE";
            SignUp(driver, "Steve", "Dai", "1999999999", "steve2032@mailinator.com", "1234-Abcd", "1234-Abcd");

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(4));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.LinkText(firstN))).Click();

            //13
            LogOut(driver);

            Thread.Sleep(4000);

            //14
            driver.Quit();

        }
    }
}