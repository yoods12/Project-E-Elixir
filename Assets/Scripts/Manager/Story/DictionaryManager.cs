using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DictionaryManager : MonoBehaviour
{
    public static DictionaryManager Instance { get; private set; }
    private const string KeyElems = "UnlockedElements";
    private const string KeyMols = "UnlockedMolecules";

    [Header("전체 ElementSO, MoleculeSO 에셋")]
    [SerializeField] private ElementSO[] allElements;
    [SerializeField] private MoleculeSO[] allMolecules;

    private List<ElementSO> unlockedElements = new List<ElementSO>();
    private List<MoleculeSO> unlockedMolecules = new List<MoleculeSO>();

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadDictionary();
    }

    public void UnlockEntriesForQuest(QuestData quest)
    {
        bool anyNew = false;

        foreach (var elem in quest.requiredElements)
            if (!unlockedElements.Contains(elem))
            {
                unlockedElements.Add(elem);
                anyNew = true;
            }

        foreach (var mol in quest.requiredMolecules)
            if (!unlockedMolecules.Contains(mol))
            {
                unlockedMolecules.Add(mol);
                anyNew = true;
            }

        if (anyNew)
        {
            SaveDictionary();
            UploadDictionary();
        }
    }

    private void SaveDictionary()
    {
        // SO 에셋의 이름(asset name)을 키로 저장
        var elemNames = unlockedElements.Select(e => e.name);
        PlayerPrefs.SetString(KeyElems, string.Join(",", elemNames));

        var molNames = unlockedMolecules.Select(m => m.name);
        PlayerPrefs.SetString(KeyMols, string.Join(",", molNames));

        PlayerPrefs.Save();
    }

    public void LoadDictionary()
    {
        unlockedElements.Clear();
        unlockedMolecules.Clear();

        if (PlayerPrefs.HasKey(KeyElems))
        {
            var raw = PlayerPrefs.GetString(KeyElems);
            foreach (var n in raw.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                // 에셋 이름으로 찾아서 추가
                var so = allElements.FirstOrDefault(e => e.name == n);
                if (so != null) unlockedElements.Add(so);
                else Debug.LogWarning($"[Dict] ElementSO not found by name '{n}'");
            }
        }

        if (PlayerPrefs.HasKey(KeyMols))
        {
            var raw = PlayerPrefs.GetString(KeyMols);
            foreach (var n in raw.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                var so = allMolecules.FirstOrDefault(m => m.name == n);
                if (so != null) unlockedMolecules.Add(so);
                else Debug.LogWarning($"[Dict] MoleculeSO not found by name '{n}'");
            }
        }
    }

    private void UploadDictionary()
    {
        var ui = FindObjectOfType<DictionaryUI>();
        if (ui != null) ui.Refresh();
    }

    public void ResetDictionary()
    {
        unlockedElements.Clear();
        unlockedMolecules.Clear();
        PlayerPrefs.DeleteKey(KeyElems);
        PlayerPrefs.DeleteKey(KeyMols);
        PlayerPrefs.Save();
    }

    public IReadOnlyList<ElementSO> GetUnlockedElements() => unlockedElements;
    public IReadOnlyList<MoleculeSO> GetUnlockedMolecules() => unlockedMolecules;
}
