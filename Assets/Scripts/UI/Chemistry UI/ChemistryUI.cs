using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChemistryUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI npcNameText;
    [SerializeField] private TextMeshProUGUI hintText;

    [SerializeField] private Button previousHintButton;
    [SerializeField] private Button nextHintButton;


    void Start()
    {
        var quest = DayManager.Instance.GetCurrentQuest();
        npcNameText.text = quest.NpcName;
        hintText.text = quest.hint.Count > 0 ? quest.hint[0] : "";
    }
    public void PreviousHint()
    {
        var quest = DayManager.Instance.GetCurrentQuest();
        int currentIndex = quest.hint.IndexOf(hintText.text);
        if (currentIndex > 0)
        {
            hintText.text = quest.hint[currentIndex - 1];
        }
    }
    public void NextHint()
    {
        var quest = DayManager.Instance.GetCurrentQuest();
        int currentIndex = quest.hint.IndexOf(hintText.text);
        if (currentIndex < quest.hint.Count - 1)
        {
            hintText.text = quest.hint[currentIndex + 1];
        }
    }
}
