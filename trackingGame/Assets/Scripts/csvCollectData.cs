using System;
using System.Collections;
using System.Numerics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class csvCollectData : MonoBehaviour
{
    public Camera cam;
    public Target target;
    public Button button;
    ArrayList arrTarget = new ArrayList(); 
    ArrayList arrCam = new ArrayList();
    ArrayList degreeError = new ArrayList();
    TargetThread targetThread;
    Vector2 turn;
    int score= 0;
    float timer;
    private float _delay;
    string saveFilePath ="";
    void Start()
    {
        saveFilePath = Application.dataPath + "/../ParticipantData/";
        button.gameObject.SetActive(false);
        turn.x = target.transform.position.x;
        turn.y = target.transform.position.y;


    }

    // Update is called once per frame
    void Update()
    {

        turn.x += Input.GetAxis("Mouse X");
        turn.y += Input.GetAxis("Mouse Y");
        //Vector3D v1 = new Vector3D(target.transform.position.x, target.transform.position.y, target.transform.position.z);
        Vector3D v1 = new Vector3D(target.transform.localPosition.x, target.transform.localPosition.y, 0);
        Vector3D v2 = new Vector3D(turn.x, turn.y, 0);
        Vector2D vd1 = new Vector2D(target.transform.localPosition.x, target.transform.localPosition.y);
        Vector2D vd2 = new Vector2D(turn.x, turn.y);

        double angleDegrees = v2.AngleBetween(v1);
        double angleDegreesd = vd2.AngleBetween(vd1);

        rayCast(); 
        Vector3 cameraAngle = new Vector3(cam.transform.localEulerAngles.x, target.transform.position.y, target.transform.position.z);
        float DegreeE=  Mathf.Acos(dotProduct(Normalize(target.transform.position),Normalize(cameraAngle)));
        Debug.Log("target: " +target.transform.localPosition.x +" cursor :" + cam.transform.localEulerAngles.x);
        degreeError.Add(angleDegreesd);
        arrTarget.Add(target.transform.position);
        arrCam.Add(cam.transform.localEulerAngles);


        timer += Time.deltaTime;
        if (timer > _delay)
        {
            _delay += 5f;
        }
        if (this.score >= 500) gameOver();


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
                //Debug.Log("im a thread" + hit.transform.name);
                float DegreeE = Mathf.Acos(dotProduct(Normalize(target.transform.position), Normalize(cam.transform.localEulerAngles)));
                Debug.Log(DegreeE);
                degreeError.Add(0);
                this.score++;
            }

        }
    }

    public void savecsv()
    {

        targetThread = new TargetThread(this.arrTarget, this.arrCam, saveFilePath,this.degreeError);
        print(targetThread.IsAlive);
        targetThread.Start();
        targetThread.Join();
        print(targetThread.IsAlive);
        Debug.Log("saving data");
        SceneManager.LoadScene(0);
        


    }
    public  void gameOver()
    {
        button.gameObject.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public Vector3 Normalize(Vector3 v1)
    {
        Vector3 vector3 = new Vector3(0,0,0);   
        double magnitude = Math.Sqrt(v1.x * v1.x + v1.y * v1.y + v1.z * v1.z);
        double x;
        double y;
        double z;
        if (magnitude != 0)
        {
            x = v1.x / magnitude;
            y = v1.y / magnitude;
            z = v1.z / magnitude;
            Vector3 v3 = new Vector3((float)x, (float)y, (float)z);
            vector3 = v3;
        }
        
        return vector3;
    }

    public float dotProduct(Vector3 v1, Vector3 v2)
    {

        float result = Vector3.Dot(v1,v2);
        return result;
    }

}

