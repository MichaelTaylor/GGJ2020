using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [Header("Basic Variables")]
    [Range(0f, 10f)]
    [SerializeField]
    private float _speed;
    private bool _isInvincible;

    [Range(0f, 2f)]
    [SerializeField]
    private float _invincibilityLength;

    private bool _isDead;

    [Header("Health Variables")]
    [Range(0f, 30f)]
    [SerializeField]
    private float _health;
    private float _maxHealth;
    private float _initialMaxHealth;

    [Range(1f, 10f)]
    [SerializeField]
    private float _maxHealthSubtractValue;

    [Range(1f, 20f)]
    [SerializeField]
    private float _healthRestoreRate;

    [Header("Health Lerp Variables")]
    [Range(1f, 10f)]
    [SerializeField]
    private float _healthLerpSpeed;
    private bool _restoringHealth;
    private float _targetHealthValue;

    //Components
    private SpriteRenderer _spriteRend;
    private Rigidbody2D _rb2D;
    private Animator _anim;
    private Renderer _rend;

    private void Start()
    {
        _maxHealth = _health;
        _initialMaxHealth = _maxHealth;
        _targetHealthValue = _maxHealth;

        _spriteRend = GetComponent<SpriteRenderer>();
        _rb2D = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _rend = GetComponent<Renderer>();

        GameManager.instance.ReportPlayerObject(this);
    }


    private void Update()
    {
        if (!_isDead)
        {
            Movement();
        }
        
        CheckHealth();
        RestoreHealth();

        if (GameManager.instance != null)
        {
            GameManager.instance.UpdateMaxHealthBorder(_maxHealth / _initialMaxHealth);
        }
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

    private void CheckHealth()
    {
        _health = Mathf.Lerp(_health, _targetHealthValue, _healthLerpSpeed * Time.deltaTime);
        if (!_isDead)
        {
            if (_targetHealthValue <= 0)
            {
                IsDeadLogic();
            }
        }
        else
        {
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
                _targetHealthValue = _maxHealth;
            }
        }
        
        if (_rend != null)
        {
            _rend.material.SetFloat("_Threshold", FadeValue());
        }

        if (GameManager.instance != null)
        {
            GameManager.instance.UpdateFadeValue(FadeValue());
        }
        
    }

    private float FadeValue()
    {
        return Mathf.Abs(((_health / _initialMaxHealth)) - 1f);
    }

    private void DamageLogic()
    {
        if (!_isInvincible)
        {
            if (_health > 0)
            {
                _targetHealthValue -= 10f;
                SubstractMaxHealth();

                if (_targetHealthValue < 0)
                {
                    IsDeadLogic();
                }
                else
                {
                    GameManager.instance.SetQuoteText();
                    _isInvincible = true;
                    StartCoroutine(ResetInvincibility());
                }
            }
        }
    }

    private IEnumerator ResetInvincibility()
    {
        yield return new WaitForSeconds(_invincibilityLength);
        _isInvincible = false;
    }

    private void SubstractMaxHealth()
    {
        if (_maxHealth > 0f)
        {
            _maxHealth -= _maxHealthSubtractValue;
        }
    }

    private void IsDeadLogic()
    {
        //game over
        if (!_isDead)
        {
            _isDead = true;
            _anim.SetBool("IsMoving", false);
            GameManager.instance.ActivateGameOverFade();
        }
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
            DamageLogic();
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
