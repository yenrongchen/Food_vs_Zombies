using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int _width, _height;
    [SerializeField] private Tile _tilePrefab;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private int _wave = 0;

    private Dictionary<Vector2, Tile> _tiles;

    void Start()
    {
        GenerateGrid();
        SetTilesbyWave(_wave);
        _wave = 0;
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
                _tiles[new Vector2(x, y)] = spawnTile;
            }
        }
    }

    public void SetTilesbyWave(int wave)
    {
        _wave = wave;
        foreach (var tile in _tiles)
        {
            int y = (int)tile.Key.y;
            bool canInteract = false;

            switch (wave)
            {
                case 0:
                case 1:
                    canInteract = y == 2;
                    break;
                case 2:
                    canInteract = (y >= 1 && y <= 3);
                    break;
                case 3:
                    canInteract = true;
                    break;
                default:
                    canInteract = false;
                    break;
            }

            tile.Value.SetInteract(canInteract);
        }
    }

    public void setWave(int wave)
    {
        _wave = wave;
        if (_wave > 3) _wave = 3;
        SetTilesbyWave(_wave);
    }

    /* // DEBUG: press space for next wave
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextWave();
        }
    }*/ 


}
