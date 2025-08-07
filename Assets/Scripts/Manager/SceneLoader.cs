using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance { get; private set; }
    private string lastScene;

    private void Awake()
    {
        // 싱글톤
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
    // Dictionary 로 갈 때, 호출 직전에 현재 씬을 저장
    public void LoadDictionary()
    {
        lastScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("Dictionary");
    }

    // 뒤로 가기
    public void LoadPrevious()
    {
        if (!string.IsNullOrEmpty(lastScene))
            SceneManager.LoadScene(lastScene);
        else
            LoadChemistryScene();  // 안전장치: 이전 정보가 없으면 메인으로
    }
}
