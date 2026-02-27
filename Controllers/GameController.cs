using System;
using Microsoft.AspNetCore.Mvc;
using TicTacToe.Domain;
using TicTacToe.Players; 

namespace TicTacToe.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GameController : ControllerBase
{
    private static GameManager gameManager = new GameManager();
    
    private static readonly IPlayer computerPlayer = PlayerFactory.CreatePlayer("minimax", CellState.O);

    [HttpGet("status")]
    public IActionResult GetStatus()
    {
        string[][] boardState = new string[Board.Dimension][];
        for (int r = 0; r < Board.Dimension; r++)
        {
            boardState[r] = new string[Board.Dimension];
            for (int c = 0; c < Board.Dimension; c++)
            {
                CellState cell = gameManager.GameBoard[r, c];
                boardState[r][c] = cell == CellState.Empty ? "-" : cell.ToString();
            }
        }

        return Ok(new
        {
            CurrentTurn = gameManager.CurrentTurn.ToString(),
            Status = gameManager.Status.ToString(),
            Board = boardState
        });
    }

    [HttpPost("play")]
    public IActionResult PlayTurn(int row, int col)
    {
        try
        {
            bool success = gameManager.PlayTurn(row, col);
            
            if (!success)
            {
                return BadRequest(new { Error = "Invalid move. Cell is taken or out of bounds." });
            }

            string moveMessage = $"You played [{row},{col}]. ";

            if (gameManager.Status == BoardStatus.InProgress && gameManager.CurrentTurn == CellState.O)
            {
                int[] bestMove = computerPlayer.GetMove(gameManager.GameBoard);
                gameManager.PlayTurn(bestMove[0], bestMove[1]);
                moveMessage += $"Computer played [{bestMove[0]},{bestMove[1]}].";
            }

            string[][] boardState = new string[Board.Dimension][];
            for (int r = 0; r < Board.Dimension; r++)
            {
                boardState[r] = new string[Board.Dimension];
                for (int c = 0; c < Board.Dimension; c++)
                {
                    CellState cell = gameManager.GameBoard[r, c];
                    boardState[r][c] = cell == CellState.Empty ? "-" : cell.ToString();
                }
            }

            return Ok(new
            {
                Message = moveMessage,
                CurrentTurn = gameManager.CurrentTurn.ToString(),
                Status = gameManager.Status.ToString(),
                Board = boardState
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }

    [HttpPost("reset")]
    public IActionResult ResetGame()
    {
        gameManager = new GameManager();
        return Ok(new { Message = "Game has been reset." });
    }
}