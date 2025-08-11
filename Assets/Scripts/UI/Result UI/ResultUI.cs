using UnityEngine;

public class ResultUI : MonoBehaviour
{
    public void OnClickSound()
    {
        FindObjectOfType<SFXPlayer>().PlaySFX(0);
    }
}
