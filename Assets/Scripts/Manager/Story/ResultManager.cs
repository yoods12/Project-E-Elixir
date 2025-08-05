using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ResultManager : MonoBehaviour
{
    [SerializeField] private Transform resultListParent;
    [SerializeField] private GameObject resultSlotPrefab;
    [SerializeField] private Button nextDayButton;

    void Start()
    {
        var todays = DayManager.Instance.GetTodaysQuests();
        foreach (var q in todays)
        {
            var go = Instantiate(resultSlotPrefab, resultListParent);
            var slot = go.GetComponent<ResultSlotUI>();
            slot.Init(q, DayManager.Instance.GetResult(q));
        }

        nextDayButton.onClick.RemoveAllListeners();
        nextDayButton.onClick.AddListener(() =>
            DayManager.Instance.OnNextDay()
        );
    }
}
