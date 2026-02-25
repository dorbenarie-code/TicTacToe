using System;
using TicTacToe.Domain;

namespace TicTacToe.Players;

public class MinimaxPlayer : IPlayer
{
    private CellState myMark;

    public MinimaxPlayer(CellState mark)
    {
        myMark = mark;
    }

    public int[] GetMove(Board currentBoard)
    {
        int bestScore = (myMark == CellState.X) ? int.MinValue : int.MaxValue;
        int[] bestMove = new int[2];
        
        CellState opponentMark = (myMark == CellState.X) ? CellState.O : CellState.X;

        for (int r = 0; r < Board.Dimension; r++)
        {
            for (int c = 0; c < Board.Dimension; c++)
            {
                if (currentBoard[r, c] == CellState.Empty)
                {
                    Board draftBoard = new Board(currentBoard);
                    
                    draftBoard.TryMakeMove(r, c, myMark);

                    int score = Minimax(draftBoard, opponentMark, 0);

                    if (myMark == CellState.X)
                    {
                        if (score > bestScore)
                        {
                            bestScore = score;
                            bestMove[0] = r;
                            bestMove[1] = c;
                        }
                    }
                    else 
                    {
                        if (score < bestScore)
                        {
                            bestScore = score;
                            bestMove[0] = r;
                            bestMove[1] = c;
                        }
                    }
                }
            }
        }

        return bestMove;
    }

    private int Minimax(Board currentBoard, CellState currentTurn, int depth)
    {
        BoardStatus status = currentBoard.GetStatus();

        if (status == BoardStatus.XWins) return 10 - depth;
        else if (status == BoardStatus.OWins) return -10 + depth;
        else if (status == BoardStatus.Draw) return 0;

        if (status == BoardStatus.InProgress)
        {
            if (currentTurn == CellState.X)
            {
                int bestScore = int.MinValue;
                for (int r = 0; r < Board.Dimension; r++)
                {
                    for (int c = 0; c < Board.Dimension; c++)
                    {
                        if (currentBoard[r, c] == CellState.Empty)
                        {
                            Board draftBoard = new Board(currentBoard);
                            draftBoard.TryMakeMove(r, c, CellState.X);
                            
                            int score = Minimax(draftBoard, CellState.O, depth + 1);
                            bestScore = Math.Max(bestScore, score);
                        }
                    }
                }
                return bestScore;
            }
            else 
            {
                int bestScore = int.MaxValue;
                for (int r = 0; r < Board.Dimension; r++)
                {
                    for (int c = 0; c < Board.Dimension; c++)
                    {
                        if (currentBoard[r, c] == CellState.Empty)
                        {
                            Board draftBoard = new Board(currentBoard);
                            draftBoard.TryMakeMove(r, c, CellState.O);
                            
                            int score = Minimax(draftBoard, CellState.X, depth + 1);
                            bestScore = Math.Min(bestScore, score);
                        }
                    }
                }
                return bestScore;
            }
        }

        return 0;
    }
}