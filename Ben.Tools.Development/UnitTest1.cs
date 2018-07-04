using Newtonsoft.Json;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Diagnostics;
using System.Threading;

namespace Ben.Tools.Development
{
    public class WebElementPosition
    {
        public double PosX { get; set; }
        public double PosY { get; set; }
    }

    [TestFixture]
    public class WebBrowserActionsTests
    {
        [Test]
        public void Actions()
        {
            var chromeOptions = new ChromeOptions();

            chromeOptions.AddArguments("--start-maximized");

            var WebDriver = new ChromeDriver(chromeOptions);

            WebDriver.Navigate().GoToUrl("http://bo-preprod.apreslachat.com/accueil-administrateur");

            UpdateValue(WebDriver, "#UserName", "suhji");
            UpdateValue(WebDriver, "#Password", "123456");

            Click(WebDriver, "span:contains(\"Connexion\")");

            //"a:contains(\"Produits\")"
            // click
            // wait until avialable et tester.
        }

        #region CSS
        public dynamic GetCssProperty(IWebDriver WebDriver, string selectorJquery, string cssPropertyName) =>
            GetElementsAsDynamic(WebDriver, selectorJquery, $"css('{cssPropertyName}')");

        public void UpdateCssProperty<CSSPropertyType>(IWebDriver WebDriver, string selectorJquery, string cssPropertyName, CSSPropertyType cssPopertyValue) =>
            ExecuteCommand(WebDriver, selectorJquery, $"css('{cssPropertyName}', '{cssPopertyValue}')");
        #endregion

        #region Text & Val
        public string GetText(IWebDriver WebDriver, string selectorJquery) =>
            GetElementsAsDynamic(WebDriver, selectorJquery, "text()");

        public string GetValue(IWebDriver WebDriver, string selectorJquery) =>
            GetElementsAsDynamic(WebDriver, selectorJquery, "val()");

        public void AddText(IWebDriver WebDriver, string selectorJquery, string addText) =>
            ExecuteCommand(WebDriver, selectorJquery, $"text('{GetText(WebDriver, selectorJquery)}{addText}')");

        public void AddValue(IWebDriver WebDriver, string selectorJquery, string addText) =>
            ExecuteCommand(WebDriver, selectorJquery, $"val('{GetValue(WebDriver, selectorJquery)}{addText}')");

        public void UpdateText(IWebDriver WebDriver, string selectorJquery, string newText) =>
            ExecuteCommand(WebDriver, selectorJquery, $"text('{newText}')");

        public void UpdateValue(IWebDriver WebDriver, string selectorJquery, string newText) =>
            ExecuteCommand(WebDriver, selectorJquery, $"val('{newText}')");
        #endregion

        #region Click
        public void Click(IWebDriver WebDriver, WebElementPosition position) =>
            Click(WebDriver, position.PosX, position.PosY);

        public void Click(IWebDriver WebDriver, double posX, double posY) =>
            ExecuteCommandAtPosition(WebDriver, "click()", posX, posY);

        public void Click(IWebDriver WebDriver, string selectorJquery) =>
            ExecuteCommand(WebDriver, selectorJquery, "click()");
        #endregion

        #region Get Elements
        public WebElementPosition GetElementsAsPosition(IWebDriver WebDriver, string jquerySelector)
        {
            var positionDynamic = GetElementsAsDynamic(WebDriver, jquerySelector, "position()");

            return new WebElementPosition()
            {
                PosX = positionDynamic.left.Value,
                PosY = positionDynamic.top.Value,
            };
        }

        public dynamic GetElementsAsDynamic(IWebDriver WebDriver, string jquerySelector, string jqueryCommand = null) =>
            JsonConvert.DeserializeObject<dynamic>(GetElementsAsJson(WebDriver, jquerySelector, jqueryCommand));

