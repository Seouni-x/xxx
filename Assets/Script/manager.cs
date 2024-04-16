using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class manager : MonoBehaviour {
    public AudioSource playerMusic;
    public AudioSource playerMusic2;
    public AudioClip[] clip;
    public Transform camera;
    public float moveSpeed = 8;
    GameObject player;
    [SerializeField] LayerMask groundLayerIndex;
    public Camera mainCamera;
    Vector3 offset;
    int score = 0;
    public static int life = 10;
    public Text bld;
    public Text wz;
    public Transform gun;
    public ParticleSystem GunFire;
    public GameObject smoke1;
    Animator anim;
   public Animator ele;
    public Transform spawn1;
    public Transform spawn2;
    public Transform spawn3;
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    
    float time1 = 4f;
    float time2 = 6f;
    float time3 = 10f;
    int Mlife1 = 5;
    int Mlife2 = 8;
    bool isshow = true;
    float ti = 0;
    LineRenderer line;
    float DTime=2.5f;
    float eleD=0f;
    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player");
        anim = GetComponent<Animator>();
        offset = this.transform.position;//玩家的开始位置
        line = GetComponent<LineRenderer>();
        line.startWidth = 0.03f;
        line.endWidth = 0.03f;
    }
	
	// Update is called once per frame
	void Update () {

       
        Spawn();
        blood();
        if (life <= 0)
        {   
            DTime -= Time.deltaTime;
            //Debug.Log(playerMusic.clip);//playerMusic.clip = clip[3]; //playerMusic.Play();        
            playerMusic.PlayOneShot(clip[3]);
            anim.SetBool("isDeath",true);
            if (DTime <= 0) {
            SceneManager.LoadScene(0);
                life = 10;
            }
        }

        if (score >= 200)
        {
            SceneManager.LoadScene(3);
        }
        anim.SetBool("isWalk", false);
        line.positionCount = 2;
        line.startColor = Color.yellow;
        line.endColor = Color.red;
        line.SetPosition(0, gun.position);
        line.SetPosition(1, gun.position);
      
         //人物跟随鼠标方向旋转
        Shexian();

        }
    void FixedUpdate()
    {
        //角色移动
        if (life > 0)
        {
             Move();

            //if (Input.GetKey("w"))
            //{

            //    player.transform.Translate(new Vector3(0, 0, -0.1f), Space.World);
            //    anim.SetBool("isWalk", true);
            //}

            //else if (Input.GetKey("s"))
            //{
            //    player.transform.Translate(new Vector3(0, 0, 0.1f), Space.World);
            //    anim.SetBool("isWalk", true);
            //}
            //else if (Input.GetKey("d"))
            //{
            //    player.transform.Translate(new Vector3(-0.1f, 0, 0), Space.World);
            //    anim.SetBool("isWalk", true);
            //}
            //else if (Input.GetKey("a"))
            //{
            //    player.transform.Translate(new Vector3(0.1f, 0, 0), Space.World);
            //}
            if (Input.GetKey("space"))
            {
                player.transform.Translate(new Vector3(0, 0.1f, 0), Space.World);
            }
        }
       
    }

    void Move()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        Vector3 screenRight = transform.right;             //以人物局部空间为参考系移动
        Vector3 screenForward = transform.forward;
        //screenForward.y = 0;                            //不能有竖直分量

        Vector3 sumVector = screenForward * v + screenRight * h;                //矢量之和
        Vector3 movement = new Vector3(h, 0, v).normalized;
        float speed = movement.magnitude * moveSpeed;


        if (speed > 0)
        {
            anim.SetFloat("Walk", speed);

        }
        else
        {
            anim.SetFloat("Walk", 0);
            anim.SetFloat("Idle", speed);
        }

        //if (!(h == 0 && v == 0))
        //{
        //    transform.rotation = Quaternion.LookRotation(sumVector);
        //}
        transform.Translate(sumVector * moveSpeed * Time.deltaTime, Space.World);       //Space.World绝对不能少
    }

 


    //void LateUpdate()
    //{
    //    //相机跟随玩家移动，移动量=玩家当前位置-玩家的上一个位置
    //    mainCamera.transform.position += this.transform.position - offset;
    //    offset = this.transform.position;
        
    //}

    //怪物出生点
    void Spawn()
    {
        time1 -= Time.deltaTime;
        if (time1 <= 0)
        {
            Instantiate(enemy1, spawn1.position, Quaternion.identity);
            time1 = 3f;
        }
        time2 -= Time.deltaTime;
        if (time2 <= 0)
        {
            Instantiate(enemy2, spawn2.position, Quaternion.identity);
            time2 = 6f;
        }
        time3 -= Time.deltaTime;
        if (time3 <= 0)
        {
            Instantiate(enemy3, spawn3.position, Quaternion.identity);
            time3 = 4f;
        }
    }

    //射线
    void Shexian()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, 2000))//, 200, groundLayerIndex
        {
            //人物跟随鼠标转动
            Vector3 target = hitInfo.point;
            target.y = transform.position.y;
            transform.LookAt(target);

            if (Input.GetMouseButton(1))
            { 
                if (isshow)
                {
                    line.SetPosition(1, hitInfo.point);
                    //Debug.DrawLine(gun.position, hitInfo.point, Color.yellow);
                
                    GunFire.Play();
                    playerMusic.clip = clip[0];
                    playerMusic.Play();
                    if (hitInfo.collider.tag == "Monster1" || hitInfo.collider.tag == "Monster2")
                    {
                        Mlife1--;
                        playerMusic2.clip = clip[1];
                        playerMusic2.Play();
                        hitInfo.collider.gameObject.transform.SendMessage("particle", hitInfo.point, SendMessageOptions.DontRequireReceiver);
                        if (Mlife1 <= 0)
                        {
                            playerMusic2.clip = clip[4];
                            playerMusic2.Play();
                            Destroy(hitInfo.collider.gameObject);
                            Mlife1 = 5;
                            score += 10;
                            wz.text = "得分 : " + score;
                        }
                    }
                    if (hitInfo.collider.tag == "Monster3")
                    {
                        
                        Mlife2--;
                        playerMusic2.clip = clip[2];
                        playerMusic2.Play();
                        hitInfo.collider.gameObject.transform.SendMessage("particle", hitInfo.point, SendMessageOptions.DontRequireReceiver);
                        if (Mlife2 <= 0)
                        {
                            //eleD += Time.deltaTime;
                            hitInfo.collider.gameObject.transform.SendMessage("animat",SendMessageOptions.DontRequireReceiver);
                            playerMusic2.clip = clip[5];
                            playerMusic2.Play();
                            //if (eleD >=2f) { 
                            StartCoroutine(delay(()=> { Destroy(hitInfo.collider.gameObject); },1.5f));
                            Mlife2 = 8;
                            score += 30;
                            wz.text = "得分 : " + score;
                     //   }
                        }
                    }
                    isshow = false;
                }
                else if(!isshow){
                    ti += Time.deltaTime;
                    if (ti > 0.1f)
                    {
                        isshow = true;
                        ti = 0;
                    }
                }
            }
        }
    }

    //血量显示
    public void blood()
    {
        if (floower.sm<=10&&floower.sm>=0)
        {
            bld.text = "血量 : " + floower.sm;
        }
        //if (floower.sm <= 0)
        //{
        //    bld.text = "血量:" + 0;
        //}
        
    }
    IEnumerator delay(Action ac, float t)
    {
        yield return new WaitForSeconds(t);
        ac();
    }
}
