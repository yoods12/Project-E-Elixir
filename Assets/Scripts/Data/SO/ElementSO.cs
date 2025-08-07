using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/ElementSO")]
public class ElementSO : ScriptableObject
{
    public string symbol; // ���� ��ȣ
    public Sprite elementIcon; // UI
    public int id; // ���� �ĺ���
    public string displayName;

    public int dictionaryId; // ���������� ID
}
