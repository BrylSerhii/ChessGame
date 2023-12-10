using System;
using UnityEngine;
public class ChessPiece : MonoBehaviour
{
    public GameController GameController;

    public int _xBoard;
    public int _yBoard;

    public string pieceType;
    public string playerColor;
    public bool isWhite;

    public bool DoubleStep = false;
    public bool MovingY = false;
    public bool MovingX = false;

    private Vector3 oldPosition;
    private Vector3 newPositionY;
    private Vector3 newPositionX;
    public bool moved = false;
    public float HighestRankY = 3.5f;
    public float LowestRankY = -3.5f;
    public float MoveSpeed = 20;
    public void SetPlayerColor(string color)
    {
        playerColor = color;
    }

    public void SetPieceType(string type)
    {
        pieceType = type;
    }


    public void Start()
    {
        if (GameController == null) GameController = FindObjectOfType<GameController>();
        SpawnAllPieces();
    }
    void Update()
    {
        if (MovingY || MovingX)
        {
            if (Mathf.Abs(oldPosition.x - newPositionX.x) == Mathf.Abs(oldPosition.y - newPositionX.y))
            {
                MoveDiagonally();
            }
            else
            {
                MoveSideBySide();
            }
        }
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
                piece.isWhite = color == "white";
                piece.SetBoardPosition(x, y);
                piece.SetPlayerColor(color);
                piece.SetPieceType(pieceType);
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

        public void MouseDragginDown(GameObject gameObject)
        {
            // Translate the chess piece's position from world space to screen space
            screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

            // Calculate the offset between the top left corner of the screen and the chess piece's position
            offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        }

    void OnMouseDown()
    {
        if (GameController.SelectedPiece != null && GameController.SelectedPiece.GetComponent<ChessPiece>().IsMoving() == true)
        {
            // Prevent clicks during movement
            return;
        }

        if (GameController.SelectedPiece == this.gameObject)
        {
            GameController.DeselectPiece();
        }
        else
        {
            if (GameController.SelectedPiece == null)
            {
                GameController.SelectPiece(this.gameObject);
            }
            else
            {
                if (this.tag == GameController.SelectedPiece.tag)
                {
                    GameController.SelectPiece(this.gameObject);
                }
                else if ((this.tag == "White" && GameController.SelectedPiece.tag == "Black") || (this.tag == "Black" && GameController.SelectedPiece.tag == "White"))
                {
                    GameController.SelectedPiece.GetComponent<ChessPiece>().MovePiece(this.transform.position);
                }
            }
        }
    }


    public bool MovePiece(Vector3 newPosition, bool castling = false)
    {
        ChessPiece encounteredEnemy = null;

        newPosition.z = this.transform.position.z;
        this.oldPosition = this.transform.position;

        if (castling || IsValidMove(oldPosition, newPosition, out encounteredEnemy))
        {
            // Double-step
            if (this.name.Contains("Pawn") && Mathf.Abs(oldPosition.y - newPosition.y) == 2)
            {
                this.DoubleStep = true;
            }
            // Promotion
           
            // Castling
        
            this.moved = true;

            this.newPositionY = newPosition;
            this.newPositionY.x = this.transform.position.x;
            this.newPositionX = newPosition;
            MovingY = true; // Start movement

            Destroy(encounteredEnemy);
            return true;
        }
        else
        {
            GameController.GetComponent<AudioSource>().Play();
            return false;
        }
    }
    public virtual bool IsValidMove(Vector3 oldPosition, Vector3 newPosition, out ChessPiece encounteredEnemy, bool excludeCheck = false)
    {
        bool isValid = false;
        encounteredEnemy = GetPieceOnPosition(newPosition.x, newPosition.y);
        // Default implementation
        return true;
    }

    public ChessPiece GetPieceOnPosition(float positionX, float positionY, string color = null)
    {
        // Get all instances of the ChessPiece class
        ChessPiece[] allPieces = FindObjectsOfType<ChessPiece>();

        foreach (ChessPiece piece in allPieces)
        {
            // Check if the piece's position matches the given position
            if (piece.transform.position.x == positionX && piece.transform.position.y == positionY)
            {
                // If a color is specified, check if the piece's color matches the given color
                if (color == null || piece.playerColor.ToLower() == color.ToLower())
                {
                    return piece;
                }
            }
        }

        // If no matching piece is found, return null
        return null;
    }

    void MoveSideBySide()
    {
        if (MovingY == true)
        {
            this.transform.SetPositionAndRotation(Vector3.Lerp(this.transform.position, newPositionY, Time.deltaTime * MoveSpeed), this.transform.rotation);
            if (this.transform.position == newPositionY)
            {
                MovingY = false;
                MovingX = true;
            }
        }
        if (MovingX == true)
        {
            this.transform.SetPositionAndRotation(Vector3.Lerp(this.transform.position, newPositionX, Time.deltaTime * MoveSpeed), this.transform.rotation);
            if (this.transform.position == newPositionX)
            {
                this.transform.SetPositionAndRotation(newPositionX, this.transform.rotation);
                MovingX = false;
                if (GameController.SelectedPiece != null)
                {
                    GameController.DeselectPiece();
                    GameController.EndTurn();
                }
            }
        }
    }

    void MoveDiagonally()
    {
        if (MovingY == true)
        {
            this.transform.SetPositionAndRotation(Vector3.Lerp(this.transform.position, newPositionX, Time.deltaTime * MoveSpeed), this.transform.rotation);
            if (this.transform.position == newPositionX)
            {
                this.transform.SetPositionAndRotation(newPositionX, this.transform.rotation);
                MovingY = false;
                MovingX = false;
                if (GameController.SelectedPiece != null)
                {
                    GameController.DeselectPiece();
                    GameController.EndTurn();
                }
            }
        }
    }
    public bool IsMoving()
    {
        return MovingX || MovingY;
    }

    enum Direction
    {
        Horizontal,
        Vertical,
        Diagonal
    }
}

