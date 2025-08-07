using UnityEngine;

[CreateAssetMenu(fileName = "MoleculeSO", menuName = "Scriptable Objects/MoleculeSO")]
public class MoleculeSO : ScriptableObject
{
    public string formula; // ȭ�н�
    public Sprite moleculeIcon; // UI ������
    public string displayName;

    public int dictionaryId; // ���������� ID
}
