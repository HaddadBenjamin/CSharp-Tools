using System;
using System.Diagnostics;
using BenTools.Wrappers.Models;
using Newtonsoft.Json;
using OpenQA.Selenium;

namespace BenTools.Wrappers
{
    public class WebBrowserWrapper : IDisposable
    {
        #region Field(s)
        public readonly IWebDriver WebDriver;
        #endregion

        #region Constructor(s)
        public WebBrowserWrapper(IWebDriver webDriver)
        {
            WebDriver = webDriver;
        }
        #endregion

        #region WebElement(s) Behaviour(s)
        #region CSS
        public dynamic GetCssProperty(string selectorJquery, string cssPropertyName) => GetDynamicElement(selectorJquery, $"css('{cssPropertyName}')");

        public void UpdateCssProperty<CSSPropertyType>(string selectorJquery, string cssPropertyName, CSSPropertyType cssPopertyValue) => ExecuteQuery(selectorJquery, $"css('{cssPropertyName}', '{cssPopertyValue}')");
        #endregion

        #region Text & Val
        public string GetText(string selectorJquery) => GetDynamicElement(selectorJquery, "text()");

        public string GetValue(string selectorJquery) => GetDynamicElement(selectorJquery, "val()");

        public void AddText(string selectorJquery, string addText) =>ExecuteQuery(selectorJquery, $"text('{GetText(selectorJquery)}{addText}')");
   
        public void AddValue(string selectorJquery, string addText) => ExecuteQuery(selectorJquery, $"val('{GetValue(selectorJquery)}{addText}')");

        public void UpdateText(string selectorJquery, string newText) => ExecuteQuery(selectorJquery, $"text('{newText}')");

        public void UpdateValue(string selectorJquery, string newText) => ExecuteQuery(selectorJquery, $"val('{newText}')");

        public void UpdateValue(string selectorJquery, string newText, int eachCharacterMilliseconds = 250)
        {
            var firstCharacter = true;
            var timer = new Stopwatch();

            timer.Start();

            foreach (var @char in newText.ToCharArray())
            {
                if (firstCharacter)
                {
                    ExecuteQuery(selectorJquery, $"val('{@char}')");
                    firstCharacter = false;
                }
                else
                {
                    while (timer.ElapsedMilliseconds < eachCharacterMilliseconds);

                    AddValue(selectorJquery, @char.ToString());

                    timer.Restart();
                }
            }
        }
        #endregion

        #region Checked
        public void UpdateChecked(string jquerySelector, bool toCheck = true) => ExecuteQuery(jquerySelector, $"prop('checked', {toCheck.ToString().ToLower()})");

        public bool IsChecked(string jquerySelector) => GetDynamicElement(jquerySelector, $"prop('checked')") == true;
        #endregion

        #region Click
        public void Click(string selectorJquery) => ExecuteQuery(selectorJquery, "click()");
        #endregion

        #region Get Elements
        public string GetUniqueJQuerySelector(WebElementPosition position)
        {
            var query = "return " + BuildPositionQuery("getUniqueSelector()", position);

            return (string)((IJavaScriptExecutor)WebDriver).ExecuteScript(query);
        }

        public WebElementPosition GetElementPosition(string jquerySelector) => ElementDynamicToPosition(GetDynamicElement(GetJsonElement(jquerySelector, "position()")));

        public dynamic GetDynamicElement(string jquerySelector, string jqueryCommand = null) => JsonConvert.DeserializeObject<dynamic>(GetJsonElement(jquerySelector, jqueryCommand));

        public string GetJsonElement(string jquerySelector, string jqueryCommand = "")
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

        public WebElementPosition WaitPositionElement(string jquerySelector, int timeOutMilliseconds = 5000, int waitTimeMilliseconds = 500) =>
            ElementDynamicToPosition(WaitDynamicElement(jquerySelector, "position()", timeOutMilliseconds, waitTimeMilliseconds));

        public dynamic WaitDynamicElement(string jquerySelector, string jqueryCommand = "", int timeOutMilliseconds = 5000, int waitTimeMilliseconds = 500) =>
            JsonConvert.DeserializeObject<dynamic>(WaitJsonElement(jquerySelector, jqueryCommand, timeOutMilliseconds, waitTimeMilliseconds));

