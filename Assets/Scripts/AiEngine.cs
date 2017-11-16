using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AiEngine : MonoBehaviour {

    public InsultEngine insultEngine;
    public List<string> usedDefenses;
    public List<string> usedInsults;

    /// <summary>
    /// Devuelve el insulto del enemigo (no usado anteriormente).</summary>
    /// <returns>Insulto del enemigo.</returns>
    public string GetEnemyInsult() {
        List<string> insultsLeft = new List<string>(insultEngine.GetInsults());
        insultsLeft = insultsLeft.Except(usedInsults).ToList();
        string insult = insultsLeft[Random.Range(0, insultsLeft.Count - 1)];
        usedInsults.Add(insult);
        return insult;
    }

    /// <summary>
    /// Devuelve la defensa del enemigo (no usada anteriormente) para un determinado insulto.</summary>
    /// <param name="insult">Insulto.</param>
    /// <returns>Defensa del enemigo.</returns>
    public string GetEnemyDefense(string insult) {
        string defense;
        string rightDefense = insultEngine.GetDefenseFor(insult);

        // 50% de posibilidades de acertar.
        bool enemyGuessRight = Mathf.RoundToInt(Random.value) != 0;
        if (enemyGuessRight) {
            defense = rightDefense;
        } else {
            List<string> defensesLeft = new List<string>(insultEngine.GetDefenses());
            defensesLeft.Remove(rightDefense);
            defensesLeft = defensesLeft.Except(usedDefenses).ToList();
            defense = defensesLeft[Random.Range(0, defensesLeft.Count - 1)];
        }

        usedDefenses.Add(defense);
        return defense;
    }
}

