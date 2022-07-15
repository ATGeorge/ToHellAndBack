using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ToHellAndBack
{
    public class MenuManager : MonoBehaviour
    {
        public void StartButtonPressed()
        {
            EventManager.Instance.MenuStartButtonPressed();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public void QuitButtonPressed()
        {
            Application.Quit();
        }
    }
}
