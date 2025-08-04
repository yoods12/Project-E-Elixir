using System.Transactions;
using UnityEngine;

public class DayManager : MonoBehaviour
{  
    public static DayManager Instance { get; private set; }

    [SerializeField] private DayData[] days; // 1���� ~ n���� SO ����Ʈ
    [SerializeField] private LevelManager levelManager;

    private int currentDayIndex = 0;

    private void Awake()
    {
        // �̱���
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        
    }

}
