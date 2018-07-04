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

    public class ProductsData
    {
        public string ProductName { get; set; } = "";
        public string BrandName { get; set; } = "";
    }

    [TestFixture]
    public class WebBrowserActionsTests
    {
        [Test]
        public void Actions()
        {
            var products = new[]
            {
                new ProductsData {ProductName = "iPhone 2"},
                new ProductsData {ProductName = "iPhone 2", BrandName = "APPLE"},
                new ProductsData {BrandName = "WHIRLPOOL"},
            };

            var chromeOptions = new ChromeOptions();

            chromeOptions.AddArguments("--start-maximized");

            var WebDriver = new ChromeDriver(chromeOptions);

            WebDriver.Navigate().GoToUrl("http://bo-preprod.apreslachat.com/accueil-administrateur");

            UpdateValue(WebDriver, "#UserName", "suhji");
            UpdateValue(WebDriver, "#Password", "123456");

            Click(WebDriver, "span:contains(\"Connexion\")");

            WebDriver.Navigate().GoToUrl("http://bo-preprod.apreslachat.com/produits/recherche");

            foreach (var product in products)
            {
                var productNamePosition = WaitElementAsPosition(WebDriver, "#productName");

                UpdateValue(WebDriver, productNamePosition, product.ProductName);
                UpdateValue(WebDriver, "#brandName", product.BrandName);
                UpdateValue(WebDriver, "#productCreationDate", string.Empty);
                UpdateChecked(WebDriver, "#MerchantCheckBox", false);
                UpdateValue(WebDriver, "#merchantIds", "0");

                Click(WebDriver, ".search_product");

                WaitCondition(WebDriver, "#products_table tr:visible", "length", numberOfProducts => numberOfProducts > 2, 150000);
            }

            WebDriver.Quit();
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

        public void UpdateText(IWebDriver WebDriver, WebElementPosition position, string newText) =>
            ExecuteCommandAtPosition(WebDriver, $"val('{newText}')", position);

        public void UpdateValue(IWebDriver WebDriver, string selectorJquery, string newText) =>
            ExecuteCommand(WebDriver, selectorJquery, $"val('{newText}')");

        public void UpdateValue(IWebDriver WebDriver, WebElementPosition position, string newText) =>
            ExecuteCommandAtPosition(WebDriver, $"val('{newText}')", position);
        #endregion

        #region Checked
        public void UpdateChecked(IWebDriver WebDriver, string jquerySelector, bool toCheck = true) =>
            ExecuteCommand(WebDriver, jquerySelector, $"prop('checked', {toCheck.ToString().ToLower()})");

        public bool IsChecked(IWebDriver WebDriver, string jquerySelector) =>
            GetElementsAsDynamic(WebDriver, jquerySelector, $"prop('checked')") == true;
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

        public WebElementPosition WaitElementAsPosition(IWebDriver WebDriver, string jquerySelector, int timeOutMilliseconds = 5000, int waitTimeMilliseconds = 500)
        {
            var positionDynamic = WaitElementAsDynamic(WebDriver, jquerySelector, "position()", timeOutMilliseconds, waitTimeMilliseconds);

            return new WebElementPosition()
            {
                PosX = positionDynamic.left.Value,
                PosY = positionDynamic.top.Value,
            };
        }

        public dynamic WaitElementAsDynamic(IWebDriver WebDriver, string jquerySelector, string jqueryCommand = "", int timeOutMilliseconds = 5000, int waitTimeMilliseconds = 500) =>
            JsonConvert.DeserializeObject<dynamic>(WaitElementAsJson(WebDriver, jquerySelector, jqueryCommand, timeOutMilliseconds, waitTimeMilliseconds));

        public string WaitElementAsJson(IWebDriver WebDriver, string jquerySelector, string jqueryCommand = "", int timeOutMilliseconds = 5000, int waitTimeMilliseconds = 500)
        {
            var timeOutTimer = new Stopwatch();
            var waitTimer = new Stopwatch();

            timeOutTimer.Start();
            waitTimer.Start();

            while (timeOutTimer.ElapsedMilliseconds < timeOutMilliseconds)
            {
                if (waitTimer.ElapsedMilliseconds > waitTimeMilliseconds)
                {
                    var rawJson = GetElementsAsJson(WebDriver, jquerySelector, jqueryCommand);

                    if (!string.IsNullOrWhiteSpace(rawJson))
                        return rawJson;

                    waitTimer.Restart();
                }
            }
            
            throw new TimeoutException(nameof(timeOutMilliseconds));
        }

        public void WaitCondition(IWebDriver WebDriver, string jquerySelector, string jqueryCommand, Func<dynamic, bool> condition, int timeOutMilliseconds = 5000, int waitTimeMilliseconds = 500)
        {
            var timeOutTimer = new Stopwatch();
            var waitTimer = new Stopwatch();

            timeOutTimer.Start();
            waitTimer.Start();

            while (timeOutTimer.ElapsedMilliseconds < timeOutMilliseconds)
            {
                if (waitTimer.ElapsedMilliseconds > waitTimeMilliseconds)
                {
                    var rawJson = GetElementsAsJson(WebDriver, jquerySelector, jqueryCommand);

                    if (!string.IsNullOrWhiteSpace(rawJson))
                    {
                        var dynamicReply = JsonConvert.DeserializeObject<dynamic>(rawJson);

                        if (condition(dynamicReply))
                            return;
                    }

                    waitTimer.Restart();
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

            var command = "$('body').find('*').filter(function() { return $(this).position().left >= " + (posX - 1) +
                          " && $(this).position().left <= " + (posX + 1) +
                          " && $(this).position().top >= " + (posY - 1) +
                          " && $(this).position().top <= " + posY + 1 + 
                          "; })." + jqueryCommand;

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
}
