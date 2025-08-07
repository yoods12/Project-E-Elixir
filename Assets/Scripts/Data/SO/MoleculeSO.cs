using UnityEngine;

[CreateAssetMenu(fileName = "MoleculeSO", menuName = "Scriptable Objects/MoleculeSO")]
public class MoleculeSO : ScriptableObject
{
    public string formula; // 화학식
    public Sprite moleculeIcon; // UI 아이콘
    public string displayName;

    public int dictionaryId; // 사전에서의 ID
}
