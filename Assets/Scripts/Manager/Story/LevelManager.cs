using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    public int currentLevel = 1; // 현재 레벨
    public int maxLevel = 3; // 최대 레벨

    public List<QuestData> mandatoryQuest; // 필수 퀘스트 목록

    private void Awake()
    {
        // 싱글톤 패턴 구현
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void LoadNextLevel()
    {
        if (currentLevel < maxLevel)
        {
            currentLevel++;
            // 여기에 레벨 로딩 로직 추가
        }
        else
        {
            Debug.Log("최대 레벨에 도달했습니다.");
        }
    }
    public void ResetLevel()
    {
        currentLevel = 1;
        // 여기에 레벨 초기화 로직 추가
    }
    public void LevelUp()
    {
        if (currentLevel < maxLevel)
        {
            currentLevel++;

        }
        else
        {
            Debug.Log("최대 레벨에 도달했습니다.");
        }
    }

    public void CheckMandatoryQuest()
    {


    }
}
