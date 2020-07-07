using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class gameOver_Maniger: MonoBehaviour
{
   public void Move_Scene()
    {
        SceneManager.LoadScene("Main");
    }
}
