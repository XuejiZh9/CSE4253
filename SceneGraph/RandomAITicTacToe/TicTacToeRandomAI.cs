using System;
using System.Collections.Generic;
using CrawfisSoftware.TicTacToeFramework;

namespace CrawfisSoftware
{
    class TicTacToeRandomAI
    {
        private static IGameBoard<int, CellState> gameBoard;
        private static IQueryGameState<int, CellState> gameQuery;
        private static IGameScore<CellState> gameScore;
        private static IPlayer playerX;
        private static IPlayer playerO;
        private static ITurnbasedScheduler scheduler;
        private static System.Random random = new System.Random();
        private static bool gameOver = false;

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Random AI Tic Tac Toe!");
            CreateGame();
            CreateConsolePlayers(gameBoard);
            CreateScheduler(playerX, playerO);
            CreateGameScore(gameBoard, gameQuery);
            SubscribeToEvents(gameBoard);
            //PresentInstructions();

            while (!gameOver)
            {
                IPlayer player = scheduler.SelectPlayer();
                player.TakeTurn();
                gameScore.CheckGameState();
            }
        }

        private static void GameScore_GameOver(object sender, CellState winner)
        {
            Console.WriteLine();
            gameOver = true;
            if (winner == CellState.Blank)
            {
                Console.WriteLine("Game is a Draw :-(");
            }
            else
            {
                Console.WriteLine("{0} wins! :-)", winner);
            }
        }

        private static void CreateGame()
        {
            var game = new TicTacToeBoard<CellState>(CheckIfBlankOrTheSame);
            gameBoard = game;
            gameQuery = game;
        }

        private static void CreateConsolePlayers(IGameBoard<int, CellState> gameBoard)
        {
            playerX = new PlayerConsoleUnsafe(CellState.X, gameBoard);
            playerO = new PlayerRandomChoice(CellState.O, gameBoard, gameQuery);
        }

        private static void CreateScheduler(IPlayer playerX, IPlayer playerO)
        {
            scheduler = new SequentialTurnsScheduler(new List<IPlayer>() { playerX, playerO });
        }

        private static void CreateGameScore(IGameBoard<int, CellState> gameBoard, IQueryGameState<int, CellState> gameQuery)
        {
            var gameScoreComposite = new GameScoreComposite<CellState>();

            var gameScorer1 = new GameScoreThreeInARow(gameBoard, gameQuery);
            gameScoreComposite.AddGameScore(gameScorer1);

            var gameScorer2 = new GameScoreBoardFilled<CellState>(gameQuery);
            gameScoreComposite.AddGameScore(gameScorer2);

            // add the max number of turns scorer
            var gameScorer3 = new GameScoreMaxNumberOfTurns<CellState>(15, gameBoard);
            gameScoreComposite.AddGameScore(gameScorer3);

            gameScore = gameScoreComposite;
            gameScore.GameOver += GameScore_GameOver;
        }

        private static void SubscribeToEvents(IGameBoard<int, CellState> gameBoard)
        {
            gameBoard.ChangeCellRequested += GameBoard_ChangeCellRequested;
            gameBoard.CellChanged += GameBoard_CellChanged;
        }

        private static void GameBoard_CellChanged(int cellID, CellState oldCellState, CellState newCellState)
        {
            Console.WriteLine("Cell {0} changed to {1}", cellID, newCellState);
        }

        private static void GameBoard_ChangeCellRequested(int cellID, CellState currentCellState, CellState proposedCellState)
        {
            Console.WriteLine("Attempt to change cell {0} from {1} to {2}", cellID, currentCellState, proposedCellState);
        }

        private static void PresentInstructions()
        {

        }

        private static bool CheckIfBlankOrTheSame(int cellID, CellState currentCellValue, CellState newCellState)
        {
            if (currentCellValue == CellState.Blank && currentCellValue != newCellState)
                return true;
            return false;
        }
    }
}
