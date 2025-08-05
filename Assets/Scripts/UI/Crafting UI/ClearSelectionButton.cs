using UnityEngine;
using UnityEngine.UI;

public class ClearSelectionButton : MonoBehaviour
{
    [SerializeField] private Button clearButton;

    void Awake()
    {
        clearButton.onClick.RemoveAllListeners();
        clearButton.onClick.AddListener(ClearSelection);
    }

    private void ClearSelection()
    {
        // ElementSelector �ʿ��� ���� ������ �ʱ�ȭ
        ElementSelector.ClearSelection();

        // ���� CraftingUI �� ���� ǥ�ð� �ִٸ�, �Բ� ����
        CraftingUI.UpdateSlots(ElementSelector.GetSelectedElements());
    }
}
