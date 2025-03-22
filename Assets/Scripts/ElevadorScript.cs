using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevadorScript : MonoBehaviour
{

    public float velocidad = 3;
    private Vector3 direccion = Vector3.up;
    private int limiteSuperior = 4;
    private int limiteInferior = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y >= limiteSuperior){
            direccion = Vector3.down;
        }
        if (transform.position.y <= limiteInferior){
            direccion = Vector3.up;
        }
        transform.Translate(direccion * Time.deltaTime * velocidad); 
    }
}
