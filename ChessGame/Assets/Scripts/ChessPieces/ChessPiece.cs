using System;
using UnityEngine;
public class ChessPiece : MonoBehaviour
{

    public int _xBoard;
    public int _yBoard;

    public string pieceType;
    public string playerColor;
    public bool isWhite;

    public void Start()
    {
        SpawnAllPieces();
    }
    public void SetBoardPosition(int x, int y)
    {
        _xBoard = x;
        _yBoard = y;
    }
    public void SetPosition(GameObject obj)
    {
        ChessPiece cm = obj.GetComponent<ChessPiece>();

        //Overwrites either empty space or whatever was there
        positions[cm.GetXBoard(), cm.GetYBoard()] = obj;
    }

    public int GetXBoard()
    {
        return _xBoard;
    }

    public int GetYBoard()
    {
        return _yBoard;
    }

    public GameObject GetPosition(int x, int y)
    {
        return positions[x, y];
    }
    public void SetPositionEmpty(int x, int y)
    {
        positions[x, y] = null;
    }

    private GameObject[,] positions = new GameObject[8, 8];

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

    public GameObject MovePlatePrefab;


    void SpawnSinglePiece(int x, int y, string pieceType, string color)
    {
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
            GameObject newPiece = Instantiate(prefab, new Vector3(x, y, -1), Quaternion.identity);
            ChessPiece piece = null;

            switch (pieceType)
            {
                case "White Pawn":
                case "Black Pawn":
                    piece = newPiece.AddComponent<Pawn>();
                    break;
                case "White Rook":
                case "Black Rook":
                    piece = newPiece.AddComponent<Rook>();
                    break;
                case "White Knight":
                case "Black Knight":
                    piece = newPiece.AddComponent<Knight>();
                    break;
                case "White Bishop":
                case "Black Bishop":
                    piece = newPiece.AddComponent<Bishop>();
                    break;
                case "White Queen":
                case "Black Queen":
                    piece = newPiece.AddComponent<Queen>();
                    break;
                case "White King":
                case "Black King":
                    piece = newPiece.AddComponent<King>();
                    break;
            }

            // Initialize the piece
            if (piece != null)
            {
                piece.SetBoardPosition(x, y);
                piece.playerColor = color;
                piece.pieceType = pieceType;
            }
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

    private Vector3 screenPoint;
    private Vector3 offset;

    void OnMouseDown()
    {
        // Translate the chess piece's position from world space to screen space
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

        // Calculate the offset between the top left corner of the screen and the chess piece's position
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    void OnMouseDrag()
    {
        // Translate the mouse position from screen space to world space
        Vector3 cursorScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorScreenPoint) + offset;

        // Round the position to the nearest cell
        cursorPosition.x = Mathf.Round(cursorPosition.x);
        cursorPosition.y = Mathf.Round(cursorPosition.y);

        // Clamp the position to the board's boundaries
        cursorPosition.x = Mathf.Clamp(cursorPosition.x, 0, 7); // Assuming an 8x8 board
        cursorPosition.y = Mathf.Clamp(cursorPosition.y, 0, 7);

        // Move the chess piece to the cursor's position
        transform.position = cursorPosition;
    }

}

