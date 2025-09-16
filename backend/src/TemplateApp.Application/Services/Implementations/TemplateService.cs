using Microsoft.EntityFrameworkCore;
using TemplateApp.Domain.Entities;
using TemplateApp.Domain.Exceptions;
using TemplateApp.Domain.Interfaces;
using TemplateApp.Persistence.PostgreSQL.Database;

namespace TemplateApp.Application.Services.Implementations;

public class TemplateService(ApplicationDbContext dbcontext) :
    ITemplateService
{
    public async Task<List<Template>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await dbcontext.Set<Template>()
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Template> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        var entity = await dbcontext.Set<Template>().FindAsync(id, cancellationToken);
        return entity ?? throw new EntityNotFoundException(typeof(Template).Name, id);
    }

    public async Task<Template> CreateAsync(
        Template entity, CancellationToken cancellationToken = default)
    {
        dbcontext.Set<Template>().Add(entity);
        await dbcontext.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(
        long id, Template template, CancellationToken cancellationToken = default)
    {
        var oldEntity = await dbcontext.Set<Template>().FindAsync(id, cancellationToken);
        if (oldEntity is null) throw new EntityNotFoundException(nameof(Template), id);

        UpdateValues(oldEntity, template);
        await dbcontext.SaveChangesAsync(cancellationToken);
    }

    private void UpdateValues(Template oldEntity, Template newEntity)
    {
        oldEntity.Name = newEntity.Name;
        oldEntity.Content = newEntity.Content;
    }

     public async Task DeleteAsync(long id, CancellationToken cancellationToken = default)
    {
        var entity = await dbcontext.Set<Template>().FindAsync(id, cancellationToken);
        if (entity is null) throw new EntityNotFoundException(nameof(Template), id);

        dbcontext.Set<Template>().Remove(entity);
        await dbcontext.SaveChangesAsync(cancellationToken);
    }

}
