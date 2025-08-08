using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class DictionarySlotUI : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI labelText;

    private Button _btn;

    // �� ������ ��� �ִ� ������(�� �� �ϳ��� ä����)
    private ElementSO elementRef;
    private MoleculeSO moleculeRef;

    private void Awake()
    {
        if (iconImage == null) iconImage = GetComponentInChildren<Image>();
        if (labelText == null) labelText = GetComponentInChildren<TextMeshProUGUI>();
        _btn = GetComponent<Button>();
        _btn.onClick.RemoveAllListeners();
        _btn.onClick.AddListener(OnClick);
    }

    public void Init(ElementSO elem)
    {
        elementRef = elem;
        moleculeRef = null;

        iconImage.sprite = elem.elementIcon;
        labelText.text = elem.symbol;

        iconImage.gameObject.SetActive(true);
        labelText.gameObject.SetActive(true);
    }

    public void Init(MoleculeSO mol)
    {
        moleculeRef = mol;
        elementRef = null;

        iconImage.sprite = mol.moleculeIcon;
        labelText.text = mol.displayName;

        iconImage.gameObject.SetActive(true);
        labelText.gameObject.SetActive(true);
    }

    public void ClearSlots()
    {
        elementRef = null;
        moleculeRef = null;
        iconImage.gameObject.SetActive(false);
        labelText.gameObject.SetActive(false);
    }

    private void OnClick()
    {
        // ���� �ִ� ���� �г� ã�Ƽ� ����
        var explain = FindObjectOfType<DictionaryExplainUI>(includeInactive: true);
        if (explain != null) explain.Init(this);
    }

    // ���� �гο��� ���� ���� ���� Getter
    public bool TryGetElement(out ElementSO elem) { elem = elementRef; return elem != null; }
    public bool TryGetMolecule(out MoleculeSO mol) { mol = moleculeRef; return mol != null; }
}
