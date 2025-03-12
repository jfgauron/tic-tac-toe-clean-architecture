using TicTacToe.Application.Dtos;
using TicTacToe.Application.Interfaces;

namespace TicTacToe.Infrastructure;

public class InMemoryRepository<Dto, Value> : IRepository<Dto, Value>
  where Dto : Entity<Value>, new()
  where Value : struct
{
  private static Dictionary<Guid, Dto> store = new Dictionary<Guid, Dto>();

  public Task<Dto?> GetById(Guid id)
  {
    store.TryGetValue(id, out var entity);

    return Task.FromResult(entity);
  }

  public async Task<IEnumerable<Dto>> GetAll()
  {
    return await Task.FromResult(store.Values);
  }

  public Task<Guid> Create(Dto entity)
  {
    Dto result = entity;

    if (entity.Id == Guid.Empty) {
      Guid guid = Guid.NewGuid();
      result = new Dto { Id = guid, Value = entity.Value };
    }

    if (store.ContainsKey(result.Id))
      throw new InvalidOperationException("Id is already present in store.");

    store[result.Id] = result;
    
    return Task.FromResult(result.Id);
  }

  public Task Update(Dto entity)
  {
    if (entity.Id == Guid.Empty)
      throw new InvalidOperationException("Cannot update an entity without an Id.");
    
    if (!store.ContainsKey(entity.Id))
      throw new InvalidOperationException("Entity is not present in store.");

    store[entity.Id] = entity;
    
    return Task.CompletedTask;
  }

  public Task DeleteById (Guid id)
  {
    if (!store.ContainsKey(id))
      throw new InvalidOperationException("Id is not present in store.");

    return Task.FromResult(store.Remove(id));
  }
}
