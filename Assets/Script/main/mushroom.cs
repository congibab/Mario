using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mushroom : MonoBehaviour
{

    private Vector2 P_velocity; // player_velocity
    private Vector3 vel_State = new Vector3(0.1f, 0, 0); //敵の移動方向
    private float _distance; 

    [SerializeField]
    Player player;

    [SerializeField]
    private float Move_Speed = 0.5f;
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        _distance = player.transform.position.x - transform.position.x;
        _distance = _distance / Mathf.Abs(_distance);
        Debug.Log(_distance);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        //敵と壁に当たったら反対向きで移動
        if (col.gameObject.CompareTag("wall") || col.gameObject.CompareTag("Enemy"))
        {
            vel_State.x = -vel_State.x;
        }
    }

    private void Move()
    {
        transform.position += vel_State * Move_Speed * _distance;
    }
}
