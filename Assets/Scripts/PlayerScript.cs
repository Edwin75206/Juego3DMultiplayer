using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private GameManager gameManager;
    private Rigidbody cuerpoPlayer;

    public float velocidad = 2;
    public float salto = 4;

    public Text textoVidas;
    public Text textoMonedas;
    public Text textoMensaje;

    public AudioSource sonidoMoneda;
    public AudioSource sonidoSaltar;
    public AudioSource sonidoVida;

    public Button button;

    public float moverHorizontal;
    public float moverVertical;

    public Canvas canvas;
    Vector3 posicionInicial;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType <GameManager>();
        textoVidas.text = "Vidas: " + gameManager.vidas;
        textoMonedas.text = "Monedas: " + gameManager.monedas;
        textoMensaje.text = "";
        cuerpoPlayer = GetComponent<Rigidbody>();
        posicionInicial = transform.position;
        button.gameObject.SetActive(false); // Apaga el botón
        gameManager.sonidoFondo.Play();
        canvas.gameObject.SetActive(false);

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate(){
        if(gameManager.vidas > 0 && gameManager.isAlive == true){
            moverJugador();
            if(transform.position.y < -1){
                quitarVida();
            }
        }
        else if (gameManager.vidas == 0 && gameManager.isAlive == true){
            gameManager.sonidoPerder.Play();
            gameManager.sonidoFondo.Stop();
            gameManager.isAlive = false;
            textoMensaje.text = "Game Over";
            button.gameObject.SetActive(true); // Prende el boton el botón

        }
        if(gameManager.monedas == 0 && gameManager.isAlive == true){
            gameManager.sonidoGanar.Play();
            gameManager.sonidoFondo.Stop();
            gameManager.isAlive = false ;
            textoMensaje.text = "Ganaste el juego"; 
            button.gameObject.SetActive(true); // Prende el boton el botón
        }
    }

    void moverJugador(){
                //capturar movimiento naranja horizontal
        float movimientoH = Input.GetAxis("Horizontal") + moverHorizontal;
        float movimientoV = Input.GetAxis("Vertical") + moverVertical;

        //capturamos la posicion en la variable movimiento
        Vector3 movimiento = new Vector3( movimientoV * velocidad, 0.0f, movimientoH * velocidad);

        cuerpoPlayer.AddForce(movimiento);
        if(Input.GetButton("Jump") && isSuelo()){
            cuerpoPlayer.velocity += Vector3.up*salto;
            sonidoSaltar.Play();
        }
    }


    public void Saltar(){
        if(isSuelo()){
            cuerpoPlayer.velocity += Vector3.up*salto;
            sonidoSaltar.Play();
        }
    }

    public void Avanzar(){
        moverVertical = 1;
    }

    public void Retroceder(){
        moverVertical = -1;
    }

    public void Derecha(){
        moverHorizontal = 1;
    }

    public void Izquierda(){
        moverHorizontal = -1;
    }

    public void Parar(){
        moverHorizontal = 0;
        moverVertical = 0;
    }

    private bool isSuelo(){
        Collider[] colisiones = Physics.OverlapSphere(transform.position, 0.5f);
        foreach (Collider colision in colisiones)
        {
            if(colision.tag == "Suelo"){
                return true;
            }
        }
        return false;
    }

    void quitarVida(){
        gameManager.vidas -- ;
        textoVidas.text = "Vidas: " + gameManager.vidas;
        transform.position = posicionInicial;
        sonidoVida.Play();
        cuerpoPlayer.velocity = Vector3.zero;
    }

     void OnTriggerEnter(Collider moneda){

        if(moneda.gameObject.CompareTag("Moneda")){
            moneda.gameObject.SetActive(false);
            gameManager.monedas --;
            sonidoMoneda.Play();
            textoMonedas.text = "Monedas: " + gameManager.monedas;
        }
    }

}
