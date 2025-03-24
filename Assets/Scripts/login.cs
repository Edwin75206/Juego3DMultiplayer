using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Photon.Pun.Demo.Cockpit;

public class login : MonoBehaviour
{
    public InputField userText;
    public InputField passwordText;
    public Canvas loginCanvas;
    public Canvas registrarCanvas;
    public Text errorMensaje;
    public Text userInfoText;
    public Text userId;

    private PlayerScript player;
    private string apiUrl = "https://juego3dmultiplayer.onrender.com/api/jugadores/login";

    void Start()
    {
        registrarCanvas.gameObject.SetActive(false);
        player = FindObjectOfType<PlayerScript>();
    }

    public void OnLoginButtonPressed()
    {
        StartCoroutine(LoginRequest());
    }

    public void RegistrarUsuario()
    {
        registrarCanvas.gameObject.SetActive(true);
        loginCanvas.gameObject.SetActive(false);
    }

    IEnumerator LoginRequest()
    {
        // Crear JSON con credenciales
        string jsonData = "{\"username\":\"" + userText.text + "\",\"password\":\"" + passwordText.text + "\"}";
        byte[] jsonBytes = System.Text.Encoding.UTF8.GetBytes(jsonData);

        using (UnityWebRequest request = new UnityWebRequest(apiUrl, "POST"))
        {
            request.uploadHandler = new UploadHandlerRaw(jsonBytes);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                // Depuración: imprime la respuesta completa
                string responseText = request.downloadHandler.text;
                Debug.Log("Respuesta del servidor: " + responseText);

                // Parsear la respuesta
                TokenResponse response = JsonUtility.FromJson<TokenResponse>(responseText);

                // Guardar token si lo necesitas
                PlayerPrefs.SetString("jwt_token", response.token);

                // Cerrar canvas de login
                loginCanvas.gameObject.SetActive(false);

                // Limpiar los campos
                userText.text = "";
                passwordText.text = "";

                // Marcar al jugador como logueado
                if (player != null)
                {
                    player.login = true;
                }

                // Mostrar datos en la UI
                userInfoText.text = response.username;
                userId.text = response.idJugador.ToString();

                // Opcional: guardar el ID para usarlo en otro script
                PlayerPrefs.SetInt("id_jugador", response.idJugador);
                PlayerPrefs.SetString("username", response.username);
            }
            else
            {
                errorMensaje.text = "Error credenciales incorrectas: " + request.error;
                userText.text = "";
                passwordText.text = "";
            }
        }
    }

    [System.Serializable]
    public class TokenResponse
    {
        public string token;
        public int idJugador;
        public string username;
    }
}
