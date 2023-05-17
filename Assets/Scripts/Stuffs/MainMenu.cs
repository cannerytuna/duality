using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

namespace Game.Stuffs
{
    public class MainMenu : MonoBehaviour
    {
        Resolution[] resolutions;
        public TMP_Dropdown resolutionDropdown;
        bool isFullScreen = false;

        public void StartGame()
        {
            SceneManager.LoadScene("Game");
        }

        public void QuitApplication()
        {
            Application.Quit();
        }


        public void SetResolution(int resolutionIndex)
        {
            var x = resolutions[resolutionIndex];
            Screen.SetResolution(x.width, x.height, isFullScreen);
        }


        public void FullscrenToggle(bool toggle)
        {
            isFullScreen = toggle;
            Screen.fullScreen = toggle;
        }

        public void YellowMan()
        {
            PlayerInfo.leftIsFavoriteCharacter = true;
            StartGame();
        }

        public void GreenMan()
        {
            Debug.Log("s");
            PlayerInfo.leftIsFavoriteCharacter = false;
            StartGame();
        }

        void Start()
        {
            resolutionDropdown.ClearOptions();

            resolutions = Screen.resolutions;
            List<string> resolutionStrings = new List<string>();
            int currentResolutionIndex = 0;
            for (int i = 0; i < resolutions.Length; i++)
            {
                resolutionStrings.Add(resolutions[i].width + " x " + resolutions[i].height);

                if (resolutions[i].width == Screen.currentResolution.width &&
                    resolutions[i].height == Screen.currentResolution.height)
                {
                    currentResolutionIndex = i;
                }
            }

            resolutionDropdown.AddOptions(resolutionStrings);
            resolutionDropdown.value = currentResolutionIndex;
            resolutionDropdown.RefreshShownValue();
        
        }

    }
}
