using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinWaveBehaviour : MonoBehaviour
{
    [Header("Basic Variables")]
    [SerializeField]
    private Vector3 _sinDir;

    [Range(0f, 0.1f)]
    [SerializeField]
    private float _sinSpeed;

    private void Update()
    {
        transform.position += _sinDir * Mathf.Sin(Time.time * 3f) * _sinSpeed;
    }
}
