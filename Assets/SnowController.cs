using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowController : MonoBehaviour
{
    [Header("Globe Settings")]
    public float snowTime;
    private float currentTime = 0.0f;
    public float musicFadeTime;

    private bool fadeRunning = false;
    private bool checkingSpeed = false;
    private float shakeTime;

    [Header("Things")]
    public Animator santaAnim;
    public Animator snowManAnim;
    public ParticleSystem[] emitters;
    public AudioSource musicSource;

    
    
    

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

            if (Input.acceleration.magnitude > 1.5f)
            {
                StopCoroutine(KyoteFrames());
            }

            if (Input.acceleration.magnitude <= 1.2f)
            {
                StartCoroutine(KyoteFrames());
            }

        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            currentTime = snowTime;
        }

        


        if (!checkingSpeed)
        {
            currentTime -= Time.deltaTime;
            
            if (currentTime > 0)
            {
                santaAnim.SetBool("Bobbing", true);
                snowManAnim.SetBool("Waving", true);
                if (!fadeRunning) StartCoroutine(Volume(1f));
                for (int i = 0; i < emitters.Length; i++)
                {
                    if (!emitters[i].isPlaying)
                    {
                        emitters[i].Play();
                    }
                }

            }
            else
            {
                santaAnim.SetBool("Bobbing", false);
                snowManAnim.SetBool("Waving", false);
                if (!fadeRunning) StartCoroutine(Volume(0f));
                for (int i = 0; i < emitters.Length; i++)
                {
                    emitters[i].Stop();
                }
            }
        }
    }

    private IEnumerator Volume(float target)
    {
        fadeRunning = true;
        float startVol = musicSource.volume;

        float t = 0f;

        while(t < musicFadeTime)
        {
            
            t += Time.deltaTime;
            musicSource.volume = Mathf.Lerp(startVol, target, t/musicFadeTime);
            

            yield return null;
        }
        fadeRunning = false;
    }

    private IEnumerator KyoteFrames()
    {
        yield return new WaitForSeconds(0.5f);
        checkingSpeed = false;

        snowTime = shakeTime * 2;
        currentTime = snowTime;
    }
}
