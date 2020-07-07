using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Coin : MonoBehaviour
{

    [SerializeField]
    Text Coin_ui;
 
    // Update is called once per frame
    void Update()
    {
        // コインの数をUIで表示
        Coin_ui.text = "Coin = " + Game_sys.Coin;
    }
}
