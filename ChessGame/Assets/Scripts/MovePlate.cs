using UnityEngine;

public class MovePlate : MonoBehaviour
{
    GameObject reference = null;
    ChessBoard chessBoard = null; // Add this

    public void SetReference(GameObject obj)
    {
        reference = obj;
    }

    public int GetXBoard()
    {
        return (int)transform.position.x;
    }

    public int GetYBoard()
    {
        return (int)transform.position.y;
    }

    void OnMouseUp()
    {
        if (reference != null)
        {
            ChessPiece piece = reference.GetComponent<ChessPiece>();
            if (piece != null)
            {
                // Get the ChessBoard instance
                chessBoard = GameObject.FindObjectOfType<ChessBoard>();
                if (chessBoard != null)
                {
                    piece.Move(GetXBoard(), GetYBoard(), chessBoard);
                    // Add this line to destroy the move plates after moving
                    piece.DestroyMovePlates();
                }
            }
        }
    }
}
