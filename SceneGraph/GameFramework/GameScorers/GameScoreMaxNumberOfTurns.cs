using System;

namespace CrawfisSoftware.TicTacToeFramework
{
    public class GameScoreMaxNumberOfTurns<TCellStateEnum> : IGameScore<TCellStateEnum> where TCellStateEnum : struct, System.Enum
    {
        private readonly int maxNumberOfTurns;
        private int numberOfTurnsTaken = 0;

        public event Action<IGameScore<TCellStateEnum>, TCellStateEnum> GameOver;

        public GameScoreMaxNumberOfTurns(int maxNumberOfTurns, IGameBoard<int, TCellStateEnum> gameboard)
        {
            this.maxNumberOfTurns = maxNumberOfTurns;
            gameboard.ChangeCellRequested += TurnTaken;
        }

        private void TurnTaken(int arg1, TCellStateEnum arg2, TCellStateEnum arg3)
        {
            numberOfTurnsTaken++;
        }

        public void CheckGameState()
        {
            if(numberOfTurnsTaken >= maxNumberOfTurns)
            {
                GameOver?.Invoke(this, default(TCellStateEnum));
            }
        }
    }
}
