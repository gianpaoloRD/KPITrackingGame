using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Target : MonoBehaviour
{
    [SerializeField]
    private float _speed = 1f;
    [SerializeField]
    private float _delay = 6;
    List<int> numbers = new List<int>{ 1, 2, 1, 2, 1, 2 };
    List<float> speeds = new List<float> { 0.2f, 0.2f, 0.4f, 0.4f, 0.6f, 0.6f };
    float timer;
    float i = 5;
    float randomNumber;
    int rand = 0;
    int var = 9;
    float randomNumberDiference = 0;
    public float radius = 60.0f; // Radius of the circular path
    public Vector3 center = new Vector3(0, 50, 0);    // Center point of the circle
    public float angle = 0.0f; // Current angle
    public Vector3 initialDirection = Vector3.forward;
    private float previousAngle;
    void Start()
    {

        float x = center.x + radius * Mathf.Cos(angle);
        float z = center.z + radius * Mathf.Sin(angle);
        initialDirection= new Vector3(x,0,z);
        //center = transform.position;

        transform.position = center + radius * initialDirection.normalized;
    }

    // Update is called once per frame
    void Update()
    {

        //print(_delay);
        timer += Time.deltaTime;
        if (timer > this._delay && speeds.Count !=0 )
        {
            
            int randomNumberMovement = Random.Range(0, speeds.Count -1);
            //Debug.Log(" randoNumber: " + randomNumberMovement);
            var = numbers[randomNumberMovement];
            
                //Debug.Log(" item " + (numbers.Count-1));
            
            
            _speed = speeds[randomNumberMovement];
            print(numbers.Count  + "  :  "+ randomNumberMovement +"  :  " +_speed);
            if ( speeds.Count > 0 )
            {
               numbers.RemoveAt(randomNumberMovement);
                speeds.RemoveAt(randomNumberMovement);
            }
            //print(numbers.Count  + "  :  " + randomNumber + "  :  " + _speed);



            this._delay += 10;
            //this.delayTime += 5;

        }
        if (speeds.Count == 0 )
        {
            var = 3;
        }
        Moving(randomNumber, var);
            
    }

    void Moving(float randomNumberX,int randomNumberMovement)
    {

        rand = randomNumberMovement;
        /*
        if (transform.position.x > -4 && transform.position.x < 4 && transform.position.y > 40 && transform.position.y < 55 && transform.position.z > -10 && transform.position.z < 10) // player
        {
            transform.Translate(new Vector3(-1, 1, 0) * Time.deltaTime * _speed);
        }
        else if (transform.position.x > 40)// righ limit x
        {
            transform.Translate(new Vector3(-1, 0, 0) * Time.deltaTime * _speed);
            this.randomNumberMovement = 1;
        }
        else if (transform.position.x < -40) // left limit x
        {
            transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime * _speed);
            this.randomNumberMovement = 0;
        }
        else if (transform.position.y < 40) // donw limit y
        {
            transform.Translate(Vector3.up * Time.deltaTime * _speed);
            this.randomNumberMovement = 2;
        }
        else if (transform.position.y >= 60) // up limit y
        {
            transform.Translate(Vector3.down * Time.deltaTime * _speed);
            this.randomNumberMovement = 3;
        }
        else if (transform.position.z > 30) // foward limit z
        {
            transform.Translate(new Vector3(0, 0, -1) * Time.deltaTime * _speed);
            this.randomNumberMovement = 5;
        }
        else if (transform.position.z < -30) // back limit z
        {
            transform.Translate(new Vector3(0, 0, 1) * Time.deltaTime * _speed);
            this.randomNumberMovement = 4;
        }
        */

        if(true)
        {
            switch (rand)
            {
                case 1:

                    moveLeft();
                    //moveRigth();
                    //Debug.Log(" random y: " + randomNumberY);
                    break;
                case 2:

                    moveRigth();

                    //transform.Translate(Vector3.left * Time.deltaTime * _speed);
                    //Debug.Log(" random y: " + randomNumberY);
                    break;
                case 3:
                    transform.Translate(Vector3.zero * Time.deltaTime * _speed);
                    //Debug.Log(" random y: " + randomNumberY);
                    break;
                case 4:
                    transform.Translate(Vector3.down * Time.deltaTime * _speed);
                    //Debug.Log(" random y: " + randomNumberY);
                    break;
                case 5:
                    transform.Translate(Vector3.forward * Time.deltaTime * _speed);
                    //Debug.Log(" random y: " + randomNumberY);
                    break;
                case 6:
                    transform.Translate(Vector3.back * Time.deltaTime * _speed);
                    //Debug.Log(" random y: " + randomNumberY);
                    break;

            }
            //rand = randomNumberMovement;
        }

        
    }

    void moveLeft()
    {
        // Calculate the new position
        float x = center.x + radius * Mathf.Cos(angle);
        float z = center.z + radius * Mathf.Sin(angle);
        //print(z - transform.position.z + " left :z");
        //print(x - transform.position.x + " left :x");
        // Update the position of the GameObject
        transform.Translate(new Vector3(x - transform.position.x, 0, z - transform.position.z));

        // Increment the angle to make the ball move


        // Increment the angle to make the ball move// Increment the angle to make the ball move
        //if (Mathf.Abs(angle) > 6f) angle += -0.001f * Time.deltaTime;
        angle += _speed * Time.deltaTime;

        // Ensure the angle stays within [0, 2*PI] for smooth circular motion
        //print(angle + "QQQQQQQQQQQ");
        if (angle > 2 * Mathf.PI)
        {
           
            angle -= 2 * Mathf.PI;
        }
        //if (angle < -2 * Mathf.PI)
        //{
        //angle += 2 * Mathf.PI;
        //}
        //print(" leftttt");

    }
    void moveRigth()
    {
        float x = center.x + radius * Mathf.Cos(angle);
        float z = center.z + radius * Mathf.Sin(angle);
        //print(z- transform.position.z + " z");
        //print(x - transform.position.x + " :x");
        // Update the position of the GameObject
        transform.Translate(new Vector3(x - transform.position.x, 0, z - transform.position.z));
        //print(new Vector3(x - transform.position.x, 0, z - transform.position.z));

        //if (angle > 6f) angle += -0.001f * Time.deltaTime;
        angle += -_speed * Time.deltaTime;

        // Ensure the angle stays within [0, 2*PI] for smooth circular motion
        //print(angle + "QQQQQQQQQQQ");
        if (angle < -2 * Mathf.PI)
        {
           
            angle += 2 * Mathf.PI;
        }
        // Ensure the angle stays within [0, 2*PI] for smooth circular motion

        //if (angle < -2 * Mathf.PI)
        //{
        //angle += 2 * Mathf.PI;
        //}
        //print(angle + " ang");
    }
}
