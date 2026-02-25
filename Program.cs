using System;
using TicTacToe.Domain;
using TicTacToe.Players; 

GameManager game = new GameManager(); 

IPlayer playerX = new MinimaxPlayer(CellState.X);
IPlayer playerO = new HumanPlayer(CellState.O);

while (game.Status == BoardStatus.InProgress) 
{
    Console.Clear(); 
    Render(game.GameBoard); 
    Console.WriteLine(); 
    Console.WriteLine($"It's {game.CurrentTurn}'s turn!"); 

    IPlayer currentPlayer = (game.CurrentTurn == CellState.X) ? playerX : playerO;
    
    
    int[] move = currentPlayer.GetMove(game.GameBoard);
    
    bool success = game.PlayTurn(move[0], move[1]); 

    if (success == false) //
    {
        Console.WriteLine("Invalid move! Press Enter to try again..."); 
        Console.ReadLine(); 
    }
}

Console.Clear(); 
Render(game.GameBoard); 
Console.WriteLine(); 
Console.WriteLine($"Game Over! Result: {game.Status}"); 


static void Render(Board b) //
{
    for (int r = 0; r < Board.Dimension; r++) 
    {
        for (int c = 0; c < Board.Dimension; c++) 
        {
            char ch = b[r, c] switch 
            {
                CellState.X => 'X', 
                CellState.O => 'O', 
                _ => ' ' 
            };

            Console.Write($" {ch} "); 

            if (c < Board.Dimension - 1) Console.Write("|"); 
        }

        Console.WriteLine(); //
        if (r < Board.Dimension - 1) Console.WriteLine("---+---+---"); 
    }
}