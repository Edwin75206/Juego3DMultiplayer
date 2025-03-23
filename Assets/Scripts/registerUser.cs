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

    // Start is called before the first frame update
    private string apiUrl = "http://127.0.0.1:3000/api/jugadores/jugador";

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
        string jsonData = "{\"first_name\":\"" + firstNameText.text + "\",\"last_name\":\"" + lastNameText.text + "\",\"email\":\"" + emailText.text + "\",\"phone\":\"" + telefonoText.text + "\",\"username\":\"" + userText.text + "\",\"password\":\"" + passwordText.text + "\"}";
        byte[] jsonBytes = System.Text.Encoding.UTF8.GetBytes(jsonData);

        using (UnityWebRequest request = new UnityWebRequest(apiUrl, "POST"))
        {
            request.uploadHandler = new UploadHandlerRaw(jsonBytes);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                // Extraer el token del JSON de respuesta
                string responseText = request.downloadHandler.text;
                TokenResponse response = JsonUtility.FromJson<TokenResponse>(responseText);
                PlayerPrefs.SetString("jwt_token", response.token); // Guardar el token
                //Debug.Log("Login exitoso");
                mensaje.text = "Registro exitoso";
                loginCanvas.gameObject.SetActive(false);
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
                //Debug.Log("Error credenciales incorrectas: " + request.error);
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
    public class TokenResponse
    {
        public string token;
    }
}
