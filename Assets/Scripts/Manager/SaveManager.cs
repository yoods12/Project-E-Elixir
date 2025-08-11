using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }

    private const string KeyLevel = "CurrentLevel";
    private const string KeyDay = "CurrentDay";
    private const string KeyCompleted = "CompletedQuest_";   // + (questId or name)
    private const string KeyElems = "UnlockedElements";  // CSV of names
    private const string KeyMols = "UnlockedMolecules"; // CSV of names

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // ---------- Level / Day ----------
    public bool HasSave() => PlayerPrefs.HasKey(KeyLevel) || PlayerPrefs.HasKey(KeyDay);
    public int LoadLevel() => PlayerPrefs.GetInt(KeyLevel, 1);
    public int LoadDay() => PlayerPrefs.GetInt(KeyDay, 1);

    public void SaveLevel(int lvl) { PlayerPrefs.SetInt(KeyLevel, lvl); PlayerPrefs.Save(); }
    public void SaveDay(int day) { PlayerPrefs.SetInt(KeyDay, day); PlayerPrefs.Save(); }

    // ---------- Quest Completed ----------
    // NOTE: 가능하면 quest.name 대신 quest.questId 같은 안정 키 사용 권장
    public void SaveCompleted(QuestData quest, bool isCompleted)
    {
        PlayerPrefs.SetInt(KeyCompleted + quest.name, isCompleted ? 1 : 0);
    }

    public bool LoadCompleted(QuestData quest)
    {
        return PlayerPrefs.GetInt(KeyCompleted + quest.name, 0) == 1;
    }

    // 이어하기 진입 시 SO에 반영
    public void ApplyCompletedToSO(QuestData[] allQuests)
    {
        foreach (var q in allQuests)
            q.isCompleted = LoadCompleted(q);
    }

    // 새로하기: SO 리셋 + PlayerPrefs 키 삭제
    public void ResetAllQuests(QuestData[] quests)
    {
        foreach (var q in quests)
        {
            q.isCompleted = false;
            PlayerPrefs.DeleteKey(KeyCompleted + q.name);
        }
        PlayerPrefs.Save();
    }

    // ---------- Dictionary (언락) ----------
    // 이름 기반 저장(에셋 이름/심볼이 고유하다고 가정)
    public void SaveDictionary(IEnumerable<ElementSO> elems, IEnumerable<MoleculeSO> mols)
    {
        var elemNames = elems?.Select(e => e.name) ?? Enumerable.Empty<string>();
        var molNames = mols?.Select(m => m.name) ?? Enumerable.Empty<string>();

        PlayerPrefs.SetString(KeyElems, string.Join(",", elemNames));
        PlayerPrefs.SetString(KeyMols, string.Join(",", molNames));
        PlayerPrefs.Save();
    }

    public void LoadDictionary(out HashSet<string> elemNames, out HashSet<string> molNames)
    {
        elemNames = new HashSet<string>();
        molNames = new HashSet<string>();

        if (PlayerPrefs.HasKey(KeyElems))
        {
            foreach (var n in PlayerPrefs.GetString(KeyElems).Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                elemNames.Add(n);
        }
        if (PlayerPrefs.HasKey(KeyMols))
        {
            foreach (var n in PlayerPrefs.GetString(KeyMols).Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                molNames.Add(n);
        }
    }

    public void ResetDictionaryPrefs()
    {
        PlayerPrefs.DeleteKey(KeyElems);
        PlayerPrefs.DeleteKey(KeyMols);
        PlayerPrefs.Save();
    }

    // ---------- Whole wipe ----------
    public void DeleteSave()
    {
        PlayerPrefs.DeleteKey(KeyLevel);
        PlayerPrefs.DeleteKey(KeyDay);
        // 퀘스트 완료 키들은 ResetAllQuests에서 각각 지우는 걸 권장
        PlayerPrefs.Save();
    }
}
