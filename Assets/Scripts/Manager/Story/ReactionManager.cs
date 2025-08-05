using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ReactionManager : MonoBehaviour
{
    [Header("모든 레시피 (Input → 결과 SO)")]
    [SerializeField] private ReactionRecipeSO[] allRecipes;

    // key: "id1:count1,id2:count2,..." → value: ElementSO or MoleculeSO
    private Dictionary<string, ScriptableObject> recipeMap;

    public static ReactionManager Instance { get; private set; }

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

        BuildRecipeMap();
    }

    private void BuildRecipeMap()
    {
        recipeMap = new Dictionary<string, ScriptableObject>();

        foreach (var r in allRecipes)
        {
            // 1) "id:count" 쌍 생성
            var pairs = new List<string>();
            for (int i = 0; i < r.inputElements.Count; i++)
                pairs.Add($"{r.inputElements[i].id}:{r.inputCounts[i]}");

            // 2) 정렬 후 key 생성
            pairs.Sort();
            var key = string.Join(",", pairs);

            // 3) 레시피 맵에 등록 (r.result는 ScriptableObject—ElementSO 또는 MoleculeSO)
            recipeMap[key] = r.result;
        }
    }

    /// <summary>
    /// 선택된 원소 리스트로 합성 시도.
    /// 성공하면 ElementSO 또는 MoleculeSO (ScriptableObject), 실패하면 null.
    /// </summary>
    public ScriptableObject Combine(List<ElementSO> selected)
    {
        // 1) 선택된 원소들의 개수를 센다
        var countMap = new Dictionary<int, int>();
        foreach (var e in selected)
        {
            if (!countMap.ContainsKey(e.id)) countMap[e.id] = 0;
            countMap[e.id]++;
        }

        // 2) 만약 “한 가지 원소(id)가 1개만” 선택된 경우, 그 ElementSO 자체를 반환
        if (countMap.Count == 1 && countMap.Values.First() == 1)
        {
            // selected 리스트에 있는 그 원소를 그대로 반환
            return selected[0];
        }

        // 3) 그 외에는 기존 레시피 맵으로 key를 만들어 lookup
        var parts = countMap
            .Select(kv => $"{kv.Key}:{kv.Value}")
            .OrderBy(s => s);
        var key = string.Join(",", parts);

        if (!recipeMap.TryGetValue(key, out var resultSO))
        {
            Debug.LogWarning($"[Combine] 레시피 없음: {key}");
            return null;
        }
        return resultSO;
    }

}
