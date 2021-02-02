using System;

namespace BattleRoyale
{
    interface AI
    {
        GameObject[,] Update(Player player, GameObject[,] grid);
    }
}
