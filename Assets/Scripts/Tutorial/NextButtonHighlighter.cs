using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class NextButtonHighlighter : MonoBehaviour
{
    [SerializeField] Button nextButton;

    // 대화가 끝났을 때 호출
    public void OnDialogueEnd()
    {
        // UI 선택(패드/키보드에서 강조 상태로 진입)
        EventSystem.current.SetSelectedGameObject(nextButton.gameObject);
        nextButton.Select();

        // 마우스 유저도 잘 보이게 펄싱 시작(선택)
        StartCoroutine(PulseOutline());
    }

    // 선택: 버튼에 Outline 컴포넌트 추가해두기
    IEnumerator PulseOutline()
    {
        var ol = nextButton.GetComponent<Outline>();
        if (!ol) yield break;

        float t = 0f;
        while (true)
        {
            t += Time.unscaledDeltaTime * 2f;
            float w = Mathf.Lerp(1f, 5f, (Mathf.Sin(t) + 1f) * 0.5f);
            ol.effectDistance = new Vector2(w, w);
            yield return null;
        }
    }
}
