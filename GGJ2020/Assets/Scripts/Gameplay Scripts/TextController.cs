using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour
{
    [Range(0f, 0.04f)]
    [SerializeField]
    private float _scrollSpeed;

    private string _currentSentence;
    private bool _isDisplaying;
    public bool isDispalying { get { return _isDisplaying; } }

    private Text _textComponent;

    private void Start()
    {
        _textComponent = GetComponent<Text>();
    }

    public void StartSentence(string sentence)
    {
        if (!_isDisplaying)
        {
            _currentSentence = sentence;
            StartCoroutine(TypeSentence(_currentSentence));
            _isDisplaying = true;
        }
    }

    public void StartFinalSentence(string sentence)
    {
        StopAllCoroutines();
        StartCoroutine(ResetText(0f));
        StartCoroutine(TypeSentence(sentence));
    }

    private IEnumerator TypeSentence(string sentence)
    {
        _textComponent.text = "";

        foreach(char letter in sentence.ToCharArray())
        {
            _textComponent.text += letter;
            GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(_scrollSpeed);
        }

        StartCoroutine(ResetText(3f));
    }

    private IEnumerator ResetText(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        _textComponent.text = "";
        _isDisplaying = false;
    }
}
