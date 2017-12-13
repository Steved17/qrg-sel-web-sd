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

        /*
         *  Constructor - initialize driver and elements (firstName, lastName, phone, email, password, confirmPassword, signUp)
         */
        public TravelAgencyRegisterPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        /*
         *  Enter first name to firstName element
         */
        public void SetFirstName(string firstNameInput)
        {
            firstName.SendKeys(firstNameInput);
        }

        /*
         *  Enter last name to lastName element
         */
        public void SetLastName(string lastNameInput)
        {
            lastName.SendKeys(lastNameInput);
        }

        /*
         *  Enter phone number to phone element
         */
        public void SetPhone(string phoneNumberInput)
        {
            phone.SendKeys(phoneNumberInput);
        }

        /*
         *  Enter email address to email element
         */
        public void SetEmail(string emailAddressInput)
        {
            email.SendKeys(emailAddressInput);
        }

        /*
         *  Enter password to password element
         */
        public void SetPassword(string passwordInput)
        {
            this.password.SendKeys(passwordInput);
        }

        /*
         *  Enter confirm password to confirmPassword element
         */
        public void SetConfirmPassword(string confirmPasswordInput)
        {
            confirmPassword.SendKeys(confirmPasswordInput);
        }

        /*
         *  Click on signUp element
         */
        public void ClickOnSignUp()
        {
            signUp.Click();
        }

        /*
         *  Complete sign up process by entering all required information
         */
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
