using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void ChooseLevel()
    {
        SceneManager.LoadScene("FirstLevel");
    }

    public void QuitGame()
    {
        Debug.Log("Выход из игры");
        Application.Quit();
    }
}
