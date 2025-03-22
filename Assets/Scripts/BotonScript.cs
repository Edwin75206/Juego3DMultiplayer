using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotonScript : MonoBehaviour
{
    public OcultaScript scriptOculta;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider pelota){
        if(pelota.CompareTag("Jugador")){
            scriptOculta.plataforma.SetActive(true);

        }
    }
}
