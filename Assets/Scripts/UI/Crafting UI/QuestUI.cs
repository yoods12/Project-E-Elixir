using System.Collections.Generic;
using TMPro;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.UI;

public class QuestUI : MonoBehaviour
{
    //[SerializeField] private TextMeshProUGUI npcNameText;
    //[SerializeField] private TextMeshProUGUI hintText;
    [SerializeField] private Button combineButton;
    [SerializeField] private TextMeshProUGUI testResultText;

    void Start()
    {
        var quest = DayManager.Instance.GetCurrentQuest();
        //npcNameText.text = quest.NpcName;
        //hintText.text = quest.hint.Count > 0 ? quest.hint[0] : "";

        combineButton.onClick.RemoveAllListeners();
        combineButton.onClick.AddListener(OnCombine);
    }

    private void OnCombine()
    {
        // 1) 선택된 원소 및 합성 시도
        List<ElementSO> selected = ElementSelector.GetSelectedElements();
        ScriptableObject product = ReactionManager.Instance.Combine(selected);

        // 2) 반환값 먼저 찍어보기
        if (product == null)
        {
            Debug.Log("[Combine] 합성 결과: null");
        }
        else
        {
            Debug.Log($"[Combine] 합성 결과: {product.GetType().Name} ({product.name})");
        }

        // 3) 현재 퀘스트의 요구 분자/원소 확인
        var quest = DayManager.Instance.GetCurrentQuest();
        Debug.Log("[Combine] 요구 분자 목록:");
        foreach (var req in quest.requiredMolecules)
            Debug.Log($"  - {req.name}");
        Debug.Log("[Combine] 요구 원소 목록:");
        foreach (var req in quest.requiredElements)
            Debug.Log($"  - {req.name}");

        // 4) 성공 여부 판단
        bool success = false;
        if (product is MoleculeSO m)
        {
            // == 비교 대신 이름 비교(참조 문제가 의심될 때)
            if (quest.requiredMolecules.Exists(r => r.name == m.name))
            {
                success = true; 
                quest.isCompleted = true;
                DictionaryManager.Instance.UnlockEntriesForQuest(quest);
            }
        }
        else if (product is ElementSO e)
        {
            if (quest.requiredElements.Exists(r => r.name == e.name))
            {
                success = true;
                quest.isCompleted = true;
                DictionaryManager.Instance.UnlockEntriesForQuest(quest);
            }
        }

        // 5) 화면에 표시 & DayManager 호출
        testResultText.text = success ? "성공!" : "실패!";
        Debug.Log($"[Combine] 성공 여부: {success}");
        DayManager.Instance.OnQuestAttempted(success);
    }
}
