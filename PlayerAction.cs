using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public GameManager manager;
    public float Speed;
    
    float h;
    float v;
    bool isHorizonMove;
    Rigidbody2D rigid;
    Animator anim;
    Vector3 dirVec;
    GameObject scanObj;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {   
        //이동 값
        h = manager.isAction ? 0 : Input.GetAxisRaw("Horizontal");
        v = manager.isAction ? 0 : Input.GetAxisRaw("Vertical");
        // 업다운버튼
        bool hDown = manager.isAction ? false : Input.GetButtonDown("Horizontal");
        bool vDown = manager.isAction ? false : Input.GetButtonDown("Vertical");
        bool hUp = manager.isAction ? false : Input.GetButtonUp("Horizontal");
        bool vUp = manager.isAction ? false : Input.GetButtonUp("Vertical");

        //수직수평이동
        if (hDown)
        {
            isHorizonMove = true;
        }
        else if (vDown)
        {
            isHorizonMove = false;
        }
        else if (hUp || vUp)
        {
            isHorizonMove = h != 0;
        }
        //애니메이션
        if (anim.GetInteger("hAxisRaw")!=h)
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("hAxisRaw", (int)h);
        }
        else if (anim.GetInteger("vAxisRaw") != v)
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("vAxisRaw", (int)v);
        }
        else
        {
            anim.SetBool("isChange", false);
        }
        //방향
        if(vDown && v == 1)
        {
            dirVec = Vector3.up;
        }
        else if(vDown && v == -1)
        { 
            dirVec = Vector3.down; 
        }
        else if (hDown && h == 1)
        {
            dirVec = Vector3.right;
        }
        else if (hDown && h == -1)
        {
            dirVec = Vector3.left;
        }

        // 오브제 스캔
        if(Input.GetButtonDown("Jump")&& scanObj != null)
        {
            manager.Action(scanObj);
        }
    }

    public void FixedUpdate()
    {   
        //이동
        Vector2 moveVec = isHorizonMove ? new Vector2(h, 0) : new Vector2(0, v);
        rigid.velocity = moveVec * Speed;

        //드로우레이 앞 사물스캔
        Debug.DrawRay(rigid.position, dirVec*0.8f, Color.green, 0 );
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, dirVec, 0.8f,LayerMask.GetMask("Obj"));

        if(rayHit.collider != null)
        {
            scanObj = rayHit.collider.gameObject;
        }
        else
        {
            scanObj = null;
        }
    }
}
