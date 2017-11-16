using UnityEngine;
using UnityEngine.UI;

public class UiEngine : MonoBehaviour {

    public Text avatarDialogue;
    public Text enemyDialogue;
    public AudioSource fightSoundFx;
    public Transform optionsParent;
    public GameObject pauseMenu;
    public AudioSource backgroundMusic;

    void Update() {
        if (Input.GetKeyUp(KeyCode.Escape)) {
            pauseMenu.SetActive(!pauseMenu.activeSelf);
            if (pauseMenu.activeSelf) {
                // Pausa la música
                backgroundMusic.Pause();
            } else {
                // Reinicia la música
                backgroundMusic.UnPause();
            }
        }
    }

    /// <summary>
    /// Resetea la UI.</summary>
    public void ResetUI() {
        SetAvatarDialogue("");
        SetEnemyDialogue("");
        SetDialogueOptions(new Button[0]);
    }

    /// <summary>
    /// Muestra nuevos botones de diálogo.</summary>
    /// <param name="options">Los botones de diálogo.</param>
    public void SetDialogueOptions(Button[] options) {
        // Destruye botones previos
        foreach (Transform optionChild in optionsParent) {
            Destroy(optionChild.gameObject);
        }

        // Añade nuevos botones
        float y = -30;
        foreach (Button option in options) {
            option.GetComponent<RectTransform>().SetParent(optionsParent, false);
            option.GetComponent<RectTransform>().localPosition = new Vector3(option.GetComponent<RectTransform>().localPosition.x, y, 0);
            y += option.GetComponent<RectTransform>().rect.y * 2.25f; // separados verticalmente por 25 % de su altura
        }
    }

    /// <summary>
    /// Muestra el texto de diálogo del avatar.</summary>
    public void SetAvatarDialogue(string text) {
        avatarDialogue.text = text;
    }

    /// <summary>
    /// Muestra el texto de diálogo del enemigo.</summary>
    public void SetEnemyDialogue(string text) {
        enemyDialogue.text = text;
    }

    /// <summary>
    /// Reproduce el efecto de sonido de lucha.</summary>
    public void PlayFightSoundFx() {
        fightSoundFx.Play();
    }

    /// <summary>
    /// Detiene el efecto de sonido de lucha.</summary>
    public void StopFightSoundFx() {
        fightSoundFx.Stop();
    }

    /// <summary>
    /// Deshabilita las opciones de diálogo.</summary>
    public void DisableDialogueOptions() {
        foreach (Transform optionChild in optionsParent) {
            optionChild.GetComponent<Button>().interactable = false;
        }
    }
}
