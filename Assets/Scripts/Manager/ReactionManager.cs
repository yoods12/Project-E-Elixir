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
        // 싱글톤
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        BuildRecipeMap();
    }
    private void BuildRecipeMap()
    {
        recipeMap = new Dictionary<string, MoleculeSO>();

        foreach(var r in allRecipes) // 모든 레시피 순회
        {
            var pairs = new List<string>();
            for(int i = 0; i<r.inputElements.Count; i++)
            {
                // "원소ID:개수" 형태로 문자열 생성
                pairs.Add($"{r.inputElements[i].id}:{r.inputCounts[i]}");
            }

            pairs.Sort(); // 정렬

            var key = string.Join(",", pairs); // ","로 연결하여 키 생성

            recipeMap[key] = r.resultMolecule; // 결과 분자 저장
        }
    }

    public MoleculeSO Combine(List<ElementSO> selected)
    {
        var countMap = new Dictionary<int, int>();
        // 선택된 원소들의 개수를 세는 딕셔너리
        foreach (var e in selected)
        {
            if (!countMap.ContainsKey(e.id)) countMap[e.id] = 0;
            countMap[e.id]++;
        }

        // 딕셔너리를 정렬하여 키 생성
        var key = string.Join(",", countMap.Select(kv => $"{kv.Key}:{kv.Value}").OrderBy(s => s));

        recipeMap.TryGetValue(key, out var molecule);

        return molecule;
    }
}
