using UnityEngine;

namespace Asteroids
{
    public class Moving : MonoBehaviour
    {
        PlayerController player;

        private void Awake()
        {
            player = FindObjectOfType<PlayerController>();
        }

        void Update()
        {
            Move();
        }

        private void Move()
        {
            float deltaX = player.Velocity.x * 10 * Time.deltaTime;
            float deltaY = player.Velocity.y * 10 * Time.deltaTime;
            Vector3 newPosition = new Vector3(transform.position.x + deltaX, transform.position.y + deltaY, transform.position.z);
            transform.SetPositionAndRotation(newPosition, transform.rotation);
        }
    }
}
