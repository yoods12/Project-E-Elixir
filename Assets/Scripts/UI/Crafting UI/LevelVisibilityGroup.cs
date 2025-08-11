using UnityEngine;

public class LevelVisibilityGroup : MonoBehaviour
{
    [Header("�� ���� �̻��̸� Ȱ��ȭ�� ������Ʈ��")]
    [SerializeField] private int requiredLevel = 2;
    [SerializeField] private GameObject[] targets; // ���� Level2Element, Level3Element ���� �巡��

    private void OnEnable()
    {
        if (DayManager.Instance != null)
        {
            DayManager.Instance.OnLevelChanged += Apply;
            Apply(DayManager.Instance.currentLevel);
        }
    }

    private void OnDisable()
    {
        if (DayManager.Instance != null)
            DayManager.Instance.OnLevelChanged -= Apply;
    }

    private void Apply(int level)
    {
        bool on = level >= requiredLevel;
        foreach (var go in targets)
            if (go) go.SetActive(on);
    }
}
