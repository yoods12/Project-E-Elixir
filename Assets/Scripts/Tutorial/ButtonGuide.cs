using UnityEngine;
using UnityEngine.UI;

public class ButtonGuide : MonoBehaviour
{
    public RectTransform targetButton;
    public RectTransform guideArrow; // 화살표 이미지

    void Update()
    {
        if (targetButton == null) return;
        // 화살표를 버튼 위로 위치
        guideArrow.position = targetButton.position + Vector3.up * 60f;

        // 위아래 흔들기
        float y = Mathf.Sin(Time.unscaledTime * 4f) * 10f;
        guideArrow.anchoredPosition += new Vector2(0, y);
    }
}
