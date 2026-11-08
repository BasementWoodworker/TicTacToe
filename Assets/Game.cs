using UnityEngine;
using UnityEngine.InputSystem;

public class Game : MonoBehaviour
{
    private Camera camera;
    public GameObject linePrefab;
    private GameObject boardGO;
    public Sprite circleSprite;
    public Sprite crossSprite;
    public GameText gameText;
    private float height;
    private float width;
    private CellValue[,] boardState;
    private CellValue currentPlayerSymbol = CellValue.Cross;
    private CellValue winner = CellValue.Empty;
    Vector3 pos;

    public enum CellValue { Empty, Cross, Circle };

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        camera = GetComponent<Camera>();
        boardState = new CellValue[3, 3];
        gameText.OnTurnChange(currentPlayerSymbol);
        // Reading board dimensions and coordinates
        boardGO = GameObject.Find("Board");
        Collider collider = boardGO.GetComponent<Collider>();
        width = collider.bounds.size.x;
        height = collider.bounds.size.y;
        pos = boardGO.transform.position;

        // Creating separator lines
        // Horizontal
        for (int i = 1; i <= 2; i++)
        {
            GameObject lineGO = Instantiate<GameObject>(linePrefab);
            lineGO.transform.position = new Vector3(0, height*(i / 3f) - height / 2f, 0.5f);
        }
        // Vertical
        for (int i = 1; i <= 2; i++)
        {
            GameObject lineGO = Instantiate<GameObject>(linePrefab);
            lineGO.transform.position = new Vector3(width * (i / 3f) - width / 2f, 0, 0.5f);
            lineGO.transform.Rotate(new Vector3(0, 0, 90));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame && winner == CellValue.Empty)
        {
            Vector3 mousePosition = Mouse.current.position.ReadValue();
            Ray ray = camera.ScreenPointToRay(mousePosition);
            RaycastHit raycastHit;
            bool isHit = Physics.Raycast(ray, out raycastHit);
            if (isHit && raycastHit.collider.gameObject == boardGO)
            {
                float x = raycastHit.point.x + width/2; // 0-10
                float y = raycastHit.point.y + height/2; // 0-10
                int row;
                int column;

                if (x < width / 3f) column = 0;
                else if (x < (2f / 3f) * width) column = 1;
                else column = 2;

                if (y < width / 3f) row = 2;
                else if (y < (2f / 3f) * width) row = 1;
                else row = 0;

                CellValue clickedCellValue = boardState[row, column];
                if (clickedCellValue == CellValue.Empty)
                {
                    boardState[row, column] = currentPlayerSymbol;
                    AddGameObjectForCell(row, column);
                    currentPlayerSymbol = currentPlayerSymbol == CellValue.Circle ? CellValue.Cross : CellValue.Circle;
                    winner = CheckWinCondition();
                    bool isBoardFull = CheckBoardFull();
                    if (winner != CellValue.Empty)
                    {
                        gameText.OnGameEnd(winner);
                    } else
                    {
                        if (isBoardFull) gameText.OnGameEnd(CellValue.Empty);
                        else gameText.OnTurnChange(currentPlayerSymbol);
                    }
                }
            }
        }
    }

    void AddGameObjectForCell(int row, int column)
    {
        Sprite sprite = currentPlayerSymbol == CellValue.Circle ? circleSprite : crossSprite;
        GameObject symbol = new GameObject("Symbol_" + row + "_" + column);
        SpriteRenderer sr = symbol.AddComponent<SpriteRenderer>();
        sr.sprite = sprite;
        float spriteScale = 0.25f;
        symbol.transform.localScale = new Vector3(spriteScale,spriteScale,1);

        Vector3 center = new Vector3(
            pos.x - width / 2f + (column + 0.5f) * width/3f,
            pos.y + height / 2f - (row + 0.5f) * height/3f,
            pos.z - 0.1f
        );

        symbol.transform.position = center;
    }

    CellValue CheckWinCondition()
    {
        // Rows and columns
        for (int i = 0; i < 3; i++)
        {
            if (boardState[i, 0] != CellValue.Empty &&
                boardState[i, 0] == boardState[i, 1] &&
                boardState[i, 1] == boardState[i, 2])
                return boardState[i, 0];

            if (boardState[0, i] != CellValue.Empty &&
                boardState[0, i] == boardState[1, i] &&
                boardState[1, i] == boardState[2, i])
                return boardState[0, i];
        }

        // Diagonals
        if (boardState[1, 1] != CellValue.Empty)
        {
            if (boardState[0, 0] == boardState[1, 1] && boardState[1, 1] == boardState[2, 2])
                return boardState[1, 1];

            if (boardState[0, 2] == boardState[1, 1] && boardState[1, 1] == boardState[2, 0])
                return boardState[1, 1];
        }

        return CellValue.Empty;
    }

    bool CheckBoardFull()
    {
        bool result = true;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (boardState[i, j] == CellValue.Empty) result = false;
            }
        }
        return result;
    }
}
