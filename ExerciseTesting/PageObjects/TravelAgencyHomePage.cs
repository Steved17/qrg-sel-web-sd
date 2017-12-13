using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ExerciseTesting.PageObjects
{
    class TravelAgencyHomePage
    {
        IWebDriver driver;

        // Clicking on myAccount element gives options to log in and sign up
        [FindsBy(How = How.LinkText, Using = "MY ACCOUNT")]
        IWebElement myAccount;

        [FindsBy(How = How.LinkText, Using = "Sign Up")]
        IWebElement signUp;

        /*
         *  Constructor - initialize driver and elements (myAccount, signUp)
         */
        public TravelAgencyHomePage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        /*
         *  Click on the myAccount element
         */
        public void ClickOnMyAccount()
        {
            myAccount.Click();
        }

        /*
         *  Click on the signUp element
         */
        public void ClickOnSignUp()
        {
            signUp.Click();
        }

        /*
         *  Get the current url
         */
        public string GetUrl()
        {
            return driver.Url;
        }

        /*
         *  Return a boolean value indicates whether myAccount element is displayed
         *  
         *  True if displayed (user is logged out), False if not displayed (user is not logged out)
         */
        public bool UserIsLoggedOut()
        {
            return myAccount.Displayed;
        }
    }
}
