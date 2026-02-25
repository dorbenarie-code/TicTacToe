using Xunit;
using TicTacToe.Domain;

namespace TicTacToe.Tests;

public class BoardTests
{
    [Fact]
    public void GetStatus_WhenNewBoard_ReturnsInProgress()
    {
        Board board = new Board();

        BoardStatus status = board.GetStatus();

        Assert.Equal(BoardStatus.InProgress, status);
    }

    [Fact]
    public void TryMakeMove_WhenCellAlreadyFilled_ReturnsFalseAndDoesNotChangeCell()
    {
        Board board = new Board();

        bool first = board.TryMakeMove(0, 0, CellState.X);
        bool second = board.TryMakeMove(0, 0, CellState.O);

        Assert.True(first);
        Assert.False(second);
        Assert.Equal(CellState.X, board[0, 0]);
    }

    [Fact]
    public void BoardCopyConstructor_WhenCopyIsModified_OriginalBoardIsNotChanged()
    {
        Board original = new Board();
        original.TryMakeMove(0, 0, CellState.X);

        Board copy = new Board(original);
        copy.TryMakeMove(0, 1, CellState.O);

        Assert.Equal(CellState.X, original[0, 0]);
        Assert.Equal(CellState.Empty, original[0, 1]);

        Assert.Equal(CellState.X, copy[0, 0]);
        Assert.Equal(CellState.O, copy[0, 1]);
    }

    [Fact]
    public void GetStatus_WhenTopRowIsAllX_ReturnsXWins()
    {
        Board board = new Board();
        board.TryMakeMove(0, 0, CellState.X);
        board.TryMakeMove(0, 1, CellState.X);
        board.TryMakeMove(0, 2, CellState.X);

        BoardStatus status = board.GetStatus();

        Assert.Equal(BoardStatus.XWins, status);
    }

    [Fact]
    public void GetStatus_WhenBoardIsFullWithNoWinner_ReturnsDraw()
    {
        Board board = new Board();

        board.TryMakeMove(0, 0, CellState.X);
        board.TryMakeMove(0, 1, CellState.O);
        board.TryMakeMove(0, 2, CellState.X);

        board.TryMakeMove(1, 0, CellState.X);
        board.TryMakeMove(1, 1, CellState.O);
        board.TryMakeMove(1, 2, CellState.O);

        board.TryMakeMove(2, 0, CellState.O);
        board.TryMakeMove(2, 1, CellState.X);
        board.TryMakeMove(2, 2, CellState.X);

        BoardStatus status = board.GetStatus();

        Assert.Equal(BoardStatus.Draw, status);
    }
}