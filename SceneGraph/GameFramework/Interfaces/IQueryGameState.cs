using System.Collections.Generic;

namespace CrawfisSoftware.TicTacToeFramework
{
    public interface IQueryGameState<TCellID, TCellStateEnum>
    {
        IEnumerable<TCellID> GetMatchingCells(TCellStateEnum cellState);
        TCellStateEnum GetCellState(TCellID cellID);
    }
}