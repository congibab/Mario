using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_ball : MonoBehaviour
{
    Vector3 fire_move;
    Vector3 View_pos;
    float rot_speed = 500.0f;
    float rot_vec;
    // Start is called before the first frame update
    void Start()
    {
       fire_move = new Vector3(0.2f * Player.move_vec, 0, 0);
       rot_vec = Player.move_vec;
    }

    // Update is called once per frame
    void Update()
    {
        View_pos = Camera.main.WorldToViewportPoint(transform.position);
        
        if(View_pos.x > 1 || View_pos.x < 0 || View_pos.y > 1 || View_pos.y < 0) Destroy(gameObject);
        //View_pos.x = Mathf.Clamp01(View_pos.x);
        //View_pos.y = Mathf.Clamp01(View_pos.y);

        transform.position += fire_move;
        transform.Rotate(0, 0, Time.deltaTime * rot_speed * rot_vec);
    }


    private void OnCollisionEnter2D(Collision2D col)
    {
        //敵と壁に当たったら反対向きで移動
        if (col.gameObject.CompareTag("wall") || col.gameObject.CompareTag("Block"))
        {
            Destroy(gameObject);
        }

        if (col.gameObject.CompareTag("Enemy"))
        {
            Destroy(col.gameObject);
            Destroy(gameObject);
        }
    }
}
