using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ReactionRecipeSO", menuName = "Scriptable Objects/ReactionRecipeSO")]
public class ReactionRecipeSO : ScriptableObject
{
    public List<ElementSO> inputElements; // 입력되는 원소들
    public List<int> inputCounts; // 각 원소의 개수
    public MoleculeSO resultMolecule; // 생성되는 분자
}
