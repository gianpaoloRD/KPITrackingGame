using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField]
    private float _speed = 1f;
    [SerializeField]
    private float _delay = 1;
    float timer;
    float randomNumberX;
    int randomNumberY;
    int rand = 0;
    float randomNumberDiference = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > _delay)
        {
            
            while(randomNumberX == randomNumberY)
            {
                randomNumberY = Random.Range(0, 5);
            }
            randomNumberX = randomNumberY;
            Debug.Log(" random y: " + randomNumberY);
            _delay += 1f;
            
        }
        Moving(randomNumberX, randomNumberY);
    }

    void Moving(float randomNumberX,int randomNumberY)
    {

        rand = randomNumberY;

        if (transform.position.x > -4 && transform.position.x < 4 && transform.position.y > 40 && transform.position.y < 55 && transform.position.z > -10 && transform.position.z < 10) // player
        {
            transform.Translate(new Vector3(-1, 1, 0) * Time.deltaTime * _speed);
        }
        else if (transform.position.x > 40)// righ limit x
        {
            transform.Translate(new Vector3(-1, 0, 0) * Time.deltaTime * _speed);
            this.randomNumberY = 1;
        }
        else if (transform.position.x < -40) // left limit x
        {
            transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime * _speed);
            this.randomNumberY = 0;
        }
        else if (transform.position.y < 40) // donw limit y
        {
            transform.Translate(Vector3.up * Time.deltaTime * _speed);
            this.randomNumberY = 2;
        }
        else if (transform.position.y >= 60) // up limit y
        {
            transform.Translate(Vector3.down * Time.deltaTime * _speed);
            this.randomNumberY = 3;
        }
        else if (transform.position.z > 30) // foward limit z
        {
            transform.Translate(new Vector3(0, 0, -1) * Time.deltaTime * _speed);
            this.randomNumberY = 5;
        }
        else if (transform.position.z < -30) // back limit z
        {
            transform.Translate(new Vector3(0, 0, 1) * Time.deltaTime * _speed);
            this.randomNumberY = 4;
        }

        else
        {
            switch (rand)
            {
                case 0:
                    transform.Translate(Vector3.right * Time.deltaTime * _speed);
                    //Debug.Log(" random y: " + randomNumberY);
                    break;
                case 1:
                    transform.Translate(Vector3.left * Time.deltaTime * _speed);
                    //Debug.Log(" random y: " + randomNumberY);
                    break;
                case 2:
                    transform.Translate(Vector3.up * Time.deltaTime * _speed);
                    //Debug.Log(" random y: " + randomNumberY);
                    break;
                case 3:
                    transform.Translate(Vector3.down * Time.deltaTime * _speed);
                    //Debug.Log(" random y: " + randomNumberY);
                    break;
                case 4:
                    transform.Translate(Vector3.forward * Time.deltaTime * _speed);
                    //Debug.Log(" random y: " + randomNumberY);
                    break;
                case 5:
                    transform.Translate(Vector3.back * Time.deltaTime * _speed);
                    //Debug.Log(" random y: " + randomNumberY);
                    break;

            }
            rand = randomNumberY;
        }

        

        
        
        
    }
}
