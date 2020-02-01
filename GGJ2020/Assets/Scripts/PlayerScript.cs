using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [Header("Health Variables")]
    [Range(0f, 30f)]
    [SerializeField]
    private float _health;
    private float _maxHealth;

    [Range(1f, 20f)]
    [SerializeField]
    private float _healthRestoreRate;

    [Header("Health Lerp Variables")]
    [Range(1f, 10f)]
    [SerializeField]
    private float _healthLerpSpeed;
    private bool _restoringHealth;
    private float _targetHealthValue;

    [Header("Basic Variables")]
    [Range(0f, 10f)]
    [SerializeField]
    private float _speed;

    private bool _isDead;

    private SpriteRenderer _spriteRend;
    private Rigidbody2D _rb2D;
    private Animator _anim;

    private void Start()
    {
        _maxHealth = _health;
        _targetHealthValue = _maxHealth;

        _spriteRend = GetComponent<SpriteRenderer>();
        _rb2D = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }


    private void Update()
    {
        if (!_isDead)
        {
            Movement();
        }
        
        //Jump();
        CheckHealth();
        RestoreHealth();
    }

    private void Movement()
    {
        float hor = Input.GetAxis("Horizontal");

        Vector3 mov = new Vector3(hor, _rb2D.velocity.y, 0f);

        transform.position += mov * _speed * Time.deltaTime;

        if (Mathf.Abs(hor) > 0)
        {
            if (hor > 0)
            {
                _spriteRend.flipX = false;
            }
            else
            {
                _spriteRend.flipX = true;
            }

            _anim.SetBool("IsMoving", true);
        }
        else
        {
            _anim.SetBool("IsMoving", false);
        }
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            _rb2D.AddForce(Vector3.up * 5f, ForceMode2D.Impulse);
        }
    }

    private void CheckHealth()
    {
        if (!_isDead)
        {
            _health = Mathf.Lerp(_health, _targetHealthValue, _healthLerpSpeed * Time.deltaTime);
        }
        else
        {
            _health = 0f;
            _targetHealthValue = 0;
        }
        
    }

    private void RestoreHealth()
    {
        if (_restoringHealth)
        {
            if (_health < _maxHealth)
            {
                _targetHealthValue += _healthRestoreRate * Time.deltaTime;
            }
            else
            {
                _health = _maxHealth;
            }
        }

        float fadeValue = ((_health / _maxHealth)) - 1f;

        if (GameManager.instance != null)
        {
            GameManager.instance.UpdateFadeValue(Mathf.Abs(fadeValue));
        }
        
    }

    private void IsDeadLogic()
    {
        //game over
        _isDead = true;
        _anim.SetBool("IsMoving", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("RestoreZone"))
        {
            if (!_restoringHealth)
            {
                _restoringHealth = true;
            }
        }

        if (collision.CompareTag("DamageZone"))
        {
            if (_health > 0)
            {
                _targetHealthValue -= 10f;

                if (_targetHealthValue < 0)
                {
                    IsDeadLogic();
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("RestoreZone"))
        {
            if (_restoringHealth)
            {
                _restoringHealth = false;
            }
        }
    }
}
