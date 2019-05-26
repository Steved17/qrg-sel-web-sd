using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExerciseTesting.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using CsvHelper;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Linq;
using System.Threading;

// Using linkedtext causes annoying problems

namespace ExerciseTesting.TestCases
{
    [TestClass]
    public class TC_TravelAgencySignUp
    {
        private IWebDriver driver;
        private string websiteUrl = "http://www.phptravels.net/";                   //Address of website that user is supposed to go to 
        private string alreadyRegistered = " has already been registered.";         //Error message saying that the email has already been registered on the site
        private TravelAgencyHomePage objHomePage;
        private TravelAgencyRegisterPage objRegisterPage;
        private TravelAgencyAcountPage objAccountPage;

        //Read the csv data into an IEnumerable object
        IEnumerable<CSVDate.SignUpMockDate> GetCSVDate()
        {
            var source = new StreamReader(@"SignUpMockData.csv");

            var reader = new CsvReader(source);

            //CSVReader will now read the whole file into an enumerable
            IEnumerable<CSVDate.SignUpMockDate> records = reader.GetRecords<CSVDate.SignUpMockDate>();

            return records;
        }

        /// <summary>
        /// Initilize a chrome driver and its settings, maximize window, go to desired url
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(websiteUrl);

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        /// <summary>
        /// Terminate chrome driver
        /// </summary>
        [TestCleanup]
        public void CleanUp()
        {
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
        [TestCategory("Exercise")]
        public void VerifySignUpLogOut()
        {
            // Assign a new home page object
            objHomePage = new TravelAgencyHomePage(driver);

            //Assert that the page navigated to is in fact the page desired
            Assert.AreEqual(websiteUrl, objHomePage.GetUrl(), "FAILED: User is not taken to the correct site.\n" 
                + "Current Site: " + objHomePage.GetUrl() + "\n" + "Correct Site: " + websiteUrl);


            By preloader = By.Id("//[@id='preloader']");
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(preloader));

            // Wait until user is logged out
            //WebDriverWait waitGZ = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            //waitGZ.Until(ExpectedConditions.ElementToBeClickable(By.LinkText("MY ACCOUNT")));


            objHomePage.ClickOnMyAccount();
            objHomePage.ClickOnSignUp();

            // Assign a new register page object
            objRegisterPage = new TravelAgencyRegisterPage(driver);

            // 6 - 12 Sign up
            objRegisterPage.SignUp("Steve", "Dai", "1999999999", "stevenGGG@mailinator.com", "1234-Abcd", "1234-Abcd");

            //make your own wait function somewhere else


            // Wait until user is logged in
            //WebDriverWait waitUntilUserIsLoggedIn = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            //waitUntilUserIsLoggedIn.Until(ExpectedConditions.ElementToBeClickable(By.LinkText("STEVE")));

            // Assign a new account page object
            objAccountPage = new TravelAgencyAcountPage(driver, "STEVE");

            //Assert that the desired account has actually been created and logged in
            Assert.IsTrue(objAccountPage.UserIsLoggedIn(), "FAILED: User has not been created and logged in.");


            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(preloader));

            // 13
            objAccountPage.ClickOnUserName();
            objAccountPage.ClickOnLogOut();

            // Assign a new home page object
            objHomePage = new TravelAgencyHomePage(driver);

            // Wait until user is logged out
            //WebDriverWait waitUntilUserIsLoggedOut = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            //waitUntilUserIsLoggedOut.Until(ExpectedConditions.ElementToBeClickable(By.LinkText("MY ACCOUNT")));

            //Assert that the user has been logged out
            Assert.IsTrue(objHomePage.UserIsLoggedOut(), "FAILED: User has not been logged out.");

            //****************************** add success message/report ***********************************
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
        [TestCategory("Exercise")]
        public void VerifyMultipleSignUps()
        {
            //Read the csv data into an IEnumerable object
            IEnumerable<CSVDate.SignUpMockDate> data = GetCSVDate();

            //Turn IEnumerable object into an array
            List<CSVDate.SignUpMockDate> dataList = data.ToList(); ;
            CSVDate.SignUpMockDate[] dataArray = dataList.ToArray();

            Random rand = new Random();

            //i < 20 because we want 20 random entries from the csv sheet
            for (int i = 0; i < 6; i++)
            {
                int index = rand.Next(1000);    //1000 because there are 1000 entries in the csv sheet

                // Assign a new home page object
                objHomePage = new TravelAgencyHomePage(driver);

                By preloader = By.Id("//[@id='preloader']");
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                wait.Until(ExpectedConditions.InvisibilityOfElementLocated(preloader));

                // Wait until user is logged out
                //WebDriverWait waitUntilUserIsLoggedOut = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                //waitUntilUserIsLoggedOut.Until(ExpectedConditions.ElementToBeClickable(By.LinkText("MY ACCOUNT")));

                // 4
                objHomePage.ClickOnMyAccount();

                // This solves things happening too fast problem - there is also button that has Sign Up as text
                //Thread.Sleep(1000);

                // Wait until sign up appears
                //WebDriverWait waitUntilSignUpAppears = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                //waitUntilSignUpAppears.Until(ExpectedConditions.ElementToBeClickable(By.LinkText("Sign Up")));

                // 5
                objHomePage.ClickOnSignUp();

                // Assign a new register page object
                objRegisterPage = new TravelAgencyRegisterPage(driver);

                try
                {
                    // 6 - 12 Sign up
                    objRegisterPage.SignUp(dataArray[index].first_name, dataArray[index].last_name, dataArray[index].mobile, dataArray[index].email, dataArray[index].password, dataArray[index].password);
                }
                catch
                {
                    Debug.WriteLine(dataArray[index].email + alreadyRegistered);
                    continue;
                }


                // Use if statement -> increase counter -> continue the for loop

                // check if the email has been registed already

                /*
                try
                {
                    // Wait until user is logged in
                    WebDriverWait waitUntilUserIsLoggedIn = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                    waitUntilUserIsLoggedIn.Until(ExpectedConditions.ElementToBeClickable(By.LinkText(dataArray[index].first_name.ToUpper())));
                }
                catch
                {
                    Debug.WriteLine(dataArray[index].email + alreadyRegistered);
                    continue;
                }
                */

                Debug.WriteLine(dataArray[index].email + " is good.");
                // Assign a new account page object
                objAccountPage = new TravelAgencyAcountPage(driver, dataArray[index].first_name.ToUpper());

                //Assert that the desired account has actually been created and logged in
                Assert.IsTrue(objAccountPage.UserIsLoggedIn(), "FAILED: User has not been created and logged in.");

                wait.Until(ExpectedConditions.InvisibilityOfElementLocated(preloader));

                // 13
                objAccountPage.ClickOnUserName();
                objAccountPage.ClickOnLogOut();
            }

        }

    }
}
