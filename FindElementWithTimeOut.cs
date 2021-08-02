class GeckoFind
{
    public bool ElementById(string id, int timer)
    {
        WebDriverWait wait = new WebDriverWait(Gecko.driver, TimeSpan.FromSeconds(timer));
        try
        {
            IWebElement desiredElement = wait.Until(d => Gecko.driver.FindElementById(id));
            return true;
        }
        catch(OpenQA.Selenium.WebDriverTimeoutException)
        {
            return false;
        }
    }
    public bool ElementByXpath(string xpath, int timer)
    {
        WebDriverWait wait = new WebDriverWait(Gecko.driver, TimeSpan.FromSeconds(timer));
        try
        {
            IWebElement desiredElement = wait.Until(d => Gecko.driver.FindElementByXPath(xpath));
            return true;
        }
        catch(OpenQA.Selenium.WebDriverTimeoutException)
        {
            return false;
        }
    }
    public bool ElementByLinktext(string linkText, int timer)
    {
        WebDriverWait wait = new WebDriverWait(Gecko.driver, TimeSpan.FromSeconds(timer));
        try
        {
            IWebElement desiredElement = wait.Until(d => Gecko.driver.FindElementByLinkText(linkText));
            return true;
        }
        catch(OpenQA.Selenium.WebDriverTimeoutException)
        {
            return false;
        }
    }
}
