
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

        /// <summary>
        /// ClearForm() must be called before this function. Because we have reset filters
        /// before calling this function, we don't need to check whether it is already
        /// selected or not.
        /// </summary>
        /// <param name="text"></param>
        /// <returns>VacanciesSearchPage</returns>
        public VacanciesSearchPage SelectDepartment(string text)
        {
            var departments = _driver.FindElement(By.XPath("//button[contains(text(),'All departments')]"));
            departments.Click();
            var link = _driver.FindElement(By.XPath($"//a[contains(text(),'{text}')]"));
            link.Click();
            return this;
        }

        /// <summary>
        /// ClearForm() must be called before this function. Because we have reset filters
        /// before calling this function, we don't need to check whether it is already
        /// selected or not.
        /// </summary>
        /// <param name="languages"></param>
        /// <returns>VacanciesSearchPage</returns>
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

        /// <summary>
        /// Reliable clear form using javascript. The Clear_filters link is covered 
        /// by drop-down menues and prevents the driver click to work.
        /// </summary>
        public void ClearForm()
        {
            var script = @"var btn = document.evaluate(""//button[text()='Clear filters']"", document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue; btn.click();";
            ((IJavaScriptExecutor)_driver).ExecuteScript(script);
        }

        public VacanciesSearchPage Navigate(string v)
        {
            _driver.Navigate().GoToUrl(v);
            return this;
        }

        /// <summary>
        /// Finds search results elements using data-vacancy attribute. 
        /// It's more reliable than using class name.
        /// </summary>
        public IReadOnlyList<IWebElement> SearchResults
            => _driver.FindElements(By.CssSelector("a[data-vacancy]:not([data-vacancy=''])"));
    }
}
