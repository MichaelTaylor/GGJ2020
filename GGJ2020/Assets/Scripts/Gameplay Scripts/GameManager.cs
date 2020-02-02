using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private float _postProcessFadeValue;

    [Range(0f, 0.1f)]
    [SerializeField]
    private float _postProcessFadeRate;

    private PlayerScript _player;
    public PlayerScript player { get { return _player; } }

    private float _fadeValue;
    private bool _isFading;

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

    public void ReportPlayerObject(PlayerScript player)
    {
        _player = player;
    }

    public void ReportComponentFader(ComponentFader fader)
    {
        _userInteraceManager.ReportComponentFader(fader);
    }

    private void ActivateFadeOut()
    {
        _userInteraceManager.FadeOut();
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
        AudioManager.instance.SetMasterVolume(GetVolumeValue());
    }

    public float GetFadeValue()
    {
        return _fadeValue;
    }

    public float GetVolumeValue()
    {
        float value = 40f * (_fadeValue - 1);
        return value;
    }

    public void UpdateMaxHealthBorder(float maxHealthMultiplyer)
    {
        if (_userInteraceManager != null)
        {
            _userInteraceManager.UpdateBorderValue(maxHealthMultiplyer);
        }
    }

    public void SetQuoteText()
    {
        if (_userInteraceManager != null)
        {
            _userInteraceManager.SetQuoteText();
        }
    }

    public void ActivateGameOverFade()
    {
        if (_userInteraceManager != null)
        {
            if (!_isFading)
            {
                _userInteraceManager.FadeIn();
                StartCoroutine(ResetScene());
                _isFading = true;
            }
        }
    }

    private IEnumerator ResetScene()
    {
        yield return new WaitForSeconds(3f);
        
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
        ActivateFadeOut();
        _isFading = false;
    }
}
