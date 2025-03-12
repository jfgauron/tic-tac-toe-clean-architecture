using TicTacToe.Application.Dtos;
using TicTacToe.Domain.Entities;

namespace TicTacToe.Presentation.Views;

public class GameView
{
  public Guid Id { get; }
  public string[] Grid { get; }
  public string Winner { get; }

  private GameView(Guid id, string[] grid, string winner)
  {
    Id = id;
    Grid = grid;
    Winner = winner;
  }

  private static Dictionary<Cell, string> _moves = new Dictionary<Cell, string>
  {
    { Cell.Empty, "" },
    { Cell.X, "X" },
    { Cell.O, "O" }
  };

  public static GameView FromDto(GameDto dto)
  {
    Board board = dto.Value.Board;

    return new GameView(dto.Id, MakeGrid(board.Rows), CellToString(board.GetWinner()));
  }

  private static string[] MakeGrid(Line[] rows)
  {
    string[] grid = new string[rows.Length * rows.Length];
    int index = 0;

    foreach (Line line in rows)
    {
      foreach (Cell cell in line.Cells)
      {
        grid[index++] = CellToString(cell);
      }
    }

    return grid;
  }

  private static string CellToString(Cell cell)
  {
    return _moves[cell];
  }
}
