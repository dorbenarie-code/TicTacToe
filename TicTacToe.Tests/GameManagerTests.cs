using System;
using Xunit;
using TicTacToe.Domain;

namespace TicTacToe.Tests;

public class GameManagerTests
{
    [Fact]
    public void Constructor_WhenNewGame_StartsWithXAndInProgressAndEmptyBoard()
    {
        GameManager game = new GameManager();

        Assert.Equal(CellState.X, game.CurrentTurn);
        Assert.Equal(BoardStatus.InProgress, game.Status);
        Assert.Equal(CellState.Empty, game.GameBoard[0, 0]);
        Assert.Equal(CellState.Empty, game.GameBoard[2, 2]);
    }

    [Fact]
    public void PlayTurn_WhenMoveIsValid_PlacesMarkAndSwitchesTurn()
    {
        GameManager game = new GameManager();

        bool ok = game.PlayTurn(0, 0);

        Assert.True(ok);
        Assert.Equal(CellState.X, game.GameBoard[0, 0]);
        Assert.Equal(CellState.O, game.CurrentTurn);
        Assert.Equal(BoardStatus.InProgress, game.Status);
    }

    [Fact]
    public void PlayTurn_WhenCellIsAlreadyFilled_ReturnsFalseAndDoesNotSwitchTurn()
    {
        GameManager game = new GameManager();

        bool first = game.PlayTurn(0, 0);
        bool second = game.PlayTurn(0, 0);

        Assert.True(first);
        Assert.False(second);
        Assert.Equal(CellState.X, game.GameBoard[0, 0]);
        Assert.Equal(CellState.O, game.CurrentTurn);
    }

    [Fact]
    public void PlayTurn_WhenGameIsOver_DoesNotSwitchTurnAndFurtherPlayThrows()
    {
        GameManager game = new GameManager();

        game.PlayTurn(0, 0);
        game.PlayTurn(1, 0);
        game.PlayTurn(0, 1);
        game.PlayTurn(1, 1);
        bool winningMove = game.PlayTurn(0, 2);

        Assert.True(winningMove);
        Assert.Equal(BoardStatus.XWins, game.Status);
        Assert.Equal(CellState.X, game.CurrentTurn);

        Assert.Throws<InvalidOperationException>(() => game.PlayTurn(2, 2));
    }
}