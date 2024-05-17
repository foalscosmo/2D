using UnityEngine;

namespace Character.CharacterScriptable
{
    [CreateAssetMenu(menuName = "Create BotStats", fileName = "BotStats", order = 0)]
    public class BotStats : ScriptableObject
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private float rotateSpeed;
        [SerializeField] private Vector3 moveDirection;
        [SerializeField] private float smoothTime;
        [SerializeField] private bool isRunning;
        [SerializeField] private bool isRotating;
        private Vector3 previousMoveDirection = Vector3.zero;

        public Vector3 PreviousMoveDirection
        {
            get => previousMoveDirection;
            set => previousMoveDirection = value;
        }
        public bool IsRotating
        {
            get => isRotating;
            set => isRotating = value;
        }
        public bool IsRunning
        {
            get => isRunning;
            set => isRunning = value;
        }
        public float VelocityX { get; set; }

        public float MoveSpeed
        {
            get => moveSpeed;
            set => moveSpeed = value;
        }

        public float RotationSpeed
        {
            get => rotateSpeed;
            set => rotateSpeed = value;
        }

        public Vector3 MoveDirection
        {
            get => moveDirection;
            set => moveDirection = value;
        }

        public float SmoothTime
        {
            get => smoothTime;
            set => smoothTime = value;
        }
    }
}
