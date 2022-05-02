using System;
using System.Linq;

namespace CrawfisSoftware.TicTacToeFramework
{
    public class GameScoreBoardFilled<TCellStateEnum> : IGameScore<TCellStateEnum> where TCellStateEnum : struct, System.Enum
    {
        private readonly IQueryGameState<int, CellState> gameboardQuery;

        public event Action<IGameScore<TCellStateEnum>, TCellStateEnum> GameOver;

        public GameScoreBoardFilled(IQueryGameState<int, CellState> gameboardQuery)
        {
            this.gameboardQuery = gameboardQuery;
        }
        public void CheckGameState()
        {
            // We delay announcing a winner in case the game driver wants to check every other turn or so.
            int numberOfBlankSpaces = gameboardQuery.GetMatchingCells(default).ToList().Count;
            if (numberOfBlankSpaces == 0)
            {
                GameOver?.Invoke(this, default);
            }
        }
    }
}