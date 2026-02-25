using Xunit;
using TicTacToe.Domain;
using TicTacToe.Players;

namespace TicTacToe.Tests;

public class MinimaxPlayerTests
{
    [Fact]
    public void GetMove_WhenOpponentAboutToWin_BlocksImmediateThreat()
    {
        Board board = new Board();
        board.TryMakeMove(0, 0, CellState.O);
        board.TryMakeMove(0, 1, CellState.O);
        MinimaxPlayer computerPlayer = new MinimaxPlayer(CellState.X);

        int[] move = computerPlayer.GetMove(board);

        Assert.Equal(0, move[0]);
        Assert.Equal(2, move[1]);
    }

    [Fact]
    public void GetMove_WhenWinningMoveAvailable_TakesWin()
    {
        Board board = new Board();
        board.TryMakeMove(1, 0, CellState.X);
        board.TryMakeMove(1, 1, CellState.X);
        MinimaxPlayer ai = new MinimaxPlayer(CellState.X);

        int[] move = ai.GetMove(board);

        Assert.Equal(1, move[0]);
        Assert.Equal(2, move[1]);
    }
}