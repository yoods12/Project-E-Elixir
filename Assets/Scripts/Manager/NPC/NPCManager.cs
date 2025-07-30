using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    [SerializeField] private Transform npcContainer; // �ڽ����� Instantiate
    private List<GameObject> spawned = new List<GameObject>();

    public void SpawnForDay(NPCData[] npcs)
    {
        foreach (var go in spawned) Destroy(go);
        spawned.Clear();

        foreach(var npcPrefab in npcs)
        {
            var go = Instantiate(npcPrefab.gameObject, npcContainer);
            go.SetActive(true);
            spawned.Add(go);
        }
    }
}
