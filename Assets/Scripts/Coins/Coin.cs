using DG.Tweening;
using UnityEngine;

namespace Coins
{
    public class Coin : MonoBehaviour
    {
        [SerializeField] private LayerMask playerMask;
        [SerializeField] private float rotationSpeed;
        [SerializeField] private float floatHeightMax;
        [SerializeField] private float floatHeightMin;
        [SerializeField] private float floatDuration;
    
        private Tween rotationTween;
    
        private void Start()
        {
            // Sequence for combined animations
            var sequence = DOTween.Sequence();
    
            // Rotation
            sequence.Join(transform.DORotate(new Vector3(0f, 0f, 360f), 
                    1f / rotationSpeed * 360f, RotateMode.LocalAxisAdd)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Restart));
    
            // Floating
            sequence.Join(transform.DOMoveY(transform.position.y + Random.Range(floatHeightMin, floatHeightMax), floatDuration)
                .SetEase(Ease.InOutQuad)
                .SetLoops(-1, LoopType.Yoyo));
    
            rotationTween = sequence;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if ((playerMask & (1 << other.gameObject.layer)) == 0) return;
            rotationTween?.Kill();
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            rotationTween?.Kill();
        }
    }
}
