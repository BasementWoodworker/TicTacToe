using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject linePrefab;
    private GameObject boardGO;
    private float height;
    private float width;
    Vector3 pos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Reading board dimensions and coordinates
        boardGO = GameObject.Find("Board");
        Collider collider = boardGO.GetComponent<Collider>();
        height = collider.bounds.size.x;
        width = collider.bounds.size.y;
        pos = boardGO.transform.position;

        // Creating separator lines
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                // float
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
