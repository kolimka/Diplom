using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void RestartGame()
    {
        // Добавьте здесь код для перезапуска игры, например, загрузку сцены или сброс текущего уровня.
        SceneManager.LoadScene("FirstLevel"); // Загрузка уровня по его имени.
    }
}
