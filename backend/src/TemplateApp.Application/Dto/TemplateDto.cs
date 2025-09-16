using TemplateApp.Domain.Entities;

namespace TemplateApp.Application.Dto;

public class TemplateRequest
{
    public required string Name { get; set; }
    public required string Content { get; set; }
}

public class TemplateResponse
{
    public long Id { get; set; }
    public required string Name { get; set; }
    public required string Content { get; set; }
}

public static class TemplateDtoExtension
{
    public static Template FromRequest(this TemplateRequest templateDto)
    {
        return new Template
        {
            Name = templateDto.Name,
            Content = templateDto.Content
        };
    }

    public static TemplateResponse ToResponse(this Template template)
    {
        return new TemplateResponse
        {
            Id = template.Id,
            Name = template.Name,
            Content = template.Content
        };
    }

}
