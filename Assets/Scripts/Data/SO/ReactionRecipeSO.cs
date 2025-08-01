using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ReactionRecipeSO", menuName = "Scriptable Objects/ReactionRecipeSO")]
public class ReactionRecipeSO : ScriptableObject
{
    public List<ElementSO> inputElements; // �ԷµǴ� ���ҵ�
    public List<int> inputCounts; // �� ������ ����
    public MoleculeSO resultMolecule; // �����Ǵ� ����
}
