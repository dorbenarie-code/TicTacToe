using System;
using TicTacToe.Domain;

namespace TicTacToe.Players
{
    public class HumanPlayer : IPlayer
    {
        private readonly CellState myMark;

        public HumanPlayer(CellState mark)
        {
            myMark = mark;
        }

        public int[] GetMove(Board currentBoard)
        {
            while (true)
            {
                Console.WriteLine($"Player {myMark}, it's your turn.");
                Console.Write($"Enter row (0 to {Board.Dimension - 1}): ");
                string? rowInput = Console.ReadLine();
                
                Console.Write($"Enter column (0 to {Board.Dimension - 1}): ");
                string? colInput = Console.ReadLine();

                if (!int.TryParse(rowInput, out int row) || !int.TryParse(colInput, out int col))
                {
                    Console.WriteLine("Invalid input. Please enter valid numbers.");
                    continue; // חוזרים לתחילת הלולאה
                }
                if (row < 0 || row >= Board.Dimension || col < 0 || col >= Board.Dimension)
                {
                    Console.WriteLine($"Out of bounds. Please enter numbers between 0 and {Board.Dimension - 1}.");
                    continue;
                }
                if (currentBoard[row, col] != CellState.Empty)
                {
                    Console.WriteLine("That cell is already occupied. Try again.");
                    continue;
                }
                return new int[] { row, col };
            }
        }
    }
}