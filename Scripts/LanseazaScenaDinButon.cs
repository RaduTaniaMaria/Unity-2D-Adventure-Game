using UnityEngine;
using UnityEngine.SceneManagement;

public class LanseazaScenaDinButon : MonoBehaviour
{
    // Aceasta este functia care va porni jocul
    public void SchimbaScena(int indexScena)
    {
        SceneManager.LoadScene(indexScena);
    }
}