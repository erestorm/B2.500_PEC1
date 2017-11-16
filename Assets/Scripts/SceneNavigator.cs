using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavigator : MonoBehaviour {

    /// <summary>
    /// Carga la escena de juego.</summary>
    public void StartGame() {
        SceneManager.LoadScene("Game");
    }

    /// <summary>
    /// Sale del juego.</summary>
    public void ExitGame() {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }

    /// <summary>
    /// Carga la escena final.</summary>
    public void EndGame() {
        SceneManager.LoadScene("End");
    }
}
