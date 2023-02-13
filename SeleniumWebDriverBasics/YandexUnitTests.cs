using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using static System.Net.WebRequestMethods;

namespace SeleniumWebDriverBasics
{
    [TestFixture]
    public class YandexUnitTests
    {
        private IWebDriver driver;
        private string baseUrl;

        [SetUp]
        public void TestSetup()
        {
            var service = FirefoxDriverService.CreateDefaultService();
            this.driver = new FirefoxDriver(service);
            this.driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            this.baseUrl = "https://mail.yandex.com/";
            this.driver.Navigate().GoToUrl(this.baseUrl);
            this.driver.Manage().Window.Maximize();
        }

        [Test]
        public void LoginToYandex()
        {
            //Arrange
            var expectedAccountName = "katetest.selenium";

            //Act
            LoginToYandexMail();

            IsElementVisible(By.XPath("//span[text()='katetest.selenium']"));
            var actualAccountName = this.driver.FindElement(By.XPath("//span[text()='katetest.selenium']")).Text;

            //Assert
            Assert.AreEqual(expectedAccountName, actualAccountName);
        }

        [Test]
        public void CreateDraftEmail()
        {
            //Arrange
            var expectedSubject = "Test Selenium";

            //Act
            LoginToYandexMail();

            IsElementVisible(By.XPath("//a[@href='#compose']"));

            this.driver.FindElement(By.XPath("//a[@href='#compose']")).Click();
            IsElementVisible(By.XPath("//div[@class='composeYabbles']"));
            this.driver.FindElement(By.XPath("//div[@class='composeYabbles']")).SendKeys("testkate837@gmail.com");
            this.driver.FindElement(By.XPath("//input[@class='composeTextField ComposeSubject-TextField']")).Click();
            this.driver.FindElement(By.XPath("//input[@class='composeTextField ComposeSubject-TextField']")).SendKeys("Test Selenium");
            this.driver.FindElement(By.XPath("//div[@class='cke_wysiwyg_div cke_reset cke_enable_context_menu cke_editable cke_editable_themed cke_contents_ltr cke_htmlplaceholder']")).SendKeys("Test Selenium from Kate");
            this.driver.FindElement(By.XPath("//a[@href='#draft']")).Click();
            IsElementVisible(By.XPath("//span[@title='Test Selenium']"));

            var actualSubject = this.driver.FindElement(By.XPath("//span[@title='Test Selenium']")).Text;

            //Assert
            Assert.AreEqual(expectedSubject, actualSubject);
        }

        [Test]
        public void GetInfoFromDraftEmail()
        {
            //Arrange
            var expectedSubject = "Test Selenium01";
            var expectedAdressee = "testkate837@gmail.com";
            var expectedMessage = "Test Selenium from Kate";


            //Act
            LoginToYandexMail();

            IsElementVisible(By.XPath("//a[@href='#compose']"));

            this.driver.FindElement(By.XPath("//a[@href='#compose']")).Click();
            IsElementVisible(By.XPath("//div[@class='composeYabbles']"));
            this.driver.FindElement(By.XPath("//div[@class='composeYabbles']")).SendKeys("testkate837@gmail.com");
            this.driver.FindElement(By.XPath("//input[@class='composeTextField ComposeSubject-TextField']")).Click();
            this.driver.FindElement(By.XPath("//input[@class='composeTextField ComposeSubject-TextField']")).SendKeys("Test Selenium01");
            this.driver.FindElement(By.XPath("//div[@class='cke_wysiwyg_div cke_reset cke_enable_context_menu cke_editable cke_editable_themed cke_contents_ltr cke_htmlplaceholder']")).SendKeys("Test Selenium from Kate");
            this.driver.FindElement(By.XPath("//a[@href='#draft']")).Click();
            IsElementVisible(By.XPath("//span[@title='Test Selenium']"));

            var actualSubject = this.driver.FindElement(By.XPath("//span[@title='Test Selenium01']")).Text;
            var actualAdressee = this.driver.FindElement(By.XPath("//span[@title='testkate837@gmail.com']")).Text;
            var actualMessage = this.driver.FindElement(By.XPath("//span[@title='Test Selenium from Kate']")).Text;

            //Assert
            Assert.AreEqual(expectedSubject, actualSubject);
            Assert.AreEqual(expectedAdressee, actualAdressee);
            Assert.AreEqual(expectedMessage, actualMessage);
        }

