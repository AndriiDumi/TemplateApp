using PuppeteerSharp;
using PuppeteerSharp.Media;

namespace TemplateApp.Application.Services.Implementations;

public static class PdfService
{
    public static async Task<byte[]> GeneratePdfAsync(string htmlContent)
    {
        using var browser = await Puppeteer.LaunchAsync(new LaunchOptions
        {
            Headless = true,
        });

        using var page = await browser.NewPageAsync();

        await page.SetContentAsync(htmlContent);
        await page.EvaluateExpressionAsync("document.fonts.ready");

        var pdfData = await page.PdfDataAsync(new PdfOptions
        {
            Format = PaperFormat.A4,
            PrintBackground = true,
            MarginOptions = new MarginOptions
            {
                Top = "50px",
                Bottom = "20px",
                Left = "50px",
                Right = "20px"
            }
        });

        return pdfData;
    }

    public static async Task InitializePuppeteerAsync()
    {
        var browserFetcher = new BrowserFetcher();
        await browserFetcher.DownloadAsync();
    }

}
