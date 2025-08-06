using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }
    private const string KeyLevel = "CurrentLevel";

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public bool HasSave() => PlayerPrefs.HasKey(KeyLevel);
    public int LoadLevel() => PlayerPrefs.GetInt(KeyLevel, 1);
    public void SaveLevel(int lvl)
    {
        PlayerPrefs.SetInt(KeyLevel, lvl);
        PlayerPrefs.Save();
    }
    public void DeleteSave() => PlayerPrefs.DeleteKey(KeyLevel);

    public void ResetAllQuests(QuestData[] quests)
    {
        foreach (var q in quests)
            q.isCompleted = false;
    }
}
