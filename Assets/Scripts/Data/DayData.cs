using UnityEngine;

[CreateAssetMenu(menuName ="Scriptable Objects/DayData")]
public class DayData : ScriptableObject
{
    public int dayNumber; // ����������
    public Sprite dayTitle; // ��¥ ǥ�� �̹���

    public NPCData[] visitNpcs; //���� NPC ������
}
