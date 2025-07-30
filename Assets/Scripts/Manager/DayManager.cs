using System.Transactions;
using UnityEngine;

public class DayManager : MonoBehaviour
{  
    public static DayManager Instance { get; private set; }

    [SerializeField] private DayData[] days; // 1���� ~ n���� SO ����Ʈ
    [SerializeField] private NPCManager npcManager;

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
        LoadDayFromPrefs();
        LoadDay();
    }

    private void LoadDay() // ���� ���� ���� ȭ�鿡 ����, NPC Ȱ��ȭ/��Ȱ��ȭ ó��
    {
        if (days == null || days.Length == 0) {  return; }

        // �ε��� ���� ����
        currentDayIndex = Mathf.Clamp(currentDayIndex, 0, days.Length - 1);
        var data = days[currentDayIndex];

        npcManager.SpawnForDay(data.visitNpcs);
    }

    private void UpdateNPCsForDay(DayData data) // DayData.VisitNpcs�� ���Ե� NPC�� Ȱ��ȭ/ �������º�Ȱ��ȭ
    {
        // �����ִ� NPCdata ã��
        var allNpcs = FindObjectsOfType<NPCData>();
        foreach( var npc in allNpcs)
        {
            bool shouldShow = System.Array.IndexOf(data.visitNpcs, npc) >= 0;
            npc.gameObject.SetActive(shouldShow);
        }
    }

    public void NextDay()
    {
        // �ε��� ���� �� ���� ����
        currentDayIndex = Mathf.Clamp(currentDayIndex + 1, 0, days.Length - 1);
        SaveDay();
        LoadDay();
    }
    private void SaveDay() // PlayerPrefs�� currentDayIndenx����
    {
        PlayerPrefs.SetInt("CurrentDayIndex", currentDayIndex);
        PlayerPrefs.Save();
    }

    private void LoadDayFromPrefs() // PlayerPrefs���� ����� ���� �ε��� �ҷ�����
    {
        currentDayIndex = PlayerPrefs.GetInt("CurrentDayIndex", 0);
    }

    public DayData GetCurrentDayData() // �ܺο��� DayData ���ٽ� ���
    {
        if (days == null || days.Length == 0) return null;
        return days[Mathf.Clamp(currentDayIndex, 0, days.Length - 1)];
    }

    public void ResetDay()
    {
        currentDayIndex = 0;

        // ����Ἥ ����
        PlayerPrefs.SetInt("CurrentDayIndex", currentDayIndex);
        PlayerPrefs.Save();

        LoadDay();  // 1���� UI��NPC ����
    }
}
