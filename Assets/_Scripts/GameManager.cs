using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace ToHellAndBack
{
    public class GameManager : Singleton<GameManager>
    {
        public float CurrentRunClock;
        public bool IsHuman;
        public bool IsZombie;

        private void OnEnable()
        {
            EventManager.Instance.OnMenuStartButtonPressed += StartRun;
            EventManager.Instance.OnPlayerCaught += EndHumanMode;
            EventManager.Instance.OnPlayerZombieStart += StartZombieMode;
            EventManager.Instance.OnUIFadeFinished += RestartGame;
        }

        private void OnDisable()
        {
            if (EventManager.Instance != null)
            {
                EventManager.Instance.OnMenuStartButtonPressed -= StartRun;
                EventManager.Instance.OnPlayerCaught -= EndHumanMode;
                EventManager.Instance.OnPlayerZombieStart -= StartZombieMode;
                EventManager.Instance.OnUIFadeFinished -= RestartGame;
            }
        }

        private void Update()
        {
            if (IsHuman) CurrentRunClock += Time.deltaTime;
            else if(IsZombie)
            {
                if (CurrentRunClock <= 0f)
                {
                    EventManager.Instance.GameOver();
                }
                CurrentRunClock -= Time.deltaTime;
            }
        }

        private void StartRun() => IsHuman = true;

        private void EndHumanMode() => IsHuman = false;

        private void StartZombieMode() => IsZombie = true;

        private void RestartGame()
        {
            IsZombie = false;
            CurrentRunClock = 0f;
            SceneManager.LoadScene(0);
        }
    }
}
