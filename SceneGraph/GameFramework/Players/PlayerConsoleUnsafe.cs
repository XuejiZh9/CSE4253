using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrawfisSoftware.TicTacToeFramework
{
    public class PlayerConsoleUnsafe : IPlayer
    {
        private readonly CellState playerMark;
        private readonly IGameBoard<int, CellState> gameBoard;

        public PlayerConsoleUnsafe(CellState playerMark, IGameBoard<int,CellState> gameBoard)
        {
            this.playerMark = playerMark;
            this.gameBoard = gameBoard;
        }

        public void TakeTurn()
        {
            Console.Write("Player {0}'s turn. Enter cellID (1-9):  ", playerMark);
            bool end = false;
            while (!end)
            {
                var input = Console.ReadLine();
                try
                {
                    int cellID = int.Parse(input) - 1;
                    if (cellID >= 0 && cellID < 9)
                    {
                        gameBoard.ChangeCellAttempt(cellID, playerMark);
                        end = true;
                    }else
                    {
                        Console.Write("Invalid input! Enter again: ");
                    }
                }
                catch (Exception)
                {
                    Console.Write("Invalid input! Enter again: ");
                }
            }
        }
    }
}
