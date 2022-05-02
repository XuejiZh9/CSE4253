using System;
using System.Collections.Generic;

namespace CrawfisSoftware.TicTacToeFramework
{
    public interface IGameBoard<TCellID, TCellStateEnum> where TCellStateEnum : System.Enum
    {
        event Action<TCellID, TCellStateEnum, TCellStateEnum> ChangeCellRequested;
        event Action<TCellID, TCellStateEnum, TCellStateEnum> CellChanged;

        void ChangeCellAttempt(TCellID cellID, TCellStateEnum newCellState);
    }
}