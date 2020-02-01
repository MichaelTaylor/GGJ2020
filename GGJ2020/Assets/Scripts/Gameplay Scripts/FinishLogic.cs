using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLogic : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.instance.ActivateGameOverFade();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, "Finish Trigger Gizmo.png", true);
    }
}
