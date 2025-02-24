using ServiceLayer.Models;

namespace ServiceLayer.Tag;

public interface ITagService
{
    Task<IEnumerable<TagDTO>> GetAllTagsAsync();
    Task<TagDTO> GetTagByIdAsync(int tagId);
    Task<TagDTO> AddTagAsync(TagDTO tag);
    Task<TagDTO> UpdateTagAsync(TagDTO tag);
    Task<TagDTO> DeleteTagAsync(int tagId);
}
