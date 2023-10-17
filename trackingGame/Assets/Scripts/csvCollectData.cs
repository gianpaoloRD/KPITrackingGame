using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class csvCollectData : MonoBehaviour
{
    public Camera cam;
    public Target target;
    public Button button;
    ArrayList arrTarget = new ArrayList(); 
    ArrayList arrCam = new ArrayList(); 
    TargetThread targetThread;
    int score= 0;
    float timer;
    private float _delay;
    string saveFilePath ="";
    void Start()
    {
        saveFilePath = Application.dataPath + "/../ParticipantData/";
        button.gameObject.SetActive(false);


    }

    // Update is called once per frame
    void Update()
    {
        rayCast();
       
        arrTarget.Add(target.transform.position);
        arrCam.Add(cam.transform.localEulerAngles);
        //Debug.Log(cam.transform.localEulerAngles.x);
        //Debug.Log(cam.transform.localEulerAngles);

        timer += Time.deltaTime;
        if (timer > _delay)
        {
            _delay += 5f;
        }
        if (this.score >= 250) gameOver();

        


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
                Debug.Log("im a thread" + hit.transform.name);
                this.score++;
            }

        }
    }

    public void savecsv()
    {

        targetThread = new TargetThread(arrTarget, arrCam, saveFilePath);
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

    

}

