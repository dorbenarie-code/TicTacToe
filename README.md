# TicTacToe (C# / .NET 8)

Console TicTacToe built with a clean OOP design and an unbeatable computer opponent using the Minimax algorithm.

## What’s inside
- **Board**: holds the 3x3 grid, validates moves, detects win/draw.
- **GameManager**: controls turns and game state on top of the board.
- **Players (IPlayer)**:
  - `HumanPlayer` for console input with safe validation
  - `MinimaxPlayer` for the computer move selection (recursive search + depth for faster wins / delayed losses)
- **Program.cs**: console UI + main game loop

## Project layout (high level)
- `TicTacToe/` – main game (Domain + Players + Console UI)
- `TicTacToe.Tests/` – xUnit test suite

## Tests
xUnit tests cover the core engine and critical Minimax scenarios:
- Board behavior (valid moves, status detection, deep copy)
- Minimax: blocks immediate loss, takes immediate win
- Game flow via GameManager (turn switching, invalid moves, end-of-game behavior)

## Run
Prerequisite: **.NET 8 SDK**

```bash
dotnet run
Test
dotnet test