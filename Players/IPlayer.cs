using TicTacToe.Domain;

namespace TicTacToe.Players
{
    public interface IPlayer
    {
        int[] GetMove(Board currentBoard);
    }
}