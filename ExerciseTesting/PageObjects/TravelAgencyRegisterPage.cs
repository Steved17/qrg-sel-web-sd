using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;

namespace ExerciseTesting.PageObjects
{
    class TravelAgencyRegisterPage
    {
        private IWebDriver driver;

        [FindsBy(How = How.Name, Using = "firstname")]
        IWebElement firstName;

        [FindsBy(How = How.Name, Using = "lastname")]
        IWebElement lastName;

        [FindsBy(How = How.Name, Using = "phone")]
        IWebElement phone;

        [FindsBy(How = How.Name, Using = "email")]
        IWebElement email;

        [FindsBy(How = How.Name, Using = "password")]
        IWebElement password;

        [FindsBy(How = How.Name, Using = "confirmpassword")]
        IWebElement confirmPassword;

        [FindsBy(How = How.CssSelector, Using = "#headersignupform > div:nth-child(9) > button")]
        IWebElement signUp;

        /// <summary>
        /// Constructor - initialize driver and elements (firstName, lastName, phone, email, password, confirmPassword, signUp)
        /// </summary>
        /// <param name="driver">Chrome Driver</param>
        public TravelAgencyRegisterPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        /// <summary>
        /// Click on signUp element
        /// </summary>
        public void ClickOnSignUp()
        {
            signUp.Click();
        }

        /// <summary>
        /// Complete sign up process by entering all required information
        /// </summary>
        /// <param name="firstNameInput">first name</param>
        /// <param name="lastNameInput">last name</param>
        /// <param name="phoneNumberInput">phone number</param>
        /// <param name="emailAddressInput">email address</param>
        /// <param name="passwordInput">password</param>
        /// <param name="confirmPasswordInput">confirm password</param>
        public void SignUp(string firstNameInput, string lastNameInput, string phoneNumberInput, string emailAddressInput, string passwordInput, string confirmPasswordInput)
        {
            // put wait in driver init class

            // Wait until input fields are ready
            WebDriverWait waitZ = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            waitZ.Until(ExpectedConditions.ElementToBeClickable(By.Name("firstname")));

            firstName.SendKeys(firstNameInput);

            lastName.SendKeys(lastNameInput);

            phone.SendKeys(phoneNumberInput);

            email.SendKeys(emailAddressInput);

            password.SendKeys(passwordInput);

            confirmPassword.SendKeys(confirmPasswordInput);

            // Wait until input fields are ready
            WebDriverWait waitX = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            waitX.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("#headersignupform > div:nth-child(9) > button")));

            ClickOnSignUp();
        }
    }
}
