using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class floower : MonoBehaviour {
    //float moveSpeed = 5f;
    public AudioSource playerMusic;
    GameObject target;
    public GameObject nav;
    public GameObject smoke;
    CanvasGroup panel;
    public static int sm=10;
    float t = 0;
     Slider HP;
    // Use this for initialization
    void Start () {
        target = GameObject.Find("Player");
        HP = GameObject.Find("Canvas").GetComponentInChildren<Slider>();
        panel = GameObject.FindGameObjectWithTag("234").GetComponent<CanvasGroup>();
        panel.alpha = 0;
    }
	
	// Update is called once per frame
	void Update () {
        nav.GetComponent<UnityEngine.AI.NavMeshAgent>().destination = target.transform.position;
    }

    public void particle(Vector3 pos) {
        GameObject go = Instantiate(smoke, pos, Quaternion.identity) as GameObject;
        go.transform.parent = this.transform;
    }

    //碰撞检测
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(ienu());
            manager.life--;
            sm--;
            playerMusic.Play();
            //manager.playerMusic2.clip = manager.clip[6];
            //manager.playerMusic2.Play();
            HP.value = manager. life;
            if (manager.life <= 0||sm<=0)
            {
                playerMusic.Stop();
            }
        }
    }
    void OnCollisionStay(Collision other)
    {
        t += Time.deltaTime;
        if (other.gameObject.tag == "Player")
        {
            if (t > 0.5f)
            {
                StartCoroutine(ienu());
                manager.life--;
                sm--;
                playerMusic.Play();
                //manager.playerMusic2.clip = manager.clip[6];
                //manager.playerMusic2.Play();
                HP.value = manager.life;
                t = 0;
                if (manager.life <= 0||sm<=0)
                {
                    playerMusic.Stop();
                }
            }
        }
    }


    public void animat() {
        if (this.gameObject.tag == "Monster3")
        {
            GetComponent<Animator>().SetBool("eleD", true);
        }
    }
   IEnumerator ienu() {
        for (float t = 0; t < 0.2f; t += Time.deltaTime)
        {
            if (t < 0.1f)
            {
                panel.alpha = 1;
            }
            else { panel.alpha = 0; }
            yield return 0;
        }
    }
}
