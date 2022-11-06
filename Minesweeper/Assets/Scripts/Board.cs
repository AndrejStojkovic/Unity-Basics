using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Board : MonoBehaviour {
    public Tilemap tileMap { get; private set; }

    public Tile tileUnknown, tileEmpty, tileMine, tileExploded, tileFlag;
    public Tile[] tileNumber;

    private void Awake() {
        tileMap = GetComponent<Tilemap>();
    }

    public void Draw(Cell[,] state) {
        if (tileMap == null) return;

        int width = state.GetLength(0);
        int height = state.GetLength(1);

        for(int i = 0; i < width; i++) {
            for(int j = 0; j < height; j++) {
                Cell cell = state[i, j];
                tileMap.SetTile(cell.position, GetTile(cell));
            }
        }
    }

    private Tile GetTile(Cell cell) {
        if(cell.revealed) {
            return GetRevealedTile(cell);
        } else if(cell.flagged) {
            return tileFlag;
        } else {
            return tileUnknown;
        }
    }

    private Tile GetRevealedTile(Cell cell) {
        switch(cell.type) {
            case Cell.Type.Empty: return tileEmpty;
            case Cell.Type.Mine: return cell.exploded ? tileExploded : tileMine;
            case Cell.Type.Number: return GetNumberTile(cell);
            default: return null;
        }
    }

    private Tile GetNumberTile(Cell cell) {
        if (cell.number < 1 && cell.number > 8) return null;
        return tileNumber[cell.number - 1];
    }
}
