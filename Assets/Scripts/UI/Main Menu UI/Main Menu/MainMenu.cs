using JetBrains.Annotations;
using System;
using System.Collections;
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
        int level = SaveManager.Instance.LoadLevel();
        int day = SaveManager.Instance.LoadDay();
        DayManager.Instance.StartContinue(level, day);
        SceneLoader.Instance.LoadChemistryScene();
    }
    public void OnClickSound()
    {
        FindObjectOfType<SFXPlayer>().PlaySFX(0);
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