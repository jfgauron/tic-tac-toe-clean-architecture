using TicTacToe.Domain.Entities;

using TicTacToe.Application.Interfaces;
using TicTacToe.Application.Dtos;

namespace TicTacToe.Application.Services;

public class GameService
{
  private readonly IRepository<GameDto, Game> _gameRepository;

  public GameService(IRepository<GameDto, Game> gameRepository)
  {
    _gameRepository = gameRepository;
  }

  public async Task<GameDto> GetGameById(Guid id)
  {
    GameDto dto = await _gameRepository.GetById(id) ?? throw new InvalidOperationException("Invalid game id.");

    return dto;
  }

  public async Task<GameDto> CreateGame(int size)
  {
    Game game = new (size);
    GameDto dto = new() { Id = Guid.Empty, Value = game };

    dto.Id = await _gameRepository.Create(dto);

    return dto;
  }

  public async Task<GameDto> MakeMove(Guid gameId, int x, int y)
  {
    GameDto dto = await _gameRepository.GetById(gameId) ?? throw new InvalidOperationException("Invalid game id.");
    dto.Value = dto.Value.MakeMove(x, y);

    await _gameRepository.Update(dto);

    return dto;
  }
}