        public string WaitJsonElement(string jquerySelector, string jqueryCommand = "", int timeOutMilliseconds = 5000, int waitTimeMilliseconds = 500)
        {
            var timeOutTimer = new Stopwatch();
            var waitTimer = new Stopwatch();

            timeOutTimer.Start();
            waitTimer.Start();

            while (timeOutTimer.ElapsedMilliseconds < timeOutMilliseconds)
            {
                if (waitTimer.ElapsedMilliseconds > waitTimeMilliseconds)
                {
                    var rawJson = GetJsonElement(jquerySelector, jqueryCommand);

                    if (!string.IsNullOrWhiteSpace(rawJson))
                        return rawJson;

                    waitTimer.Restart();
                }
            }

            throw new TimeoutException(nameof(timeOutMilliseconds));
        }

        public void WaitElement(string jquerySelector, string jqueryCommand = "", int timeOutMilliseconds = 5000, int waitTimeMilliseconds = 500) =>
            WaitJsonElement(jquerySelector, jqueryCommand, timeOutMilliseconds, waitTimeMilliseconds);

        public void WaitCondition(string jquerySelector, string jqueryCommand, Func<dynamic, bool> condition, int timeOutMilliseconds = 5000, int waitTimeMilliseconds = 500)
        {
            while (true)
            {
                var elementDynamic = WaitDynamicElement(jquerySelector, jqueryCommand, timeOutMilliseconds, waitTimeMilliseconds);

                if (condition(elementDynamic))
                    return;
            }
        }

        public void Wait(int timeToWaitMilliseconds)
        {
            var timer = new Stopwatch();

            timer.Start();

            while (timer.ElapsedMilliseconds < timeToWaitMilliseconds) ;
        }
        #endregion

        #region Execute Query
        public void ExecuteQuery(string jquerySelector = null, string jqueryCommand = null)
        {
            if (string.IsNullOrWhiteSpace(jquerySelector) && string.IsNullOrWhiteSpace(jqueryCommand))
                throw new ArgumentException(nameof(jquerySelector), "jquerySelector et jqueryCommand are empties");

            if (!string.IsNullOrWhiteSpace(jqueryCommand) && !string.IsNullOrWhiteSpace(jquerySelector))
                jqueryCommand = "." + jqueryCommand;

            var query = !string.IsNullOrWhiteSpace(jquerySelector) ?
                $"$('{jquerySelector}'){jqueryCommand}" :
                $"{jqueryCommand}";

            ((IJavaScriptExecutor)WebDriver).ExecuteScript(query);
        }

        public void ExecuteQueryAtPosition(string jqueryQuery, WebElementPosition position)
        {
            if (string.IsNullOrWhiteSpace(jqueryQuery))
                throw new ArgumentException(nameof(jqueryQuery), "jqueryCommand is empty");

            var query = BuildPositionQuery(jqueryQuery, position);

            ((IJavaScriptExecutor)WebDriver).ExecuteScript(query);
        }
        #endregion
        #endregion

        #region Web Driver Behaviour(s)
        public void GoToUrl(string Url) => WebDriver.Navigate().GoToUrl(Url);
        #endregion

        #region Interface Behaviour(s)
        public void Dispose() => WebDriver.Quit();
        #endregion

        #region Intern Behaviour(s)
        private WebElementPosition ElementDynamicToPosition(dynamic elementPositionDynamic) =>
            new WebElementPosition()
            {
                PosX = elementPositionDynamic.left.Value,
                PosY = elementPositionDynamic.top.Value,
            };

        private string BuildPositionQuery(string jqueryCommand, WebElementPosition elementPosition) =>
            "$('body').find('*').filter(function() { return $(this).position().left >= " + (elementPosition.PosX - 1) +
                " && $(this).position().left <= " + (elementPosition.PosX + 1) +
                " && $(this).position().top >= " + (elementPosition.PosY - 1) +
                " && $(this).position().top <= " + elementPosition.PosY + 1 +
                "; })." + jqueryCommand;
        #endregion
    }
}