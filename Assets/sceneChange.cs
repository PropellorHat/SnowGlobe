using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneChange : MonoBehaviour
{
    public int scene;
    private void Update()
    {
        if(Input.touchCount > 0)
        {
            SceneManager.LoadScene(scene);
        }
    }
}
