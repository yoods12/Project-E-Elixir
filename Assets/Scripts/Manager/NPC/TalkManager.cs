using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    public static TalkManager Instance { get; private set; }
    Dictionary<int, string[]> talkData;

    private void Awake()
    {        // �̱���
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        talkData = new Dictionary<int, string[]>();
        GenerateData();

    }

    void GenerateData()
    {
        talkData.Add(1, new string[] { "������, ���� �� ���̴� �� ã�� �־��. ���� ���� �۰� ���⺸�� ������ �� ���� �ʳ���?" });
    }

    public string GetTalk(int id, int talkIndex)
    {
        return talkData[id][talkIndex];
    }
}
