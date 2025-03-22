using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngineNetworking;
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


    // Start is called before the first frame update

    private string apiUrl = "";
    void Start()
    {
        StartCoroutine(LoginRequest());
    }

    public void RegistrarUsuario(){
        registrarCanvas.gameObject.SetActive(true);
        loginCanvas.gameObject.SetActive(false);
    }

    IEnumerator LoginRequest(){
        string jsonData = "{\"username\":\"" + userText.text "\",\"password\":\"" + passwordText.text "\"}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
