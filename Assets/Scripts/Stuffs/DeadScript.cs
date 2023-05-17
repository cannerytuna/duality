using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Stuffs
{
    public class DeadScript : MonoBehaviour
    {
        public void Restart()
        {
            SceneManager.LoadScene("Game");
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}
