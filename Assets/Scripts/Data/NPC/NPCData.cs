using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class NPCData : MonoBehaviour
{
    public int id;
    public string npcName;
    public Sprite portrait; // �ʻ�ȭ
    public List<MoleculeSO> quest; // ����Ʈ�� �ʿ��� ���ڵ�

    public string GetTalkLine(int talkIndex) //talk Manager���� npc��� ��������
    {
        return TalkManager.Instance.GetTalk(id, talkIndex);
    }


}
