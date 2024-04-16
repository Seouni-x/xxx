using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Turn : MonoBehaviour {
    public GameObject gy;
    public AudioSource ac;
    public Slider sl;
    float time = 0;
    bool isxx = false;
	// Use this for initialization
	void Start () {
        gy.SetActive(false);
        //sl.value = ac.volume;
    }
	
	// Update is called once per frame
	void Update () {
        SetVolume();

        if (isxx == true) {
            time += Time.deltaTime;
            if (time >= 3f)
        {
            //Debug.Log(time);
            gy.SetActive(false);
            time = 0;
        }
        }
    }

    public void SetVolume()
    {
        ac.volume = sl.value;
    }

    public void c1()
    {
        SceneManager.LoadScene(1);
    }
    public void c2()
    {
      #if UNITY_EDITOR
                 UnityEditor.EditorApplication.isPlaying = false;
        
      #else
         Application.Quit();
     #endif
    }
    public void c3()
    {
        isxx = true;
        gy.SetActive(true);
       
       
    }
    public void c4()
    {
        SceneManager.LoadScene(2);
    }
}
