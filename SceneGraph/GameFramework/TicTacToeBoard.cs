using System;
using System.Collections.Generic;
using System.Linq;

namespace CrawfisSoftware.TicTacToeFramework
{
    public class TicTacToeBoard<TCellStateEnum> : IGameBoard<int, TCellStateEnum>, IQueryGameState<int,TCellStateEnum> 
        where TCellStateEnum : struct, System.Enum
    {
        private TCellStateEnum[] gameBoard = new TCellStateEnum[9];
        private readonly Func<int, TCellStateEnum, TCellStateEnum, bool> CanChangeCell;

        public event Action<int, TCellStateEnum, TCellStateEnum> ChangeCellRequested;
        public event Action<int, TCellStateEnum, TCellStateEnum> CellChanged;

        public TicTacToeBoard(Func<int, TCellStateEnum, TCellStateEnum, bool> CanChangeCell)
        {
            this.CanChangeCell = CanChangeCell;
        }

        public void ChangeCellAttempt(int cellID, TCellStateEnum newCellState)
        {
            TCellStateEnum currentCellValue = gameBoard[cellID];
            ChangeCellStrategy(cellID, newCellState, currentCellValue);
        }

        public IEnumerable<int> GetMatchingCells(TCellStateEnum cellState)
        {
            int index = 0;
            foreach (TCellStateEnum cell in gameBoard)
            {
                if (cellState.Equals(cell))
                    yield return index;
                index++;
            }
        }

        protected virtual void ChangeCellStrategy(int cellID, TCellStateEnum newCellState, TCellStateEnum currentCellValue)
        {
            ChangeCellRequested?.Invoke(cellID, currentCellValue, newCellState);
            if (CanChangeCell(cellID, currentCellValue, newCellState))
            {
                gameBoard[cellID] = newCellState;
                CellChanged?.Invoke(cellID, currentCellValue, newCellState);
            }
        }

        public TCellStateEnum GetCellState(int cellID)
        {
            return gameBoard[cellID];
        }
    }
}
