using Fonsion.be.Application.Common.Interfaces;
using Fonsion.be.Domain.Images;
using Fonsion.be.Infrastructure.Common.Persistence;

namespace Fonsion.be.Infrastructure.Images.Persistance;

public class ImagesRepository : IImagesRepository
{
    private readonly FonsionDbContext _dbContext;

    public ImagesRepository(FonsionDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddImageAsync(Image image)
    {
         await _dbContext.Images.AddAsync(image) ;
    }
}