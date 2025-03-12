namespace TicTacToe.Application.Dtos;

public abstract class Entity<T> where T : struct
{
  public Guid Id { get; set; }
  public T Value { get; set; }
}