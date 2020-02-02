using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource _sfxSource;

    [SerializeField]
    private AudioMixer _mixer;

    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetMasterVolume(float value)
    {
        _mixer.SetFloat("MasterVol", value);
    }

    public void PlaySFX(AudioClip newClip)
    {
        if (!_sfxSource.isPlaying)
        {
            _sfxSource.clip = newClip;
            _sfxSource.Play();
        }
    }
}
