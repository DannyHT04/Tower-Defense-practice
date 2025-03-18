using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class TileClicking : MonoBehaviour
{
    public GameObject towerPrefab;  // Assign your tower prefab in the Inspector
    private List<Tilemap> tilemaps = new List<Tilemap>();
    public GameObject topPanel, bottomPanel;
    public Grid grid;
    public Vector3 spawnPosition;

    private bool isShopping = false;

    void Start()
    {

        foreach (Transform child in grid.transform)
        {
            Tilemap tilemap = child.GetComponent<Tilemap>();
            if (tilemap != null)
            {
                tilemaps.Add(tilemap);
            }
        }
    }
    void Update()
    {
        if (isShopping) return;
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            float screenHeight = Screen.height;


            // saving the position of where the mouse was click
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePosition);
            mouseWorldPos.z = 0;

         
            Vector3Int cellPosition = grid.WorldToCell(mouseWorldPos);
            foreach (Tilemap tilemap in tilemaps)
            {
                if (tilemap.GetTile(cellPosition) != null && tilemap.gameObject.name.ToLower().Contains("road"))
                {
                    Debug.Log("Clicked on a road tile. Cannot place a tower here!");
                    return;
                }
            }

            if (mousePosition.y > screenHeight / 2)
            {
                // Debug.Log("Clicked on the TOP half of the screen");
                bottomPanel.SetActive(true);
                topPanel.SetActive(false);
                isShopping = true;
            }
            else
            {
                // Debug.Log("Clicked on the BOTTOM half of the screen");
                topPanel.SetActive(true);
                bottomPanel.SetActive(false);
                isShopping = true;
            }

           
            spawnPosition = grid.GetCellCenterWorld(cellPosition);
        }






        // if (Input.GetMouseButtonDown(0))


        //     // placing tower on grid
        //     if (!TowerAlreadyExists(spawnPosition))
        //     {
        //         Instantiate(towerPrefab, spawnPosition, Quaternion.identity);
        //     }
        // }
    }

    bool TowerAlreadyExists(Vector3 position)
    {
        Collider2D hit = Physics2D.OverlapPoint(position);
        return hit != null; // If something exists at this position, return true
    }


    public void CloseShop()
    {
        topPanel.SetActive(false);
        bottomPanel.SetActive(false);
        isShopping = false;
    }
}
