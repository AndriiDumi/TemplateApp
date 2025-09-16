using TemplateApp.Domain.Entities;

namespace TemplateApp.Domain.Interfaces;

public interface ITemplateService
{
    Task<List<Template>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<Template> GetByIdAsync(long id, CancellationToken cancellationToken = default);

    Task<Template> CreateAsync(Template entity, CancellationToken cancellationToken = default);

    Task UpdateAsync(long id, Template template, CancellationToken cancellationToken = default);

    Task DeleteAsync(long id, CancellationToken cancellationToken = default);
}