        public string GetElementsAsJson(IWebDriver WebDriver, string jquerySelector, string jqueryCommand = "")
        {
            if (string.IsNullOrWhiteSpace(jquerySelector) && string.IsNullOrWhiteSpace(jqueryCommand))
                throw new ArgumentException(nameof(jquerySelector), "jquerySelector et jqueryCommand are empties");

            if (!string.IsNullOrWhiteSpace(jqueryCommand) && !string.IsNullOrWhiteSpace(jquerySelector))
                jqueryCommand = "." + jqueryCommand;

            var command = !string.IsNullOrWhiteSpace(jquerySelector) ?
                $"return JSON.stringify($('{jquerySelector}'){jqueryCommand})" :
                $"return JSON.stringify({jqueryCommand})";

            return (string)((IJavaScriptExecutor)WebDriver).ExecuteScript(command);
        }

        public WebElementPosition GetElementUntilItBecomeAvailableAsPosition(IWebDriver WebDriver, string jquerySelector, int timeOutMilliseconds = 5000, int waitTimeMilliseconds = 500)
        {
            var positionDynamic = GetElementUntilItBecomeAvailableAsDynamic(WebDriver, jquerySelector, "position()", timeOutMilliseconds, waitTimeMilliseconds);

            return new WebElementPosition()
            {
                PosX = positionDynamic.left.Value,
                PosY = positionDynamic.top.Value,
            };
        }

        public dynamic GetElementUntilItBecomeAvailableAsDynamic(IWebDriver WebDriver, string jquerySelector, string jqueryCommand = "", int timeOutMilliseconds = 5000, int waitTimeMilliseconds = 500) =>
            JsonConvert.DeserializeObject<dynamic>(GetElementUntilItBecomeAvailableAsJson(WebDriver, jquerySelector, jqueryCommand, timeOutMilliseconds, waitTimeMilliseconds));

        public string GetElementUntilItBecomeAvailableAsJson(IWebDriver WebDriver, string jquerySelector, string jqueryCommand = "", int timeOutMilliseconds = 5000, int waitTimeMilliseconds = 500)
        {
            var timeOutTimer = new Stopwatch();
            var waitTimer = new Stopwatch();

            while (timeOutTimer.ElapsedMilliseconds < timeOutMilliseconds)
            {
                if (waitTimer.ElapsedMilliseconds > waitTimeMilliseconds)
                {
                    var rawJson = GetElementsAsJson(WebDriver, jquerySelector, jqueryCommand);

                    if (!string.IsNullOrWhiteSpace(rawJson))
                        return rawJson;

                    waitTimer.Reset();
                }
            }
            
            throw new TimeoutException(nameof(timeOutMilliseconds));
        }
        #endregion

        #region Execute Command
        public void ExecuteCommand(IWebDriver WebDriver, string jquerySelector = null, string jqueryCommand = null)
        {
            if (string.IsNullOrWhiteSpace(jquerySelector) && string.IsNullOrWhiteSpace(jqueryCommand))
                throw new ArgumentException(nameof(jquerySelector), "jquerySelector et jqueryCommand are empties");

            if (!string.IsNullOrWhiteSpace(jqueryCommand) && !string.IsNullOrWhiteSpace(jquerySelector))
                jqueryCommand = "." + jqueryCommand;

            var command = !string.IsNullOrWhiteSpace(jquerySelector) ?
                $"$('{jquerySelector}'){jqueryCommand}" :
                $"{jqueryCommand}";

            ((IJavaScriptExecutor)WebDriver).ExecuteScript(command);
        }

        public void ExecuteCommandAtPosition(IWebDriver WebDriver, string jqueryCommand, WebElementPosition position) =>
            ExecuteCommandAtPosition(WebDriver, jqueryCommand, position.PosX, position.PosY);

        public void ExecuteCommandAtPosition(IWebDriver WebDriver, string jqueryCommand, double posX, double posY)
        {
            if (string.IsNullOrWhiteSpace(jqueryCommand))
                throw new ArgumentException(nameof(jqueryCommand), "jqueryCommand is empty");

            var command = $"$(document.elementFromPoint({posX}, {posY})).{jqueryCommand}";

            ((IJavaScriptExecutor)WebDriver).ExecuteScript(command);
        }
        #endregion
    }

    //public abstract class ASeleniumWrapper : IDisposable
    //{
    //    protected string jQueryPathOrUrl { get; set; } = "https://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js";
    //    protected List<RemoteWebDriver> WebDrivers { get; set; }
    //    private string JQueryScript { get; set; }

