using AutomationTask2.Tests.PageModels;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace AutomationTask2.Tests
{
    public class VacanciesSearchResultTests
    {
        protected VacanciesSearchPage vPage;
        protected IWebDriver _driver;

        [OneTimeSetUp]
        public void OpenPage()
        {
            _driver = new ChromeDriver();
            _driver.Manage().Window.Maximize();
            vPage = new VacanciesSearchPage(_driver);
            vPage.Navigate("https://cz.careers.veeam.com/vacancies");
            if (vPage.CookieScriptClose != null)
            {
                vPage.CookieScriptClose.Click();
            }
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            _driver.Quit();
        }

        [SetUp]
        public void ClearForm()
        {
            vPage.ClearForm();
        }

        [Test]
        [TestCase("", "Research & Development", new[] { "English" }, 11)]
        [TestCase("Webdriver C# best practices", "Research & Development", new[] { "English" }, 0)]
        [TestCase("Page Object", "Research & Development", new[] { "English" }, 0)]
        [TestCase("implicit/explicit waits", "Research & Development", new[] { "English" }, 0)]
        [TestCase("reliable/fragile locators", "Research & Development", new[] { "English" }, 0)]
        [TestCase("NUnit", "Research & Development", new[] { "English" }, 0)]
        [TestCase("C#", "Research & Development", new[] { "English" }, 7)]
        public void SearchResultsBeCorrect(string keyword, string department, string[] languages, int numberOfResults)
        {
            vPage
                .SelectDepartment(department)
                .SelectLanguages(languages)
                .Keywords.Clear();
            vPage
                .Keywords.SendKeys(keyword);
            Assert.That(vPage
                .SearchResults
                .Count, Is.EqualTo(numberOfResults));
        }
    }
}
