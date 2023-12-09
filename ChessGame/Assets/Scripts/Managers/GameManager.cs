using UnityEngine;

public class GameManager : MonoBehaviour
{
    // The game board
    [SerializeField] private GameObject[,] positions = new GameObject[8, 8];

    // The selected piece
    private GameObject Piece_Selected;

    // The current player's color
    public string currentPlayerColor = "White";

    // Singleton instance
    public static GameManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.LogError("Another instance of GameManager already exists!");
        }
    }

    public void ChangeTurn()
    {
        // Switch the current player's color
        currentPlayerColor = currentPlayerColor == "White" ? "Black" : "White";
    }


    void Update()
    {
        // Check if the left mouse button was clicked
        if (Input.GetMouseButtonDown(0))
        {
            // Create a ray from the camera to the position of the mouse cursor
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Perform a raycast
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if (hit)
            {
                // Get the world space position
                Vector3 worldPosition = hit.point;

                // Convert the world space position to grid coordinates
                int targetX = Mathf.RoundToInt(worldPosition.x);
                int targetY = Mathf.RoundToInt(worldPosition.y);

                // Check if a piece was hit
                ChessPiece piece = hit.transform.GetComponent<ChessPiece>();
                if (piece && piece.playerColor == currentPlayerColor)
                {
                    // Store the selected piece
                    Piece_Selected = hit.transform.gameObject;

                    // Check if the mouse button is pressed
                    if (Input.GetMouseButtonDown(0))
                    {
                        piece.MouseDragginDown(Piece_Selected);
                    }
                    // Check if the mouse is being dragged
                    else if (Input.GetMouseButton(0))
                    {
                        piece.MouseDragging(Piece_Selected);
                    }

                    // Call the Move method of the piece's script with the target position
                    piece.Move(Piece_Selected, targetX, targetY);

                    // Switch the current player's color
                    ChangeTurn();
                
                }
            }
        }

    }


}