        [Test]
        public void SendDraftEmail()
        {
            //Arrange
            var expectedSubject = "Test Sending Selenium";

            //Act
            LoginToYandexMail();

            IsElementVisible(By.XPath("//a[@href='#compose']"));

            this.driver.FindElement(By.XPath("//a[@href='#compose']")).Click();
            IsElementVisible(By.XPath("//div[@class='composeYabbles']"));
            this.driver.FindElement(By.XPath("//div[@class='composeYabbles']")).SendKeys("testkate837@gmail.com");
            this.driver.FindElement(By.XPath("//input[@class='composeTextField ComposeSubject-TextField']")).Click();
            this.driver.FindElement(By.XPath("//input[@class='composeTextField ComposeSubject-TextField']")).SendKeys("Test Sending Selenium");
            this.driver.FindElement(By.XPath("//div[@class='cke_wysiwyg_div cke_reset cke_enable_context_menu cke_editable cke_editable_themed cke_contents_ltr cke_htmlplaceholder']")).SendKeys("Test Selenium from Kate 2.0");
            this.driver.FindElement(By.XPath("//a[@href='#draft']")).Click();
            IsElementVisible(By.XPath("//span[@title='Test Sending Selenium']"));
            this.driver.FindElement(By.XPath("//span[@title='Test Sending Selenium']")).Click();
            this.driver.FindElement(By.XPath("//a[@class='Button2 Button2_pin_circle-circle Button2_view_default Button2_size_l']")).Click();
            this.driver.FindElement(By.XPath("//a[@href='#sent']")).Click();
            IsElementVisible(By.XPath("//span[@title='Test Sending Selenium']"));

            var actualSubject = this.driver.FindElement(By.XPath("//span[@title='Test Sending Selenium']")).Text;

            //Assert
            Assert.AreEqual(expectedSubject, actualSubject);
        }

        [Test]
        public void SendNewEmail()
        {
            //Arrange
            var expectedSubject = "Test Email";

            //Act
            LoginToYandexMail();

            IsElementVisible(By.XPath("//a[@href='#compose']"));

            this.driver.FindElement(By.XPath("//a[@href='#compose']")).Click();
            IsElementVisible(By.XPath("//div[@class='composeYabbles']"));
            this.driver.FindElement(By.XPath("//div[@class='composeYabbles']")).SendKeys("testkate837@gmail.com");
            this.driver.FindElement(By.XPath("//input[@class='composeTextField ComposeSubject-TextField']")).Click();
            this.driver.FindElement(By.XPath("//input[@class='composeTextField ComposeSubject-TextField']")).SendKeys("Test Email");
            this.driver.FindElement(By.XPath("//div[@class='cke_wysiwyg_div cke_reset cke_enable_context_menu cke_editable cke_editable_themed cke_contents_ltr cke_htmlplaceholder']")).SendKeys("Test Email");
            this.driver.FindElement(By.XPath("//a[@class='Button2 Button2_pin_circle-circle Button2_view_default Button2_size_l']")).Click();
            this.driver.FindElement(By.XPath("//a[@href='#sent']")).Click();
            IsElementVisible(By.XPath("//span[@title='Test Email']"));

            var actualSubject = this.driver.FindElement(By.XPath("//span[@title='Test Email']")).Text;

            //Assert
            Assert.AreEqual(expectedSubject, actualSubject);
        }

