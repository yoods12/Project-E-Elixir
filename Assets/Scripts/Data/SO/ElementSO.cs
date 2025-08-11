using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Globalization;

[CreateAssetMenu(menuName = "Scriptable Objects/ElementSO")]
public class ElementSO : ScriptableObject
{
    public string symbol; // 원소 기호
    public Sprite elementIcon; // UI
    public int id; // 내부 식별자
    public string displayName;

    public string koreaName; // 한글 이름

    public int dictionaryId; // 사전에서의 ID
    public List<string> description; // 원소 설명
    public Sprite descriptionImage;
}
