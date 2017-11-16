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

    private void InitInsults() {
        // TODO: Leer de fichero JSON o XML
        insultsDict.Add("Mi lengua es más hábil que cualquier espada.", "Primero deberías dejar de usarla como un plumero.");
        insultsDict.Add("¡Ordeñaré hasta la última gota de sangre de tu cuerpo!", "Qué apropiado, tú peleas como una vaca.");
        insultsDict.Add("Ya no hay técnicas que te puedan salvar.", "Sí que las hay, sólo que nunca las has aprendido.");
        insultsDict.Add("Ahora entiendo lo que significan basura y estupidez.", "Me alegra que asistieras a tu reunión familiar diaria.");
        insultsDict.Add("¡Eres como un dolor en la parte baja de la espalda!", "Ya te están fastidiando otra vez las almorranas, ¿Eh?");
        insultsDict.Add("Mi nombre es temido en cada sucio rincón de esta isla.", "Ah, ¿Ya has obtenido ese trabajo de barrendero?");
        insultsDict.Add("Hoy te tengo preparada una larga y dura lección.", "Y yo tengo un SALUDO para ti, ¿Te enteras?");
        insultsDict.Add("Espero que tengas un barco para una rápida huida.", "¿Por qué? ¿Acaso querías pedir uno prestado?");
        insultsDict.Add("Sólo he conocido a uno tan cobarde como tú.", "Te habrá enseñado todo lo que sabes.");
        insultsDict.Add("Nunca me verán luchar tan mal como tú lo haces.", "¿TAN rápido corres?");
        insultsDict.Add("Si tu hermano es como tú, mejor casarse con un cerdo.", "Me haces pensar que alguien ya lo ha hecho.");
        insultsDict.Add("Cada palabra que sale de tu boca es una estupidez.", "Quería asegurarme de que estuvieras a gusto conmigo.");
        insultsDict.Add("Mi espada es famosa en todo el Caribe.", "Qué pena me da que nadie haya oído hablar de ti.");
        insultsDict.Add("Mis enemigos más sabios corren al verme llegar .", "¿Incluso antes de que huelan tu aliento?");
        insultsDict.Add("¡Tengo el coraje y la técnica de un maestro!", "Estaría acabado si la usases alguna vez.");
        insultsDict.Add("¡En mi última pelea terminé con la manos llenas de sangre!", "Espero que ya hayas aprendido a no tocarte la nariz.");
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

