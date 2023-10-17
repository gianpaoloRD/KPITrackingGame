using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MouseMovement : MonoBehaviour
{
    public Vector2 turn;
    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        transform.localRotation = Quaternion.Euler(0, 0, 0);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        cam = GetComponent<Camera>();
    }


    // Update is called once per frame
    void Update()
    {
        
        moveCursor();
        //rayCast();

    }

    void moveCursor()
    {
        turn.x += Input.GetAxis("Mouse X");
        turn.y += Input.GetAxis("Mouse Y");
        // Debug.Log(turn.y);
        if (turn.y >= 70)
        {
            //Debug.Log(transform.localEulerAngles.x);
            turn.y = 70;
            transform.localRotation = Quaternion.Euler(-turn.y, turn.x, 0);


        }
        else if (turn.y <= -50)
        {
            //Debug.Log(transform.localEulerAngles.x);
            turn.y = -50;
            transform.localRotation = Quaternion.Euler(-turn.y, turn.x, 0);


        }
        else
        {
            transform.localRotation = Quaternion.Euler(-turn.y, turn.x, 0);
        }
    }

    void rayCast()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Target target = hit.collider.gameObject.GetComponent<Target>();
            if (target != null)
            {
                //print("I'm looking at " + hit.transform.name);
            }

        }
    }
}

