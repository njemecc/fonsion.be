using Fonsion.be.Domain.Images;

namespace Fonsion.be.Application.Common.Interfaces;

public interface IImagesRepository
{
    Task AddImageAsync(Image image);
}