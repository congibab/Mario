using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class tilemap : MonoBehaviour
{
    GridLayout grid;
    static public Vector3Int cell_pos;

    // Start is called before the first frame update
    void Start()
    {
        grid = GetComponent<GridLayout>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //get tile map position
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            var test1 = col.transform.position.x + Player.col_size.x/2;
            var test2 = col.transform.position.y + Player.col_size.y/2;
         
            cell_pos = grid.WorldToCell(new Vector3(test1, test2, transform.position.z));
            //Debug.Log(cell_pos);
        }
    }
}
