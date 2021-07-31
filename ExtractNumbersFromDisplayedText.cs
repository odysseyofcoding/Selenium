    // Selenium.WebDriver,4.0.0-beta2
    
    public static int ExtractNumbers()
    {
        var elementOfNumber = Gecko.driver.FindElementByXPath("");
        return Convert.ToInt32(new string(elementOfNumber.Text.Where(c => char.IsDigit(c)).ToArray()));
    }
