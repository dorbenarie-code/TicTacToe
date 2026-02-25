namespace TicTacToe.Domain;

public enum CellState
{
    Empty,
    X,
    O
}

public enum BoardStatus
{
    InProgress,
    Draw,
    XWins,
    OWins
}

public class Board
{
    public const int Dimension = 3;
    private CellState[,] grid;
    private int filledCells;

    public Board()
    {
        grid = new CellState[Dimension, Dimension];
        filledCells = 0;
    }
    public Board(Board otherBoard)
    {
        grid = new CellState[Dimension, Dimension];
        for(int r = 0; r < Dimension; r++)
        {
            for(int c = 0; c < Dimension; c++)
            {
                grid[r, c] = otherBoard[r, c];
            }
        }
        filledCells = otherBoard.filledCells;
    }



    public CellState this[int row, int col]
    {
        get { return grid[row, col]; }
    }

    public bool TryMakeMove(int row, int col, CellState mark)
    {
        if (mark != CellState.X && mark != CellState.O)
        {
            return false;
        }
        if (row < 0 || row >= Dimension || col < 0 || col >= Dimension)
        {
            return false;
        }
        if (grid[row, col] != CellState.Empty)
        {
            return false;
        }
        
        grid[row, col] = mark;
        filledCells++;
        
        return true;
    }

    public BoardStatus GetStatus()
    {
        CellState winner;
        
        for (int i = 0; i < Dimension; i++)
        {
            if (IsLineWinner(grid[i, 0], grid[i, 1], grid[i, 2], out winner))
            {
                if (winner == CellState.X) return BoardStatus.XWins;
                return BoardStatus.OWins;
            }

            if (IsLineWinner(grid[0, i], grid[1, i], grid[2, i], out winner))
            {
                if (winner == CellState.X) return BoardStatus.XWins;
                return BoardStatus.OWins;
            }
        }

        if (IsLineWinner(grid[0, 0], grid[1, 1], grid[2, 2], out winner))
        {
            if (winner == CellState.X) return BoardStatus.XWins;
            return BoardStatus.OWins;
        }

        if (IsLineWinner(grid[0, 2], grid[1, 1], grid[2, 0], out winner))
        {
            if (winner == CellState.X) return BoardStatus.XWins;
            return BoardStatus.OWins;
        }

        if (filledCells == Dimension * Dimension)
        {
            return BoardStatus.Draw;
        }
        
        return BoardStatus.InProgress;
    }

    private static bool IsLineWinner(CellState a, CellState b, CellState c, out CellState winner)
    {
        if (a != CellState.Empty && a == b && b == c)
        {
            winner = a;
            return true;
        }
        
        winner = CellState.Empty;
        return false;
    }
}