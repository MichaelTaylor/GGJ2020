using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLogic : MonoBehaviour
{
    [SerializeField]
    private QuoteData _finalQuote;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(GameManager.instance.ActivateFinishTriggerFade(_finalQuote.quote));
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, "Finish Trigger Gizmo.png", true);
    }
}
