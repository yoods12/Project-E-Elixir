using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "QuestData", menuName = "Scriptable Objects/Quest")]
public class QuestData : ScriptableObject
{
    public int level; // ����Ʈ ����
    public string NpcName; // ����Ʈ�� �ִ� NPC �̸�
    public Sprite NpcIcon; // Npc ������
    public bool isCompleted; // ����Ʈ �Ϸ� ����

    public List<string> hint; // ��Ʈ
    public List<MoleculeSO> requiredMolecules; // �䱸 ����
    public List<ElementSO> requiredElements; // �䱸 ����

    public int goldReward; // ��� ����

    public bool isMandatory; // �ʼ� ����Ʈ ����

}
