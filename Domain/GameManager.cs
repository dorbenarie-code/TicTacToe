using System;
using TicTacToe.Domain; 

public class GameManager
{
    private readonly Board board;
    private CellState currentTurn;

    public Board GameBoard => board;
    public CellState CurrentTurn => currentTurn;
    public BoardStatus Status => board.GetStatus();

    public GameManager()
    {
        board = new Board();
        currentTurn = CellState.X; 
    }

    public bool PlayTurn(int row, int col)
    {
        if (Status != BoardStatus.InProgress)
        {
            throw new InvalidOperationException("Cannot play a turn when the game is not in progress.");
        }
        if (!board.TryMakeMove(row, col, currentTurn))
        {
            return false;
        }
        
        if (Status == BoardStatus.InProgress)
        {
            SwitchTurn();
        }

        return true;
    }

    private void SwitchTurn()
    {
        currentTurn = (currentTurn == CellState.X) ? CellState.O : CellState.X;
    }
}