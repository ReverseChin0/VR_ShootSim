using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject canvas;
    public Text txt;

    public int cont = 0;

    // Start is called before the first frame update
    void Start()
    {
        
        txt.text = "Puntuación Final: 0";
        canvas.SetActive(false);
    }

    public void SetCont()
    {
        cont += 1;
    }


    public void Fin()
    {
        canvas.SetActive(true);
        txt.text = "Puntuación Final: " + cont.ToString();
    }

}
