using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToHellAndBack
{
    public class ZombieHordeManager : Singleton<ZombieHordeManager>
    {
        public bool isAdvancing;

        private void OnEnable()
        {
            EventManager.Instance.OnPlayerCaught += Advance;
        }

        private void OnDisable()
        {
            if (EventManager.Instance != null)
            {
                EventManager.Instance.OnPlayerCaught -= Advance;
            }
        }

        private void Advance()
        {
            isAdvancing = true;
        }

        private void Update()
        {
            if (isAdvancing)
            {
                Vector3 positionChange = new Vector3(0f, 0f, RoadManager.Instance.Speed * Time.deltaTime);
                transform.position += positionChange;
            }
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.CompareTag("Zombie"))
            {
                if (!isAdvancing) EventManager.Instance.ZombieDodged();
                Destroy(collider.gameObject);
            }
        }
    }
}
