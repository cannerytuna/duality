using System.Collections.Generic;
using Game.Core;
using UnityEngine;

namespace Game.Stuffs
{
    public static class PlayerInfo
    {
        public static int playersAtGoal = 0;
        
        public static float t = 0;
        public const float SecondsTillJump = 0.2f;
        public static State playerState = State.canJump;
        public static bool leftIsFavoriteCharacter = true;

        public static List<Movement> players = new();

        public static void FindPlayers()
        {
            playersAtGoal = 0;
            players.Clear();
            Debug.Log(players.Count);
            foreach (var player in UnityEngine.GameObject.FindGameObjectsWithTag("Player"))
            {
                var script = player.GetComponent<Movement>();
                players.Add(script);
                script.hasSwapCube = false;
            }
            Debug.Log(players.Count);
        }
    }
}