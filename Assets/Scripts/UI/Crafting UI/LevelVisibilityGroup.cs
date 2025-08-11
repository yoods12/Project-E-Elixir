using UnityEngine;

public class LevelVisibilityGroup : MonoBehaviour
{
    [Header("이 레벨 이상이면 활성화할 오브젝트들")]
    [SerializeField] private int requiredLevel = 2;
    [SerializeField] private GameObject[] targets; // 여기 Level2Element, Level3Element 등을 드래그

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
