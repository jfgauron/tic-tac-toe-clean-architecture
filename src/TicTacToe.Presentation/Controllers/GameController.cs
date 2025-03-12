using TicTacToe.Application.Services;
using TicTacToe.Application.Dtos;
using TicTacToe.Presentation.Views;
using Microsoft.AspNetCore.Mvc;
using TicTacToe.Presentation.Requests;

namespace TicTacToe.Presentation.Controllers;

[Route("api/games")]
[ApiController]
public class GameController : ControllerBase
{
  private readonly GameService _gameService;

  public GameController(GameService gameService)
  {
    _gameService = gameService;
  }

  [HttpGet("{gameId}")]
  public async Task<ActionResult<GameView>> GetGameById(Guid gameId)
  {
    GameDto dto = await _gameService.GetGameById(gameId);
    GameView view = GameView.FromDto(dto);

    return Ok(view);
  }

  [HttpPost]
  public async Task<ActionResult<GameView>> CreateGame([FromBody] CreateGameRequest request)
  {
    GameDto dto = await _gameService.CreateGame(request.GridSize);
    GameView view = GameView.FromDto(dto);

    return CreatedAtAction(nameof(GetGameById), new { gameId = view.Id }, view);
  }

  [HttpPatch("{gameId}")]
  public async Task<ActionResult<GameView>> MakeMove(Guid gameId, [FromBody] MakeMoveRequest request)
  {
    GameDto dto = await _gameService.MakeMove(gameId, request.X, request.Y);
    GameView view = GameView.FromDto(dto);

    return Ok(view);
  }
}
