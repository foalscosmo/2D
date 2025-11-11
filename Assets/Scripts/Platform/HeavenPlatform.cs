using UnityEngine;

namespace Platform
{
    public class HeavenPlatform : MonoBehaviour
    {
        [SerializeField] private LayerMask player;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if ((player & (1 << other.gameObject.layer)) != 0)
                other.transform.position = new Vector2(-10f, 0f);
        }
    }
}