using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Game.Core;
using UnityEngine.SceneManagement;

namespace Game.Stuffs
{
    [Serializable]
    public class LevelManager : MonoBehaviour
    {
        public List<Level> levels = new List<Level>();
        public GameObject spike;
        public GameObject swapCube;
        public GameObject playerObject;
        public Sprite orangeGuy;
        public Sprite greenGuy;
        public GameObject WASDDisplay;
        public GameObject EDisplay;
        private bool nextUpdateLoop = false;

        

        private void Update()
        {
            if (nextUpdateLoop)
            {
                NextLevel();
            }
        }

        public void DestroyAllObstacles()
        {
            foreach (var obj in GameObject.FindGameObjectsWithTag("Obstacle"))
            {
                DestroyImmediate(obj);
            }

            foreach (var player in GameObject.FindGameObjectsWithTag("Player"))
            {
                DestroyImmediate(player);
            }

            PlayerInfo.players.Clear();
        }

        public void InstantiateAllObstacles()
        {
            for (int row = 0;  row < Const.Rows; row++) 
            {
                for (int half = 0; half < Const.PlayerCount; half++)
                {
                    for (int col = 0; col < Const.Columns; col++)
                    {
                        InstantiateObstacle(half,row,col);
                    }
                }
            }
            
            PlayerInfo.FindPlayers();
        }

        private void InstantiateObstacle(int half, int row, int col)
        {
            Obstacle obj = CurrentLevel.level[half, row, col];
            switch (obj)
            {
                case Obstacle.None: break;
                case Obstacle.spike:
                    Instantiate(spike, LevelGrid.GridPosToWorldPos(LevelGrid.IndexToGridPos(half, row, col)), Quaternion.identity);
                    break;
                case Obstacle.swapCube:
                    Instantiate(swapCube, LevelGrid.GridPosToWorldPos(LevelGrid.IndexToGridPos(half, row, col)), Quaternion.identity);
                    break;
                case Obstacle.spawn:
                    SpawnPlayer(half, row, col);
                    break;
            }

            switch (CurrentLevel.levelIndex)
            {
                case 0:
                    WASDDisplay.SetActive(true);
                    break;
                default:
                    WASDDisplay.SetActive(false);
                    break;
            }
            
        }

        private void SpawnPlayer(int half, int row, int col)
        {
            var obj = Instantiate(playerObject, LevelGrid.GridPosToWorldPos(LevelGrid.IndexToGridPos(half, row, col)), Quaternion.identity);

            if ((half == 0 && PlayerInfo.leftIsFavoriteCharacter) || (half == 1 && !PlayerInfo.leftIsFavoriteCharacter))
                obj.GetComponent<Movement>().player = Player.one;
            else
                obj.GetComponent<Movement>().player = Player.two;

            if (half == 0)
                obj.GetComponent<SpriteRenderer>().sprite = orangeGuy;
            else
                obj.GetComponent<SpriteRenderer>().sprite = greenGuy;
        }

        public void NextLevel()
        {
            CurrentLevel.levelIndex++;

            DestroyAllObstacles();
            if (CurrentLevel.levelIndex < levels.Count)
            {
                SetCurrentLevel(CurrentLevel.levelIndex);
                InstantiateAllObstacles();
            }
            else
            {
                WinGame();
            }

            nextUpdateLoop = false;
        }

        private void WinGame()
        {
            SceneManager.LoadScene("Win");
        }

        public Obstacle ObstacleAtGridPos (Vector3Int gridPos)
        {
            Vector3Int index = LevelGrid.GridPosToIndex(gridPos);
            if (index.x > 1)
            {
                return Obstacle.win;
            }
            return CurrentLevel.level[index.x, index.y, index.z];
        }

        public void SetCurrentLevel(int levelIndex)
        {
            Level level = levels[levelIndex];
            for (int column = 0; column < Const.Columns; column++)
            {
                Column currentColumn = GetColumn(column, level.firstHalf);
                for (int slot = 0; slot < Const.Rows; slot++)
                {
                    CurrentLevel.level[0, slot, column] = GetObstacle(slot, currentColumn);
                }

                currentColumn = GetColumn(column, level.secondHalf);
                for (int slot = 0; slot < Const.Rows; slot++)
                {
                    CurrentLevel.level[1, slot, column] = GetObstacle(slot, currentColumn);
                }

            }
        }








        //IGNORE EVERYTHING PAST THIS POINT
        public Column GetColumn(int index, LevelHalf levelHalf) 
        { 
            //this is so awful
            switch(index)
            {
                case 0: return levelHalf.column1;
                case 1: return levelHalf.column2;
                case 2: return levelHalf.column3;
                case 3: return levelHalf.column4;
                case 4: return levelHalf.column5;
                case 5: return levelHalf.column6;
            }
            Debug.Log("Error homie");
            return null;
        }
        public Obstacle GetObstacle(int index, Column column)
        {
            //this also sucks so bad
            switch(index) 
            {
                case 0: return column.obstacle1;
                case 1: return column.obstacle2;
                case 2: return column.obstacle3;
                case 3: return column.obstacle4;
                case 4: return column.obstacle5;
                case 5: return column.obstacle6;
                case 6: return column.obstacle7;
                case 7: return column.obstacle8;
            }
            Debug.Log("Error homie");
            return Obstacle.None;
        }

    }

    [Serializable]
    public class Level
    {
        // this is the worst shit ive written in my life
        public LevelHalf firstHalf = new LevelHalf();
        public LevelHalf secondHalf = new LevelHalf();
    }

    [Serializable]
    public class LevelHalf
    {
        // oh fuck oh no
        public Column column1 = new Column();
        public Column column2 = new Column();
        public Column column3 = new Column();
        public Column column4 = new Column();
        public Column column5 = new Column();
        public Column column6 = new Column();
    }

    [Serializable]
    public class Column
    {
        // :(
        public Obstacle obstacle1 = Obstacle.None;
        public Obstacle obstacle2 = Obstacle.None;
        public Obstacle obstacle3 = Obstacle.None;
        public Obstacle obstacle4 = Obstacle.None;
        public Obstacle obstacle5 = Obstacle.None;
        public Obstacle obstacle6 = Obstacle.None;
        public Obstacle obstacle7 = Obstacle.None;
        public Obstacle obstacle8 = Obstacle.None;
    }


}
