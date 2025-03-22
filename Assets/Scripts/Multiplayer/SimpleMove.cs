using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class SimpleMove : MonoBehaviourPunCallbacks
{
    public float velocidad = 5f;
    public float velocidadRotacion = 200f;
    public float fuerzaSalto = 7f;
    private bool enSuelo = true;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            Movimiento();
            Rotacion();
        }
    }

    void Movimiento()
    {
        float movimientoHorizontal = Input.GetAxis("Horizontal");
        float movimientoVertical = Input.GetAxis("Vertical");

        Vector3 desplazamiento = new Vector3(movimientoHorizontal, 0, movimientoVertical) * velocidad * Time.deltaTime;
        transform.Translate(desplazamiento, Space.Self);
    }

    void Rotacion()
    {
        float rotacion = Input.GetAxis("Mouse X") * velocidadRotacion * Time.deltaTime;
        transform.Rotate(0, rotacion, 0);
    }
}
