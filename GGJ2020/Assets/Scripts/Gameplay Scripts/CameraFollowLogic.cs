using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowLogic : MonoBehaviour
{
    [Header("Basic Variables")]
    [SerializeField]
    private Transform _target;

    [Range(1f, 10f)]
    [SerializeField]
    private float _lerpSpeed;

    [Range(1f, 5f)]
    [SerializeField]
    private float _yPosAddition;

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
        LerpToTarget();
    }

    private void LerpToTarget()
    {
        if (_target != null)
        {
            Vector3 targetPostion = new Vector3(_target.position.x, _target.position.y + _yPosAddition, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPostion, _lerpSpeed * Time.deltaTime);
        }
    }
}
