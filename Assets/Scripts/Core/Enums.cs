using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core
{
    public enum Obstacle {
        None = 1,
        spike = 2,
        swapCube = 3,
        spawn = 4,
        win = 5
    }

    public enum Player { one, two }
    
    public enum State { canJump, cantJump }
    
    public static class CurrentLevel
    {
        public static int levelIndex = 0;
        public static Obstacle[,,] level = new Obstacle[Const.PlayerCount, Const.Rows, Const.Columns];
        
    }

    public static class Const
    {
        public const int PlayerCount = 2;
        public const int Rows = 8;
        public const int Columns = 6;
    }
}
