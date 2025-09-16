using Microsoft.AspNetCore.Mvc;
using TemplateApp.Domain.Interfaces;
using TemplateApp.Application.Dto;
using Microsoft.AspNetCore.RateLimiting;
using System.Text.Json;
using HandlebarsDotNet;
using TemplateApp.Application.Services.Implementations;

[EnableRateLimiting("fixed")]
[ApiController]
[Route("api/templates")]
public class TemplatesController(ITemplateService templateService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> GetAll(
        CancellationToken cancellationToken
    )
    {
        var result = await templateService.GetAllAsync(cancellationToken);
        var response = result.Select(r => r.ToResponse());
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(
        long id,
        CancellationToken cancellationToken
    )
    {
        var result = await templateService.GetByIdAsync(id, cancellationToken);
        var response = result.ToResponse();
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] TemplateRequest request,
        CancellationToken cancellationToken
    )
    {
        var result = await templateService.CreateAsync(request.FromRequest(), cancellationToken);
        return Created();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        long id,
        [FromBody] TemplateRequest request,
        CancellationToken cancellationToken
    )
    {
        await templateService.UpdateAsync(id, request.FromRequest(), cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(
        long id,
        CancellationToken cancellationToken
    )
    {
        await templateService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }

    [HttpPost("{id}/generate-pdf")]
    public async Task<IActionResult> GeneratePdf(
        long id,
        [FromBody] JsonElement jsonData,
        CancellationToken cancellationToken
    )
    {
        var template = await templateService.GetByIdAsync(id, cancellationToken);

        var data = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonData.GetRawText());

        var compiledTemplateDelegate = Handlebars.Compile(template.Content);
        var resultHtml = compiledTemplateDelegate(data);

        resultHtml = System.Net.WebUtility.HtmlDecode(resultHtml);

        var pdfBytes = await PdfService.GeneratePdfAsync(resultHtml);
        return File(pdfBytes, "application/pdf", "document.pdf");
    }

}
