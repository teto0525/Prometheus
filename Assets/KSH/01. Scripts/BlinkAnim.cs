using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkAnim : MonoBehaviour
{
    float time;
    public Text ready;

    // Update is called once per frame
    void Update()
    {
        if (time < 0.5f)
        {
            GetComponent<Text>().color = new Color(255, 206, 54, 255 - time);
        }
        else
        {
            GetComponent<Text>().color = new Color(255, 206, 54, time) ;
            if(time > 1f)
            {
                time = 0;
            }
        }

        time += Time.deltaTime;
        
    }
}
