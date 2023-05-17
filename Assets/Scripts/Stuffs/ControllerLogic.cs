using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Game.Core;

namespace Game.Stuffs
{

    public class ControllerLogic : MonoBehaviour
    {

        public void ThrowAwaySwapCube(InputAction.CallbackContext x)
        {
            if (x.phase != InputActionPhase.Performed)
            {
                return;
            }
            foreach (Movement player in PlayerInfo.players)
            {
                if (player != null)
                {
                    player.hasSwapCube = false;
                }
            }
        }
        
        void Update()
        {
            switch (PlayerInfo.playerState)
            {
                case State.canJump:
                    break;
                case State.cantJump:
                    if (PlayerInfo.t > PlayerInfo.SecondsTillJump)
                    {
                        PlayerInfo.t = 0;
                        PlayerInfo.playerState = State.canJump;
                    }
                    PlayerInfo.t += Time.deltaTime;
                    break;
            }
        }
        
        public void OnMove(InputAction.CallbackContext movementValue)
        {
            if (movementValue.phase != InputActionPhase.Performed)
            {
                return;
            }
            Vector2 movementVector = movementValue.ReadValue<Vector2>();

            if (PlayerInfo.playerState == State.canJump && PlayerInfo.players != null)
            {
                PlayerInfo.playerState = State.cantJump;
                for (int i = 0; i < PlayerInfo.players.Count; i++)
                {
                    if (PlayerInfo.players[i] == null)
                    {
                        continue;
                    }
                    
                    if (PlayerInfo.players[i].player == Player.one)
                    {
                        PlayerInfo.players[i].UpdateGrid(movementVector);
                    }
                    else
                    {
                        PlayerInfo.players[i].UpdateGrid(new Vector2(-movementVector.x, movementVector.y));
                    }
                }
                
                
                // foreach (Movement player in PlayerInfo.players)
                // {
                //     if (player.player == Player.one)
                //     {
                //         player.UpdateGrid(movementVector);
                //     }
                //     else
                //     {
                //         player.UpdateGrid(new Vector2(-movementVector.x, movementVector.y));
                //     }
                // }
            }

        }
    }

}