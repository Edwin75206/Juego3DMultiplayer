using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private PlayerScript playerScript;
    

    
    public int vidas = 3;
    public int monedas = 10;
    public float tiempo = 0;
    public bool isAlive = true;



    public AudioSource sonidoPerder;
    public AudioSource sonidoGanar;
    public AudioSource sonidoFondo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
