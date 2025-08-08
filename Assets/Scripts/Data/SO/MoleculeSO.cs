using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MoleculeSO", menuName = "Scriptable Objects/MoleculeSO")]
public class MoleculeSO : ScriptableObject
{
    public string formula; // ȭ�н�
    public Sprite moleculeIcon; // UI ������
    public string displayName;

    public string koreaName; // �ѱ� �̸�

    public int dictionaryId; // ���������� ID
    public List<string> description; 
}
