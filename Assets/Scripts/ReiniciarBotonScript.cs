using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReiniciarBotonScript : MonoBehaviour
{

    public void Restart(){
        SceneManager.LoadScene("SampleScene");
    }    
}
