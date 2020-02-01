﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private float _postProcessFadeValue;

    [Range(0f, 0.1f)]
    [SerializeField]
    private float _postProcessFadeRate;

    private float _fadeValue;

    [Header("Other Compoenents")]
    [SerializeField]
    private UserInterfaceManager _userInteraceManager;

    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ReportComponentFader(ComponentFader fader)
    {
        _userInteraceManager.ReportComponentFader(fader);
    }

    public void UpdateFadeValue(float fadeValue)
    {
        CameraRenderTexture camTexture = Camera.main.GetComponent<CameraRenderTexture>();

        if (camTexture != null)
        {
            camTexture.SetFadeValue(fadeValue);
        }

        if (_userInteraceManager != null)
        {
            float playerHealthValue = Mathf.Abs(fadeValue - 1);
            _userInteraceManager.SetHealthBarValue(playerHealthValue);
        }

        _fadeValue = Mathf.Abs(fadeValue - 1);
    }

    public float GetFadeValue()
    {
        return _fadeValue;
    }
}
