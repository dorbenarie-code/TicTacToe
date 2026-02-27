using System;
using TicTacToe.Domain;
namespace TicTacToe.Players;
public class MinimaxPlayer : IPlayer
{
    private readonly CellState myMark;

    public MinimaxPlayer(CellState mark)
    {
        myMark = mark;
    }

    public int[] GetMove(Board currentBoard)
    {
        bool isMaximizing = (myMark == CellState.X);
        int bestScore = isMaximizing ? int.MinValue : int.MaxValue;
        int[] bestMove = new int[2];
        
        CellState opponentMark = isMaximizing ? CellState.O : CellState.X;
        for (int r = 0; r < Board.Dimension; r++)
        {
            for (int c = 0; c < Board.Dimension; c++)
            {
                if (currentBoard[r, c] == CellState.Empty)
                {
                    Board draftBoard = new Board(currentBoard);
                    draftBoard.TryMakeMove(r, c, myMark);

                    int score = Minimax(draftBoard, opponentMark, 0);

                    bool foundBetterMove = isMaximizing ? (score > bestScore) : (score < bestScore);
                    
                    if (foundBetterMove)
                    {
                        bestScore = score;
                        bestMove[0] = r;
                        bestMove[1] = c;
                    }
                }
            }
        }

        return bestMove;
    }

    private static int Minimax(Board currentBoard, CellState currentTurn, int depth)
    {
        BoardStatus status = currentBoard.GetStatus();

        if (status == BoardStatus.XWins) return 10 - depth;
        if (status == BoardStatus.OWins) return -10 + depth;
        if (status == BoardStatus.Draw) return 0;

        bool isMaximizing = (currentTurn == CellState.X);
        int bestScore = isMaximizing ? int.MinValue : int.MaxValue;
        CellState nextTurn = isMaximizing ? CellState.O : CellState.X;

        // אותה לוגיקה גם ברקורסיה - חוסך אלפי הקצאות זיכרון!
        for (int r = 0; r < Board.Dimension; r++)
        {
            for (int c = 0; c < Board.Dimension; c++)
            {
                if (currentBoard[r, c] == CellState.Empty)
                {
                    Board draftBoard = new Board(currentBoard);
                    draftBoard.TryMakeMove(r, c, currentTurn);
                    
                    int score = Minimax(draftBoard, nextTurn, depth + 1);

                    if (isMaximizing)
                    {
                        bestScore = Math.Max(bestScore, score);
                    }
                    else
                    {
                        bestScore = Math.Min(bestScore, score);
                    }
                }
            }
        }

        return bestScore;
    }
}