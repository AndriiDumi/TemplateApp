namespace TemplateApp.Domain.Entities;

public class Template
{
    public long Id { get; set; }
    public required string Name { get; set; }
    public required string Content { get; set; } //HTML
}
