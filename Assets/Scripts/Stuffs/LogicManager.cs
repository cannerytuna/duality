using System;
using System.Collections;
using System.Collections.Generic;
using Game.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Stuffs
{
    public class LogicManager : MonoBehaviour
    {
        LevelManager levelManager;
        bool canDie = true;

        //Main game starter at the very beginning
        private void Awake()
        {
            levelManager = GetComponent<LevelManager>();
            levelManager.SetCurrentLevel(CurrentLevel.levelIndex);
            levelManager.InstantiateAllObstacles();
        }


        public void OnDead()
        {
            if (canDie)
            {
                canDie = false;
                SceneManager.LoadScene("DeadScreen");
            }
        }
    }
}