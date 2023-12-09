using System;
using UnityEngine;

public class Pawn : ChessPiece
{
    // The game board
    public GameObject[] Board;

    // Whether this is the pawn's first move
    private bool isFirstMove = true;

    public override void Move(GameObject gameObject, int targetX, int targetY)
    {
        if (isValidMove(targetX, targetY))
        {

            // Get the position of the selected pawn on the board
            Vector2Int position = new Vector2Int();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (BoardManager.instance.Board[j, i] == this.gameObject)
                    {
                        position = new Vector2Int(i, j);
                        break;
                    }
                }
            }

            // The pawn can always move forward one square
            if (position.y + 1 < 8)
            {
                GameObject boardPiece = BoardManager.instance.Board[position.y + 1, position.x];
                if (boardPiece != null)
                {
                    boardPiece.GetComponent<SpriteRenderer>().enabled = true;
                }
            }

            // If this is the pawn's first move, it can move forward two squares
            if (isFirstMove && position.y + 2 < 8)
            {
                GameObject boardPiece = BoardManager.instance.Board[position.y + 2, position.x];
                if (boardPiece != null)
                {
                    boardPiece.GetComponent<SpriteRenderer>().enabled = true;
                }
            }

            // The pawn can capture diagonally
            if (position.y + 1 < 8 && position.x - 1 >= 0)
            {
                GameObject boardPiece = BoardManager.instance.Board[position.y + 1, position.x - 1];
                if (boardPiece != null)
                {
                    boardPiece.GetComponent<SpriteRenderer>().enabled = true;
                }
            }
            if (position.y + 1 < 8 && position.x + 1 < 8)
            {
                GameObject boardPiece = BoardManager.instance.Board[position.y + 1, position.x + 1];
                if (boardPiece != null)
                {
                    boardPiece.GetComponent<SpriteRenderer>().enabled = true;
                }
            }

            FinishMove();
        }
    }
    public override bool isValidMove(int targetX, int targetY)
    {
        // Calculate the difference between the current position and the target position
        int deltaX = targetX - _xBoard;
        int deltaY = targetY - _yBoard;

        // Pawns can only move forward
        if (isWhite)
        {
            // White pawns can only move up the board (increase in y)
            if (deltaY <= 0)
            {
                return false;
            }
        }
        else
        {
            // Black pawns can only move down the board (decrease in y)
            if (deltaY >= 0)
            {
                return false;
            }
        }

        // Pawns can only move one square forward, or two squares if it's their first move
        if (Math.Abs(deltaY) > 1 && !isFirstMove || Math.Abs(deltaY) > 2)
        {
            return false;
        }

        // Pawns can only move straight forward, unless they're capturing
        if (deltaX != 0)
        {
            // If the pawn is not moving diagonally by one square, it's an invalid move
            if (Math.Abs(deltaX) != 1 || Math.Abs(deltaY) != 1)
            {
                return false;
            }

            // TODO: Check if there's an enemy piece at the target position
        }

        // If none of the invalid conditions were met, the move is valid
        return true;
    }

}
