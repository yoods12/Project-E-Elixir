using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ElementSelector : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private ElementSO element;
    [SerializeField] private Text countText;

    private static Dictionary<ElementSO, int> selectedCounts = new Dictionary<ElementSO, int>();

    void Start()
    {
        UpdateCountUI();
        CraftingUI.UpdateSlots(GetSelectedElements());
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
            AddOne();
        else if (eventData.button == PointerEventData.InputButton.Right)
            RemoveOne();
    }

    private void AddOne()
    {
        if (!selectedCounts.ContainsKey(element))
            selectedCounts[element] = 0;
        selectedCounts[element]++;
        UpdateCountUI();

        // ���� UI ������Ʈ
        CraftingUI.UpdateSlots(GetSelectedElements());
    }

    private void RemoveOne()
    {
        if (selectedCounts.ContainsKey(element) && selectedCounts[element] > 0)
        {
            selectedCounts[element]--;
            if (selectedCounts[element] == 0)
                selectedCounts.Remove(element);
        }
        UpdateCountUI();

        // ���� UI ������Ʈ
        CraftingUI.UpdateSlots(GetSelectedElements());
    }

    private void UpdateCountUI()
    {
        if (selectedCounts.TryGetValue(element, out int cnt) && cnt > 0)
        {
            countText.text = cnt.ToString();
            countText.gameObject.SetActive(true);
        }
        else
        {
            countText.gameObject.SetActive(false);
        }
    }

    public static List<ElementSO> GetSelectedElements()
    {
        var list = new List<ElementSO>();
        foreach (var kv in selectedCounts)
            for (int i = 0; i < kv.Value; i++)
                list.Add(kv.Key);
        return list;
    }
    public static void ClearSelection()
    {
        // 1) ������ �ʱ�ȭ
        selectedCounts.Clear();

        // 2) ȭ����� ��� ElementSelector �� ã�� UI ����
        foreach (var sel in GameObject.FindObjectsOfType<ElementSelector>())
            sel.UpdateCountUI();
    }

}
