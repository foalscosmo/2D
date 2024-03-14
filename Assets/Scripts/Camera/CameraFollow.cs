using Save_Load;
using UnityEngine;

// Namespace for camera-related functionality
namespace Camera
{
    // Class responsible for making the camera follow a target
    public class CameraFollow : MonoBehaviour, IDataPersistence
    {
        // Reference to the target object to follow
        public Transform target;

        // Smoothing factor for camera movement
        public float smoothSpeed = 0.125f;

        // Update is called once per frame
        private void Update()
        {
            // Check if target is null, if so, exit early
            if (target is null) return;

            // Calculate the desired position for the camera
            var position = target.position;
            var currentPosition = transform.position;
            var desiredPosition = new Vector3(position.x, position.y, currentPosition.z);
            
            // Smoothly move the camera towards the desired position
            var smoothedPosition = Vector3.Lerp(currentPosition, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }

        // Method to load camera data from a saved game state
        public void LoadData(GameData data)
        {
            // Set the camera's position based on the loaded data
            transform.position = data.cameraPos;
        }

        // Method to save camera data to a game state
        public void SaveData(ref GameData data)
        {
            // Save the camera's position to the game data
            data.cameraPos = transform.position;
        }
    }
}
