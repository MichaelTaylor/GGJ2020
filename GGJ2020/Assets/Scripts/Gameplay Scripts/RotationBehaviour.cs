using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationBehaviour : MonoBehaviour
{
    [SerializeField]
    private Vector3 _rotDir;

    [Range(1f, 10f)]
    [SerializeField]
    private float _rotSpeed;

    void Update()
    {
        transform.eulerAngles += _rotDir * _rotSpeed * Time.deltaTime;
    }
}