        [Test]
        public void SentEmailIsNotPresentInDraftFolder()
        {
            //Arrange
            var expectedMessage = "No messages in Drafts";

            //Act
            LoginToYandexMail();

            IsElementVisible(By.XPath("//a[@href='#compose']"));

            this.driver.FindElement(By.XPath("//a[@href='#compose']")).Click();
            IsElementVisible(By.XPath("//div[@class='composeYabbles']"));
            this.driver.FindElement(By.XPath("//div[@class='composeYabbles']")).SendKeys("testkate837@gmail.com");
            this.driver.FindElement(By.XPath("//input[@class='composeTextField ComposeSubject-TextField']")).Click();
            this.driver.FindElement(By.XPath("//input[@class='composeTextField ComposeSubject-TextField']")).SendKeys("Test Email02");
            this.driver.FindElement(By.XPath("//div[@class='cke_wysiwyg_div cke_reset cke_enable_context_menu cke_editable cke_editable_themed cke_contents_ltr cke_htmlplaceholder']")).SendKeys("Test Email");
            this.driver.FindElement(By.XPath("//a[@class='Button2 Button2_pin_circle-circle Button2_view_default Button2_size_l']")).Click();
            this.driver.FindElement(By.XPath("//a[@href='#draft']")).Click();
            IsElementVisible(By.XPath("//span[@title='Test Email02']"));

            var actualMessage = this.driver.FindElement(By.XPath("//span[text()='No messages in Drafts']")).Text;

            //Assert
            Assert.AreEqual(expectedMessage, actualMessage);
        }

        [Test]
        public void LogoutFromYandex()
        {
            //Arrange
            var expectedTitle = "Yandex Mail";

            //Act
            LoginToYandexMail();

            IsElementVisible(By.XPath("//span[text()='katetest.selenium']"));
            this.driver.FindElement(By.XPath("//span[text()='katetest.selenium']")).Click();
            this.driver.FindElement(By.XPath("//span[text()='Log out']")).Click();
            IsElementVisible(By.XPath("//h1[text()='Yandex Mail']"));
            var actualTitle = this.driver.FindElement(By.XPath("//h1[text()='Yandex Mail']")).Text;

            //Assert
            Assert.AreEqual(expectedTitle, actualTitle);
        }

        [Test]
        public void GetPossibilityToAddNewAccount()
        {
            //Arrange
            var expectedMessage = "Yandex Mail";

            //Act
            LoginToYandexMail();

            IsElementVisible(By.XPath("//span[text()='katetest.selenium']"));
            this.driver.FindElement(By.XPath("//span[text()='katetest.selenium']")).Click();
            this.driver.FindElement(By.XPath("//span[text()='Add account']")).Click();
            IsElementVisible(By.XPath("//span[text()='Log in with Yandex ID']"));
            var actualMessage = this.driver.FindElement(By.XPath("//span[text()='Log in with Yandex ID']")).Text;

            //Assert
            Assert.AreEqual(expectedMessage, actualMessage);
        }

        public void IsElementVisible(By element, int timeoutSecs = 20)
        {
            new WebDriverWait(this.driver, TimeSpan.FromSeconds(timeoutSecs)).Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(element));
        }

        public void LoginToYandexMail()
        {
            this.driver.FindElement(By.XPath("//a[@class='Button2 Button2_type_link Button2_view_default Button2_size_m']")).Click();
            this.driver.FindElement(By.Id("passp-field-login")).SendKeys("katetest.selenium@yandex.com");
            this.driver.FindElement(By.Id("passp:sign-in")).Click();
            IsElementVisible(By.Id("passp-field-passwd"));
            this.driver.FindElement(By.Id("passp-field-passwd")).SendKeys("testSelenium001");
            this.driver.FindElement(By.Id("passp:sign-in")).Click();
        }

        [TearDown]
        public void TestTearDown()
        {
            this.driver.Close();
            this.driver.Quit();
        }
    }
}
