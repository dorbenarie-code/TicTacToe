using System;
using TicTacToe.Domain;

namespace TicTacToe.Players
{
    public class HumanPlayer : IPlayer
    {
        private CellState myMark;

        public HumanPlayer(CellState mark)
        {
            myMark = mark;
        }

        public int[] GetMove(Board currentBoard)
        {
            int[] move = new int[2];
            bool isValid = false;

            while (!isValid)
            {
                Console.WriteLine($"Player {myMark}, it's your turn.");
                Console.Write($"Enter row (0 to {Board.Dimension - 1}): ");
                string? rowInput = Console.ReadLine();
                
                Console.Write($"Enter column (0 to {Board.Dimension - 1}): ");
                string? colInput = Console.ReadLine();

                if (int.TryParse(rowInput, out int row) && int.TryParse(colInput, out int col))
                {
                    if (row >= 0 && row < Board.Dimension && col >= 0 && col < Board.Dimension)
                    {
                        if (currentBoard[row, col] == CellState.Empty)
                        {
                            move[0] = row;
                            move[1] = col;
                            isValid = true;
                        }
                        else
                        {
                            Console.WriteLine("That cell is already occupied. Try again.");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Out of bounds. Please enter numbers between 0 and {Board.Dimension - 1}.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter valid numbers.");
                }
            }

            return move;
        }
    }
}