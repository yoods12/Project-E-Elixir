using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "QuestData", menuName = "Scriptable Objects/Quest")]
public class QuestData : ScriptableObject
{
    public int level; // 퀘스트 레벨
    //public int id; // 퀘스트 ID
    public string NpcName; // 퀘스트를 주는 NPC 이름
    public Sprite NpcIcon; // Npc 아이콘
    public bool isCompleted; // 퀘스트 완료 여부

    public List<string> hint; // 힌트
    public List<MoleculeSO> requiredMolecules; // 요구 분자
    public List<ElementSO> requiredElements; // 요구 원소
    //public List<ItemSO> rewards; // 보상 아이템
}
