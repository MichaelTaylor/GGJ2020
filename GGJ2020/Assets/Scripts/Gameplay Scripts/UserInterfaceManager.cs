﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PoolSystems;

public class UserInterfaceManager : MonoBehaviour
{
    [Header("Health Variables")]
    [SerializeField]
    private Image _healthBar;

    [SerializeField]
    private Image _healthBorder;

    private float _borderValue;
    private float _initialBorderValue;

    private RectTransform _borderTransform;

    [Header("Quote Variables")]
    [SerializeField]
    private QuotePool _quotePool;

    [SerializeField]
    private Text _quoteText;

    [Range(1f, 5f)]
    [SerializeField]
    private float _quoteTimeLength;
    private float _quoteTimer;

    [Header("Fade Image Variables")]
    [SerializeField]
    private Image _fadeImage;

    [SerializeField]
    private Animator _fadeAnim;

    private List<ComponentFader> _componentFaderList = new List<ComponentFader>();

    private void Start()
    {
        _borderTransform = _healthBorder.GetComponent<RectTransform>();

        if (_borderTransform != null)
        {
            _borderValue = _borderTransform.sizeDelta.x;
            _initialBorderValue = _borderValue;
        }
        
    }

    private void Update()
    {
        if (_borderTransform != null)
        {
            _borderTransform.sizeDelta = new Vector2(_borderValue, _borderTransform.sizeDelta.y);
        }
    }

    public void UpdateBorderValue(float multiplyer)
    {
        float value = _initialBorderValue * multiplyer;
        _borderValue = Mathf.Lerp(_borderValue, value, 2f * Time.deltaTime);
    }

    public void FadeIn()
    {
        _fadeAnim.SetTrigger("FadeIn");
    }

    public void FadeOut()
    {
        _fadeAnim.SetTrigger("FadeOut");
    }

    public void SetHealthBarValue(float value)
    {
        _healthBar.fillAmount = value;
    }

    public void SetQuoteText()
    {
        if (_quotePool.quoteData.Count > 0)
        {
            int quoteIndex = Random.Range(0, _quotePool.quoteData.Count);
            string quote = _quotePool.quoteData[quoteIndex].quote;

            _quoteText.text = quote;

            _quotePool.quoteData.RemoveAt(quoteIndex);
            _quoteTimer += Time.time;
            StartCoroutine(ResetText());
        }
    }

    private IEnumerator ResetText()
    {
        yield return new WaitForSeconds(_quoteTimeLength);

        if (Time.time >= _quoteTimer)
        {
            _quoteText.text = "";
        }
    }

    public void ReportComponentFader(ComponentFader fader)
    {
        if (!_componentFaderList.Contains(fader))
        {
            _componentFaderList.Add(fader);
        }
    }
}
