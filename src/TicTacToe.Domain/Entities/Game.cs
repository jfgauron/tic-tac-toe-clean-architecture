namespace TicTacToe.Domain.Entities;

public readonly struct Game
{
  public Board Board { get; }
  private readonly Cell _nextMove;

  private Game(Board board, Cell nextMove)
  {
    _nextMove = nextMove;
    Board = board;
  }

  public Game(int size)
  {
    _nextMove = Cell.X;
    Board = Board.Empty(size);
  }

  public Game MakeMove(int x, int y)
  {
    var newBoard = Board.MakeMove(x, y, _nextMove);
    var nextMove = _nextMove == Cell.X ? Cell.O : Cell.X;

    return new Game(newBoard, nextMove);
  }
  
}