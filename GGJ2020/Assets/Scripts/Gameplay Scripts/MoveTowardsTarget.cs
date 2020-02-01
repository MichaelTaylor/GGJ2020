using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsTarget : MonoBehaviour
{

    [SerializeField]
    private bool _destroySelf;

    [Header("Follow Variables")]
    [Range(1f, 10f)]
    [SerializeField]
    private float _followSpeed;

    [Range(1f, 20f)]
    [SerializeField]
    private float _distanceThreshold;
    private float _dist;

    private Transform _target;

    private void GetTarget()
    {
        if (GameManager.instance != null)
        {
            if (_target == null)
            {
                if (GameManager.instance.player != null)
                {
                    _target = GameManager.instance.player.transform;
                }
            }
        }
    }

    private void Update()
    {
        GetTarget();

        if (_target != null)
        {
            _dist = Vector3.Distance(transform.position, _target.position);

            if (_dist <= _distanceThreshold)
            {
                FollowTarget();
            }
        }
    }

    private void FollowTarget()
    { 
        transform.position = Vector3.MoveTowards(transform.position, _target.position, _followSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (_destroySelf)
            {
                Destroy(gameObject);
            }            
        }

        if (collision.CompareTag("RestoreZone"))
        {
            Destroy(gameObject);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        UnityEditor.Handles.color = Color.red;
        UnityEditor.Handles.DrawWireDisc(transform.position, new Vector3(0, 0, 1), _distanceThreshold);
    }
#endif
}
