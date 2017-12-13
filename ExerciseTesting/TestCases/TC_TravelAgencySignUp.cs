using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExerciseTesting.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using CsvHelper;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Linq;

namespace ExerciseTesting.TestCases
{
    [TestClass]
    public class TC_TravelAgencySignUp
    {
        IEnumerable<CSVDate.SignUpMockDate> GetCSVDate()
        {
            var source = new StreamReader(@"SignUpMockData.csv");

            var reader = new CsvReader(source);

            //CSVReader will now read the whole file into an enumerable
            IEnumerable<CSVDate.SignUpMockDate> records = reader.GetRecords<CSVDate.SignUpMockDate>();

            return records;

            //First 5 records in CSV file will be printed to the Output Window
            //foreach (CSVDate.SignUpMockDate record in records.Take(5))
            //{
            //Debug.WriteLine("{0} {1}, {2}, {3}, {4}, {5}", record.id, record.first_name, record.last_name, record.mobile, record.email, record.password);
            //}      
        }

        IWebDriver driver;

        TravelAgencyHomePage objHomePage;

        TravelAgencyRegisterPage objRegisterPage;

        TravelAgencyAcountPage objAccountPage;

        /*
         * Initilize a chrome driver and its settings 
         */
        [TestInitialize]
        public void Initialize()
        {
            driver = new ChromeDriver();

            // Maximize browser window
            driver.Manage().Window.Maximize();

            // Go to a specific webpage 
            driver.Navigate().GoToUrl("http://www.phptravels.net/");
        }

        /*
         * Terminate chrome driver
         */
        [TestCleanup]
        public void CleanUp()
        {
            Thread.Sleep(4000);

            driver.Quit();
        }

        /*
         * Verify that the page navigated to is in fact the page desired
         * 
         * Verify that the desired account has actually been created and logged in
         * 
         * Verify that the user has been logged out
         */
        [TestMethod]
        public void VerifySignUpLogOut()
        {
            // Assign a new home page object
            objHomePage = new TravelAgencyHomePage(driver);

            //Assert that the page navigated to is in fact the page desired
            Assert.AreEqual("http://www.phptravels.net/", objHomePage.GetUrl());


            objHomePage.ClickOnMyAccount();
            objHomePage.ClickOnSignUp();

            // Assign a new register page object
            objRegisterPage = new TravelAgencyRegisterPage(driver);

            // Sign up
            objRegisterPage.SignUp("Steve", "Dai", "1999999999", "steve2064@mailinator.com", "1234-Abcd", "1234-Abcd");

            // Wait until user is logged in
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(4));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.LinkText("STEVE")));

            // Assign a new account page object
            objAccountPage = new TravelAgencyAcountPage(driver, "STEVE");

            //Assert that the desired account has actually been created and logged in
            Assert.IsTrue(objAccountPage.UserIsLoggedIn());

            objAccountPage.ClickOnUserName();
            objAccountPage.ClickOnLogOut();

            // Assign a new home page object
            objHomePage = new TravelAgencyHomePage(driver);

            //Assert that the user has been logged out
            Assert.IsTrue(objHomePage.UserIsLoggedOut());
        }

        [TestMethod]
        public void VerifyMultipleSignUps()
        {
            IEnumerable<CSVDate.SignUpMockDate> data = GetCSVDate();

            List<CSVDate.SignUpMockDate> dataList;

            dataList = data.ToList();

            CSVDate.SignUpMockDate[] dataArray;

            dataArray = dataList.ToArray();

            Random rand = new Random();

            for (int i = 0; i < 10; i++)
            {
                int index;
                index = rand.Next(1000);

                // Assign a new home page object
                objHomePage = new TravelAgencyHomePage(driver);

                objHomePage.ClickOnMyAccount();
                objHomePage.ClickOnSignUp();

                // Assign a new register page object
                objRegisterPage = new TravelAgencyRegisterPage(driver);

                // Sign up
                objRegisterPage.SignUp(dataArray[index].first_name, dataArray[index].last_name, dataArray[index].mobile, dataArray[index].email, dataArray[index].password, dataArray[index].password);

                try
                {
                    // Wait until user is logged in
                    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(2));
                    wait.Until(ExpectedConditions.ElementToBeClickable(By.LinkText(dataArray[index].first_name.ToUpper())));
                }
                catch
                {
                    Debug.WriteLine(dataArray[index].email + " " + "has already been registered.");
                    continue;
                }

                // Assign a new account page object
                objAccountPage = new TravelAgencyAcountPage(driver, dataArray[index].first_name.ToUpper());

                //Assert that the desired account has actually been created and logged in
                Assert.IsTrue(objAccountPage.UserIsLoggedIn());

                objAccountPage.ClickOnUserName();
                objAccountPage.ClickOnLogOut();


            }

        }

    }
}
