using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DictionaryUI : MonoBehaviour
{
    [SerializeField] private Transform elementsContent;
    [SerializeField] private Transform moleculesContent;

    [SerializeField] private Button backButton;

    void Start()
    {
        backButton.onClick.AddListener(() =>
            SceneLoader.Instance.LoadPrevious()
        );
        Refresh();  // 기존 UI 갱신
    }

    public void Refresh()
    {
        // 1) 모든 슬롯 Clear
        foreach (var slot in elementsContent.GetComponentsInChildren<DictionarySlotUI>())
            slot.ClearSlots();
        foreach (var slot in moleculesContent.GetComponentsInChildren<DictionarySlotUI>())
            slot.ClearSlots();

        // 2) 언락된 원소만 Init()
        foreach (var elem in DictionaryManager.Instance.GetUnlockedElements())
        {
            var slotGO = elementsContent.Find(elem.symbol);
            if (slotGO == null) continue;
            var slot = slotGO.GetComponent<DictionarySlotUI>();
            slot.Init(elem);
        }

        // 3) 언락된 분자만 Init()
        foreach (var mol in DictionaryManager.Instance.GetUnlockedMolecules())
        {
            var slotGO = moleculesContent.Find(mol.displayName);
            if (slotGO == null) continue;
            var slot = slotGO.GetComponent<DictionarySlotUI>();
            slot.Init(mol);
        }
    }
    public void OnClickSoundindex0()
    {
        FindObjectOfType<SFXPlayer>().PlaySFX(0);
    }
    public void OnClickSoundindex1()
    {
        FindObjectOfType<SFXPlayer>().PlaySFX(1);
    }
}
