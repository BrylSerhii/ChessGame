using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public ChessBoard chessBoard; // Assign this in the Inspector

    private GameObject selectedPiece;

    void Update()
    {

        CheckUserInput();
    }

    void CheckUserInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Get mouse position in world coordinates
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Check if a chess piece was clicked
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
            if (hit.collider != null && hit.collider.GetComponent<ChessPiece>() != null)
            {
                // Select the piece
                selectedPiece = hit.collider.gameObject;

                // Generate move plates for the piece
                selectedPiece.GetComponent<ChessPiece>().GenerateMovePlates();
            }
            // Check if a move plate was clicked
            else if (hit.collider != null && hit.collider.GetComponent<MovePlate>() != null)
            {
                // Move the selected piece to the new position
                MovePlate movePlate = hit.collider.GetComponent<MovePlate>();
                selectedPiece.GetComponent<ChessPiece>().Move(movePlate.GetXBoard(), movePlate.GetYBoard(), chessBoard);

                // Destroy the move plates
                selectedPiece.GetComponent<ChessPiece>().DestroyMovePlates();
            }
        }
    }

}
