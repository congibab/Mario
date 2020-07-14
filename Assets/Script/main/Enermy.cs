using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enermy : MonoBehaviour
{
    private Vector2 P_velocity; // player_velocity
    private Vector2 Bound = new Vector2 (0, 5f);

    private Vector3 vel_State = new Vector3(0.1f, 0, 0); //敵の移動方向

    private SpriteRenderer sprite_R;

    [SerializeField]
    private float Move_Speed = 0.5f;

    [SerializeField]
    private Sprite[] Die;

    // Start is called before the first frame update
    void Start()
    {
        sprite_R = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        E_Move();
    }
    //============================================================

    private void OnCollisionEnter2D(Collision2D col)
    {
        //playerが敵を上から当たったら敵を倒す
        if (col.gameObject.CompareTag("Player"))
        {
            Rigidbody2D rb = col.gameObject.GetComponent<Rigidbody2D>();
            P_velocity = rb.velocity;
            if (P_velocity.y < 0 && col.transform.position.y > transform.position.y)
            {
                sprite_R.sprite = Die[0];
                rb.AddForce(Bound, ForceMode2D.Impulse);
                Destroy(gameObject, 0.5f);
            } 
        }

        //敵と壁に当たったら反対向きで移動
        if (col.gameObject.CompareTag("wall") || col.gameObject.CompareTag("Enemy"))
        {
            vel_State.x = -vel_State.x;
        }
    }

    //敵の移動
    private void E_Move()
    {
        transform.position += vel_State * Move_Speed;
    }
    

}
