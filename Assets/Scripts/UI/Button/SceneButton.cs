using UnityEngine;
using UnityEngine.UI;

public class SceneButton : MonoBehaviour
{
    public string sceneName;          // �ν����Ϳ� �� �̸� �Է�
    void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() =>
          SceneLoader.Instance.LoadScene(sceneName)
        );
    }
}
