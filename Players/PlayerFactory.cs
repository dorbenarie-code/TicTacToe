using System;
using TicTacToe.Domain;
namespace TicTacToe.Players

{
    public static class PlayerFactory
    {
        public static IPlayer CreatePlayer(string playerType, CellState mark)
        {
            return playerType.ToLower() switch
            {
                "minimax" => new MinimaxPlayer(mark),
                "human" => new HumanPlayer(mark),
                "random" => throw new NotImplementedException("Random player is not implemented yet!"),
                _ => throw new ArgumentException($"Unknown player type: {playerType}", nameof(playerType))
            };
        }
    }
}