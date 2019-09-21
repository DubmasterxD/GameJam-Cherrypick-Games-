using UnityEngine;

namespace Asteroids
{
    public class Buffs : MonoBehaviour
    {
        enum Bonuses { Plus, ASBoost, Destroy };
        [SerializeField] Bonuses bonus;
        [SerializeField] float playSpaceEdge = 5.07f;

        Stats stats;

        private void Awake()
        {
            stats = FindObjectOfType<Stats>();
        }

        private void Update()
        {
            StayInsidePlaySpace();
        }

        private void StayInsidePlaySpace() //TODO cleanup
        {
            if (transform.position.x > playSpaceEdge)
            {
                transform.SetPositionAndRotation(new Vector3(-playSpaceEdge, transform.position.y, 0), transform.rotation);
            }
            if (transform.position.x < -playSpaceEdge)
            {
                transform.SetPositionAndRotation(new Vector3(playSpaceEdge, transform.position.y, 0), transform.rotation);
            }
            if (transform.position.y > playSpaceEdge && transform.position.y < FindObjectOfType<SpawningBuffs>().waitPlace.y - 1)
            {
                transform.SetPositionAndRotation(new Vector3(transform.position.x, -playSpaceEdge, 0), transform.rotation);
            }
            if (transform.position.y > FindObjectOfType<SpawningBuffs>().waitPlace.y - 1 && transform.position.y < FindObjectOfType<SpawningBuffs>().waitPlace.y)
            {
                transform.SetPositionAndRotation(new Vector3(transform.position.x, FindObjectOfType<SpawningBuffs>().waitPlace.y + 5, 0), Quaternion.identity);
            }
            if (transform.position.y < -playSpaceEdge)
            {
                transform.SetPositionAndRotation(new Vector3(transform.position.x, playSpaceEdge, 0), transform.rotation);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                GiveBonus();
            }
        }

        private void GiveBonus()
        {
            ReturnToPool();
            switch (bonus)
            {
                case Bonuses.Plus:
                    stats.AddLife();
                    break;
                case Bonuses.ASBoost:
                    stats.IncreaseAttackSpeed(7 / 8f);
                    break;
                case Bonuses.Destroy:
                    Bonus3Action();
                    break;
                default:
                    break;
            }
        }

        private void Bonus3Action() //TODO
        {
            for (int i = 0; i < 100 / FindObjectOfType<SpawningObstacles>().activeRate; i++)
            {
                stats.AddPoint();
            }
        }

        private void ReturnToPool()
        {
            transform.SetPositionAndRotation(FindObjectOfType<SpawningBuffs>().waitPlace, transform.rotation); //TODO
        }
    }
}
