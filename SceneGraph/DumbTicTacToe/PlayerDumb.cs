using CrawfisSoftware.TicTacToeFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrawfisSoftware
{
    class PlayerDumb : IPlayer
    {
        private readonly IGameBoard<int, CellState> gameBoard;
        private readonly System.Random random;
        private readonly CellState cellMarker;

        public PlayerDumb(CellState cellMarker, IGameBoard<int, CellState> gameBoard)
            : this(cellMarker, gameBoard, new System.Random())
        {
        }

        public PlayerDumb(CellState cellMarker, IGameBoard<int, CellState> gameBoard, System.Random random)
        {
            this.cellMarker = cellMarker;
            this.gameBoard = gameBoard;
            this.random = random;
        }

        public void TakeTurn()
        {
            var index = random.Next(9);
            Console.WriteLine("Dumb Player {0}'s turn. It select cellID {1}", cellMarker, index+1);
            gameBoard.ChangeCellAttempt(index, cellMarker);
        }
    }
}
