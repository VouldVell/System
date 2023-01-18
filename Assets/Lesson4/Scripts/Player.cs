using Unity.Netcode;
using UnityEngine;


namespace System_Programming.Lesson4
{
    public class Player : NetworkBehaviour
    {
        [SerializeField] private GameObject playerPrefab;


        private void Start()
        {
            SpawnCharacter();
        }

        private void SpawnCharacter()
        {
            if (!IsServer)
            {
                return;
            }
            var spawner = FindObjectOfType<SpawnPoints>();
            if (spawner == null) Instantiate(playerPrefab).GetComponent<NetworkObject>().SpawnWithOwnership(OwnerClientId);
            else
            {
                var randomPoint = spawner.Points[Random.Range(0, spawner.Points.Length)];
                Instantiate(playerPrefab, randomPoint.position, randomPoint.rotation)
                    .GetComponent<NetworkObject>().SpawnWithOwnership(OwnerClientId);
            }
        }
    }
}