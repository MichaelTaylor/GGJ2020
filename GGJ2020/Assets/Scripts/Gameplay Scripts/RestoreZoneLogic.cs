using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestoreZoneLogic : MonoBehaviour
{
    [SerializeField]
    private AudioClip _restoreSFX;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Time.timeSinceLevelLoad > 1f)
            {
                AudioManager.instance.PlaySFX(_restoreSFX);
            }
        }
    }
}
