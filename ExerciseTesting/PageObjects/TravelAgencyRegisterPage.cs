using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ExerciseTesting.PageObjects
{
    class TravelAgencyRegisterPage
    {
        IWebDriver driver;

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
        /// Enter first name to firstName element
        /// </summary>
        /// <param name="firstNameInput">first name</param>
        public void SetFirstName(string firstNameInput)
        {
            firstName.SendKeys(firstNameInput);
        }

        /// <summary>
        /// Enter last name to lastName element
        /// </summary>
        /// <param name="lastNameInput">last name</param>
        public void SetLastName(string lastNameInput)
        {
            lastName.SendKeys(lastNameInput);
        }

        /// <summary>
        /// Enter phone number to phone element
        /// </summary>
        /// <param name="phoneNumberInput">phone number</param>
        public void SetPhone(string phoneNumberInput)
        {
            phone.SendKeys(phoneNumberInput);
        }

        /// <summary>
        /// Enter email address to email element
        /// </summary>
        /// <param name="emailAddressInput">email address</param>
        public void SetEmail(string emailAddressInput)
        {
            email.SendKeys(emailAddressInput);
        }

        /// <summary>
        /// Enter password to password element
        /// </summary>
        /// <param name="passwordInput">password</param>
        public void SetPassword(string passwordInput)
        {
            password.SendKeys(passwordInput);
        }

        /// <summary>
        /// Enter confirm password to confirmPassword element
        /// </summary>
        /// <param name="confirmPasswordInput">confirm password</param>
        public void SetConfirmPassword(string confirmPasswordInput)
        {
            confirmPassword.SendKeys(confirmPasswordInput);
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
            SetFirstName(firstNameInput);

            SetLastName(lastNameInput);

            SetPhone(phoneNumberInput);

            SetEmail(emailAddressInput);

            SetPassword(passwordInput);

            SetConfirmPassword(confirmPasswordInput);

            ClickOnSignUp();
        }
    }
}
