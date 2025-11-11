using System;
using System.Collections;
using Player.PlayerMovement;
using UnityEngine;

namespace Platform
{
    public class PhoenixAction : MonoBehaviour
    {
        [SerializeField] private CharacterDetection characterDetection;
        [SerializeField] private Transform spawnPoint;


        private void OnEnable()
        {
            characterDetection.OnCheckPoint += UpdateSpawnPoint;
            characterDetection.OnPlayerDeath += Respawn;
        }

        private void OnDisable()
        {
            characterDetection.OnCheckPoint -= UpdateSpawnPoint;
            characterDetection.OnPlayerDeath -= Respawn;
        }

        private void UpdateSpawnPoint(Vector2 point)
        {
            spawnPoint.position =  new  Vector2(point.x, point.y);
        }

        private void Respawn()
        {
            StartCoroutine(RespawnCoroutine());
        }

        private IEnumerator RespawnCoroutine()
        {
            yield return new WaitForSecondsRealtime(0.5f);
            transform.position = spawnPoint.position;
        }
        
        
    }
}