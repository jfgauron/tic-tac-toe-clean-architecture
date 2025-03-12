using TicTacToe.Application.Dtos;

namespace TicTacToe.Application.Interfaces;

public interface IRepository<Dto, Value>
  where Dto : Entity<Value>
  where Value : struct
{
  Task<Dto?> GetById(Guid id);

  Task<IEnumerable<Dto>> GetAll();

  Task<Guid> Create(Dto entity);

  Task Update(Dto entity);

  Task DeleteById (Guid id);
}