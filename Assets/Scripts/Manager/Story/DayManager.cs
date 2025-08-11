using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DayManager : MonoBehaviour
{
    public static DayManager Instance { get; private set; }

    [Header("QuestData SO 리스트 (1~3레벨, 총 32개)")]
    [SerializeField] public QuestData[] allQuests;

    [SerializeField] private int dailyQuestCount = 3;
    [SerializeField] private float completedWeight = 0.05f;
    [SerializeField] private float uncompletedWeight = 2f;

    [SerializeField] private List<QuestData> level1MandatoryQuests; // 레벨 1 퀘스트
    [SerializeField] private List<QuestData> level2MandatoryQuests; // 레벨 2 퀘스트
    //[SerializeField] private List<QuestData> levelMandatory3Quests; // 레벨 3 퀘스트

    private int currentday = 1; // 현재 날짜 (1로 시작)
    private List<QuestData> todaysQuests;
    private Dictionary<QuestData, bool> todaysResults;
    private int currentQuestIndex;
    public int currentLevel { get; private set; } = 1;
    public event Action<int> OnLevelChanged;
    private void Awake()
    {
        // 싱글톤 설정
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += (_, __) => BroadcastLevel();
        FindObjectOfType<BGMPlayer>().PlayBGM(0);
    }

    /// <summary>
    /// 새 게임: 저장 삭제, 퀘스트 클리어 플래그 리셋, 1레벨부터 시작
    /// </summary>
    public void StartNewGame()
    {
        // 1) 저장 데이터 삭제
        SaveManager.Instance.DeleteSave();
        // 2) SO상 isCompleted 플래그 모두 초기화
        SaveManager.Instance.ResetAllQuests(allQuests);

        DictionaryManager.Instance.ResetDictionary();
        currentday = 1;
        currentLevel = 1;

        foreach (var go in GameObject.FindGameObjectsWithTag("Level2Element"))
            go.SetActive(false);
        foreach (var go in GameObject.FindGameObjectsWithTag("Level3Element"))
            go.SetActive(false);

        SaveManager.Instance.SaveLevel(currentLevel);
        SaveManager.Instance.SaveDay(currentday);

        BeginDay();
        SceneLoader.Instance.LoadChemistryScene();

        FindObjectOfType<BGMPlayer>().PlayBGM(1);
    }

    /// <summary>
    /// 이어하기: 저장된 레벨로 시작
    /// </summary>
    public void StartContinue(int savedLevel, int day)
    {
        currentday = day;
        currentLevel = savedLevel;
        SaveManager.Instance.ApplyCompletedToSO(allQuests); // ← 추가
        BeginDay();
        SceneLoader.Instance.LoadChemistryScene();
        FindObjectOfType<BGMPlayer>().PlayBGM(1);

    }

    private void BeginDay()
    {
        SaveManager.Instance.ApplyCompletedToSO(allQuests);

        // 1) 오늘의 퀘스트 뽑기
        var candidates = new List<QuestData>();
        foreach (var q in allQuests)
            if (q.level <= currentLevel) candidates.Add(q);

        todaysQuests = new List<QuestData>();
        for (int i = 0; i < Mathf.Min(dailyQuestCount, candidates.Count); i++)
        {
            // 가중치 샘플링
            float totalW = 0;
            foreach (var c in candidates)
                totalW += c.isCompleted ? completedWeight : uncompletedWeight;

            float r = UnityEngine.Random.value * totalW;
            float acc = 0;
            int sel = 0;
            for (int j = 0; j < candidates.Count; j++)
            {
                acc += candidates[j].isCompleted ? completedWeight : uncompletedWeight;
                if (acc >= r) { sel = j; break; }
            }

            todaysQuests.Add(candidates[sel]);
            candidates.RemoveAt(sel);
        }

        // 2) 결과 기록용 사전 초기화
        todaysResults = new Dictionary<QuestData, bool>();
        foreach (var q in todaysQuests)
            todaysResults[q] = false;

        currentQuestIndex = 0;
    }

    /// <summary>
    /// ChemistryUI.Start 에서 바로 안전하게 호출할 수 있도록 guard 추가
    /// </summary>
    public QuestData GetCurrentQuest()
    {
        // 만약 Start에서 뽑히지 않았다면(테스트용으로 Chemistry 씬만 플레이했을 때)
        if (todaysQuests == null || todaysQuests.Count == 0)
            BeginDay();

        // 인덱스 보정
        if (currentQuestIndex < 0 || currentQuestIndex >= todaysQuests.Count)
            currentQuestIndex = 0;

        return todaysQuests[currentQuestIndex];
    }

    /// <summary>
    /// Crafting 씬에서 합성 시도 후 호출
    /// </summary>
    public void OnQuestAttempted(bool success)
    {
        var q = GetCurrentQuest();
        todaysResults[q] = todaysResults[q] || success;

        currentQuestIndex++;
        if (currentQuestIndex < todaysQuests.Count)
        {
            Debug.Log($"퀘스트 {currentQuestIndex}/{todaysQuests.Count} 완료: {success}");
        }
        else
        {   
            Debug.Log("오늘의 퀘스트가 모두 완료되었습니다! 결과 씬으로 넘어갑니다.");
            SceneLoader.Instance.LoadResultScene(); // 결과 씬으로 이동
            FindObjectOfType<BGMPlayer>().PlayBGM(2);
        }
    }

    // 결과 씬에서 데이터를 가져갈 때
    public List<QuestData> GetTodaysQuests() => new List<QuestData>(todaysQuests);
    public bool GetResult(QuestData q) => todaysResults[q];
    private void OnDestroy()
    {
        if (Instance == this)
            SceneManager.sceneLoaded -= (_, __) => BroadcastLevel();
    }

    private void SetLevel(int newLevel)
    {
        if (newLevel == currentLevel) return;
        currentLevel = newLevel;
        SaveManager.Instance.SaveLevel(currentLevel);
        BroadcastLevel();
    }
    private void BroadcastLevel()
    {
        OnLevelChanged?.Invoke(currentLevel);
    }
    public void LevelCheck()
    {
        if (currentLevel == 1)
        {
            bool allCleared = true;
            foreach (var q in level1MandatoryQuests)
                if (!q.isCompleted) { allCleared = false; break; }

            if (allCleared)
            {
                SetLevel(2); // ← 2로 상승
                Debug.Log("레벨 1 완료! 레벨 2로.");
            }
        }
        else if (currentLevel == 2)
        {
            bool allCleared = true;
            foreach (var q in level2MandatoryQuests)
                if (!q.isCompleted) { allCleared = false; break; }

            if (allCleared)
            {
                SetLevel(3); // ← 기존 코드 버그 수정: 2가 아니라 3
                Debug.Log("레벨 2 완료! 레벨 3로.");
            }
        }
    }



    /// <summary>
    /// 결과 씬의 Next Day 버튼에서 호출
    /// </summary>
    public void OnNextDay()
    {
        for (int i = 0; i < todaysQuests.Count; i++)
        {
            var q = todaysQuests[i];
            bool completed = todaysResults[q];

            // 1) SO의 isCompleted도 실제로 true로 반영
            if (completed && !q.isCompleted)
                q.isCompleted = true;

            // 2) 저장
            SaveManager.Instance.SaveCompleted(q, completed);

            // 3) 사전 언락
            if (completed)
                DictionaryManager.Instance.UnlockEntriesForQuest(q);
        }

        currentday++;
        LevelCheck();
        SaveManager.Instance.SaveLevel(currentLevel);
        SaveManager.Instance.SaveDay(currentday);

        BeginDay();
        SceneLoader.Instance.LoadChemistryScene();
        FindObjectOfType<BGMPlayer>().PlayBGM(1);
    }

}
