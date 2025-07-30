using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/ElementSO")]
public class ElementSO : ScriptableObject
{
    public string symbol; // ���� ��ȣ
    public Sprite icon; // UI
    public int id; // ���� �ĺ���
    public string displayName;
}
