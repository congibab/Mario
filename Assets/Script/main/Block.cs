using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    private int num = 0;

    private Vector2 P_velocity; // player速度
    private SpriteRenderer sprite_R;
    private Animator anim;

    [SerializeField]
    private Sprite[] black;

    [SerializeField]
    private GameObject myPrefab;

    // Start is called before the first frame update
    void Start()
    {
        sprite_R = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        //block更新とコインを得る
        if (col.gameObject.CompareTag("Player"))
        {
            //num++;
            P_velocity = col.gameObject.GetComponent<Rigidbody2D>().velocity;
            if(P_velocity.y == 0 && col.transform.position.y < transform.position.y &&
               sprite_R.sprite != black[0])
            {
                Vector2 coin_pos = transform.position;
                coin_pos.y += 1.0f;
                var coin = Instantiate(myPrefab, coin_pos, Quaternion.identity);
                Destroy(coin, 0.5f);
                Game_sys.Coin++;
                anim.enabled = false;
                sprite_R.sprite = black[0];
            }
        }
    }
}
