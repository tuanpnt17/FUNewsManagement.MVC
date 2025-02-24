using AutoMapper;
using RepositoryLayer.Tags;
using ServiceLayer.Models;

namespace ServiceLayer.Tag
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;
        private readonly IMapper _mapper;

        public TagService(ITagRepository tagRepository, IMapper mapper)
        {
            _tagRepository = tagRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TagDTO>> GetAllTagsAsync()
        {
            var tags = await _tagRepository.ListAllAsync();
            var tagsDto = _mapper.Map<IEnumerable<TagDTO>>(tags);
            return tagsDto;
        }

        public Task<TagDTO> GetTagByIdAsync(int tagId)
        {
            throw new NotImplementedException();
        }

        public Task<TagDTO> AddTagAsync(TagDTO tag)
        {
            throw new NotImplementedException();
        }

        public Task<TagDTO> UpdateTagAsync(TagDTO tag)
        {
            throw new NotImplementedException();
        }

        public Task<TagDTO> DeleteTagAsync(int tagId)
        {
            throw new NotImplementedException();
        }
    }
}
