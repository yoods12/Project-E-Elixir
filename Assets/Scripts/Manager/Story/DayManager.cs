using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DayManager : MonoBehaviour
{
    public static DayManager Instance { get; private set; }

    [Header("QuestData SO ����Ʈ (1~3����, �� 32��)")]
    [SerializeField] public QuestData[] allQuests;

    [SerializeField] private int dailyQuestCount = 3;
    [SerializeField] private float completedWeight = 0.05f;
    [SerializeField] private float uncompletedWeight = 2f;

    [SerializeField] private List<QuestData> level1MandatoryQuests; // ���� 1 ����Ʈ
    [SerializeField] private List<QuestData> level2MandatoryQuests; // ���� 2 ����Ʈ
    //[SerializeField] private List<QuestData> levelMandatory3Quests; // ���� 3 ����Ʈ

    private int currentday = 1; // ���� ��¥ (1�� ����)
    private List<QuestData> todaysQuests;
    private Dictionary<QuestData, bool> todaysResults;
    private int currentQuestIndex;
    public int currentLevel { get; private set; } = 1;
    public event Action<int> OnLevelChanged;
    private void Awake()
    {
        // �̱��� ����
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
    /// �� ����: ���� ����, ����Ʈ Ŭ���� �÷��� ����, 1�������� ����
    /// </summary>
    public void StartNewGame()
    {
        // 1) ���� ������ ����
        SaveManager.Instance.DeleteSave();
        // 2) SO�� isCompleted �÷��� ��� �ʱ�ȭ
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
    /// �̾��ϱ�: ����� ������ ����
    /// </summary>
    public void StartContinue(int savedLevel, int day)
    {
        currentday = day;
        currentLevel = savedLevel;
        SaveManager.Instance.ApplyCompletedToSO(allQuests); // �� �߰�
        BeginDay();
        SceneLoader.Instance.LoadChemistryScene();
        FindObjectOfType<BGMPlayer>().PlayBGM(1);

    }

    private void BeginDay()
    {
        SaveManager.Instance.ApplyCompletedToSO(allQuests);

        // 1) ������ ����Ʈ �̱�
        var candidates = new List<QuestData>();
        foreach (var q in allQuests)
            if (q.level <= currentLevel) candidates.Add(q);

        todaysQuests = new List<QuestData>();
        for (int i = 0; i < Mathf.Min(dailyQuestCount, candidates.Count); i++)
        {
            // ����ġ ���ø�
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

        // 2) ��� ��Ͽ� ���� �ʱ�ȭ
        todaysResults = new Dictionary<QuestData, bool>();
        foreach (var q in todaysQuests)
            todaysResults[q] = false;

        currentQuestIndex = 0;
    }

    /// <summary>
    /// ChemistryUI.Start ���� �ٷ� �����ϰ� ȣ���� �� �ֵ��� guard �߰�
    /// </summary>
    public QuestData GetCurrentQuest()
    {
        // ���� Start���� ������ �ʾҴٸ�(�׽�Ʈ������ Chemistry ���� �÷������� ��)
        if (todaysQuests == null || todaysQuests.Count == 0)
            BeginDay();

        // �ε��� ����
        if (currentQuestIndex < 0 || currentQuestIndex >= todaysQuests.Count)
            currentQuestIndex = 0;

        return todaysQuests[currentQuestIndex];
    }

    /// <summary>
    /// Crafting ������ �ռ� �õ� �� ȣ��
    /// </summary>
    public void OnQuestAttempted(bool success)
    {
        var q = GetCurrentQuest();
        todaysResults[q] = todaysResults[q] || success;

        currentQuestIndex++;
        if (currentQuestIndex < todaysQuests.Count)
        {
            Debug.Log($"����Ʈ {currentQuestIndex}/{todaysQuests.Count} �Ϸ�: {success}");
        }
        else
        {   
            Debug.Log("������ ����Ʈ�� ��� �Ϸ�Ǿ����ϴ�! ��� ������ �Ѿ�ϴ�.");
            SceneLoader.Instance.LoadResultScene(); // ��� ������ �̵�
            FindObjectOfType<BGMPlayer>().PlayBGM(2);
        }
    }

    // ��� ������ �����͸� ������ ��
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
                SetLevel(2); // �� 2�� ���
                Debug.Log("���� 1 �Ϸ�! ���� 2��.");
            }
        }
        else if (currentLevel == 2)
        {
            bool allCleared = true;
            foreach (var q in level2MandatoryQuests)
                if (!q.isCompleted) { allCleared = false; break; }

            if (allCleared)
            {
                SetLevel(3); // �� ���� �ڵ� ���� ����: 2�� �ƴ϶� 3
                Debug.Log("���� 2 �Ϸ�! ���� 3��.");
            }
        }
    }



    /// <summary>
    /// ��� ���� Next Day ��ư���� ȣ��
    /// </summary>
    public void OnNextDay()
    {
        for (int i = 0; i < todaysQuests.Count; i++)
        {
            var q = todaysQuests[i];
            bool completed = todaysResults[q];

            // 1) SO�� isCompleted�� ������ true�� �ݿ�
            if (completed && !q.isCompleted)
                q.isCompleted = true;

            // 2) ����
            SaveManager.Instance.SaveCompleted(q, completed);

            // 3) ���� ���
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
