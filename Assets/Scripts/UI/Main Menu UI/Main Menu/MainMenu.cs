using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button continueButton;
    [SerializeField] private QuestData[] allQuests;  // SO 에셋 32개

    private void Awake()
    {
        Debug.Log($"[MainMenu] newGameButton={newGameButton}, continueButton={continueButton}, SaveManager.Instance={(SaveManager.Instance == null ? "NULL" : "OK")}");

        newGameButton.onClick.RemoveAllListeners();
        newGameButton.onClick.AddListener(OnNewGame);

        continueButton.onClick.RemoveAllListeners();
        continueButton.onClick.AddListener(OnContinue);

        // 저장된 키가 없으면 Continue 비활성
        continueButton.interactable = SaveManager.Instance.HasSave();
    }
    private void OnNewGame()
    {
        // 1) 모든 퀘스트 클리어 플래그 초기화
        SaveManager.Instance.ResetAllQuests(allQuests);
        // 2) 저장 키 삭제
        SaveManager.Instance.DeleteSave();
        // 3) DayManager 초기화 (1레벨부터)
        DayManager.Instance.StartNewGame();
        // 4) Chemistry 씬으로 이동
        SceneLoader.Instance.LoadChemistryScene();


    }
    private void OnContinue()
    {
        // 1) 저장된 레벨 불러와서 DayManager에 전달
        int level = SaveManager.Instance.LoadLevel();
        DayManager.Instance.StartContinue(level);
        // 2) Chemistry 씬으로 이동
        SceneLoader.Instance.LoadChemistryScene();
    }

    public void OnClickSettings()
    {
        Debug.Log("옵션");
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