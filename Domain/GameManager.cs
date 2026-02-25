using System;
using TicTacToe.Domain; 

public class GameManager
{
    private Board board;
    private CellState currentTurn;

  
    public Board GameBoard 
    { 
        get { return board; } 
    }

    public CellState CurrentTurn 
    { 
        get { return currentTurn; } 
    }

    
    public BoardStatus Status 
    { 
        get { return board.GetStatus(); } 
    }

    
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
        
        
        bool moveSuccessful = board.TryMakeMove(row, col, currentTurn);
        
        if (moveSuccessful)
        {
            
            if (Status == BoardStatus.InProgress)
            {
                SwitchTurn();
            }
            return true;
        }
        
        
        return false;
    }

    
    private void SwitchTurn()
    {
        if (currentTurn == CellState.X)
        {
            currentTurn = CellState.O;
        }
        else
        {
            currentTurn = CellState.X;
        }
    }
}