using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {
    public int width = 16;
    public int height = 16;
    public int mines = 32;

    private int revealedCells;

    private Board board;
    private Cell[,] state;
    private bool gameOver;
    public Timer timer;
    private bool timerStart;

    private void Awake() {
        board = GetComponentInChildren<Board>();
    }

    private void Start() {
        NewGame();
    }

    private void NewGame() {
        state = new Cell[width, height];
        gameOver = false;
        timerStart = false;
        revealedCells = 0;

        GenerateCells();
        GenerateMines();
        GenerateNumbers();

        Camera.main.transform.position = new Vector3(width / 2, height / 2, -10f);
        Camera.main.orthographicSize = height * 0.625f;

        board.Draw(state);
    }

    private void GenerateCells() {
        for(int i = 0; i < width; i++) {
            for(int j = 0; j < height; j++) {
                Cell cell = new Cell();
                cell.position = new Vector3Int(i, j, 0);
                cell.type = Cell.Type.Empty;

                state[i, j] = cell;
            }
        }
    }

    private void GenerateMines() {
        for(int i = 0; i < mines; i++) {
            int x = Random.Range(0, width);
            int y = Random.Range(0, height);

            /*
            while (state[x, y].type == Cell.Type.Mine) {
                x++;

                if(x >= width) {
                    x = 0;
                    y++;

                    if (y >= height) y = 0;
                }
            }*/

            while (state[x, y].type == Cell.Type.Mine) {
                x = Random.Range(0, width);
                y = Random.Range(0, height);
            }

            state[x, y].type = Cell.Type.Mine;
        }
    }

    private void GenerateNumbers() {
        for(int i = 0; i < width; i++) {
            for(int j = 0; j < height; j++) {
                Cell cell = state[i, j];

                if (cell.type == Cell.Type.Mine) continue;

                cell.number = GetNumberOfMines(i, j);
                if (cell.number > 0) cell.type = Cell.Type.Number;

                state[i, j] = cell;
            }
        }
    }

    private int GetNumberOfMines(int cellX, int cellY) {
        int count = 0;

        for(int adjX = -1; adjX <= 1; adjX++) {
            for(int adjY = -1; adjY <= 1; adjY++) {
                if (adjX == 0 && adjY == 0) continue;

                int x = cellX + adjX;
                int y = cellY + adjY;

                if (GetCell(x, y).type == Cell.Type.Mine) count++;
            }
        }

        return count;
    }

    private void Update() {
        if(!gameOver) {
            if(!timerStart && revealedCells >= 1) {
                timerStart = true;
                timer.StartTimer();
            }

            if (Input.GetMouseButtonDown(1)) {
                Flag();
            } else if (Input.GetMouseButtonDown(0)) {
                Reveal();
            }   
        }
        
    }

    private void Flag() {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int cellPos = board.tileMap.WorldToCell(worldPos);

        Cell cell = GetCell(cellPos.x, cellPos.y);

        if (cell.type == Cell.Type.Invalid || cell.revealed) return;

        cell.flagged = !cell.flagged;
        state[cellPos.x, cellPos.y] = cell;
        board.Draw(state);
    }

    private void Reveal() {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int cellPos = board.tileMap.WorldToCell(worldPos);

        Cell cell = GetCell(cellPos.x, cellPos.y);

        if (cell.type == Cell.Type.Invalid || cell.revealed || cell.flagged) return;

        switch(cell.type) {
            case Cell.Type.Empty:
                Flood(cell);
                break;
            case Cell.Type.Mine:
                Explode(cell);
                CheckWinCondition();
                break;
            default:
                cell.revealed = true;
                state[cellPos.x, cellPos.y] = cell;
                revealedCells++;
                CheckWinCondition();
                break;
        }

        board.Draw(state);
    }

    private void Flood(Cell cell) {
        if (cell.revealed) return;
        if (cell.type == Cell.Type.Invalid || cell.type == Cell.Type.Mine) return;

        revealedCells++;

        cell.revealed = true;
        state[cell.position.x, cell.position.y] = cell;

        if (cell.type == Cell.Type.Empty) {
            Flood(GetCell(cell.position.x - 1, cell.position.y));
            Flood(GetCell(cell.position.x + 1, cell.position.y));
            Flood(GetCell(cell.position.x, cell.position.y - 1));
            Flood(GetCell(cell.position.x, cell.position.y + 1));
        }
    }
    
    private void Explode(Cell cell) {
        Debug.Log("Game Over!");
        gameOver = true;
        timer.StopTimer();

        cell.revealed = true;
        cell.exploded = true;
        state[cell.position.x, cell.position.y] = cell;

        for(int i = 0; i < width; i++) {
            for(int j = 0; j < height; j++) {
                cell = state[i, j];

                if (cell.type == Cell.Type.Mine) {
                    cell.revealed = true;
                    state[i, j] = cell;
                }
            }
        }
    }

    private void CheckWinCondition() {
        int maxCells = (width * height) - mines;

        if(revealedCells == maxCells) {
            Debug.Log("Winner!");
            gameOver = true;
            timer.StopTimer();
        }
    }

    private Cell GetCell(int x, int y) {
        if (isValid(x, y))
            return state[x, y];
        else
            return new Cell();
    }

    private bool isValid(int x, int y) {
        return x >= 0 && x < width && y >= 0 && y < height;
    }
}
