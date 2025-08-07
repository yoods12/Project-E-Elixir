using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance { get; private set; }
    private string lastScene;

    private void Awake()
    {
        // �̱���
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
    public void LoadChemistryScene() => SceneManager.LoadScene("Chemistry");
    public void LoadCraftingScene() => SceneManager.LoadScene("Crafting");
    public void LoadResultScene() => SceneManager.LoadScene("Result");
    // Dictionary �� �� ��, ȣ�� ������ ���� ���� ����
    public void LoadDictionary()
    {
        lastScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("Dictionary");
    }

    // �ڷ� ����
    public void LoadPrevious()
    {
        if (!string.IsNullOrEmpty(lastScene))
            SceneManager.LoadScene(lastScene);
        else
            LoadChemistryScene();  // ������ġ: ���� ������ ������ ��������
    }
}
