using UnityEngine;

public enum TutAction { Click, SelectElement, CombineMake, OpenScene, Wait }
[CreateAssetMenu(menuName = "SO/TutorialStep")]
public class TutorialStepSO : ScriptableObject
{
    [TextArea] public string message;
    public TutAction action;
    public GameObject target;          // 강조할 UI(버튼 등, 없어도 됨)
    public ElementSO element; public int count;  // SelectElement 용
    public MoleculeSO product;                 // CombineMake 용
    public string sceneName; public float waitSec;
}
