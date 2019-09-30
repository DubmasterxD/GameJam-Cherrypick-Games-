using UnityEngine;

namespace Asteroids
{
    public class Pooling : MonoBehaviour
    {
        [SerializeField] float playSpaceEdge = 5.07f;

        private void Update()
        {
            StayInsidePlaySpace();
        }

        private void StayInsidePlaySpace()
        {
            if (transform.position.x > playSpaceEdge)
            {
                transform.SetPositionAndRotation(new Vector3(-playSpaceEdge, transform.position.y, 0), transform.rotation);
            }
            else if (transform.position.x < -playSpaceEdge)
            {
                transform.SetPositionAndRotation(new Vector3(playSpaceEdge, transform.position.y, 0), transform.rotation);
            }
            if (transform.position.y > playSpaceEdge)
            {
                transform.SetPositionAndRotation(new Vector3(transform.position.x, -playSpaceEdge, 0), transform.rotation);
            }
            else if (transform.position.y < -playSpaceEdge)
            {
                transform.SetPositionAndRotation(new Vector3(transform.position.x, playSpaceEdge, 0), transform.rotation);
            }
        }
    }
}
