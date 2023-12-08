using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class ChessBoard : MonoBehaviour
{
    [SerializeField] private BoardTile _tilePrefab;

    [SerializeField] private GameObject[,] positions = new GameObject[8, 8];

    [SerializeField] private Transform _cam;

    private string currentPlayer = "white";

    private bool gameOver = false;

    public int _xBoard;
    public int _yBoard;
    void Start()
    {

        _cam.transform.position = new Vector3((float)_xBoard / 2 - 0.5f, (float)_yBoard / 2 - 0.5f, -10);

        GenerateBoard();
        //SpawnAllPieces();
        //for (int i = 0; i < 8; i++) { SpawnPawn(i, 1); }


    }

    //public void SpawnAllPieces()
    //{
    //    // Create an instance of each piece and call their SpawnAllPieces method
    //    Pawn pawn = new Pawn();
    //    pawn.SpawnAllPieces(this);

    //    // Repeat for other pieces (Rook, Knight, Bishop, Queen, King)
    //}
    private void GenerateBoard()
    {
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                var spawnedTile = Instantiate(_tilePrefab, new Vector3(x, y), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";

                var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                spawnedTile.Init(isOffset);
            }
        }
    }

    public int GetXBoard()
    {
        return 8; // Assuming an 8x8 board
    }

    public int GetYBoard()
    {
        return 8; // Assuming an 8x8 board
    }

    public void SetPosition(GameObject piece)
    {
        ChessPiece pieceScript = piece.GetComponent<ChessPiece>();

        // Check if piece is within valid board boundaries
        if (0 <= pieceScript.GetXBoard() && pieceScript.GetXBoard() < GetXBoard() &&
            0 <= pieceScript.GetYBoard() && pieceScript.GetYBoard() < GetYBoard())
        {
            positions[pieceScript.GetYBoard(), pieceScript.GetXBoard()] = piece;
        }
        else
        {
            Debug.LogError("Invalid piece position!");
        }
    }

    public void SetPositionEmpty(int x, int y)
    {
        positions[y, x] = null;
    }

    public GameObject GetPosition(int x, int y)
    {
        return positions[y, x];
    }

    public void NextTurn()
    {
        if (currentPlayer == "white")
        {
            currentPlayer = "black";
        }
        else
        {
            currentPlayer = "white";
        }
    }

    public string GetCurrentPlayer()
    {
        return currentPlayer;
    }

    public bool IsGameOver()
    {
        return gameOver;
    }

    public void Winner(string playerWinner)
    {
        gameOver = true;

        // ... Implement game over logic and display winner ...
    }
}
