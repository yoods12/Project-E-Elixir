using NUnit.Framework;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ElementSelector : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private ElementSO element; // 이 버튼이 나타내는 원소
    [SerializeField] private Text countText; // 선택된 원소의 개수를 표시할 UI 텍스트

    private static Dictionary<ElementSO, int> selectedCounts = new Dictionary<ElementSO, int>(); // 선택된 원소와 개수를 저장
    void Start()
    {
        UpdateCountUI();
    }

    /// <summary>
    /// IPointerClickHandler: 버튼을 왼클릭/우클릭 구분해서 처리
    /// </summary>
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
    }

    private void UpdateCountUI()
    {
        // 0개면 숨기고, 1개 이상이면 텍스트로 표시
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
    /// <summary>
    /// 외부에서 현재 선택된 원소 리스트(중복 포함) 가져오기
    /// </summary>
    public static List<ElementSO> GetSelectedElements()
    {
        var list = new List<ElementSO>();
        foreach (var kv in selectedCounts)
            for (int i = 0; i < kv.Value; i++)
                list.Add(kv.Key);
        return list;
    }
}
