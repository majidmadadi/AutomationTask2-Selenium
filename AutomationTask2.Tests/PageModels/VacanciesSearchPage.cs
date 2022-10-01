
using OpenQA.Selenium;
using System.Collections.Generic;

namespace AutomationTask2.Tests.PageModels
{
    public class VacanciesSearchPage
    {
        private IWebDriver _driver;

        public VacanciesSearchPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public IWebElement CookieScriptClose
        {
            get
            {
                try
                {
                    return _driver.FindElement(By.Id("cookiescript_close"));
                }
                catch
                {
                    return null;
                }
            }
        }

        public IWebElement Keywords => _driver.FindElement(By.XPath("//input[@placeholder='Keyword']"));

        //ClearForm() must be called before this this function
        public VacanciesSearchPage SelectDepartment(string text)
        {
            var departments = _driver.FindElement(By.XPath("//button[contains(text(),'All departments')]"));
            departments.Click();
            var link = _driver.FindElement(By.XPath($"//a[contains(text(),'{text}')]"));
            link.Click();
            return this;
        }

        //ClearForm() must be called before this this function
        public VacanciesSearchPage SelectLanguages(string[] languages)
        {
            var langs = _driver.FindElement(By.XPath("//button[contains(text(),'All languages')]"));
            langs.Click();
            foreach (var lang in languages)
            {
                var label = _driver.FindElement(By.XPath($"//label[contains(text(),'{lang}')]"));
                var checkbox = _driver.FindElement(By.Id(label.GetAttribute("for")));
                checkbox.Click();
            }
            return this;
        }

        public void ClearForm()
        {
            //make sure 'clear filter' is clicked
            var script = @"var btn = document.evaluate(""//button[text()='Clear filters']"", document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue; btn.click();";
            ((IJavaScriptExecutor)_driver).ExecuteScript(script);
        }

        public VacanciesSearchPage Navigate(string v)
        {
            _driver.Navigate().GoToUrl(v);
            return this;
        }

        public IReadOnlyList<IWebElement> SearchResults
            => _driver.FindElements(By.CssSelector("a[data-vacancy]:not([data-vacancy=''])"));
    }
}
