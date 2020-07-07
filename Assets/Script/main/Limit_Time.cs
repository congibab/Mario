using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Limit_Time : MonoBehaviour
{

    [SerializeField]
    Text UI_time;

    private float _Limit_Time;

    // Start is called before the first frame update
    void Start()
    {
        _Limit_Time = Game_sys.Limit_Time;
        StartCoroutine(duration(1.0f));
    }

    // Update is called once per frame
    void Update()
    {
        //制限時間をUIで表示
        UI_time.text = "time = " + Game_sys.Limit_Time;
        
    }
    /// <summary>
    /// 時間がTime秒ずつ減らす
    /// </summary>
    /// <param name="Time">時間を遅延</param>
    /// <returns></returns>
    private IEnumerator duration(float Time)
    {
        while(Game_sys.Limit_Time > 0 )
        {
            yield return new WaitForSeconds(Time);
            Game_sys.Limit_Time--;
        }
        Debug.Log("time over");
    }
}
