using Graphics;
using UnityEngine;

namespace Managers
{
    public class GraphicsManager : MonoBehaviour
    {
        [SerializeField] private GraphicsIndex index;

        public GraphicsIndex Index => index;

        private void Awake()
        {
            ChangeGraphics();
        }

        public void ChangeGraphics()
        {
            QualitySettings.SetQualityLevel(index.Index);

        }
    }
}