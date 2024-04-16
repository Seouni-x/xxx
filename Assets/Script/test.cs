using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {

    GameObject player;
    Animator anim;

    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player");
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey("w"))
        {
            player.transform.Translate(new Vector3(0, 0, -0.2f), Space.World);
            //anim.SetBool("isWalk", true);
        }

        else if (Input.GetKey("s"))
        {
            player.transform.Translate(new Vector3(0, 0, 0.2f), Space.World);
            //anim.SetBool("isWalk", true);
        }
        else if (Input.GetKey("d"))
        {
            player.transform.Translate(new Vector3(-0.2f, 0, 0), Space.World);
            //anim.SetBool("isWalk", true);
        }
        else if (Input.GetKey("a"))
        {
            player.transform.Translate(new Vector3(0.2f, 0, 0), Space.World);
        }
        else if (Input.GetKey("space"))
        {
            player.transform.Translate(new Vector3(0, 0.1f, 0), Space.World);
        }
    }
}
