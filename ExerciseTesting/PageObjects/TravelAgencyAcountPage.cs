using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ExerciseTesting.PageObjects
{
    class TravelAgencyAcountPage
    {
        IWebDriver driver;
        string user;

        [FindsBy(How = How.LinkText, Using = "Logout")]
        IWebElement logOut;

        /*
         *  Constructor - initialize driver and elements (userName, logOut)
         */
        public TravelAgencyAcountPage(IWebDriver driver, string user)
        {
            this.driver = driver;
            this.user = user;
            PageFactory.InitElements(driver, this);
        }

        /*
         *  Click on userName element
         */
        public void ClickOnUserName()
        {
            IWebElement userName = driver.FindElement(By.LinkText(user));
            userName.Click();
        }

        /*
         *  Click on logOut element
         */
        public void ClickOnLogOut()
        {
            logOut.Click();
        }

        /*
         *  Return a boolean value indicates whether userName element is displayed
         *  
         *  True if displayed (user is logged in), False if not displayed (user is not logged in)
         */
        public bool UserIsLoggedIn()
        {
            IWebElement userName = driver.FindElement(By.LinkText(user));
            return userName.Displayed;
        }
    }
}
