using UnityEngine;

public class GameManager : MonoBehaviour
{
    // The game board
    public GameObject[] Board;

    // The selected piece
    public GameObject Piece_Selected;

    // The current player's color
    private string currentPlayerColor = "White";

    // Update is called once per frame
    void Update()
    {
        // Check if the left mouse button was clicked
        if (Input.GetMouseButtonDown(0))
        {
            // Create a ray from the camera to the position of the mouse cursor
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Perform a raycast
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                // Check if a piece was hit
                if (hit.transform.name.StartsWith(currentPlayerColor))
                {
                    // Store the selected piece
                    Piece_Selected = hit.transform.gameObject;

                    // Get the script attached to the selected piece
                    var pieceScript = Piece_Selected.GetComponent<MonoBehaviour>();

                    // Use reflection to call the Move method of the script
                    var moveMethod = pieceScript.GetType().GetMethod("Move");
                    if (moveMethod != null)
                    {
                        moveMethod.Invoke(pieceScript, null);
                    }

                    // Switch the current player's color
                    currentPlayerColor = currentPlayerColor == "White" ? "Black" : "White";
                }
            }
        }
    }
}
