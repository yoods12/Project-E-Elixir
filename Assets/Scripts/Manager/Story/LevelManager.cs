using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    public int currentLevel = 1; // ���� ����
    public int maxLevel = 3; // �ִ� ����

    public List<QuestData> mandatoryQuest; // �ʼ� ����Ʈ ���

    private void Awake()
    {
        // �̱��� ���� ����
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void LoadNextLevel()
    {
        if (currentLevel < maxLevel)
        {
            currentLevel++;
            // ���⿡ ���� �ε� ���� �߰�
        }
        else
        {
            Debug.Log("�ִ� ������ �����߽��ϴ�.");
        }
    }
    public void ResetLevel()
    {
        currentLevel = 1;
        // ���⿡ ���� �ʱ�ȭ ���� �߰�
    }
    public void LevelUp()
    {
        if (currentLevel < maxLevel)
        {
            currentLevel++;

        }
        else
        {
            Debug.Log("�ִ� ������ �����߽��ϴ�.");
        }
    }

    public void CheckMandatoryQuest()
    {


    }
}
