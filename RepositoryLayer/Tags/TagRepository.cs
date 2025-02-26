using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Data;
using RepositoryLayer.Entities;

namespace RepositoryLayer.Tags;

public class TagRepository : ITagRepository
{
    private readonly FUNewsDBContext _context;

    public TagRepository(FUNewsDBContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Tag>> ListAllAsync()
    {
        var tags = await _context.Tags.OrderByDescending(t => t.TagId).ToListAsync();
        return tags;
    }

    public Task<Tag> CreateAsync(Tag t)
    {
        throw new NotImplementedException();
    }

    public Task<int?> UpdateAsync(Tag t)
    {
        throw new NotImplementedException();
    }

    public Task<int?> DeleteAsync(Tag? t)
    {
        throw new NotImplementedException();
    }
}
