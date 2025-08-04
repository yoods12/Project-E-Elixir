using System.Transactions;
using UnityEngine;

public class DayManager : MonoBehaviour
{  
    public static DayManager Instance { get; private set; }

    [SerializeField] private DayData[] days; // 1老瞒 ~ n老瞒 SO 府胶飘
    [SerializeField] private LevelManager levelManager;

    private int currentDayIndex = 0;

    private void Awake()
    {
        // 教臂沛
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        
    }

}
