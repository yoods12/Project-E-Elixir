using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChemistryUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dayText;
    [SerializeField] private TextMeshProUGUI npcNameText;
    [SerializeField] private TextMeshProUGUI hintText;
    [SerializeField] private Image npcImage;
    [SerializeField] private Image playerImage;
    [SerializeField] private Image supporterImage;

    [SerializeField] private Image directionPlayer; // 누구의 대화인지 표시해주는 이미지
    [SerializeField] private Image directionNPC; // 누구의 대화인지 표시해주는 이미지
    [SerializeField] private Image directionSupporter; // 누구의 대화인지 표시해주는 이미지


    [SerializeField] private Button dictionaryButton;

    void Start()
    {
        var currentDay = SaveManager.Instance.LoadDay();
        var quest = DayManager.Instance.GetCurrentQuest();

        npcImage.sprite = quest.NpcIcon;
        dayText.text = $"Day {currentDay}";
        npcNameText.text = quest.NpcName;

        if (quest.isCompleted)
        {
            hintText.text = quest.repeatHint.Count > 0 ? quest.repeatHint[0] : "";
            if (quest.arrow[0] == 0)
            {
                directionPlayer.gameObject.SetActive(true);
                directionNPC.gameObject.SetActive(false);
                directionSupporter.gameObject.SetActive(false);
            }
            if (quest.arrow[0] == 1)
            {
                directionPlayer.gameObject.SetActive(false);
                directionNPC.gameObject.SetActive(true);
                directionSupporter.gameObject.SetActive(false);
            }
            if (quest.arrow[0] == 2)
            {
                directionPlayer.gameObject.SetActive(false);
                directionNPC.gameObject.SetActive(false);
                directionSupporter.gameObject.SetActive(true);
            }
        }
        else
        {
            hintText.text = quest.hint.Count > 0 ? quest.hint[0] : "";
            if (quest.arrow[0] == 0)
            {
                directionPlayer.gameObject.SetActive(true);
                directionNPC.gameObject.SetActive(false);
                directionSupporter.gameObject.SetActive(false);
            }
            if (quest.arrow[0] == 1)
            {
                directionPlayer.gameObject.SetActive(false);
                directionNPC.gameObject.SetActive(true);
                directionSupporter.gameObject.SetActive(false);
            }
            if (quest.arrow[0] == 2)
            {
                directionPlayer.gameObject.SetActive(false);
                directionNPC.gameObject.SetActive(false);
                directionSupporter.gameObject.SetActive(true);
            }
        }
        dictionaryButton.onClick.AddListener(() =>
        SceneLoader.Instance.LoadDictionary());
    }
    public void PreviousHint()
    {
        var quest = DayManager.Instance.GetCurrentQuest();
        int currentIndex = quest.hint.IndexOf(hintText.text);
        int currentIndexRepeat = quest.repeatHint.IndexOf(hintText.text);

        if (currentIndexRepeat > 0 && quest.isCompleted)
        {
            hintText.text = quest.repeatHint[currentIndex - 1];
            if (quest.arrow[currentIndexRepeat - 1] == 0)
            {
                directionPlayer.gameObject.SetActive(true);
                directionNPC.gameObject.SetActive(false);
                directionSupporter.gameObject.SetActive(false);
            }
            if (quest.arrow[currentIndexRepeat - 1] == 1)
            {
                directionPlayer.gameObject.SetActive(false);
                directionNPC.gameObject.SetActive(true);
                directionSupporter.gameObject.SetActive(false);
            }
            if (quest.arrow[currentIndexRepeat - 1] == 2)
            {
                directionPlayer.gameObject.SetActive(false);
                directionNPC.gameObject.SetActive(false);
                directionSupporter.gameObject.SetActive(true);
            }
        }
        if (currentIndex > 0 && !quest.isCompleted)
        {
            hintText.text = quest.hint[currentIndex - 1];
            if (quest.arrow[currentIndex - 1] == 0)
            {
                directionPlayer.gameObject.SetActive(true);
                directionNPC.gameObject.SetActive(false);
                directionSupporter.gameObject.SetActive(false);
            }
            if (quest.arrow[currentIndex - 1] == 1)
            {
                directionPlayer.gameObject.SetActive(false);
                directionNPC.gameObject.SetActive(true);
                directionSupporter.gameObject.SetActive(false);
            }
            if (quest.arrow[currentIndex - 1] == 2)
            {
                directionPlayer.gameObject.SetActive(false);
                directionNPC.gameObject.SetActive(false);
                directionSupporter.gameObject.SetActive(true);
            }
        }
    }
    public void NextHint()
    {
        var quest = DayManager.Instance.GetCurrentQuest();
        int currentIndex = quest.hint.IndexOf(hintText.text);
        int currentIndexRepeat = quest.repeatHint.IndexOf(hintText.text);

        if (currentIndexRepeat < quest.repeatHint.Count - 1 && quest.isCompleted)
        {
            hintText.text = quest.repeatHint[currentIndex + 1];
            if (quest.arrow[currentIndexRepeat + 1] == 0)
            {
                directionPlayer.gameObject.SetActive(true);
                directionNPC.gameObject.SetActive(false);
                directionSupporter.gameObject.SetActive(false);
            }
            if (quest.arrow[currentIndexRepeat + 1] == 1)
            {
                directionPlayer.gameObject.SetActive(false);
                directionNPC.gameObject.SetActive(true);
                directionSupporter.gameObject.SetActive(false);
            }
            if (quest.arrow[currentIndexRepeat + 1] == 2)
            {
                directionPlayer.gameObject.SetActive(false);
                directionNPC.gameObject.SetActive(false);
                directionSupporter.gameObject.SetActive(true);
            }
        }
        if (currentIndex < quest.hint.Count - 1 && !quest.isCompleted)
        {
            hintText.text = quest.hint[currentIndex + 1];
            if (quest.arrow[currentIndex + 1] == 0)
            {
                directionPlayer.gameObject.SetActive(true);
                directionNPC.gameObject.SetActive(false);
                directionSupporter.gameObject.SetActive(false);
            }
            if (quest.arrow[currentIndex + 1] == 1)
            {
                directionPlayer.gameObject.SetActive(false);
                directionNPC.gameObject.SetActive(true);
                directionSupporter.gameObject.SetActive(false);
            }
            if (quest.arrow[currentIndex + 1] == 2)
            {
                directionPlayer.gameObject.SetActive(false);
                directionNPC.gameObject.SetActive(false);
                directionSupporter.gameObject.SetActive(true);
            }
        }
    }
    public void OnClickSound()
    {
        FindObjectOfType<SFXPlayer>().PlaySFX(0);
    }

}
