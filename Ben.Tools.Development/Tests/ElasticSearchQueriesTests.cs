using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using System.Collections.Generic;

namespace Ben.Tools.Development
{
    [TestFixture]
    public class ElasticSearchQueriesTests
    {
        #region Field(s)
        private IEnumerable<ProductsData> Products;
        private WebBrowserWrapper WebBrowserWrapper;
        #endregion

        #region Callback(s)
        [OneTimeSetUp]
        public void OnInitializeAllTests()
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

            WebBrowserWrapper = new WebBrowserWrapper(WebDriver);
        }

        [OneTimeTearDown]
        public void OnFinishAllTests()
        {
            WebBrowserWrapper.Dispose();
        }
        #endregion

        #region Test(s)
        [Test]
        public void ElasticSearchProductQueries()
        {
            WebBrowserWrapper.GoToUrl("http://bo-preprod.apreslachat.com/accueil-administrateur");

            WebBrowserWrapper.UpdateValue("#UserName", "suhji");
            WebBrowserWrapper.UpdateValue("#Password", "123456");

            WebBrowserWrapper.Click("span:contains(\"Connexion\")");

            WebBrowserWrapper.GoToUrl("http://bo-preprod.apreslachat.com/produits/recherche");

            foreach (var product in Products)
            {
                var productNamePosition = WebBrowserWrapper.WaitElementAsPosition("#productName");

                WebBrowserWrapper.UpdateValue(productNamePosition, product.ProductName);
                WebBrowserWrapper.UpdateValue("#brandName", product.BrandName);
                WebBrowserWrapper.UpdateValue("#productCreationDate", string.Empty);
                WebBrowserWrapper.UpdateChecked("#MerchantCheckBox", false);
                WebBrowserWrapper.UpdateValue("#merchantIds", "0");

                WebBrowserWrapper.Click(".search_product");

                WebBrowserWrapper.WaitCondition("#products_table tr:visible", "length", numberOfProducts => numberOfProducts > 2, 150000);
            }
        }
        #endregion
    }


    //public abstract class ASeleniumWrapper : IDisposable
    //{
    //    protected List<RemoteWebDriver> WebDrivers { get; set; }

    //    public ASeleniumWrapper()
    //    {
    //        WebDrivers = InitializeWebDrivers();
    //    }

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
