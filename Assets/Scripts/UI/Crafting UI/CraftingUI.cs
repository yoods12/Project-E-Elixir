using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftingUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] slotTexts;
    [SerializeField] private Button dictionaryButton;

    private static CraftingUI _instance;
    private void Awake()
    {
        _instance = this;
    }
    void Start()
    {
        dictionaryButton.onClick.AddListener(() =>
            SceneLoader.Instance.LoadDictionary());
    }

    public static void UpdateSlots(List<ElementSO> selected)
    {
        if (_instance == null) return;

        // 1) 선택된 원소를 ElementSO 기준으로 그룹핑해서 개수 집계
        var grouped = selected
            .GroupBy(e => e)
            .Select(g => new { Element = g.Key, Count = g.Count() })
            .ToList();

        // 2) 슬롯에 채워넣기
        for (int i = 0; i < _instance.slotTexts.Length; i++)
        {
            if (i < grouped.Count)
            {
                var item = grouped[i];
                // 개수가 1 초과일 때만 " xN" 붙이기
                _instance.slotTexts[i].text =
                    item.Element.displayName +
                    (item.Count > 1 ? $" x{item.Count}" : "");
            }
            else
            {
                _instance.slotTexts[i].text = string.Empty;
            }
        }
    }

    public void ClearSlots()
    {
        ElementSelector.ClearSelection();
        UpdateSlots(new List<ElementSO>());
    }

}
