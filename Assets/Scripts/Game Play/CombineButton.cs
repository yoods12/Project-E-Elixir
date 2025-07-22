using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CombineButton : MonoBehaviour
{
    [SerializeField] private ReactionManager reactionManager;
    [SerializeField] private TextMeshProUGUI resultText;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnCombineClicked);
    }

    private void OnCombineClicked()
    {
        var selected = ElementSelector.GetSelectedElements();
        var result = reactionManager.Combine(selected);

        if (result != null)
        {
            resultText.text = result.displayName;
            Debug.Log($"DisplayName='{result.displayName}'");
        }
        else
        {
            resultText.text = "Fail";
            Debug.Log("Fail");
        }
    }

}
