using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }
    private const string KeyLevel = "CurrentLevel";
    private const string KeyDay = "CurrentDay";
    private const string KeyCompleted = "CompletedQuest";
    private const string KeyDictionary = "UnlockedDictionary";

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public bool HasSave() => PlayerPrefs.HasKey(KeyLevel);
    public int LoadLevel() => PlayerPrefs.GetInt(KeyLevel, 1);
    public int LoadDay() => PlayerPrefs.GetInt(KeyDay, 1);

    public bool LoadCompleted(QuestData quest)
    {
        return PlayerPrefs.GetInt(KeyCompleted + quest.name, 0) == 1;
    }
    public void SaveLevel(int lvl)
    {
        PlayerPrefs.SetInt(KeyLevel, lvl);
        PlayerPrefs.Save();
    }
    public void SaveDay(int day)
    {
        PlayerPrefs.SetInt(KeyDay, day);
        PlayerPrefs.Save();
    }
    public void SaveCompleted(QuestData quest, bool isCompleted)
    {
        PlayerPrefs.SetInt(KeyCompleted + quest.name, isCompleted ? 1 : 0);
        PlayerPrefs.Save();
    }
    public void DeleteSave()
    {
        PlayerPrefs.DeleteKey(KeyLevel);
        PlayerPrefs.DeleteKey(KeyDay);
    }

    public void ResetAllQuests(QuestData[] quests)
    {
        foreach (var q in quests)
            q.isCompleted = false;
    }
}
