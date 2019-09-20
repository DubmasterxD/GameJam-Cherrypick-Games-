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
        public GameObject plusPrefab;
        public GameObject x2Prefab;
        public GameObject destroyPrefab;
        private GameObject[] buffs;

        // Use this for initialization
        void Start()
        {
            buffs = new GameObject[3];
            buffs[0] = Instantiate(plusPrefab, waitPlace, Quaternion.identity);
            buffs[1] = Instantiate(x2Prefab, waitPlace, Quaternion.identity);
            buffs[2] = Instantiate(destroyPrefab, waitPlace, Quaternion.identity);
        }

        // Update is called once per frame
        void Update()
        {
            timeSinceLastSpawned += Time.deltaTime;
            stayTime += Time.deltaTime;
            if (!Game.instance.gameOver && timeSinceLastSpawned >= spawnTime)
            {
                timeSinceLastSpawned = 0;
                buffs[Random.Range(0, 3)].transform.SetPositionAndRotation(new Vector3(Random.Range(minResp[0], maxResp[0]), Random.Range(minResp[1], maxResp[1]), 0), Quaternion.identity);
                stayTime = 0;
            }
            if (!Game.instance.gameOver && stayTime >= maxStayTime)
            {
                buffs[0].transform.SetPositionAndRotation(waitPlace, Quaternion.identity);
                buffs[1].transform.SetPositionAndRotation(waitPlace, Quaternion.identity);
                buffs[2].transform.SetPositionAndRotation(waitPlace, Quaternion.identity);
            }
        }
    }
}
