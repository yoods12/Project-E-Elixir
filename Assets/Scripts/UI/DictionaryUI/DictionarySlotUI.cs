using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DictionarySlotUI : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI labelText;

    public void Init(ElementSO elem)
    {
        iconImage.sprite = elem.elementIcon;
        labelText.text = elem.symbol;
        iconImage.gameObject.SetActive(true);
        labelText.gameObject.SetActive(true);
    }

    public void Init(MoleculeSO mol)
    {
        iconImage.sprite = mol.moleculeIcon;
        labelText.text = mol.displayName;
        iconImage.gameObject.SetActive(true);
        labelText.gameObject.SetActive(true);
    }

    public void ClearSlots()
    {
        iconImage.gameObject.SetActive(false);
        labelText.gameObject.SetActive(false);
    }


}
