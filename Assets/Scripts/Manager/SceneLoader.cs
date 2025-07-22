using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadChemistryScene(string Chemistry)
    {
        SceneManager.LoadScene("Chemistry");
    }
    public void LoadCraftingScene(string Crafting)
    {
        SceneManager.LoadScene("Crafting");
    }
}
