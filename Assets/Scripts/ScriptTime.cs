using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScriptTime : MonoBehaviour
{
    private GameManager gameManager;
    public Text textoTiempo;
    // Start is called before the first frame update
    void Start()
    {
        textoTiempo.text = "Tiempo: 0:00";
        gameManager = FindObjectOfType <GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isAlive){
            textoTiempo.text = "Tiempo: " + formatearTiempo();
        }
    }

    public string formatearTiempo(){
        if(gameManager.isAlive){
            gameManager.tiempo += Time.deltaTime;
        }
        string minutos = Mathf.Floor(gameManager.tiempo / 60).ToString("00");
        string segundos = Mathf.Floor(gameManager.tiempo % 60).ToString("00");
        return minutos + ":" + segundos;
    
    }
}
