using System;

namespace CrawfisSoftware.TicTacToeFramework
{
    public class GameScoreThreeInARow : IGameScore<CellState>
    {
        private const int NumberOfWinStates = 8;
        private readonly IGameBoard<int,CellState> gameboard;
        private readonly IQueryGameState<int, CellState> gameboardQuery;
        private CellState winner = CellState.Blank;
        private int[][] countRedirects = new int[9][] { new int[]{ 0, 3, 6 }, new int[]{ 0, 4 }, new int[]{ 0, 5, 7 },
                        new int[]{ 1, 3 }, new int[]{ 1, 4, 6, 7 }, new int[]{ 1, 5 },
                        new int[]{2,3,7 }, new int[]{2,4 }, new int[]{2,5,6 } };
        private int[] sums = new int[NumberOfWinStates];

        public event Action<IGameScore<CellState>, CellState> GameOver;

        public GameScoreThreeInARow(IGameBoard<int,CellState> gameboard, IQueryGameState<int, CellState> gameboardQuery)
        {
            this.gameboard = gameboard;
            this.gameboard.CellChanged += HandleCellChange;
            this.gameboardQuery = gameboardQuery;
        }

        private void HandleCellChange(int cellID, CellState oldCellState, CellState cellState)
        {
            if( oldCellState != CellState.Blank)
            {
                ResetSums();
                ComputeSums();
            }
            else
            {
                IncrementCellCounts(cellID, cellState);
            }
        }

        private void ResetSums()
        {
            for(int i=0; i < NumberOfWinStates; i++)
            {
                sums[i] = 0;
            }
        }

        private void ComputeSums()
        {
            for(int i=0; i < 9; i++)
            {
                CellState state = gameboardQuery.GetCellState(i);
                IncrementCellCounts(i, state);
            }
        }

        private void IncrementCellCounts(int cellID, CellState cellState)
        {
            int increment = 0;
            if (cellState == CellState.X) increment = 1;
            else if (cellState == CellState.O) increment = -1;
            foreach(int sumArray in countRedirects[cellID])
            {
                sums[sumArray] += increment;
                if(sums[sumArray] == 3)
                {
                    //Game over X's win
                    winner = CellState.X;
                }
                else if (sums[sumArray] == -3)
                {
                    //Game over X's win
                    winner = CellState.O;
                }
            }
        }

        public void CheckGameState()
        {
            // We delay announcing a winner in case the game driver wants to check every other turn or so.
            if(winner != CellState.Blank)
            {
                GameOver?.Invoke(this, winner);
            }
        }
    }
}