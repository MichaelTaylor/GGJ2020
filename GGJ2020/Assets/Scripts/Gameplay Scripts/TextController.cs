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

    private Text _textComponent;

    private void Start()
    {
        _textComponent = GetComponent<Text>();
    }

    public void StartSentence(string sentence)
    {
        _currentSentence = sentence;
        StartCoroutine(TypeSentence(_currentSentence));
    }

    private IEnumerator TypeSentence(string sentence)
    {
        _textComponent.text = "";

        foreach(char letter in sentence.ToCharArray())
        {
            _textComponent.text += letter;
            yield return new WaitForSeconds(_scrollSpeed);
        }

        StartCoroutine(ResetText());
    }

    private IEnumerator ResetText()
    {
        yield return new WaitForSeconds(3f);

        _textComponent.text = "";
    }
}
