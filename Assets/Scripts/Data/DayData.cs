using UnityEngine;

[CreateAssetMenu(menuName ="Scriptable Objects/DayData")]
public class DayData : ScriptableObject
{
    public int dayNumber; // 몇일차인지
    public Sprite dayTitle; // 날짜 표시 이미지

    public NPCData[] visitNpcs; //등장 NPC 프리팹
}
