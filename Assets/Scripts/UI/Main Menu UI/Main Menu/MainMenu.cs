using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button continueButton;
    [SerializeField] private QuestData[] allQuests;  // SO ���� 32��

    private void Awake()
    {
        Debug.Log($"[MainMenu] newGameButton={newGameButton}, continueButton={continueButton}, SaveManager.Instance={(SaveManager.Instance == null ? "NULL" : "OK")}");

        newGameButton.onClick.RemoveAllListeners();
        newGameButton.onClick.AddListener(OnNewGame);

        continueButton.onClick.RemoveAllListeners();
        continueButton.onClick.AddListener(OnContinue);

        // ����� Ű�� ������ Continue ��Ȱ��
        continueButton.interactable = SaveManager.Instance.HasSave();
    }
    private void OnNewGame()
    {
        // 1) ��� ����Ʈ Ŭ���� �÷��� �ʱ�ȭ
        SaveManager.Instance.ResetAllQuests(allQuests);
        // 2) ���� Ű ����
        SaveManager.Instance.DeleteSave();
        // 3) DayManager �ʱ�ȭ (1��������)
        DayManager.Instance.StartNewGame();
        // 4) Chemistry ������ �̵�
        SceneLoader.Instance.LoadChemistryScene();


    }
    private void OnContinue()
    {
        // 1) ����� ���� �ҷ��ͼ� DayManager�� ����
        int level = SaveManager.Instance.LoadLevel();
        DayManager.Instance.StartContinue(level);
        // 2) Chemistry ������ �̵�
        SceneLoader.Instance.LoadChemistryScene();
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