using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    public static TalkManager Instance { get; private set; }
    Dictionary<int, string[]> talkData;

    private void Awake()
    {        // 싱글톤
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        talkData = new Dictionary<int, string[]>();
        GenerateData();

    }

    void GenerateData()
    {
        talkData.Add(1, new string[] { "가볍고, 눈에 안 보이는 걸 찾고 있어요. 뭔가 정말 작고 공기보다 가벼운 게 있지 않나요?" });
    }

    public string GetTalk(int id, int talkIndex)
    {
        return talkData[id][talkIndex];
    }
}
