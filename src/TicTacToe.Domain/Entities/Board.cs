namespace TicTacToe.Domain.Entities;

public readonly struct Board
{
  public Line[] Rows { get; }
  private Line[] Cols { get; }
  private Line[] Diagonals { get; }

  private Board(Line[] rows, Line[] cols, Line[] diagonals)
  {
    Rows = (Line[]) rows.Clone();
    Cols = (Line[]) cols.Clone();
    Diagonals = (Line[]) diagonals.Clone();
  }

  public Board MakeMove(int x, int y, Cell move)
  {
    if (GameIsOver())
      throw new InvalidOperationException("Game is already over.");

    if (CoordinatesAreOutOfBound(x, y))
      throw new InvalidOperationException("Invalid coordinates.");

    var newRows = (Line[])Rows.Clone();
    newRows[y] = Rows[y].MakeMove(x, move);

    var newCols = (Line[])Cols.Clone();
    newCols[x] = Cols[x].MakeMove(y, move);

    var newDiagonals = (Line[])Diagonals.Clone();
    if (CoordinatesAreOnDownwardDiagonal(x, y))
      newDiagonals[0] = Diagonals[0].MakeMove(x, move);
    if (CoordinatesAreOnUpwardDiagonal(x, y))
      newDiagonals[1] = Diagonals[1].MakeMove(x, move);

    return new Board(newRows, newCols, newDiagonals);
  }

  public Cell GetWinner()
  {
    foreach (Line line in GetLines()) {
      Cell winner = line.GetWinner();
      if (winner != Cell.Empty)
        return winner;
    }

    return Cell.Empty;
  }

  public static Board Empty(int size)
  {
    Line[] rows = new Line[size];
    Array.Fill(rows, Line.Empty(size));

    Line[] cols = new Line[size];
    Array.Fill(cols, Line.Empty(size));

    Line[] diagonals = new Line[2];
    Array.Fill(diagonals, Line.Empty(size));

    return new Board(rows, cols, diagonals);
  }

  private bool CoordinatesAreOnDownwardDiagonal(int x, int y)
  {
    return x == y;
  }

  private bool CoordinatesAreOnUpwardDiagonal(int x, int y)
  {
    return x + y == Rows.Length - 1;
  }

  private bool CoordinatesAreOutOfBound(int x, int y)
  {
    return x > Rows.Length || y > Rows.Length || x < 0 || y < 0;
  }

  private bool GameIsOver()
  {
    return GetWinner() != Cell.Empty || IsFull();
  }

  private bool IsFull()
  {
    return Rows.Cast<Line>().All(line => line.IsFull());
  }

  private Line[] GetLines()
  {
    var result = new Line[Rows.Length + Cols.Length + Diagonals.Length];
    Rows.CopyTo(result, 0);
    Cols.CopyTo(result, Rows.Length);
    Diagonals.CopyTo(result, Rows.Length + Cols.Length);

    return result;
  }

}