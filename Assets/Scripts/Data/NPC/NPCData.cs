using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class NPCData : MonoBehaviour
{
    public int id;
    public string npcName;
    public Sprite portrait; // 초상화
    public MoleculeSO requiredMolecule;

    public string GetTalkLine(int talkIndex) //talk Manager에서 npc대사 가져오기
    {
        return TalkManager.Instance.GetTalk(id, talkIndex);
    }


}
