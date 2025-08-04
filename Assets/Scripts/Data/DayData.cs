using UnityEngine;

[CreateAssetMenu(menuName ="Scriptable Objects/DayData")]
public class DayData : ScriptableObject
{
    public int dayNumber; // 몇일차인지
    public Sprite dayTitle; // 날짜 표시 이미지
    public QuestData[] quests; // 해당 날짜에 진행할 퀘스트들
}