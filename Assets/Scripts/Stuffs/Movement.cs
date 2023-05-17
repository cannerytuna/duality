using Game.Core;
using System;
using UnityEngine;

namespace Game.Stuffs
{
    public class Movement : MonoBehaviour
    {
        public bool onGoal = false;
        public bool hasSwapCube = false;
        public State state = State.canJump;
        public Player player = Player.one;
        private Vector3Int gridPosition;
        private LevelManager levelManager;
        private LogicManager logicManager;


        // Start is called before the first frame update
        void Start()
        {
            var x = GameObject.FindGameObjectWithTag("Logic");
            levelManager = x.GetComponent<LevelManager>();
            logicManager = x.GetComponent<LogicManager>();

            gridPosition = LevelGrid.WorldPosToGridPos(transform.position);
        }


        public void UpdateGrid(Vector2 moveVector)
        {
            //this is dumb
            if (hasSwapCube)
            {
                moveVector.x = -moveVector.x;
            }
            Vector3Int temp = new Vector3Int((int)moveVector.x, (int)moveVector.y);
            temp = gridPosition + temp;

            //awful awful grid detection please ignore i wont fix it
            if ((temp.x < -7 || temp.x > -2
                || temp.y > 3 || temp.y < -4)
                && (temp.x < 1 || temp.x > 6
                || temp.y > 3 || temp.y < -4))
                if (temp.x != -1 && temp.x != 0
                    || temp.y != -4)
                    return;

            gridPosition = temp;
            transform.position = LevelGrid.GridPosToWorldPos(gridPosition);
            CheckCollision(gridPosition);
        }

        //returns true or false if it should let the character move there
        public void CheckCollision(Vector3Int gridPos)
        {
            if (onGoal)
            {
                onGoal = false;
                PlayerInfo.playersAtGoal--;
            }

            
            switch (levelManager.ObstacleAtGridPos(gridPos))
            {
                case Obstacle.None:
                case Obstacle.spawn:
                    return;
                case Obstacle.spike:
                    logicManager.OnDead();
                    return;
                case Obstacle.swapCube:
                    foreach (var obj in GameObject.FindGameObjectsWithTag("Obstacle"))
                    {
                        if (obj.transform.position.x != transform.position.x || obj.transform.position.y != transform.position.y)
                            continue;
                        Destroy(obj);
                        hasSwapCube = true;
                    }
                    return;
                case Obstacle.win:
                    onGoal = true;
                    PlayerInfo.playersAtGoal++;
                    if (PlayerInfo.playersAtGoal == Const.PlayerCount)
                    {
                        levelManager.NextLevel();
                    }
                    return;
                default: 
                    Debug.Log("fucked up somethin");
                    return;
            }
        }


    }
}