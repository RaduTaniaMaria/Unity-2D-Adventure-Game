using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    // Variabila pentru panoul de instructiuni
    public GameObject panelInstruct;

    // Metoda Start - ascunde panoul la inceput
    void Start()
    {
        if (panelInstruct != null)
        {
            panelInstruct.SetActive(false);
        }
    }

    // Metoda pentru a incarca scena (Butonul JOACA)
    public void LanseazaScenaDinButon(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    // Metoda pentru a afisa instructiunile
    public void LoadPanelInstruct()
    {
        panelInstruct.SetActive(true);
    }

    // Metoda pentru butonul INAPOI
    public void Back()
    {
        panelInstruct.SetActive(false);
    }

    // Metoda IESIRE (O singura data, varianta corecta si completa)
    public void QuitGame()
    {
        Debug.Log("Jocul s-a inchis!");
        Application.Quit();

        // Aceasta parte face butonul sa mearga si in timp ce testezi in Unity Editor:
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}