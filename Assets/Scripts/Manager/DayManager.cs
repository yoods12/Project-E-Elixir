using System.Transactions;
using UnityEngine;

public class DayManager : MonoBehaviour
{  
    public static DayManager Instance { get; private set; }

    [SerializeField] private DayData[] days; // 1일차 ~ n일차 SO 리스트
    [SerializeField] private NPCManager npcManager;

    private int currentDayIndex = 0;

    private void Awake()
    {
        // 싱글톤
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        LoadDayFromPrefs();
        LoadDay();
    }

    private void LoadDay() // 현재 일자 정보 화면에 적용, NPC 활성화/비활성화 처리
    {
        if (days == null || days.Length == 0) {  return; }

        // 인덱스 범위 보정
        currentDayIndex = Mathf.Clamp(currentDayIndex, 0, days.Length - 1);
        var data = days[currentDayIndex];

        npcManager.SpawnForDay(data.visitNpcs);
    }

    private void UpdateNPCsForDay(DayData data) // DayData.VisitNpcs에 포함된 NPC만 활성화/ 나머지는비활성화
    {
        // 씬에있는 NPCdata 찾음
        var allNpcs = FindObjectsOfType<NPCData>();
        foreach( var npc in allNpcs)
        {
            bool shouldShow = System.Array.IndexOf(data.visitNpcs, npc) >= 0;
            npc.gameObject.SetActive(shouldShow);
        }
    }

    public void NextDay()
    {
        // 인덱스 증가 후 범위 보정
        currentDayIndex = Mathf.Clamp(currentDayIndex + 1, 0, days.Length - 1);
        SaveDay();
        LoadDay();
    }
    private void SaveDay() // PlayerPrefs에 currentDayIndenx저장
    {
        PlayerPrefs.SetInt("CurrentDayIndex", currentDayIndex);
        PlayerPrefs.Save();
    }

    private void LoadDayFromPrefs() // PlayerPrefs에서 저장된 일차 인덱스 불러오기
    {
        currentDayIndex = PlayerPrefs.GetInt("CurrentDayIndex", 0);
    }

    public DayData GetCurrentDayData() // 외부에서 DayData 접근시 사용
    {
        if (days == null || days.Length == 0) return null;
        return days[Mathf.Clamp(currentDayIndex, 0, days.Length - 1)];
    }

    public void ResetDay()
    {
        currentDayIndex = 0;

        // 덮어써서 저장
        PlayerPrefs.SetInt("CurrentDayIndex", currentDayIndex);
        PlayerPrefs.Save();

        LoadDay();  // 1일차 UI·NPC 갱신
    }
}
