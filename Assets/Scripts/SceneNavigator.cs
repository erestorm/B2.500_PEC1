using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavigator : MonoBehaviour {

    public void StartGame() {
        SceneManager.LoadScene("Game");
    }

    public void ExitGame() {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }

    public void EndGame() {
        SceneManager.LoadScene("End");
    }
}
