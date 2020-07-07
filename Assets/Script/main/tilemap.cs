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
            cell_pos = grid.WorldToCell(col.transform.position);
            Debug.Log(cell_pos);
        }
    }
}
