using UnityEngine;

public class Pawn : ChessPiece
{
//    public override bool IsValidMove(Vector3 oldPosition, Vector3 newPosition, out GameObject encounteredEnemy, bool excludeCheck = false)
//    {

//        bool isValid = false;
//        encounteredEnemy = GetPieceOnPosition(newPosition.x, newPosition.y);

//        // If the new position is on the rank above (White) or below (Black)
//        if ((playerColor == "white" && oldPosition.y + 1 == newPosition.y) ||
//           (playerColor == "black" && oldPosition.y - 1 == newPosition.y))
//        {
//            // If moving forward
//            if (oldPosition.x == newPosition.x && encounteredEnemy == null)
//            {
//                isValid = true;
//            }
//            // If moving diagonally
//            else if (oldPosition.x == newPosition.x - 1 || oldPosition.x == newPosition.x + 1)
//            {
//                // Check if en passant is available
//                if (encounteredEnemy == null)
//                {
//                    encounteredEnemy = GetPieceOnPosition(newPosition.x, oldPosition.y);
//                    if (encounteredEnemy != null && encounteredEnemy.DoubleStep == false)
//                    {
//                        encounteredEnemy = null;
//                    }
//                }
//                // If an enemy piece is encountered
//                if (encounteredEnemy != null && encounteredEnemy.playerColor != this.playerColor)
//                {
//                    isValid = true;
//                }
//            }
//        }
//        // Double-step
//        else if ((playerColor == "white" && oldPosition.x == newPosition.x && oldPosition.y + 2 == newPosition.y) ||
//                 (playerColor == "black" && oldPosition.x == newPosition.x && oldPosition.y - 2 == newPosition.y))
//        {
//            if (this.moved == false && GetPieceOnPosition(newPosition.x, newPosition.y) == null)
//            {
//                isValid = true;
//            }
//        }

//        return isValid;
//    }
}

