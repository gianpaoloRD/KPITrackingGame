using System;
using System.Collections;
using System.Numerics;
using TMPro;
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
    double pira = 0;
    string saveFilePath ="";
    private float previousAngle = 0.0f;
    private float previousAngle2 = 0.0f;
    bool sing = false;
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

        double radians = target.angle; // Replace this with the radians value you want to convert
        double degrees = radians * (180 / Math.PI);
        
        if (degrees > 0) { degrees = -degrees+360; }
        if (degrees < 0) { degrees = degrees*-1 ; }

       // print(degrees);
        // Calculate degree error with a 90-degree offset
        double degreeErrors = Math.Abs((cam.transform.eulerAngles.y - degrees - 90) % 360);
        double degreeErrors2 = Math.Abs((90+degrees)-cam.transform.eulerAngles.y);

        if (degreeErrors2 > 180 && degrees >0) { Math.Abs(degreeErrors2 -= 360); }
        //if (degreeErrors <= 90 ) degreeError.Add(degreeErrors);print("result:" + degreeErrors); 
        //if (degreeErrors2 >= -90 && degreeErrors2 <= 90) {degreeError.Add(degreeErrors2);  }
        //print(degreeErrors2);
        double direc = DetermineDirectionAndPrintAngles((float)degrees);
        double cursor = DetermineDirectionAndPrintAngles2(cam.transform.eulerAngles.y) ;
        //print(Math.Abs((float)degreeErrors2));

        /* 
         * 
         
                if (direc > 0 && cursor > 0 && Math.Abs((float)degreeErrors2) < 1 ||
            direc < 0 && cursor > 0 && Math.Abs((float)degreeErrors2) < 1) { //derecha
            
            sing = true;
        }
        else if ((direc < 0  && cursor < 0 && Math.Abs((float)degreeErrors2) < 1) ||
             direc > 0   && cursor < 0 && Math.Abs((float)degreeErrors2) < 1) {//izquierda

            sing = false;
        }


        if(sing)
        {
           print("derecha: " + -Math.Abs(degreeErrors2));
        }
        else
        {
            print("izquierda: " + Math.Abs(degreeErrors2));
        } 
         
         
         */
        //print("DegreeError: " +Math.Abs((float)degreeErrors2));



        //print(degreeErrors2);
        rayCast(); //print("result:" + degreeErrors);print("result2:" + degreeErrors2);
        //Vector3 cameraAngle = new Vector3(cam.transform.localEulerAngles.x, target.transform.position.y, target.transform.position.z);
        //float DegreeE=  Mathf.Acos(dotProduct(Normalize(target.transform.position),Normalize(cameraAngle)));
        //Debug.Log("target: " +target.transform.localPosition.x +" cursor :" + cam.transform.localEulerAngles.x);
        arrTarget.Add(target.transform.position);
        arrCam.Add(cam.transform.localEulerAngles);
        degreeError.Add(degreeErrors2);








        Vector3 directionToTarget = target.transform.position- cam.transform.position;
        // Dirección actual de la cámara (supongamos que mira en la dirección positiva del eje X en el plano XY).
        Vector3 cameraDirection = Vector3.right;

        // Calcula el ángulo en grados entre la dirección de la cámara y la dirección al target.
        float angleError = Vector3.SignedAngle(cameraDirection, directionToTarget, Vector3.up);

        //print(angleError +"targettt");

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
    public double DetermineDirectionAndPrintAngles(float degre)
    {
            double direction = 0;

            float referenceAngle = 0.0f; // Choose your reference angle (in degrees)
            float currentAngle = degre; // Get the current y-rotation angle of the camera

            // Calculate the change in angle
            double angleChange = currentAngle - previousAngle;
            //print(angleChange);
            // Determine the direction of movement
            //print(angleChange);
            if (angleChange < 0.0f)
                direction = angleChange;
            else if (angleChange > 0.0f)
                direction = angleChange;
            else direction = 0;

            //Debug.Log("Reference Angle: " + referenceAngle + " degrees");
            //Debug.Log("Current Angle: " + currentAngle + " degrees");
            //print("Direction of Movement: " + direction.ToString());

            previousAngle = currentAngle; // Update the previous angle for the next frame
            //print(direction);
            return direction;
        
    }
    public double DetermineDirectionAndPrintAngles2(float degre)
    {
        double direction = 0;

        float referenceAngle = 0.0f; // Choose your reference angle (in degrees)
        float currentAngle = degre; // Get the current y-rotation angle of the camera

        // Calculate the change in angle
        double angleChange = currentAngle - previousAngle2;
        //print(angleChange);
        // Determine the direction of movement

        if (angleChange < 0.0f)
            direction = angleChange;
        else if (angleChange > 0.0f)
            direction = angleChange;
        else direction = 0;

        //Debug.Log("Reference Angle: " + referenceAngle + " degrees");
        //Debug.Log("Current Angle: " + currentAngle + " degrees");
        //print("Direction of Movement: " + direction.ToString());

        previousAngle2 = currentAngle; // Update the previous angle for the next frame
        //print(direction);
        return direction;

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
    public Vector3 raydirection()
    {
        float azimuth = cam.transform.eulerAngles.y;
        float elevation = cam.transform.eulerAngles.x;

        // Convertir los ángulos de grados a radianes
        float azimuthRad = azimuth * Mathf.Deg2Rad;
        float elevationRad = elevation * Mathf.Deg2Rad;

        // Calcular el vector dirección del rayo en 3D
        Vector3 rayDirection = new Vector3(
            Mathf.Cos(elevationRad) * Mathf.Cos(azimuthRad),
            Mathf.Sin(elevationRad),
            Mathf.Cos(elevationRad) * Mathf.Sin(azimuthRad)
        );
        return rayDirection;
    }
    public float dotProduct(Vector3 v1, Vector3 v2)
    {

        float result = Vector3.Dot(v1,v2);
        return result;
    }



}

