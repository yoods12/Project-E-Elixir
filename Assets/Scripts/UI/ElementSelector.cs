using NUnit.Framework;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ElementSelector : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private ElementSO element; // �� ��ư�� ��Ÿ���� ����
    [SerializeField] private Text countText; // ���õ� ������ ������ ǥ���� UI �ؽ�Ʈ

    private static Dictionary<ElementSO, int> selectedCounts = new Dictionary<ElementSO, int>(); // ���õ� ���ҿ� ������ ����
    void Start()
    {
        UpdateCountUI();
    }

    /// <summary>
    /// IPointerClickHandler: ��ư�� ��Ŭ��/��Ŭ�� �����ؼ� ó��
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
        // 0���� �����, 1�� �̻��̸� �ؽ�Ʈ�� ǥ��
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
    /// �ܺο��� ���� ���õ� ���� ����Ʈ(�ߺ� ����) ��������
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
