using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraScript : MonoBehaviour
{
    public GameObject jugador;
    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - jugador.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate(){
        transform.position = jugador.transform.position + offset;
    }
}
