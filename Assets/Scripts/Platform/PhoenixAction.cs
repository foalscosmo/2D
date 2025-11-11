using System;
using System.Collections;
using Player;
using Player.PlayerMovement;
using UnityEngine;

namespace Platform
{
    public class PhoenixAction : MonoBehaviour
    {
        [SerializeField] private CharacterDetection characterDetection;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private PlayerSounds  playerSounds;


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
            playerSounds.CheckPointSound();
        }

        private void Respawn()
        {
            playerSounds.DeathSound();
            StartCoroutine(RespawnCoroutine());
        }

        private IEnumerator RespawnCoroutine()
        {
            yield return new WaitForSecondsRealtime(0.5f);
            transform.position = spawnPoint.position;
        }
    }
}