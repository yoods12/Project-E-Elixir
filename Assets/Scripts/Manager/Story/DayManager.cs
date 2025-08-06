using System.Collections.Generic;
using UnityEngine;

public class DayManager : MonoBehaviour
{
    public static DayManager Instance { get; private set; }

    [Header("QuestData SO 리스트 (1~3레벨, 총 32개)")]
    [SerializeField] private QuestData[] allQuests;
    [SerializeField] private int dailyQuestCount = 3;
    [SerializeField] private float completedWeight = 0.2f;
    [SerializeField] private float uncompletedWeight = 1f;

    [SerializeField] private List<QuestData> level1MandatoryQuests; // 레벨 1 퀘스트
    [SerializeField] private List<QuestData> level2MandatoryQuests; // 레벨 2 퀘스트
    //[SerializeField] private List<QuestData> levelMandatory3Quests; // 레벨 3 퀘스트

    private int currentLevel = 1; // 현재 레벨 (1로 시작)

    private List<QuestData> todaysQuests;
    private Dictionary<QuestData, bool> todaysResults;
    private int currentQuestIndex;

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

        currentLevel = 1;
        SaveManager.Instance.SaveLevel(currentLevel);

        BeginDay();
        SceneLoader.Instance.LoadChemistryScene();
    }

    /// <summary>
    /// 이어하기: 저장된 레벨로 시작
    /// </summary>
    public void StartContinue(int savedLevel)
    {
        currentLevel = savedLevel;
        BeginDay();
        SceneLoader.Instance.LoadChemistryScene();
    }

    private void BeginDay()
    {
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

            float r = Random.value * totalW;
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
        }
    }

    // 결과 씬에서 데이터를 가져갈 때
    public List<QuestData> GetTodaysQuests() => new List<QuestData>(todaysQuests);
    public bool GetResult(QuestData q) => todaysResults[q];

    public void LevelCheck()
    {
        if(currentLevel == 1)
        {
            bool allCleared = true;
            for (int i= 0; i < level1MandatoryQuests.Count; i++)
            {
                if(!level1MandatoryQuests[i].isCompleted)
                {
                    allCleared = false;
                    break;
                }
            }
            if(allCleared)
            {
                currentLevel = 2;
                Debug.Log("레벨 1 퀘스트 모두 완료! 레벨 2로 넘어갑니다.");
            }
        }
        else if(currentLevel == 2)
        {
            bool allCleared = true;
            for (int i = 0; i < level2MandatoryQuests.Count; i++)
            {
                if (!level2MandatoryQuests[i].isCompleted)
                {
                    allCleared = false;
                    break;
                }
            }
            if (allCleared)
            {
                currentLevel = 3;
                Debug.Log("레벨 2 퀘스트 모두 완료! 레벨 3로 넘어갑니다.");
            }
        }
    }
    /// <summary>
    /// 결과 씬의 Next Day 버튼에서 호출
    /// </summary>
    public void OnNextDay()
    {
        // 레벨업 체크
        LevelCheck();

        // 레벨이 올라갔으면 저장
        SaveManager.Instance.SaveLevel(currentLevel);

        // 다음 날
        BeginDay();
        SceneLoader.Instance.LoadChemistryScene();
    }
}
