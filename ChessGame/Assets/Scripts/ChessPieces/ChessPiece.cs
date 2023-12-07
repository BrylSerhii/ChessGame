using System;
using UnityEngine;
public class ChessPiece : MonoBehaviour
{

    public int _xBoard;
    public int _yBoard;

    public string pieceType;
    public string playerColor;

    public void Start()
    {
        SpawnAllPieces();
    }
    public void SetBoardPosition(int x, int y)
    {
        _xBoard = x;
        _yBoard = y;
    }

    public int GetXBoard()
    {
        return _xBoard;
    }

    public int GetYBoard()
    {
        return _yBoard;
    }
    public Vector3 GetWorldPositionFromBoardIndex(int x, int y)
    {
        // Calculate the world position based on board dimensions and tile size
        float tileSize = 1.0f; // Adjust this based on your board size
        float offset = tileSize / 2.0f;
        return new Vector3(x * tileSize + offset, 0.0f, y * tileSize + offset);
    }

    public GameObject whitePawnPrefab;
    public GameObject whiteRookPrefab;
    public GameObject whiteKnightPrefab;
    public GameObject whiteBishopPrefab;
    public GameObject whiteQueenPrefab;
    public GameObject whiteKingPrefab;

    public GameObject blackPawnPrefab;
    public GameObject blackRookPrefab;
    public GameObject blackKnightPrefab;
    public GameObject blackBishopPrefab;
    public GameObject blackQueenPrefab;
    public GameObject blackKingPrefab;

    void SpawnSinglePiece(int x, int y, string pieceType, string color)
    {
        //Debug.Log("Spawning " + pieceType + " at " + x + ", " + y);
        GameObject prefab = null;
        switch (pieceType)
        {
            case "White Pawn":
                prefab = whitePawnPrefab;
                break;
            case "White Rook":
                prefab = whiteRookPrefab;
                break;
            case "White Knight":
                prefab = whiteKnightPrefab;
                break;
            case "White Bishop":
                prefab = whiteBishopPrefab;
                break;
            case "White Queen":
                prefab = whiteQueenPrefab;
                break;
            case "White King":
                prefab = whiteKingPrefab;
                break;
            case "Black Pawn":
                prefab = blackPawnPrefab;
                break;
            case "Black Rook":
                prefab = blackRookPrefab;
                break;
            case "Black Knight":
                prefab = blackKnightPrefab;
                break;
            case "Black Bishop":
                prefab = blackBishopPrefab;
                break;
            case "Black Queen":
                prefab = blackQueenPrefab;
                break;
            case "Black King":
                prefab = blackKingPrefab;
                break;
        }

        if (prefab != null)
        {
            GameObject newPiece = Instantiate(prefab, new Vector3(x, y, 0), Quaternion.identity);
            ChessPiece piece = newPiece.AddComponent<ChessPiece>();

            // Initialize the piece
            piece.SetBoardPosition(x, y);
            piece.playerColor = color;
            piece.pieceType = pieceType;
        }
    }

    void SpawnAllPieces()
    {
        //// Spawn pawns
        for (int x = 0; x < 8; x++)
        {
            SpawnSinglePiece(x, 1, "White Pawn", "white");
            SpawnSinglePiece(x, 6, "Black Pawn", "black");
        }
        // Spawn white remaining pieces
        string[] whitePieceTypes = new string[] { "White Rook", "White Knight", "White Bishop", "White Queen", "White King", "White Bishop", "White Knight", "White Rook" };
        for (int i = 0; i < whitePieceTypes.Length; i++)
        {
            SpawnSinglePiece(i, 0, whitePieceTypes[i], "white");
        }
        // Spawn black remaining pieces
        string[] blackPieceTypes = new string[] { "Black Rook", "Black Knight", "Black Bishop", "Black Queen", "Black King", "Black Bishop", "Black Knight", "Black Rook" };
        for (int i = 0; i < blackPieceTypes.Length; i++)
        {
            SpawnSinglePiece(i, 7, blackPieceTypes[i], "black");
        }

    }



    // Implement specific movement logic for each piece type
    public virtual bool CanMove(int newX, int newY, ChessBoard board)
    {
        // Check if move is within board boundaries
        if (0 <= newX && newX < board.GetXBoard() && 0 <= newY && newY < board.GetYBoard())
        {
            // Check for diagonal movement
            if (Math.Abs(newX - _xBoard) != Math.Abs(newY - _yBoard))
            {
                return false;
            }

            // Check if path is clear and destination is valid
            for (int x = Math.Min(_xBoard, newX) + 1, y = Math.Min(_yBoard, newY) + 1; x < Math.Max(_xBoard, newX) && y < Math.Max(_yBoard, newY); x++, y++)
            {
                if (board.GetPosition(x, y) != null)
                {
                    return false;
                }
            }

            // Check if destination is friendly or empty
            if (board.GetPosition(newX, newY) == null || board.GetPosition(newX, newY).GetComponent<ChessPiece>().playerColor != playerColor)
            {
                return true;
            }
        }

        return false;
    }

    public void Move(int newX, int newY, ChessBoard board)
    {
        if (CanMove(newX, newY, board))
        {
            // Update board position
            board.SetPositionEmpty(_xBoard, _yBoard);
            _xBoard = newX;
            _yBoard = newY;
            board.SetPosition(gameObject);

            // Check for captures and other special actions based on move
        }
    }
}
