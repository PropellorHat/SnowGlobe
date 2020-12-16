using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class accelDebug : MonoBehaviour
{
    public Text text;

    private bool checkingSpeed;
    private float shakeTime;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!checkingSpeed)
        {
            if (Input.acceleration.magnitude > 1.5f)
            {
                checkingSpeed = true;
                shakeTime = 0;
            }
        }

        if (checkingSpeed)
        {
            shakeTime += Time.deltaTime;
            
            text.text = "shaking";

            if (Input.acceleration.magnitude > 1.5f)
            {
                StopAllCoroutines();
            }

            if (Input.acceleration.magnitude <= 1.2f)
            {
                StartCoroutine(KyoteFrames());
            }
            
        }
        else
        {
            text.text = shakeTime.ToString();
        }


    }

    private IEnumerator KyoteFrames()
    {
        yield return new WaitForSeconds(0.5f);
        checkingSpeed = false;
    }
}
