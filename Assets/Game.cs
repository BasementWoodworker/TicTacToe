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
        // Horizontal
        for (int i = 1; i <= 2; i++)
        {
            GameObject lineGO = Instantiate<GameObject>(linePrefab);
            lineGO.transform.position = new Vector3(0, width*(i/3f) - width/2f, 0.5f);
        }
        // Vertical
        for (int i = 1; i <= 2; i++)
        {
            GameObject lineGO = Instantiate<GameObject>(linePrefab);
            lineGO.transform.position = new Vector3(height * (i / 3f) - height / 2f, 0, 0.5f);
            lineGO.transform.Rotate(new Vector3(0, 0, 90));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GetSquareCoordinatesForClick()
    {
        // TODO
    }
}
