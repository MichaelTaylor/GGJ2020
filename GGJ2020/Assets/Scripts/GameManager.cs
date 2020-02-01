using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private float _postProcessFadeValue;

    [Range(0f, 0.1f)]
    [SerializeField]
    private float _postProcessFadeRate;

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

    public void UpdateFadeValue(float playerHealth)
    {
        CameraRenderTexture camTexture = Camera.main.GetComponent<CameraRenderTexture>();

        if (camTexture != null)
        {
            camTexture.SetFadeValue(playerHealth);
        }
    }
}
