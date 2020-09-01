using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Manger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //制限時間が0になったらGameOver画面に移動
        //Playerが地面に落ちたら
        if(Game_sys.Limit_Time <= 0 || Player.view_Pos.y < 0)
        {
            SceneManager.LoadScene("GameOver");
            Game_sys.Limit_Time = Game_sys.Max_Time;
        }

    }
}
