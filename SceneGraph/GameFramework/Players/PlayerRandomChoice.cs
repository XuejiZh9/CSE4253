using System.Linq;
using System;

namespace CrawfisSoftware.TicTacToeFramework
{
    public class PlayerRandomChoice : IPlayer
    {
        private readonly IGameBoard<int, CellState> gameBoard;
        private readonly IQueryGameState<int, CellState> gameState;
        private readonly System.Random random;
        private readonly CellState cellMarker;

        public PlayerRandomChoice(CellState cellMarker, IGameBoard<int, CellState> gameBoard, IQueryGameState<int, CellState> gameState)
            : this(cellMarker, gameBoard, gameState, new System.Random())
        {
        }

        public PlayerRandomChoice(CellState cellMarker, IGameBoard<int, CellState> gameBoard, IQueryGameState<int, CellState> gameState, System.Random random)
        {
            this.cellMarker = cellMarker;
            this.gameBoard = gameBoard;
            this.gameState = gameState;
            this.random = random;
        }

        public void TakeTurn()
        {
            var availableCells = gameState.GetMatchingCells(CellState.Blank).ToList();
            if(availableCells.Count > 0)
            {
                int index = random.Next(availableCells.Count);
                Console.WriteLine("Random AI Player {0}'s turn. It select cellID {1}", cellMarker, availableCells[index]+1);
                gameBoard.ChangeCellAttempt(availableCells[index], cellMarker);
            }
        }
    }
}
