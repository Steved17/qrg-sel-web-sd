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
        IWebDriver driver;
        string websiteUrl = "http://www.phptravels.net/";                   //Address of website that user is supposed to go to 
        string alreadyRegistered = " has already been registered.\n";       //Error message saying that the email has already been registered on the site
        TravelAgencyHomePage objHomePage;
        TravelAgencyRegisterPage objRegisterPage;
        TravelAgencyAcountPage objAccountPage;

        //Read the csv data into an IEnumerable object
        IEnumerable<CSVDate.SignUpMockDate> GetCSVDate()
        {
            var source = new StreamReader(@"SignUpMockData.csv");

            var reader = new CsvReader(source);

            //CSVReader will now read the whole file into an enumerable
            IEnumerable<CSVDate.SignUpMockDate> records = reader.GetRecords<CSVDate.SignUpMockDate>();

            return records;
        }

        /*
         * Initilize a chrome driver and its settings 
         */
        [TestInitialize]
        public void Initialize()
        {
            // 1
            driver = new ChromeDriver();

            // 2 Maximize browser window
            driver.Manage().Window.Maximize();

            // 3 Go to a specific webpage 
            driver.Navigate().GoToUrl(websiteUrl);
        }

        /*
         * Terminate chrome driver
         */
        [TestCleanup]
        public void CleanUp()
        {
            // 14 kill the driver
            driver.Quit();
        }

        /// <summary>
        /// Test Plan: Selenium Webdriver Quick Ramp Up Guide
        /// Requirement: 
        /// Steps: 1 - 14
        /// 
        /// Description:
        /// Create a user on the site with provided information and sign out
        /// 
        /// Expected Results:
        /// User is able to complete the workflow and taken to a sign in page after logging out
        /// 
        /// </summary>
        [TestMethod]
        public void VerifySignUpLogOut()
        {
            // Assign a new home page object
            objHomePage = new TravelAgencyHomePage(driver);

            //Assert that the page navigated to is in fact the page desired
            Assert.AreEqual(websiteUrl, objHomePage.GetUrl(), "FAILED: User is not taken to the correct site.\n" + "Current Site: " + objHomePage.GetUrl() + "\n" + "Correct Site: " + websiteUrl);

            // 4
            objHomePage.ClickOnMyAccount();
            // 5
            objHomePage.ClickOnSignUp();

            // Assign a new register page object
            objRegisterPage = new TravelAgencyRegisterPage(driver);

            // 6 - 12 Sign up
            objRegisterPage.SignUp("Steve", "Dai", "1999999999", "steve2072@mailinator.com", "1234-Abcd", "1234-Abcd");

            // Wait until user is logged in
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(4));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.LinkText("STEVE")));

            // Assign a new account page object
            objAccountPage = new TravelAgencyAcountPage(driver, "STEVE");

            //Assert that the desired account has actually been created and logged in
            Assert.IsTrue(objAccountPage.UserIsLoggedIn(), "FAILED: User has not been created and logged in.");

            // 13
            objAccountPage.ClickOnUserName();
            objAccountPage.ClickOnLogOut();

            // Assign a new home page object
            objHomePage = new TravelAgencyHomePage(driver);

            //Assert that the user has been logged out
            Assert.IsTrue(objHomePage.UserIsLoggedOut(), "FAILED: User has not been logged out.");
        }

        /// <summary>
        /// Test Plan: Selenium Webdriver Quick Ramp Up Guide
        /// Requirement: 
        /// Steps: 1 - 14
        /// 
        /// Description:
        /// Read data from csv sheet and use the information to create multiple users
        /// 
        /// Expected Results:
        /// User is able to complete the workflow and taken to a sign in page after logging out
        /// 
        /// </summary>
        [TestMethod]
        public void VerifyMultipleSignUps()
        {
            //Read the csv data into an IEnumerable object
            IEnumerable<CSVDate.SignUpMockDate> data = GetCSVDate();

            //Turn IEnumerable object into an array
            List<CSVDate.SignUpMockDate> dataList = data.ToList(); ;
            CSVDate.SignUpMockDate[] dataArray = dataList.ToArray();

            Random rand = new Random();

            //i < 10 because we want 10 random entries from the csv sheet
            for (int i = 0; i < 10; i++)
            {
                int index;
                index = rand.Next(1000);    //1000 because there are 1000 entries in the csv sheet

                // Assign a new home page object
                objHomePage = new TravelAgencyHomePage(driver);

                // 4
                objHomePage.ClickOnMyAccount();
                // 5
                objHomePage.ClickOnSignUp();

                // Assign a new register page object
                objRegisterPage = new TravelAgencyRegisterPage(driver);

                // 6 - 12 Sign up
                objRegisterPage.SignUp(dataArray[index].first_name, dataArray[index].last_name, dataArray[index].mobile, dataArray[index].email, dataArray[index].password, dataArray[index].password);

                try
                {
                    // Wait until user is logged in
                    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(2));
                    wait.Until(ExpectedConditions.ElementToBeClickable(By.LinkText(dataArray[index].first_name.ToUpper())));
                }
                catch
                {
                    Debug.WriteLine(dataArray[index].email + alreadyRegistered);
                    continue;
                }

                // Assign a new account page object
                objAccountPage = new TravelAgencyAcountPage(driver, dataArray[index].first_name.ToUpper());

                //Assert that the desired account has actually been created and logged in
                Assert.IsTrue(objAccountPage.UserIsLoggedIn(), "FAILED: User has not been created and logged in.");

                // 13
                objAccountPage.ClickOnUserName();
                objAccountPage.ClickOnLogOut();


            }

        }

    }
}
