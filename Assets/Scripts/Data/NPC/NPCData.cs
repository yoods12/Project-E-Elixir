using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class NPCData : MonoBehaviour
{
    public int id;
    public string npcName;
    public Sprite portrait; // �ʻ�ȭ
    public MoleculeSO requiredMolecule;

    public string GetTalkLine(int talkIndex) //talk Manager���� npc��� ��������
    {
        return TalkManager.Instance.GetTalk(id, talkIndex);
    }


}