    //    public ASeleniumWrapper()
    //    {
    //        WebDrivers = InitializeWebDrivers();

    //        JQueryScript = string.Join(
    //            Environment.NewLine,
    //            "(function ()",
    //            "{",
    //            "if (typeof jQuery === 'undefined')",
    //            "{",
    //            "var headTag = document.getElementsByTagName(\"head\")[0];",
    //            "var jqTag = document.createElement('script');",
    //            "jqTag.type = 'text/javascript';",
    //            "jqTag.src = '" + jQueryPathOrUrl + "';",
    //            "headTag.appendChild(jqTag);",
    //            "}",
    //            "})();");
    //    }

    //    protected void IncludeNotDefinedJQuery(RemoteWebDriver WebDriver)
    //    {
    //        var javaScriptExecutor = (IJavaScriptExecutor)WebDriver;

    //        javaScriptExecutor.ExecuteScript(File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Configurations", "IncludeNotDefinedJquery.js")));
    //        //javaScriptExecutor.ExecuteScript(JQueryScript);
    //    }

    //    protected abstract List<RemoteWebDriver> InitializeWebDrivers();

    //    public void Dispose()
    //    {
    //        foreach (var remoteWebDriver in WebDrivers)
    //            remoteWebDriver.Dispose();
    //    }
    //}

    //[TestFixture]
    //public class ProductsTests : ASeleniumWrapper
    //{
    //    protected override List<RemoteWebDriver> InitializeWebDrivers()
    //    {
    //        var webDrivers = new List<RemoteWebDriver>();

    //        var chromeOptions = new ChromeOptions();

    //        //chromeOptions.AddArguments("--headless");
    //        chromeOptions.AddArguments("--start-maximized");

    //        webDrivers.Add(new ChromeDriver(chromeOptions));

    //        var firefoxOptions = new FirefoxOptions();

    //        //firefoxOptions.AddArguments("--headless");
    //        firefoxOptions.AddArguments("--start-maximized");

    //        webDrivers.Add(new FirefoxDriver(firefoxOptions));

    //        //var ieOptions = new InternetExplorerOptions();

    //        //webDrivers.Add(new InternetExplorerDriver(ieOptions));

    //        return webDrivers;
    //    }

    //    [OneTimeTearDown]
    //    public void FinishTests()
    //    {
    //        base.Dispose();
    //    }

    //    private void GoToProductsTab(RemoteWebDriver WebDriver)
    //    {
    //        WebDriver.Navigate().GoToUrl("http://bo-preprod.apreslachat.com/accueil-administrateur");

    //        IncludeNotDefinedJQuery(WebDriver);

    //        // Si l'utilisateur n'est pas encore loggé alors on l'identifie.
    //        if (WebDriver.ContainsElements("input#UserName"))
    //        {
    //            WebDriver.ExecuteJQuery("input#UserName", "val('suhji')");
    //            WebDriver.ExecuteJQuery("input#Password", "val('123456')");
    //            WebDriver.ExecuteJQuery(".main_button", "click()");

    //            WebDriver.FindElement(WebDriver.WaitUntiJQuerySelector("a[href=\"/produits/recherche\"]:contains(\"Produits\")", 5000)).Click();
    //        }
    //    }

    //    public class ElasticSearchQuertTestData
    //    {
    //        public string ProductName = "";
    //        public string BrandName = "";
    //        public int MaxTimeToWaitBeforeGetResult = 5000;
    //    }

    //    [Test]
    //    public void ElasticSearchQueries_Test()
    //    {
    //        var datas = new List<ElasticSearchQuertTestData>()
    //        {
    //            new ElasticSearchQuertTestData()
    //            {
    //                BrandName = "Whirlpool",
    //                MaxTimeToWaitBeforeGetResult = 15000
    //            },
    //            new ElasticSearchQuertTestData()
    //            {
    //                ProductName = "iPhone 2",
    //                BrandName = "APPLE"
    //            },
    //            new ElasticSearchQuertTestData()
    //            {
    //                ProductName = "iPhone 2",
    //            }
    //        };

