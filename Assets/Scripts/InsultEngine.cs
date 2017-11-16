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

    /// <summary>
    /// Carga insultos de un fichero.</summary>
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
    /// Devuelve las defensas disponibles.</summary>
    /// <returns>Defensas disponibles.</returns>
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
    /// Devuelve los insultos disponibles.</summary>
    /// <returns>Insultos disponibles.</returns>
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
    /// Devuelve la defensa apropiada para un determinado insulto.</summary>
    /// <param name="insult">Insulto.</param>
    /// <returns>Defensa. Si el insulto no existe, devuelve un string vacío.</returns>
    public string GetDefenseFor(string insult) {
        string defenseFound;
        if (insultsDict.TryGetValue(insult, out defenseFound)) {
            return defenseFound;
        } else {
            return "";
        }
    }

    /// <summary>
    /// Comprueba si una defensa determinada es apropiada para un insulto dado.</summary>
    /// <param name="insult">Insulto.</param>
    /// <param name="defense">Defensa.</param>
    /// <returns><code>false</code> si el insulto no existe o la defensa no es la apropiada.
    /// <code>true</code> por el contrario.</returns>
    public bool Matches(string insult, string defense) {
        string defenseFound;
        return insultsDict.TryGetValue(insult, out defenseFound) &&  defense == defenseFound;
    }
}

