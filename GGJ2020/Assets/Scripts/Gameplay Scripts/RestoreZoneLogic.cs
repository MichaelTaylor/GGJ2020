using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestoreZoneLogic : MonoBehaviour
{
    private bool _hasPlayer;
    private float _restoreTimer;

    [Range(0f, 1f)]
    [SerializeField]
    private float _restoreTimerLimit;

    [Range(0f, 5f)]
    [SerializeField]
    private float _fadeRate;

    [SerializeField]
    private Collider2D _collider;

    [SerializeField]
    private Material _currentMaterial;

    private void Start()
    {
        //_collider = GetComponent<Collider2D>();
        //_currentMaterial = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        if (_hasPlayer)
        {
            _restoreTimer += Time.deltaTime;
        }

        if (_restoreTimer >= _restoreTimerLimit)
        {
            if (_collider != null)
            {
                _collider.enabled = false;
                Color c = _currentMaterial.color;
                c.a -= _fadeRate * Time.deltaTime;
                _currentMaterial.SetFloat("_Color", c.a);
            }
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (_hasPlayer)
            {
                _hasPlayer = true;
            }
        }
    }
}
