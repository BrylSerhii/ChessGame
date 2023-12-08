using UnityEngine;

public class Pawn : ChessPiece
{
    public override void GenerateMovePlates()
    {
        // Check the color of the pawn
        if (playerColor == "white")
        {
            // White pawns can move forward one square
            CreateMovePlate(_xBoard, _yBoard + 1);

            //// White pawns can capture diagonally
            //CreateMovePlate(_xBoard - 1, _yBoard + 1);
            //CreateMovePlate(_xBoard + 1, _yBoard + 1);
        }
        else if (playerColor == "black")
        {
            // Black pawns can move forward one square
            CreateMovePlate(_xBoard, _yBoard - 1);

            // Black pawns can capture diagonally
            CreateMovePlate(_xBoard - 1, _yBoard - 1);
            CreateMovePlate(_xBoard + 1, _yBoard - 1);
        }
    }


    public override bool CanMove(int newX, int newY, ChessBoard board)
    {
        // Check if move is within board boundaries
        if (0 <= newX && newX < board.GetXBoard() && 0 <= newY && newY < board.GetYBoard())
        {
            // Check for forward movement
            if (newX == _xBoard && newY == _yBoard + 1)
            {
                return true;
            }

            // Check for diagonal capture
            if ((newX == _xBoard - 1 || newX == _xBoard + 1) && newY == _yBoard + 1)
            {
                return true;
            }
        }

        return false;
    }
}
