using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class registerProgres : MonoBehaviour
{
    // En este ejemplo, seguimos usando los Text para 'time' y 'level' (o puedes tomar esos valores de otra fuente)
    public Text time;
    public Text level;
    
    private string apiUrl = "http://localhost:3000/api/jugadores/progreso";
    private GameManager gm;

    void Start()
    {
        // Se obtiene una referencia al GameManager para usar sus variables (vidas, monedas, etc.)
        gm = FindObjectOfType<GameManager>();
        if(gm == null)
        {
            Debug.LogError("No se encontró el GameManager en la escena.");
        }
    }
    
    // Método para extraer la parte numérica de una cadena.
    // Ejemplo: "Monedas: 1" => "1", "Vidas: 3" => "3"
    private string ExtractNumeric(string input)
    {
        if (input.Contains(":"))
        {
            int index = input.IndexOf(":");
            return input.Substring(index + 1).Trim();
        }
        return input.Trim();
    }
    
    public IEnumerator RegisterProgresoRequest()
    {
        // Recuperar el id del jugador registrado desde PlayerPrefs
        int numericPlayerId = PlayerPrefs.GetInt("id_jugador", 0);
        if(numericPlayerId == 0)
        {
            Debug.LogError("No se encontró el id del jugador en PlayerPrefs");
            yield break;
        }
        
        // Obtener los valores de score y lives desde el GameManager
        int numericScore = gm.monedas;  // Por ejemplo, monedas son el score.
        int numericLives = gm.vidas;
        
        // Convertir el campo time (ej. "00:24") a segundos usando el valor del Text (o puedes también obtenerlo de otra variable)
        int numericTime;
        string rawTime = ExtractNumeric(time.text);
        if (rawTime.Contains(":"))
        {
            string[] parts = rawTime.Split(':');
            if (parts.Length == 2 && int.TryParse(parts[0], out int minutes) && int.TryParse(parts[1], out int seconds))
            {
                numericTime = minutes * 60 + seconds;
            }
            else
            {
                Debug.LogError("Error convirtiendo el formato de time: " + rawTime);
                yield break;
            }
        }
        else
        {
            if (!int.TryParse(rawTime, out numericTime))
            {
                Debug.LogError("Error convirtiendo time a entero: " + rawTime);
                yield break;
            }
        }
        
        // Para el level, se lee del Text
        if (!int.TryParse(ExtractNumeric(level.text), out int numericLevel))
        {
            Debug.LogError("Error convirtiendo level a entero: " + level.text);
            yield break;
        }
        
        // Construir el JSON usando los valores numéricos obtenidos
        string jsonData = "{\"player_id\":" + numericPlayerId +
                          ",\"score\":" + numericScore +
                          ",\"lives\":" + numericLives +
                          ",\"time\":" + numericTime +
                          ",\"level\":" + numericLevel + "}";
                          
        Debug.Log("JSON Data: " + jsonData);
                          
        byte[] jsonBytes = System.Text.Encoding.UTF8.GetBytes(jsonData);
        
        using (UnityWebRequest request = new UnityWebRequest(apiUrl, "POST"))
        {
            request.uploadHandler = new UploadHandlerRaw(jsonBytes);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            
            yield return request.SendWebRequest();
            
            if (request.result == UnityWebRequest.Result.Success)
            {
                string responseText = request.downloadHandler.text;
                ProgresoResponse response = JsonUtility.FromJson<ProgresoResponse>(responseText);
                Debug.Log("Registro de progreso exitoso: " + response.message + " con ID: " + response.id);
            }
            else
            {
                Debug.Log("Error en el registro de progreso: " + request.error);
                Debug.Log("Respuesta del servidor: " + request.downloadHandler.text);
            }
        }
    }
    
    [System.Serializable]
    public class ProgresoResponse
    {
        public int id;
        public string message;
    }
}
