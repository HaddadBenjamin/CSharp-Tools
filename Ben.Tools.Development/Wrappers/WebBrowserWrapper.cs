using System;
using System.Diagnostics;
using Newtonsoft.Json;
using OpenQA.Selenium;

namespace Ben.Tools.Development
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
        public dynamic GetCssProperty(string selectorJquery, string cssPropertyName) =>
            GetElementsAsDynamic(selectorJquery, $"css('{cssPropertyName}')");

        public void UpdateCssProperty<CSSPropertyType>(string selectorJquery, string cssPropertyName, CSSPropertyType cssPopertyValue) =>
            ExecuteCommand(selectorJquery, $"css('{cssPropertyName}', '{cssPopertyValue}')");
        #endregion

        #region Text & Val
        public string GetText(string selectorJquery) =>
            GetElementsAsDynamic(selectorJquery, "text()");

        public string GetValue(string selectorJquery) =>
            GetElementsAsDynamic(selectorJquery, "val()");

        public void AddText(string selectorJquery, string addText) =>
            ExecuteCommand(selectorJquery, $"text('{GetText(selectorJquery)}{addText}')");

        public void AddValue(string selectorJquery, string addText) =>
            ExecuteCommand(selectorJquery, $"val('{GetValue(selectorJquery)}{addText}')");

        public void UpdateText(string selectorJquery, string newText) =>
            ExecuteCommand(selectorJquery, $"text('{newText}')");

        public void UpdateText(WebElementPosition position, string newText) =>
            ExecuteCommandAtPosition($"val('{newText}')", position);

        public void UpdateValue(string selectorJquery, string newText) =>
            ExecuteCommand(selectorJquery, $"val('{newText}')");

        public void UpdateValue(WebElementPosition position, string newText) =>
            ExecuteCommandAtPosition($"val('{newText}')", position);

        public void UpdateValue(string selectorJquery, string newText, int eachCharacterMilliseconds = 250)
        {
            var firstCharacter = true;
            var timer = new Stopwatch();

            timer.Start();

            foreach (var @char in newText.ToCharArray())
            {
                if (firstCharacter)
                {
                    ExecuteCommand(selectorJquery, $"val('{@char}')");
                    firstCharacter = false;
                }
                else
                {
                    while (timer.ElapsedMilliseconds > eachCharacterMilliseconds)
                        AddValue(selectorJquery, @char.ToString());
                }
            }
        }
        #endregion

        #region Checked
        public void UpdateChecked(string jquerySelector, bool toCheck = true) =>
            ExecuteCommand(jquerySelector, $"prop('checked', {toCheck.ToString().ToLower()})");

        public bool IsChecked(string jquerySelector) =>
            GetElementsAsDynamic(jquerySelector, $"prop('checked')") == true;
        #endregion

        #region Click
        public void Click(WebElementPosition position) =>
            Click(position.PosX, position.PosY);

        public void Click(double posX, double posY) =>
            ExecuteCommandAtPosition("click()", posX, posY);

        public void Click(string selectorJquery) =>
            ExecuteCommand(selectorJquery, "click()");
        #endregion

        #region Get Elements
        public WebElementPosition GetElementsAsPosition(string jquerySelector)
        {
            var positionDynamic = GetElementsAsDynamic(jquerySelector, "position()");

            return new WebElementPosition()
            {
                PosX = positionDynamic.left.Value,
                PosY = positionDynamic.top.Value,
            };
        }

        public dynamic GetElementsAsDynamic(string jquerySelector, string jqueryCommand = null) =>
            JsonConvert.DeserializeObject<dynamic>(GetElementsAsJson(jquerySelector, jqueryCommand));

        public string GetElementsAsJson(string jquerySelector, string jqueryCommand = "")
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

        public WebElementPosition WaitElementAsPosition(string jquerySelector, int timeOutMilliseconds = 5000, int waitTimeMilliseconds = 500)
        {
            var positionDynamic = WaitElementAsDynamic(jquerySelector, "position()", timeOutMilliseconds, waitTimeMilliseconds);

            return new WebElementPosition()
            {
                PosX = positionDynamic.left.Value,
                PosY = positionDynamic.top.Value,
            };
        }

        public dynamic WaitElementAsDynamic(string jquerySelector, string jqueryCommand = "", int timeOutMilliseconds = 5000, int waitTimeMilliseconds = 500) =>
            JsonConvert.DeserializeObject<dynamic>(WaitElementAsJson(jquerySelector, jqueryCommand, timeOutMilliseconds, waitTimeMilliseconds));

        public string WaitElementAsJson(string jquerySelector, string jqueryCommand = "", int timeOutMilliseconds = 5000, int waitTimeMilliseconds = 500)
        {
            var timeOutTimer = new Stopwatch();
            var waitTimer = new Stopwatch();

            timeOutTimer.Start();
            waitTimer.Start();

            while (timeOutTimer.ElapsedMilliseconds < timeOutMilliseconds)
            {
                if (waitTimer.ElapsedMilliseconds > waitTimeMilliseconds)
                {
                    var rawJson = GetElementsAsJson(jquerySelector, jqueryCommand);

                    if (!string.IsNullOrWhiteSpace(rawJson))
                        return rawJson;

                    waitTimer.Restart();
                }
            }

            throw new TimeoutException(nameof(timeOutMilliseconds));
        }

        public void WaitCondition(string jquerySelector, string jqueryCommand, Func<dynamic, bool> condition, int timeOutMilliseconds = 5000, int waitTimeMilliseconds = 500)
        {
            var timeOutTimer = new Stopwatch();
            var waitTimer = new Stopwatch();

            timeOutTimer.Start();
            waitTimer.Start();

            while (timeOutTimer.ElapsedMilliseconds < timeOutMilliseconds)
            {
                if (waitTimer.ElapsedMilliseconds > waitTimeMilliseconds)
                {
                    var rawJson = GetElementsAsJson(jquerySelector, jqueryCommand);

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

        public void Wait(int timeToWaitMilliseconds)
        {
            var timer = new Stopwatch();

            timer.Start();

            while (timer.ElapsedMilliseconds < timeToWaitMilliseconds) ;
        }
        #endregion

        #region Execute Command
        public void ExecuteCommand(string jquerySelector = null, string jqueryCommand = null)
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

        public void ExecuteCommandAtPosition(string jqueryCommand, WebElementPosition position) =>
            ExecuteCommandAtPosition(jqueryCommand, position.PosX, position.PosY);

        public void ExecuteCommandAtPosition(string jqueryCommand, double posX, double posY)
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
        #endregion

        #region Web Driver Behaviour(s)
        public void GoToUrl(string Url) => WebDriver.Navigate().GoToUrl(Url);
        #endregion

        #region Interface Behaviour(s)
        public void Dispose() => WebDriver.Quit();
        #endregion
    }
}