
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ToHellAndBack
{
    public class PlayerController : StaticInstance<PlayerController>
    {
        public GameObject ZombieCharacterModel;
        public float Speed;
        public bool isTakingInput;
        public bool isBeingMoved;

        private float _movementValue;

        private void Update()
        {
            if (isTakingInput)
            {
                Vector3 newPosition = new Vector3(
                    Mathf.Clamp(transform.position.x + (_movementValue * Speed * Time.deltaTime), -8f, 8f),
                    transform.position.y,
                    transform.position.z);
                transform.position = newPosition;
            }
            else if (isBeingMoved)
            {
                Vector3 newPosition = new Vector3(
                    transform.position.x,
                    transform.position.y,
                    Mathf.Clamp(transform.position.z + (RoadManager.Instance.Speed * Time.deltaTime), 0f, 17f));
                transform.position = newPosition;
                if (transform.position.z >= 17f)
                {
                    isBeingMoved = false;
                    AudioManager.Instance.Play("Zombie Roar");
                    Destroy(transform.GetChild(0).gameObject);
                    Instantiate(ZombieCharacterModel, transform);
                    transform.LookAt(new Vector3(transform.position.x, transform.position.y, 0f));
                    isTakingInput = true;
                    EventManager.Instance.PlayerZombieStart();
                }
            }
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            _movementValue = context.ReadValue<float>();
        }

        private void GetCaught()
        {
            isTakingInput = false;
            string soundName = "Human Death " + UnityEngine.Random.Range(0, 3);
            AudioManager.Instance.Play(soundName);
            transform.GetChild(0).GetComponent<Animator>().SetTrigger("Fall_Trigger");
            EventManager.Instance.OnPlayerCaught();
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.CompareTag("Zombie"))
            {
                GetCaught();
            }

            if (collider.CompareTag("ZombieHorde"))
            {
                isBeingMoved = true;
            }

            if (collider.CompareTag("Human"))
            {
                EventManager.Instance.PlayerEatHuman();
                collider.GetComponent<HumanController>().Die();
            }
        }
    }
}
