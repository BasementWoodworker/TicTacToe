using UnityEngine;
using UnityEngine.UI;
using CellValue = Game.CellValue;

public class GameText : MonoBehaviour
{
    private Text uiText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        uiText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnGameEnd(CellValue winner)
    {
        if (winner != CellValue.Empty)
        {
            uiText.text = "The winner is Player " + (winner == CellValue.Circle ? "(O)" : "(X)");
        } else
        {
            uiText.text = "Tie";
        }
    }

    public void OnTurnChange(CellValue currentPlayer)
    {
        uiText.text = "Player " + (currentPlayer == CellValue.Circle ? "(O)" : "(X)") + " turn";
    }
}
