//Cookie container for System.Net cookies
CookieCollection cookiesReq = new CookieCollection();
// Fetch cookies with selenium
var cookies = driver.Manage().Cookies.AllCookies;

//transform OpenQa cookies to System.Net cookies
foreach (var cookie in cookies)
{
    System.Net.Cookie netcookie = new System.Net.Cookie()
    {
        Domain = cookie.Domain,
        HttpOnly = cookie.IsHttpOnly,
        Name = cookie.Name,
        Path = cookie.Path,
        Secure = cookie.Secure,
        Value = cookie.Value,
    };
    if (cookie.Expiry.HasValue)
        netcookie.Expires = cookie.Expiry.Value;
    
    //Add cookie to System.Net CookieCollection 
    cookiesReq.Add(netcookie);
    Console.WriteLine(netcookie);
}
