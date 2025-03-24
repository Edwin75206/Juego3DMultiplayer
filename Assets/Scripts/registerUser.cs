using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class registerUser : MonoBehaviour
{
    public InputField firstNameText;
    public InputField lastNameText;
    public InputField emailText;
    public InputField telefonoText;
    public InputField userText;
    public InputField passwordText;
    public Text mensaje;
    public Canvas loginCanvas;
    public Canvas registrarCanvas;

    // URL de tu API para registrar jugador
    private string apiUrl = "https://juego3dmultiplayer.onrender.com/api/jugadores/jugador";

    public void OnRegisterButtonPressed()
    {
        StartCoroutine(RegisterRequest());
    }

    public void LoginUsuario()
    {
        loginCanvas.gameObject.SetActive(true);
        registrarCanvas.gameObject.SetActive(false);
    }

    IEnumerator RegisterRequest()
    {
        // Crear JSON con credenciales
        string jsonData = "{" +
            "\"nombre_jugador\":\"" + firstNameText.text + "\"," +
            "\"apellidos_jugador\":\"" + lastNameText.text + "\"," +
            "\"email\":\"" + emailText.text + "\"," +
            "\"phone\":\"" + telefonoText.text + "\"," +
            "\"username\":\"" + userText.text + "\"," +
            "\"password\":\"" + passwordText.text + "\"" +
        "}";
        byte[] jsonBytes = System.Text.Encoding.UTF8.GetBytes(jsonData);

        using (UnityWebRequest request = new UnityWebRequest(apiUrl, "POST"))
        {
            request.uploadHandler = new UploadHandlerRaw(jsonBytes);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                // Parsear la respuesta con la estructura que devuelve la API:
                string responseText = request.downloadHandler.text;
                JugadorResponse response = JsonUtility.FromJson<JugadorResponse>(responseText);

                // Guardar el id del jugador (por ejemplo, 7) en PlayerPrefs
                PlayerPrefs.SetInt("id_jugador", response.id_jugador);

                Debug.Log("Registro exitoso. ID del jugador: " + response.id_jugador);
                mensaje.text = "Registro exitoso";
                loginCanvas.gameObject.SetActive(false);

                // Limpiar campos
                firstNameText.text = "";
                lastNameText.text = "";
                emailText.text = "";
                telefonoText.text = "";
                userText.text = "";
                passwordText.text = "";
            }
            else
            {
                mensaje.text = "Error al registrar usuario";
                Debug.Log("Error en el registro de usuario: " + request.error);
                firstNameText.text = "";
                lastNameText.text = "";
                emailText.text = "";
                telefonoText.text = "";
                userText.text = "";
                passwordText.text = "";
            }
        }
    }

    [System.Serializable]
    public class JugadorResponse
    {
        public int id_jugador;
        public string message;
    }
}
