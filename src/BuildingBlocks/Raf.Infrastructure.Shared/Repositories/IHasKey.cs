namespace Raf.Infrastructure.Shared.Repositories;

public interface IHasKey<T>
{
    T Id { get; set; }
}