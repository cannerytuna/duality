using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core
{
    public static class LevelGrid
    {
        //left to right
        public static Vector2Int gridDomain = new Vector2Int(-7, 6);
        public static Vector2 worldGridDomain = new Vector2(-6.5f, 6.5f);

        //ehhhhh
        public static int gridXDelta = gridDomain.y - gridDomain.x;

        //down to up
        public static Vector2Int gridRange = new Vector2Int(-4, 3);
        public static Vector2 worldGridRange = new Vector2(-3.5f, 3.5f);

        public static int gridYDelta = gridRange.y - gridRange.x;


        
        public static Vector3Int IndexToGridPos(int half, int row, int column)
        {
            row = (Const.Rows - row) - 1;

            if (half >= Const.PlayerCount || row >= Const.Rows || column >= Const.Columns)
            {
                Debug.Log("error homie");
                return new Vector3Int(0, 0, 0);
            }
            // 6 + 1 = 7 and needs to be formatted like an index to work with the coords or whatever idk
            var x = ((half * Const.Columns) + (half * 2) + column) + gridDomain.x;
            var y = row + gridRange.x;
            return new Vector3Int(x, y, 0);
        }
        public static Vector3Int GridPosToIndex(Vector3Int pos)
        {
            int half = 0;
            var x = pos.x - gridDomain.x;
            var y = pos.y - gridRange.x;
            if (x < 0 || y < 0)
            {
                Debug.Log("values too small: " + x + ", " + y);
            }
            if (x == Const.Columns || x == Const.Columns + 1)
            {
                //returns a higher index which is caught hopefully
                return new Vector3Int(2, 0, 0);
            }
            if (x > Const.Columns)
            {
                half = 1;
                x -= (Const.Columns + 2);
                if (x > Const.Columns)
                    Debug.Log("ur x-value too high homie");
            }
            y = (Const.Rows - y) - 1;
            if (y > Const.Rows)
                Debug.Log("y value too high :(");

            //maybe good now??
            return new Vector3Int(half, y, x);
        }
        public static Vector3 GridPosToWorldPos(Vector3Int gridPos)
        {
            return new Vector3(gridPos.x + 0.5f, gridPos.y + 0.5f, 0f);
        }
        public static Vector3Int WorldPosToGridPos(Vector3 worldPos)
        {
            return new Vector3Int(Mathf.RoundToInt(worldPos.x - 0.5f), Mathf.RoundToInt(worldPos.y - 0.5f), 0);
        }
    }
}
