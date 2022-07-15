using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToHellAndBack
{
    public class RoadManager : Singleton<RoadManager>
    {
        public GameObject RoadPrefab;
        public GameObject SpawnContainer;
        public GameObject ZombiePrefab;
        public List<GameObject> ZombieModels;
        public GameObject HumanPrefab;
        public List<GameObject> HumanModels;
        public BoxCollider BackRoadCollider;
        public BoxCollider FrontRoadCollider;
        public float Speed;
        public float SpawnDelay;
        [Range(-1,1)]
        public float Direction;

        private void OnEnable()
        {
            EventManager.Instance.OnPlayerCaught += StopRoad;
            EventManager.Instance.OnPlayerZombieStart += MoveRoadUp;
            EventManager.Instance.OnGameOver += GameOver;
        }

        private void OnDisable()
        {
            if (EventManager.Instance != null)
            {
                EventManager.Instance.OnPlayerCaught -= StopRoad;
                EventManager.Instance.OnPlayerZombieStart -= MoveRoadUp;
                EventManager.Instance.OnGameOver -= GameOver;
            }
        }

        public void Update()
        {
            Vector3 positionChange = new Vector3(0f, 0f, Direction * Speed * Time.deltaTime);
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).transform.position += positionChange;
            }
            for (int i = 0; i < SpawnContainer.transform.childCount; i++)
            {
                SpawnContainer.transform.GetChild(i).transform.position += positionChange;
            }

            if (SpawnDelay > 0f && Direction != 0f)
            {
                SpawnDelay -= Time.deltaTime;
            }
            else
            {
                if (Direction == -1f)
                {
                    GameObject zombie = Instantiate(ZombiePrefab, new Vector3(Random.Range(-7f, 7f), 0.1f, 24f), Quaternion.identity, SpawnContainer.transform);
                    Instantiate(ZombieModels[Random.Range(0, ZombieModels.Count)], zombie.transform);
                }
                if (Direction == 1f)
                {
                    GameObject human = Instantiate(HumanPrefab, new Vector3(Random.Range(-7f, 7f), 0.1f, -10f), Quaternion.identity, SpawnContainer.transform);
                    Instantiate(HumanModels[Random.Range(0, HumanModels.Count)], human.transform);
                }

                SpawnDelay = Mathf.Min((60f / GameManager.Instance.CurrentRunClock), 6f);
            }
        }

        public void OnTriggerEnter(Collider collider)
        {
            if (collider.CompareTag("Road"))
            {
                Vector3 newRoadOffset = new Vector3(0f, 0f, 12f * -Direction);
                Vector3 newRoadPosition = transform.GetChild(transform.childCount - 1).transform.position + newRoadOffset;
                Instantiate(RoadPrefab, newRoadPosition, Quaternion.identity, transform);
            }

            Destroy(collider.gameObject);
        }

        private void StopRoad()
        {
            Direction = 0;

            for (int i = 0; i < transform.childCount; i++)
            {
                if (i == 0) transform.GetChild(0).transform.position = transform.GetChild(transform.childCount - 1).transform.position;
                else transform.GetChild(i).transform.position = transform.GetChild(i - 1).transform.position + new Vector3(0f, 0f, -12f);
            }
        }

        public void MoveRoadUp()
        {
            Direction = 1f;
        }

        private void GameOver()
        {
            this.enabled = false;
        }
    }
}
