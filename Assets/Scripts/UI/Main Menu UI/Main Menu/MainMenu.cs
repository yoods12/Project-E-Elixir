using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void OnClickNewGame()
    {
        SceneManager.LoadScene("Chemistry");
    }
    public void OnClickLoad()
    {
        Debug.Log("�ҷ�����");
    }
    public void OnClickSettings()
    {
        Debug.Log("�ɼ�");
    }
    public void OnClickQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}