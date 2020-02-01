using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnPoint : MonoBehaviour
{
    [SerializeField]
    private GameObject _player;

    private void Start()
    {
        Instantiate(_player, transform.position, Quaternion.identity);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, "Player Spawn Gizmo.png", true);
    }
}
