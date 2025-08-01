using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class NPCData : MonoBehaviour
{
    public int id;
    public string npcName;
    public Sprite portrait; // 초상화
    public List<MoleculeSO> quest; // 퀘스트에 필요한 분자들

    public string GetTalkLine(int talkIndex) //talk Manager에서 npc대사 가져오기
    {
        return TalkManager.Instance.GetTalk(id, talkIndex);
    }


}
