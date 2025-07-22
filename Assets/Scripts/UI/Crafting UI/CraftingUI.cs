using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CraftingUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] slotTexts;

    private static CraftingUI _instance;

    private void Awake()
    {
        _instance = this;
    }

    // 외부에서 호출: 현재 선택 리스트를 받아서 슬롯에 뿌려줌

    public static void UpdateSlots(List<ElementSO> selected)
    {
        if (_instance == null) return;
        for (int i = 0; i < _instance.slotTexts.Length; i++)
        {
            if (i < selected.Count)
                _instance.slotTexts[i].text = selected[i].displayName;
            else
                _instance.slotTexts[i].text = string.Empty;
        }
    }
    public void ClearSlots()
    {
        ElementSelector.ClearSelection();

        UpdateSlots(new List<ElementSO>());
    }
}
