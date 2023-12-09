using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class ChessBoard : MonoBehaviour
{

    [SerializeField] private BoardTile _tilePrefab;

    [SerializeField] private GameObject[,] positions = new GameObject[8, 8];

    [SerializeField] private Transform _cam;


    public int _xBoard;
    public int _yBoard;
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
