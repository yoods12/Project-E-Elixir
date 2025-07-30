using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/ElementSO")]
public class ElementSO : ScriptableObject
{
    public string symbol; // 원소 기호
    public Sprite icon; // UI
    public int id; // 내부 식별자
    public string displayName;
}
