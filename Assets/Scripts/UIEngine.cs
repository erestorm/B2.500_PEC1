using UnityEngine;
using UnityEngine.UI;

public class UIEngine : MonoBehaviour {

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

    public void ResetUI() {
        SetAvatarDialogue("");
        SetEnemyDialogue("");
        SetDialogueOptions(new Button[0]);
    }

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

    public void SetAvatarDialogue(string text) {
        avatarDialogue.text = text;
    }

    public void SetEnemyDialogue(string text) {
        enemyDialogue.text = text;
    }

    public void PlayFightSoundFx() {
        fightSoundFx.Play();
    }

    public void StopFightSoundFx() {
        fightSoundFx.Stop();
    }

    public void DisableDialogueOptions() {
        foreach (Transform optionChild in optionsParent) {
            optionChild.GetComponent<Button>().interactable = false;
        }
    }
}
