using UnityEngine;
using UnityEngine.UI;

public class ShowResult : MonoBehaviour {

    public GameObject loseText;
    public GameObject winText;
    public AudioSource backgroundMusic;
    public AudioClip winMusic;
    public AudioClip loseMusic;
    public Image backgroundImage;
    public Sprite winBackground;
    public Sprite loseBackground;

	void Start () {
        loseText.SetActive(!GameplayManager.avatarWon);
        winText.SetActive(GameplayManager.avatarWon);
        
        if(GameplayManager.avatarWon) {
            backgroundMusic.clip = winMusic;
            backgroundImage.GetComponent<Image>().sprite = winBackground;

        } else {
            backgroundMusic.clip = loseMusic;
            backgroundImage.GetComponent<Image>().sprite = loseBackground;
        }

        backgroundMusic.Play();
	}
}
