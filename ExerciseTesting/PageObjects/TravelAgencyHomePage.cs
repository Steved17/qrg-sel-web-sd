using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ExerciseTesting.PageObjects
{
    class TravelAgencyHomePage
    {
        private IWebDriver driver;

        // Use struct to store parameter and pass struct to methods

        [FindsBy(How = How.LinkText, Using = "MY ACCOUNT")]
        IWebElement myAccount;

        [FindsBy(How = How.LinkText, Using = "Sign Up")]
        IWebElement signUp;

        /// <summary>
        /// Constructor - initialize driver and elements (myAccount, signUp)
        /// </summary>
        /// <param name="driver">Chrome Driver</param>
        public TravelAgencyHomePage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        /// <summary>
        /// Click on the myAccount element
        /// </summary>
        public void ClickOnMyAccount()  //more specific naming - add button
        {
            myAccount.Click();
        }

        /// <summary>
        /// Click on the signUp element
        /// </summary>
        public void ClickOnSignUp()   //more specific naming - add button
        {
            signUp.Click();
        }

        /// <summary>
        /// Get the current url
        /// </summary>
        /// <returns>Return the string - current URL</returns>
        public string GetUrl()
        {
            return driver.Url;
        }

        /// <summary>
        /// Determines whether myAccount element is displayed
        /// </summary>
        /// <returns>True if displayed (user has been logged out), False if not displayed (user has been not logged out)</returns>
        public bool UserIsLoggedOut()
        {
            return myAccount.Displayed;
        }
    }
}
