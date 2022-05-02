using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrawfisSoftware.TicTacToeFramework
{
    public class SequentialTurnsScheduler : ITurnbasedScheduler
    {
        private readonly List<IPlayer> players = new List<IPlayer>();
        private int index = 0;

        public SequentialTurnsScheduler(IList<IPlayer> players)
        {
            if (players == null || players.Count == 0)
                throw new ArgumentException("No players to schedule");
            foreach(var player in players)
            {
                this.players.Add(player);
            }
        }

        public IPlayer SelectPlayer()
        {
            IPlayer player = players[index];
            index = (index + 1) % players.Count;
            return player;
        }
    }
}
