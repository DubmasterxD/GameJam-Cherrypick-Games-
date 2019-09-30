using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    public class SpawningBuffs : MonoBehaviour
    {
        private float timeSinceLastSpawned;
        private float stayTime;
        private Vector3 minResp = new Vector3(-5, -5, 0);
        private Vector3 maxResp = new Vector3(5, 5, 0);
        public Vector3 waitPlace = new Vector3(0, 30, 0);
        public float spawnTime = 60;
        public float maxStayTime = 5;
        [SerializeField] List<Buff> buffsPrefabs;
        public GameObject plusPrefab;
        public GameObject x2Prefab;
        public GameObject destroyPrefab;
        List<Buff> buffs;

        Game game;

        private void Awake()
        {
            game = FindObjectOfType<Game>();
        }

        void Start()
        {
            buffs = new List<Buff>();
            foreach(Buff buff in buffsPrefabs)
            {
                buffs.Add(Instantiate(buff));
                buff.Deactivate();
            }
        }
        
        void Update()
        {
            timeSinceLastSpawned += Time.deltaTime;
            stayTime += Time.deltaTime;
            if (!game.isGameOver && timeSinceLastSpawned >= spawnTime)
            {
                timeSinceLastSpawned = 0;
                buffs[Random.Range(0, 3)].Activate(Random.Range(minResp[0], maxResp[0]), Random.Range(minResp[1], maxResp[1]));
                stayTime = 0;
            }
            if (!game.isGameOver && stayTime >= maxStayTime)
            {
                foreach(Buff buff in buffs)
                {
                    buff.gameObject.SetActive(false);
                }
            }
        }
    }
}
