using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToHellAndBack
{
    public class LightManager : MonoBehaviour
    {
        public Animator Animator;
        public GameObject HumanLight;
        public GameObject ZombieLight;

        private void OnEnable()
        {
            EventManager.Instance.OnPlayerCaught += ChangeSceneLighting;
        }

        private void OnDisable()
        {
            if (EventManager.Instance != null)
            {
                EventManager.Instance.OnPlayerCaught -= ChangeSceneLighting;
            }
        }

        private void ChangeSceneLighting()
        {
            Animator.enabled = true;
        }

        private void TurnOffLight()
        {
            HumanLight.SetActive(false);
        }

        private void TurnOnLight()
        {
            ZombieLight.SetActive(true);
        }
    }
}
