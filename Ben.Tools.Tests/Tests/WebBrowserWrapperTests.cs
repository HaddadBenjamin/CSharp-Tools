using System.Collections.Generic;
using BenTools.Wrappers;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;

namespace BenTools.Tests.Tests
{
    [TestFixture]
    public class WebBrowserWrapperTests
    {
        #region Field(s)
        private List<WebBrowserWrapper> WebBrowserWrappers;
        private bool FirstTest = true;
        #endregion

        #region Callback(s)
        [OneTimeSetUp]
        public void OnInitializeAllTests()
        {
            WebBrowserWrappers = new List<WebBrowserWrapper>();

            var chromeOptions = new ChromeOptions();
            //var firefoxOptions = new FirefoxOptions();

            chromeOptions.AddArguments("--start-maximized");
            //firefoxOptions.AddArguments("--start-maximized");

            var chromeDriver = new ChromeDriver(chromeOptions);
            //var firefoxDriver = new FirefoxDriver(firefoxOptions);

            WebBrowserWrappers.Add(new WebBrowserWrapper(chromeDriver));
            //WebBrowserWrappers.Add(new WebBrowserWrapper(firefoxDriver));
        }

        [OneTimeTearDown]
        public void OnFinishAllTests()
        {
            WebBrowserWrappers.ForEach(WebBrowserWrapper => WebBrowserWrapper.Dispose());
        }
        #endregion

        #region Test(s)
        [TestCase("", "WHIRLPOOL")]
        [TestCase("iPhone 2", "")]
        [TestCase("iPhone 2", "APPLE")]
        public void ElasticSearchProductQueries(string productName, string brandName)
        {
            WebBrowserWrappers.ForEach(WebBrowserWrapper =>
            {
                if (FirstTest)
                {
                    WebBrowserWrapper.GoToUrl("http://bo-preprod.apreslachat.com/accueil-administrateur");

                    WebBrowserWrapper.UpdateValue("#UserName", "suhji", 250);
                    WebBrowserWrapper.UpdateValue("#Password", "123456", 150);

                    WebBrowserWrapper.Click("span:contains(\"Connexion\")");

                    WebBrowserWrapper.GoToUrl("http://bo-preprod.apreslachat.com/produits/recherche");

                    FirstTest = false;
                }

                WebBrowserWrapper.WaitElement("#productName");

                WebBrowserWrapper.UpdateValue("#productName", productName);
                WebBrowserWrapper.UpdateValue("#brandName", brandName, 80);
                WebBrowserWrapper.UpdateValue("#productCreationDate", string.Empty);
                WebBrowserWrapper.UpdateChecked("#MerchantCheckBox", false);
                WebBrowserWrapper.UpdateValue("#merchantIds", "0");

                WebBrowserWrapper.Click(".search_product");

                WebBrowserWrapper.WaitCondition("#products_table tr:visible", "length", numberOfProducts => numberOfProducts > 2, 150000);
            });          
            }

        [Test]
        public void PortfolioTest()
        {
            WebBrowserWrappers.ForEach(WebBrowserWrapper =>
            {
                WebBrowserWrapper.GoToUrl("http://haddad-benjamin-portfolio.azurewebsites.net/");

                WebBrowserWrapper.WaitElement(".go-to-page-top");

                WebBrowserWrapper.ScrollBodyAndWait(".go-to-page-top");

                //var position = WebBrowserWrapper.GetElementPosition(".go-to-page-top");
                //var selector = WebBrowserWrapper.GetUniqueJQuerySelector(position);
            });
        }
        #endregion
    }
}
