using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestReceiveUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI npcNameText;
    [SerializeField] private TextMeshProUGUI hintText;

    public void Init(QuestData q)
    {
        npcNameText.text = q.NpcName;
        hintText.text = q.hint.Count > 0 ? q.hint[0] : "";
    }
}