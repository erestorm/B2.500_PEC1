using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class InsultEngine : MonoBehaviour {

    private Dictionary<string, string> insultsDict = new Dictionary<string, string>();

    [System.Serializable]
    private class InsultData {
        public InsultItem[] insults;
    }

    [System.Serializable]
    private class InsultItem {
        public string insult;
        public string defense;
    }

    void Awake() {
        // Inicializa los insultos (en Awake, porque lo necesita GameplayManager.Start)
        LoadInsults();
    }

    private void LoadInsults() {
        string filePath = Path.Combine(Application.streamingAssetsPath, "insults.json");

        if (File.Exists(filePath)) {
            string dataAsJson = File.ReadAllText(filePath);
            InsultData loadedData = JsonUtility.FromJson<InsultData>(dataAsJson);

            foreach (InsultItem insultItem in loadedData.insults) {
                insultsDict.Add(insultItem.insult, insultItem.defense);
            }

            Debug.Log("Data loaded, dictionary contains: " + insultsDict.Count + " entries");
        } else {
            Debug.LogError("Cannot find file!");
        }
    }

    /// <summary>
    /// Gets a random insult.</summary>
    /// <returns>A random insult string.</returns>
    public string GetRandomInsult() {
        return GetInsults()[Random.Range(0, insultsDict.Count - 1)];
    }

    /// <summary>
    /// Gets a random defense.</summary>
    /// <returns>A random defense string.</returns>
    public string GetRandomDefense() {
        return GetDefenses()[Random.Range(0, insultsDict.Count - 1)];
    }

    /// <summary>
    /// Gets available defenses.</summary>
    /// <returns>Array of defense strings.</returns>
    public string[] GetDefenses() {
        string[] defenses = new string[insultsDict.Count + 3];
        int i = 0;
        foreach (string defense in insultsDict.Values) {
            defenses[i] = defense;
            i++;
        }
        // Añadimos 3 defensas erróneas
        defenses[i] = "Yo soy cola, tú pegamento...";
        defenses[i+1] = "Tú más...";
        defenses[i+2] = "¿Ah sí?";

        return defenses;
    }

    /// <summary>
    /// Gets available insults.</summary>
    /// <returns>Array of insult strings.</returns>
    public string[] GetInsults() {
        string[] insults = new string[insultsDict.Count];
        int i = 0;
        foreach (string insult in insultsDict.Keys) {
            insults[i] = insult;
            i++;
        }

        return insults;
    }

    /// <summary>
    /// Checks if defense matches the insult.</summary>
    /// <param name="insult"> Insult string.</param>
    /// <param name="defense"> Defense string.</param>
    /// <returns><code>false</code> if insult does not exist or defense does not match insult.
    /// <code>true</code> otherwise.</returns>
    public bool Matches(string insult, string defense) {
        string defenseFound;
        return insultsDict.TryGetValue(insult, out defenseFound) &&  defense == defenseFound;
    }
}

