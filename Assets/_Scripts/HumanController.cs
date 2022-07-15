using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToHellAndBack
{
    public class HumanController : MonoBehaviour
    {
        public ParticleSystem BloodParticleSystem;
        public float Speed;
        public bool IsAlive;

        private void Update()
        {
            if (IsAlive)
            {
                transform.LookAt(transform.position + (transform.position - PlayerController.Instance.transform.position).normalized);
                Vector3 newPosition = new Vector3(
                        Mathf.Clamp(transform.position.x + (transform.forward.x * Speed * Time.deltaTime), -8f, 8f),
                        transform.position.y,
                        transform.position.z + (transform.forward.z * Speed * Time.deltaTime));
                transform.position = newPosition;
            }
        }

        public void Die()
        {
            IsAlive = false;
            string soundName = "Human Death " + Random.Range(0, 3);
            AudioManager.Instance.Play(soundName);
            BloodParticleSystem.Stop();
            BloodParticleSystem.Play();
            Destroy(transform.GetChild(0).gameObject);
        }
    }
}