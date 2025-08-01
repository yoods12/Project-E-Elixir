using UnityEngine;
using UnityEngine.UI;

public class SceneButton : MonoBehaviour
{
    public string sceneName;          // 인스펙터에 씬 이름 입력
    void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() =>
          SceneLoader.Instance.LoadScene(sceneName)
        );
    }
}
