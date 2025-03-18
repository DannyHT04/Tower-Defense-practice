using UnityEngine;

public class ClickTile : MonoBehaviour
{
    public GameObject towerPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Clicking()
    {
        Debug.Log("Im clicked");
        Instantiate(towerPrefab, transform.position, Quaternion.identity);
    }
}
