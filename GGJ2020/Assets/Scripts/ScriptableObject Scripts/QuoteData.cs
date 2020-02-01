using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quote Data", menuName = "Scriptable Objects/Quote Data")]
public class QuoteData : ScriptableObject
{
    [Multiline]
    public string quote;
}
