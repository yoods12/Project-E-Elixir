using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Globalization;

[CreateAssetMenu(menuName = "Scriptable Objects/ElementSO")]
public class ElementSO : ScriptableObject
{
    public string symbol; // ���� ��ȣ
    public Sprite elementIcon; // UI
    public int id; // ���� �ĺ���
    public string displayName;

    public string koreaName; // �ѱ� �̸�

    public int dictionaryId; // ���������� ID
    public List<string> description; // ���� ����
    public Sprite descriptionImage;
}
