namespace Fonsion.be.Application.Common.Interfaces;

public interface IUnitOfWork
{
    Task CommitChangesAsync();
}