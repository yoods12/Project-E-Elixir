using System.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DictionaryExplainUI : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI koreaName;
    [SerializeField] private TextMeshProUGUI formula;
    [SerializeField] private TextMeshProUGUI descText;
    [SerializeField] private Image description;

    public void Init(DictionarySlotUI slot)
    {
        if (slot == null) return;

        // 1) 슬롯에 무엇이 들어있는지 확인
        if (slot.TryGetElement(out var elem))
        {
            description.sprite = elem.descriptionImage;
            iconImage.sprite = elem.dictionaryIcon;
            iconImage.gameObject.SetActive(true);
            //koreaName.text = elem.koreaName;
            //formula.text = elem.symbol;
            //descText.text = BuildElementDescription(elem);
        }
        else if (slot.TryGetMolecule(out var mol))
        {
            description.sprite = mol.descriptionImage;
            iconImage.sprite = mol.moleculeIcon;
            iconImage.gameObject.SetActive(true);
            //koreaName.text = mol.koreaName;
            //formula.text = mol.formula;
            //descText.text = BuildMoleculeDescription(mol);
        }

        gameObject.SetActive(true);
    }

    private string MakeBulletLines(System.Collections.Generic.List<string> lines)
    {
        if (lines == null || lines.Count == 0) return "설명 없음";

        var sb = new StringBuilder();
        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            sb.Append(line).Append('\n').Append('\n');
        }
        // 마지막 개행 제거
        if (sb.Length > 0 && sb[sb.Length - 1] == '\n') sb.Length--;
        return sb.ToString();
    }

    private string BuildElementDescription(ElementSO e)
    {
        return MakeBulletLines(e.description);
    }

    private string BuildMoleculeDescription(MoleculeSO m)
    {
        return MakeBulletLines(m.description);
    }
}
