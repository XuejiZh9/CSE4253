using System;

namespace CrawfisSoftware.TicTacToeFramework
{
    public interface IGameScore<TCellStateEnum> where TCellStateEnum : struct, System.Enum
    {
        event Action<IGameScore<TCellStateEnum>, TCellStateEnum> GameOver;

        void CheckGameState();
    }
}