using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void NovaPartida()
    {
        SceneManager.LoadScene("Escena");
    }

    public void SortirJoc()
    {
        Application.Quit();
    }
}