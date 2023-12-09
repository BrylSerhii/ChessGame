using UnityEngine;

public class BoardManager : MonoBehaviour
{

    // The game board
    [SerializeField] private BoardTile _tilePrefab;
    [SerializeField] private GameObject[,] positions = new GameObject[8, 8];
    [SerializeField] private Transform _cam;

    // The selected piece
    private GameObject Piece_Selected;

    public GameObject[,] Board = new GameObject[8, 8];

    // The current player's color
    private string currentPlayerColor = "White";

    public int _xBoard = 8;
    public int _yBoard = 8;

    // Singleton instance
    public static BoardManager instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.LogError("Another instance of BoardManager already exists!");
        }
    }

    void Start()
    {

        _cam.transform.position = new Vector3((float)_xBoard / 2 - 0.5f, (float)_yBoard / 2 - 0.5f, -10);

        GenerateBoard();


    }
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

                // Store the tile in the positions array
                positions[y, x] = spawnedTile.gameObject;
            }
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
}
