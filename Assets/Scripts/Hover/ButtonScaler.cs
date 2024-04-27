using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Hover
{
    public class ButtonScaler : MonoBehaviour
    {
        [SerializeField] private EventSystem eventSystem;
        private GameObject lastSelectedObj;
        private bool hasChanged;

        private void Start()
        {
            lastSelectedObj = eventSystem.currentSelectedGameObject;
            lastSelectedObj.transform.DOScale(2.1f, 0.3f);
        }

        private void Update()
        {
            if (eventSystem.currentSelectedGameObject == lastSelectedObj) return;
            lastSelectedObj.transform.DOScale(2f, 0.3f);
            lastSelectedObj = eventSystem.currentSelectedGameObject;
            lastSelectedObj.transform.DOScale(2.1f, 0.3f);
        }
    }
}