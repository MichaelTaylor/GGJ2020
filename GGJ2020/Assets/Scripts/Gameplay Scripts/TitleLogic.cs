using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleLogic : MonoBehaviour
{
    [SerializeField]
    private AnimationClip _introState;

    private void Start()
    {
        StartCoroutine(TranstionScene(_introState.length));
    }

    private IEnumerator TranstionScene(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene("MainLevel");
    }
}
