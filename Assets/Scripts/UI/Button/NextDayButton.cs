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
        // �̱������� �ö�� DayManager.Instance ȣ��
        if (DayManager.Instance != null)
            DayManager.Instance.OnNextDay();
        else
            Debug.LogError("DayManager �ν��Ͻ��� ã�� �� �����ϴ�!");
    }
}