    //        foreach (var data in datas)
    //        {
    //            foreach (var WebDriver in WebDrivers)
    //            {
    //                GoToProductsTab(WebDriver);

    //                WebDriver.ExecuteJavascriptCommand("$('#productName').val('" + data.ProductName + "')");
    //                WebDriver.ExecuteJavascriptCommand("$('#brandName').val('" + data.BrandName + "')");
    //                WebDriver.ExecuteJQuery("#MerchantCheckBox", "prop('checked', true)");
    //                WebDriver.ExecuteJQuery(".a_margin_bottom input[type=\"submit\"]", "click()");

    //                var products = WebDriver.FindElements(WebDriver.WaitUntiJQuerySelector("#products_table tr:visible", data.MaxTimeToWaitBeforeGetResult));

    //                Assert.That(products.Count(), Is.GreaterThan(2));
    //            };
    //        }
    //    }
    //}

    //public static class WebDriverExtension
    //{
    //    public static IWebDriver ExecuteJQuery(this IWebDriver webDriver, string selectorJQuery, string commandJQuery = null)
    //    {
    //        var command = string.Empty;

    //        if (string.IsNullOrWhiteSpace(selectorJQuery))
    //            throw new ArgumentException(nameof(selectorJQuery), "Jquery selector is empty.");

    //        command += string.Format("$('{0}')", selectorJQuery);

    //        if (!string.IsNullOrWhiteSpace(commandJQuery))
    //            command += string.Format(".{0}", commandJQuery);

    //        return webDriver.ExecuteJavascriptCommand(command);
    //    }

    //    public static ReturnType GetResultFromJqueryCommand<ReturnType>(this IWebDriver webDriver, string selectorJQuery = null, string commandJQuery = null)
    //    {
    //        var command = "return ";

    //        if (!string.IsNullOrWhiteSpace(selectorJQuery))
    //            command += string.Format("$('{0}')", selectorJQuery);

    //        if (string.IsNullOrWhiteSpace(commandJQuery))
    //            throw new ArgumentException(nameof(commandJQuery), "Jquery command is empty.");

    //        command += string.Format(".{0}", commandJQuery);

    //        var jsExecutor = (IJavaScriptExecutor)webDriver;

    //        return (ReturnType)jsExecutor.ExecuteScript(command);
    //    }

    //    public static IWebDriver ExecuteJavascriptCommand(this IWebDriver webDriver, string commandJQuery)
    //    {
    //        //commandJQuery = commandJQuery.Replace("\"", string.Empty);

    //        var jsExecutor = (IJavaScriptExecutor)webDriver;

    //        jsExecutor.ExecuteScript(commandJQuery);

    //        return webDriver;
    //    }

    //    public static JQuerySelector WaitUntiJQuerySelector(this IWebDriver webDriver, string selectorJQuery, long timeToWaitMilliseconds)
    //    {
    //        var stopwatch = new Stopwatch();

    //        stopwatch.Start();

    //        while (stopwatch.ElapsedMilliseconds < timeToWaitMilliseconds)
    //        {
    //            var jquerySelector = By.JQuerySelector(selectorJQuery);

    //            if (webDriver.ContainsElements(jquerySelector.GetSelector()))
    //                return jquerySelector;
    //        }

    //        return null;
    //    }

    //    public static bool ContainsElements(this IWebDriver webDriver, string jquerySelector)
    //    {
    //        return webDriver.GetResultFromJqueryCommand<Int64>(jquerySelector, "length") > 0;
    //    }

    //    public static IWebElement GetJqueryWebElement(this IWebDriver webDriver, string jquerySelector)
    //    {
    //        return webDriver.ExecuteJavaScript<IWebElement>("return $('" + jquerySelector + "')[0];");
    //    }
    //}

    //public static class ByExtension
    //{
    //    public static bool ContainsElements(this OpenQA.Selenium.By bySelector, IWebDriver webDriver)
    //    {
    //        return webDriver.ContainsElements(bySelector.GetSelector());
    //    }

    //    public static string GetSelector(this OpenQA.Selenium.By bySelector)
    //    {
    //        return bySelector.ToString().Substring(bySelector.ToString().IndexOf(":") + 1);
    //    }
    //}
}
