using CrawfisSoftware.TicTacToeFramework;
using System;
using System.Collections.Generic;

namespace CrawfisSoftware
{
    public class GameScoreComposite<TCellStateEnum> : IGameScore<TCellStateEnum> where TCellStateEnum : struct, System.Enum
    {
        private List<IGameScore<TCellStateEnum>> gameScorers = new List<IGameScore<TCellStateEnum>>();
        private bool gameOver = false;

        public event Action<IGameScore<TCellStateEnum>, TCellStateEnum> GameOver;

        public void AddGameScore(IGameScore<TCellStateEnum> scorer)
        {
            gameScorers.Add(scorer);
            scorer.GameOver += Scorer_GameOver;
        }

        private void Scorer_GameOver(IGameScore<TCellStateEnum> sender, TCellStateEnum cellState)
        {
            GameOver?.Invoke(sender, cellState);
            gameOver = true;
        }

        public void CheckGameState()
        {
            foreach(var scorer in gameScorers)
            {
                if(!gameOver)
                    scorer.CheckGameState();
            }
        }
    }
}
