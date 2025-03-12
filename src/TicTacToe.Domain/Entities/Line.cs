namespace TicTacToe.Domain.Entities;

public readonly struct Line
{
  public Cell[] Cells { get; }

  private Line(Cell[] cells)
  {
    Cells = (Cell[])cells.Clone();
  }

  public Line MakeMove(int pos, Cell move)
  {
    if (Cells[pos] != Cell.Empty)
      throw new InvalidOperationException("Cell is already occupied.");

    Cell[] newCells = (Cell[])Cells.Clone();
    newCells[pos] = move;

    return new Line(newCells);
  }

  public Cell GetWinner()
  {
    Cell winner = Cells[0];

    if (Cells.Cast<Cell>().All(cell => cell == winner))
      return winner;

    return Cell.Empty;
  }

  public bool IsFull()
  {
    return Cells.Cast<Cell>().All(cell => cell != Cell.Empty);
  }

  public static Line Empty(int size)
  {
    if (size < 3)
      throw new InvalidOperationException("Size cannot be smaller than 3");

    Cell[] cells = new Cell[size];
    Array.Fill(cells, Cell.Empty);

    return new Line(cells);
  }

}