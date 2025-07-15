using UnityEngine;
using UnityEngine.UI;

public class CombineButton : MonoBehaviour
{
    [SerializeField] private ReactionManager reactionManager;
    [SerializeField] private Transform spawnPoint;

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
            Debug.Log(result.name + " created!");
        }
        else
        {
            Debug.Log("Fail");
        }
    }
}
