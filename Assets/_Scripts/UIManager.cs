using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace ToHellAndBack
{
    public class UIManager : MonoBehaviour
    {
        public Animator UIAnimator;
        public TextMeshProUGUI ZombiesDodgedText;
        public TextMeshProUGUI HumansEatenText;
        public int ZombiesDodged;
        public int HumansEaten;

        private void OnEnable()
        {
            EventManager.Instance.OnZombieDodged += ZombieDodged;
            EventManager.Instance.OnPlayerZombieStart += ZombieStart;
            EventManager.Instance.OnPlayerEatHuman += HumanEaten;
            EventManager.Instance.OnGameOver += FadeInOverlay;
        }

        private void OnDisable()
        {
            if (EventManager.Instance != null)
            {
                EventManager.Instance.OnZombieDodged -= ZombieDodged;
                EventManager.Instance.OnPlayerZombieStart -= ZombieStart;
                EventManager.Instance.OnPlayerEatHuman -= HumanEaten;
                EventManager.Instance.OnGameOver -= FadeInOverlay;
            }
        }

        private void ZombieDodged()
        {
            ZombiesDodgedText.text = "Zombies Dodged: " + ++ZombiesDodged;
        }

        private void ZombieStart()
        {
            UIAnimator.SetTrigger("TurnToZombie_Trigger");
        }

        private void HumanEaten()
        {
            HumansEatenText.text = "Humans Eaten: " + ++HumansEaten;
        }

        private void FadeInOverlay()
        {
            UIAnimator.SetTrigger("GameOver_Trigger");
        }

        private void FinishFade()
        {
            EventManager.Instance.UIFadeFinished();
        }
    }
}
