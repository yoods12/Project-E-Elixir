using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResultSlotUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI questNameText;
    [SerializeField] private Image statusIcon;    // 성공/실패 아이콘

    public Sprite successSprite;
    public Sprite failureSprite;

    public void Init(QuestData q, bool success)
    {
        questNameText.text = q.NpcName;
        statusIcon.sprite = success ? successSprite : failureSprite;
    }
}