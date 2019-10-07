using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTestPlayer : MonoBehaviour
{
    GameManager gm;
    Camera cam;
    public float myHealt = 100.0f;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        cam = GetComponent<Camera>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("YEY");
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            Debug.DrawRay(cam.transform.position,ray.direction,Color.red,1.0f);
            if (Physics.Raycast(ray, out hit))
            {
                Transform objectHit = hit.transform;
                if (objectHit.CompareTag("Enemie"))
                {
                    Debug.Log(hit.transform.name);
                    objectHit.GetComponent<EnemieAgent>().TakeDamage(55.0f);
                }
            }
        }
    }

    public void TakeDMG(float f)
    {
        myHealt -= f;
        
        if (myHealt < 0)
        {
            Debug.Log("Me muri");
            gm.Fin();
            
        }
        else
        {
            //Debug.Log("Auch, siento que me queda el " + myHealt + "% de vida");
        }
    }
}
