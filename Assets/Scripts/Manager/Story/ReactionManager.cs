using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ReactionManager : MonoBehaviour
{
    [SerializeField] private ReactionRecipeSO[] allRecipes;
    private Dictionary<string, MoleculeSO> recipeMap;
    public static ReactionManager Instance { get; private set; }

    void Awake()
    {
        // �̱���
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        BuildRecipeMap();
    }
    private void BuildRecipeMap()
    {
        recipeMap = new Dictionary<string, MoleculeSO>();

        foreach(var r in allRecipes) // ��� ������ ��ȸ
        {
            var pairs = new List<string>();
            for(int i = 0; i<r.inputElements.Count; i++)
            {
                // "����ID:����" ���·� ���ڿ� ����
                pairs.Add($"{r.inputElements[i].id}:{r.inputCounts[i]}");
            }

            pairs.Sort(); // ����

            var key = string.Join(",", pairs); // ","�� �����Ͽ� Ű ����

            recipeMap[key] = r.resultMolecule; // ��� ���� ����
        }
    }

    public MoleculeSO Combine(List<ElementSO> selected)
    {
        var countMap = new Dictionary<int, int>();
        // ���õ� ���ҵ��� ������ ���� ��ųʸ�
        foreach (var e in selected)
        {
            if (!countMap.ContainsKey(e.id)) countMap[e.id] = 0;
            countMap[e.id]++;
        }

        // ��ųʸ��� �����Ͽ� Ű ����
        var key = string.Join(",", countMap.Select(kv => $"{kv.Key}:{kv.Value}").OrderBy(s => s));

        recipeMap.TryGetValue(key, out var molecule);

        return molecule;
    }
}
