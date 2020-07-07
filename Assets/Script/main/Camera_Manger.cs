using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Manger : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    private Vector3 pos;
    private Vector3 Camera_pos;
    // Start is called before the first frame update

    void Awake()
    {
        Camera_pos = transform.position;
    }

    void Update()
    {
        //カメラをPlayerを追跡
        pos = player.transform.position;
        transform.position = new Vector3(pos.x, Camera_pos.y, Camera_pos.z);
    }
}
