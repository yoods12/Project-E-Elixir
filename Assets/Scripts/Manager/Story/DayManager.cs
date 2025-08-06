using System.Collections.Generic;
using UnityEngine;

public class DayManager : MonoBehaviour
{
    public static DayManager Instance { get; private set; }

    [Header("QuestData SO ����Ʈ (1~3����, �� 32��)")]
    [SerializeField] private QuestData[] allQuests;
    [SerializeField] private int dailyQuestCount = 3;
    [SerializeField] private float completedWeight = 0.2f;
    [SerializeField] private float uncompletedWeight = 1f;

    [SerializeField] private List<QuestData> level1MandatoryQuests; // ���� 1 ����Ʈ
    [SerializeField] private List<QuestData> level2MandatoryQuests; // ���� 2 ����Ʈ
    //[SerializeField] private List<QuestData> levelMandatory3Quests; // ���� 3 ����Ʈ

    private int currentLevel = 1; // ���� ���� (1�� ����)

    private List<QuestData> todaysQuests;
    private Dictionary<QuestData, bool> todaysResults;
    private int currentQuestIndex;

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

        currentLevel = 1;
        SaveManager.Instance.SaveLevel(currentLevel);

        BeginDay();
        SceneLoader.Instance.LoadChemistryScene();
    }

    /// <summary>
    /// �̾��ϱ�: ����� ������ ����
    /// </summary>
    public void StartContinue(int savedLevel)
    {
        currentLevel = savedLevel;
        BeginDay();
        SceneLoader.Instance.LoadChemistryScene();
    }

    private void BeginDay()
    {
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
        }
    }

    // ��� ������ �����͸� ������ ��
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
                Debug.Log("���� 1 ����Ʈ ��� �Ϸ�! ���� 2�� �Ѿ�ϴ�.");
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
                Debug.Log("���� 2 ����Ʈ ��� �Ϸ�! ���� 3�� �Ѿ�ϴ�.");
            }
        }
    }
    /// <summary>
    /// ��� ���� Next Day ��ư���� ȣ��
    /// </summary>
    public void OnNextDay()
    {
        // ������ üũ
        LevelCheck();

        // ������ �ö����� ����
        SaveManager.Instance.SaveLevel(currentLevel);

        // ���� ��
        BeginDay();
        SceneLoader.Instance.LoadChemistryScene();
    }
}
