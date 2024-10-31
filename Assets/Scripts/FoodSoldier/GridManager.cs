using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int _width, _height;
    [SerializeField] private Tile _tilePrefab;
    [SerializeField] private Canvas _canvas;

    private Dictionary<Vector2, Tile> _tiles;

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        _tiles = new Dictionary<Vector2, Tile>();
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                float tmpX = -190f + x * 56f;
                float tmpY = -145f + y * 65f;

                var spawnTile = Instantiate(_tilePrefab, _canvas.transform);
                spawnTile.GetComponent<RectTransform>().anchoredPosition = new Vector2(tmpX, tmpY);

                spawnTile.name = $"Tile {x} {y}";
                _tiles[new Vector2(x,y)] = spawnTile;
            }
        }
    }

}
