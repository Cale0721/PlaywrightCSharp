using Microsoft.Playwright;
using PlaywrightCSharp2.Pages;
using static NUnit.Framework.Assert;


namespace PlaywrightCSharp2;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task TestWithPOM()
    {
        //Playwright
        using var playwright = await Playwright.CreateAsync();
        //Browser
        await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false
        });
        //Page
        var page = await browser.NewPageAsync();
        await page.GotoAsync("http://www.eaapp.somee.com");

        LoginPage loginPage = new LoginPage(page);
        await loginPage.ClickLogin();
        await loginPage.Login("admin", "password");
        var isExist = await loginPage.IsEmployeeDetailsExists();
        Assert.IsTrue(isExist);
    }
    
    [Test]
    public async Task Test1()
    {
        //Playwright
        using var playwright = await Playwright.CreateAsync();
        //Browser
        await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false
        });
        //Page
        var page = await browser.NewPageAsync();
        await page.GotoAsync("http://www.eaapp.somee.com");
        await page.ClickAsync("text=Login");
        await page.ScreenshotAsync(new PageScreenshotOptions
        {
            Path = "test.png"
        });
        await page.FillAsync("#UserName", "admin");
        await page.FillAsync("#Password", "password");
        await page.ClickAsync("text=Log in");
        var isExist = await page.Locator("text='Employee Details'").IsVisibleAsync();
        //Assert.IsTrue(isExist);
    }
}