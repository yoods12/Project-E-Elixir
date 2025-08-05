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
        // ElementSelector 쪽에서 선택 데이터 초기화
        ElementSelector.ClearSelection();

        // 만약 CraftingUI 에 슬롯 표시가 있다면, 함께 갱신
        CraftingUI.UpdateSlots(ElementSelector.GetSelectedElements());
    }
}
