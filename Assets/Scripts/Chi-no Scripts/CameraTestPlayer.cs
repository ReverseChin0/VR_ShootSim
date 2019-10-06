using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTestPlayer : MonoBehaviour
{
    Camera cam;

    // Start is called before the first frame update
    void Start()
    {
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
                Debug.Log(hit.transform.name);
                Transform objectHit = hit.transform;
                if (objectHit.CompareTag("Enemie"))
                {
                    EnemieAgent agent = objectHit.GetComponent<EnemieAgent>();
                    agent.TakeDamage(55.0f);
                    print(agent.name + ": Ah fuck! ");
                }
            }
        }
    }
}
