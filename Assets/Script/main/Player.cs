using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField, Range(0.0f, 10.0f)]
    private float Move_Speed = 10.0f; //移動速度

    [SerializeField, Range(0.0f, 20.0f)]
    private float Jump_Power = 5.0f; //ジャンプ＿力

    private Vector3 Move_Velocity; //移動方向
    private Vector2 Jump_Velocity; //ジャンプ方向

    private Vector3 velocity_temp;

    private float hori; //Game_Pad 左スティックの右左を取得
    private float vert; //Game_Pad 左スティックの上下を取得

    private SpriteRenderer spr;
    private Rigidbody2D rb;
    private Animator animator;
    private BoxCollider2D Box_col;
    

    //アニメーション（モーション）切り替える変数
    //------------------------------------------------------------------

    private bool Working;
    private bool is_Grounding = false; //キャラの着地判定
    public static bool die = false;
    //------------------------------------------------------------------

    private bool is_Jumping;

    void Awake()
    {
        spr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Box_col = GetComponent<BoxCollider2D>();
        is_Jumping = false;

        //item_sys = GetComponent<Item_sys>();
    }


    void Update()
    {

        if(Input.GetAxis("Horizontal") != 0)
        {
            Working = true;
        }

        if (Input.GetAxis("Horizontal") == 0)
        {
            Working = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && is_Grounding) //ジャンプ
        {
            is_Jumping = true;
        }

    }

    void FixedUpdate()
    {
        P_Moving();
        Jump();
    }

    void LateUpdate()
    {
        velocity_temp = rb.velocity;
        initAnimator();
    }

    void OnCollisionEnter2D(Collision2D col)
    {

        if (rb.velocity.y == 0 && rb.velocity.y > velocity_temp.y)
        {
            is_Grounding = true;
        }


        if (col.gameObject.CompareTag("Enemy"))
        {

            if (transform.position.y <= col.transform.position.y)
            {
                die = true;
                Char_Die();
            }
        }
    }

    //------------------------------------------------------------------
    //------------------------------------------------------------------

    /// <summary>
    ///キャラの動く処理 
    /// </summary>
    void P_Moving()
    {
        //renderer.sprite = playerImages[1];
        hori = Input.GetAxis("Horizontal");
        vert = Input.GetAxis("Vertical");

        Move_Velocity = Vector3.zero;

        if (hori < 0)
        {
            Move_Velocity = new Vector3(hori, 0, 0);
            spr.flipX = true;// renderer反転
        }

        else if (hori > 0)
        {
            Move_Velocity = new Vector3(hori, 0, 0);
            spr.flipX = false;
        }

        //rb.MovePosition(transform.position + Move_Velocity.normalized * Move_Speed * 100 * Time.deltaTime);
        transform.position += Move_Velocity * Move_Speed  * Time.deltaTime;
    }

    /// <summary>
    /// キャラのジャンプ処理
    /// </summary>
    void Jump()
    {
        if (!is_Jumping) return;
        rb.velocity = Vector2.zero;

        Jump_Velocity = new Vector2(0, Jump_Power );
        rb.AddForce(Jump_Velocity, ForceMode2D.Impulse);
        is_Grounding = false;
        is_Jumping = false;
    }

    void Char_Die()
    {
        Box_col.enabled = false;
        rb.AddForce(Jump_Velocity/2, ForceMode2D.Impulse);
    }

    /// <summary>
    /// アニメーション専用変数更新
    /// </summary>
    void initAnimator()
    {
        animator.SetBool("is_Grounding", is_Grounding);
        animator.SetBool("Working", Working);
        animator.SetBool("die", die);
    }


}