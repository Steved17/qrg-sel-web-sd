using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ExerciseTesting.PageObjects
{
    class TravelAgencyAcountPage
    {
        IWebDriver driver;
        string user;            //Name of the user that is used to check whether user has been created and logged in successfully 

        [FindsBy(How = How.LinkText, Using = "Logout")]
        IWebElement logOut;

        /// <summary>
        /// Constructor - initialize driver and elements (userName, logOut)
        /// </summary>
        /// <param name="driver">Chrome Driver</param>
        /// <param name="user">user name</param>
        public TravelAgencyAcountPage(IWebDriver driver, string user)
        {
            this.driver = driver;
            this.user = user;
            PageFactory.InitElements(driver, this);
        }

        /// <summary>
        /// Click on userName element
        /// </summary>
        public void ClickOnUserName()
        {
            IWebElement userName = driver.FindElement(By.LinkText(user));
            userName.Click();
        }

        /// <summary>
        /// Click on logOut element
        /// </summary>
        public void ClickOnLogOut()
        {
            logOut.Click();
        }

        /// <summary>
        /// Determines whether userName elemtn is displayed
        /// </summary>
        /// <returns>True if displayed (user is logged in), False if not displayed (user is not logged in)</returns>
        public bool UserIsLoggedIn()
        {
            IWebElement userName = driver.FindElement(By.LinkText(user));
            return userName.Displayed;
        }
    }
}
