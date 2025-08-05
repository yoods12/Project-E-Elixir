// NextDayButton.cs
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class NextDayButton : MonoBehaviour
{
    void Awake()
    {
        var btn = GetComponent<Button>();
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        // 싱글톤으로 올라온 DayManager.Instance 호출
        if (DayManager.Instance != null)
            DayManager.Instance.OnNextDay();
        else
            Debug.LogError("DayManager 인스턴스를 찾을 수 없습니다!");
    }
}
