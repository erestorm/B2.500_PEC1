using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class GameplayManager : MonoBehaviour {
    public static bool avatarWon = false;

    public int roundsToWin = 3;
    public float dialogueDelayInSecs = 2;
    public Button optionButtonPrefab;
    public InsultEngine insultEngine;
    public UIEngine uiEngine;
    public SceneNavigator sceneNavigator;
    private Player avatar = new Player();
    private Player enemy = new Player();

    private class Player {
        public int wins = 0;
        public string insult = "";
        public string defense = "";
    }

    void Start() {
        uiEngine.ResetUI();

        // Siempre empieza el enemigo
        EnemyInsults();
    }

    void Wait(float seconds, Action action) {
        StartCoroutine(WaitCoroutine(seconds, action));
    }

    IEnumerator WaitCoroutine(float seconds, Action action) {
        yield return new WaitForSeconds(seconds);
        action();
    }

    void EnemyInsults() {
        enemy.insult = insultEngine.GetRandomInsult();
        uiEngine.SetEnemyDialogue(enemy.insult);
        uiEngine.SetAvatarDialogue("");

        Wait(dialogueDelayInSecs, () => { AvatarPreparesToDefend(); });
    }

    void EnemyDefends() {
        enemy.defense = insultEngine.GetRandomDefense();
        uiEngine.SetEnemyDialogue(enemy.defense);

        uiEngine.PlayFightSoundFx();
        Wait(dialogueDelayInSecs, () => {
            uiEngine.StopFightSoundFx();
            if (insultEngine.Matches(avatar.insult, enemy.defense)) {
                enemy.wins++;
                if (FightIsOver()) {
                    GameOver();
                } else {
                    EnemyInsults();
                }
            } else {
                avatar.wins++;
                if (FightIsOver()) {
                    GameOver();
                } else {
                    AvatarPreparesToInsult();
                }
            }
        });
    }

    void AvatarPreparesToInsult() {
        Button[] insultButtons = new Button[insultEngine.GetInsults().Length];
        int i = 0;
        foreach (string insult in insultEngine.GetInsults()) {
            Button optionButton = Instantiate(optionButtonPrefab);
            optionButton.GetComponentInChildren<Text>().text = insult;
            optionButton.onClick.AddListener(() => {
                uiEngine.DisableDialogueOptions();
                AvatarInsults(insult);
            });
            insultButtons[i] = optionButton;
            i++;
        }
        uiEngine.SetDialogueOptions(insultButtons);
    }

    void AvatarInsults(string insult) {
        avatar.insult = insult;
        uiEngine.SetAvatarDialogue(avatar.insult);
        uiEngine.SetEnemyDialogue("");

        Wait(dialogueDelayInSecs, () => { EnemyDefends(); });
    }

    void AvatarPreparesToDefend() {
        Button[] defenseButtons = new Button[insultEngine.GetDefenses().Length];
        int i = 0;
        foreach (string defense in insultEngine.GetDefenses()) {
            Button optionButton = Instantiate(optionButtonPrefab);
            optionButton.GetComponentInChildren<Text>().text = defense;
            optionButton.onClick.AddListener(() => {
                uiEngine.DisableDialogueOptions();
                AvatarDefends(defense);
            });
            defenseButtons[i] = optionButton;
            i++;
        }
        uiEngine.SetDialogueOptions(defenseButtons);
    }

    void AvatarDefends(string defense) {
        avatar.defense = defense;
        uiEngine.SetAvatarDialogue(avatar.defense);

        uiEngine.PlayFightSoundFx();
        Wait(dialogueDelayInSecs, () => {
            uiEngine.StopFightSoundFx();
            if (insultEngine.Matches(enemy.insult, avatar.defense)) {
                avatar.wins++;
                if (FightIsOver()) {
                    GameOver();
                } else {
                    AvatarPreparesToInsult();
                }
            } else {
                enemy.wins++;
                if (FightIsOver()) {
                    GameOver();
                } else {
                    EnemyInsults();
                }
            }
        });
    }

    bool FightIsOver() {
        return avatar.wins == roundsToWin || enemy.wins == roundsToWin;
    }

    void GameOver() {
        if(avatar.wins > enemy.wins) {
            avatarWon = true;
        } else if (avatar.wins < enemy.wins) {
            avatarWon = false;
        } else {
            Debug.LogError("Las reglas del juego no permiten empate. Revisar implementación.");
        }

        sceneNavigator.EndGame();
    }
}
