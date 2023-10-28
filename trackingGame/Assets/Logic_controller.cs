using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Logic_controller : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] TextMeshProUGUI time;
    float timer;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer < 5)
        {
            int total = 4 - (int)timer;
            time.text = total.ToString();
            if (total == 0) {
                time.text = "";
            }
        }

    }
}
