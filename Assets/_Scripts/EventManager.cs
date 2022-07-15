using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToHellAndBack
{
    public class EventManager : Singleton<EventManager>
    {
        public Action<int, float> OnVolumeChanged;
        public void VolumeChanged(int groupIndex, float value) => OnVolumeChanged?.Invoke(groupIndex, value);

        public Action OnMenuStartButtonPressed;
        internal void MenuStartButtonPressed() => OnMenuStartButtonPressed?.Invoke();

        public Action OnZombieDodged;
        public void ZombieDodged() => OnZombieDodged?.Invoke();

        public Action OnPlayerCaught;
        public void PlayerCaught() => OnPlayerCaught?.Invoke();

        public Action OnPlayerZombieStart;
        public void PlayerZombieStart() => OnPlayerZombieStart?.Invoke();

        public Action OnPlayerEatHuman;
        public void PlayerEatHuman() => OnPlayerEatHuman?.Invoke();

        public Action OnGameOver;
        public void GameOver() => OnGameOver?.Invoke();

        public Action OnUIFadeFinished;
        public void UIFadeFinished() => OnUIFadeFinished?.Invoke();
    }
}
