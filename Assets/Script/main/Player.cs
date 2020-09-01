using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    private GameObject fire_ball_prefab; // fire ballで攻撃

    [SerializeField, Range(0.0f, 10.0f)]
    private float Move_Speed = 10.0f; //移動速度

    [SerializeField, Range(0.0f, 20.0f)]
    private float Jump_Power = 5.0f; //ジャンプ＿力

    private Vector3 Move_Velocity; //移動方向
    private Vector2 Jump_Velocity; //ジャンプ方向

    private Vector3 velocity_temp;

    private float hori;//Game_Pad 左スティックの右左を取得
    private float vert; //Game_Pad 左スティックの上下を取得

    private SpriteRenderer spr;
    private Rigidbody2D rb;
    private Animator animator;
    private BoxCollider2D Box_col;

    private int status_count; // 0 = small, 1 = big, 2 = fire
  
    //アニメーション（モーション）切り替える変数
    //------------------------------------------------------------------

    private bool Working;
    private bool is_Grounding = false; //キャラの着地判定
    public static bool die;

    public static Vector3 view_Pos;
    //------------------------------------------------------------------

    private bool is_Jumping;

    public static float move_vec = 0;
    public static Vector2 col_size;

    void Awake()
    {
        spr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Box_col = GetComponent<BoxCollider2D>();
        
        is_Jumping = false;
        die = false;
        status_count = 0;
        //item_sys = GetComponent<Item_sys>();
    }

    void Start()
    {
        hori = Input.GetAxis("Horizontal");
        change_status();
    }

    void Update()
    {
        view_Pos = Camera.main.WorldToViewportPoint(transform.position);
        
        if (hori != 0)
        {
            Working = true;
            set_move_vec();
        }

        if (hori == 0)
        {
            Working = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && is_Grounding) //ジャンプ
        {
            is_Jumping = true;
            StartCoroutine(Delay());
        }

        if(Input.GetKeyUp(KeyCode.Space))
        {
            is_Jumping = false;
        }

        if(Input.GetKeyUp(KeyCode.Mouse0) && status_count >= 2)
        {
            Vector3 fire_pos = new Vector3(2.0f * move_vec, 0,0);
           var fire_ball = Instantiate(fire_ball_prefab, transform.position + fire_pos, Quaternion.identity);
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
        change_status();

    }

    void OnCollisionEnter2D(Collision2D col)
    {

        if (rb.velocity.y == 0 && rb.velocity.y > velocity_temp.y)
        {
            is_Grounding = true;
        }


        if (col.gameObject.CompareTag("Enemy"))
        {
            

            if (rb.velocity.y >= 0)
            {
                if (status_count > 0) status_count--;
                
                else
                {
                    die = true;
                    Char_Die();
                }
            }
        }



        if (col.gameObject.CompareTag("mushroom"))
        {
            if (status_count == 2) status_count = 2;
            else status_count = 1;
            Destroy(col.gameObject);
        }

        if(col.gameObject.CompareTag("flower"))
        {
            status_count = 2;
            Destroy(col.gameObject);
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
        //if (!is_Jumping) return;
        //rb.velocity = Vector2.zero;

        //Jump_Velocity = new Vector2(0, Jump_Power );
        //rb.AddForce(Jump_Velocity, ForceMode2D.Impulse);
        //is_Grounding = false;
        //is_Jumping = false;

        if (is_Jumping == false) return;

        Jump_Velocity = new Vector2(0, Jump_Power);
        rb.AddForce(Jump_Velocity, ForceMode2D.Impulse);
        is_Grounding = false;
        //is_Jumping = false;
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

    void change_status()
    {
        switch (status_count)
        {
            case 0:
                animator.runtimeAnimatorController = Resources.Load("Player(small)") as RuntimeAnimatorController;
                col_size = new Vector2(0.75f, 1.0f);
                Box_col.size = col_size;
                break;

            case 1:
                animator.runtimeAnimatorController = Resources.Load("Player(Big)") as RuntimeAnimatorController;
                col_size = new Vector2(1.0f, 2.0f);
                Box_col.size = col_size;
                break;

            case 2:
                animator.runtimeAnimatorController = Resources.Load("Player(fire)") as RuntimeAnimatorController;
                col_size = new Vector2(1.0f, 2.0f);
                Box_col.size = col_size;
                break;
        }
    }

    private void set_move_vec()
    {
        if(hori < 0)
        {
            move_vec = -1;
        }

        else if(hori > 0)
        {
            move_vec = 1;
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.3f);
        is_Jumping = false;
    }
}