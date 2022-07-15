using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToHellAndBack
{
    public class ZombieController : MonoBehaviour
    {
        public float Speed;

        private void Update()
        {
            transform.LookAt(PlayerController.Instance.transform);
            Vector3 positionChange = transform.forward * Speed * Time.deltaTime;
            transform.position += positionChange;
        }
    }
}
