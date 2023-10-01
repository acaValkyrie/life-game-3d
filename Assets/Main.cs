using System.Collections;
using System.Collections.Generic;
using System.Net;
using DefaultNamespace;
using TMPro;
using UnityEditor;
using UnityEngine;

public class Main : MonoBehaviour
{
    [SerializeField] private GameObject _cellPrefab;
    private const int numCellsPerSide = 15;
    
    [SerializeField][Range(0, numCellsPerSide*numCellsPerSide*numCellsPerSide)] private int initialAliveCellNum = 100;
    
    [SerializeField][Range(0, 3*3*3 -1)] private int deathBorderMin = 4;
    [SerializeField][Range(0, 3*3*3 -1)] private int birthBorderMin = 8;
    [SerializeField][Range(0, 3*3*3 -1)] private int birthBorderMax = 14;
    [SerializeField][Range(0, 3*3*3 -1)] private int deathBorderMax = 16;
    
    
    Cell[,,] _cells = new Cell[numCellsPerSide, numCellsPerSide, numCellsPerSide];
    
    DisplayText _displayText;
    [SerializeField] private TextMeshProUGUI _textPrefab;

    // Start is called before the first frame update
    void Start()
    {
        for(int x = 0; x < numCellsPerSide; x++) {
            for(int y = 0; y < numCellsPerSide; y++) {
                for(int z = 0; z < numCellsPerSide; z++) {
                    _cells[x, y, z] = new Cell(new Vector3(x, y, z), _cellPrefab);
                }
            }
        }
        
        // 初期状態のセルの誕生
        for (int i = 0; i <= initialAliveCellNum; i++) {
            int x = Random.Range(0, numCellsPerSide-1);
            int y = Random.Range(0, numCellsPerSide-1);
            int z = Random.Range(0, numCellsPerSide-1);
            _cells[x, y, z].Enable();
        }

        _displayText = new DisplayText(_textPrefab);
    }

    private float _timeCurrent = 0.0f;
    private const float TimeInterval = 0.5f;
    private int _generation = 0;
    // Update is called once per frame
    void Update() {
        _timeCurrent += Time.deltaTime;
        if (TimeInterval > _timeCurrent) return;
        _timeCurrent = 0.0f;
        _generation++;
        _displayText.SetText("Generation: " + _generation.ToString());

        Cell[,,] currentCells = _cells;
        for (int x = 0; x < numCellsPerSide; x++) {
            for (int y = 0; y < numCellsPerSide; y++) {
                for (int z = 0; z < numCellsPerSide; z++) {
                    // 周囲の生きているセルを数える
                    int aroundAliveCount = CountAroundAlives(ref currentCells, x, y, z);
                    
                    // aroundAliveCount に応じてセルを生かすか殺すか決める
                    if ( birthBorderMin <= aroundAliveCount && aroundAliveCount <= birthBorderMax) {
                        _cells[x, y, z].Enable();
                    }
                    if (aroundAliveCount <= deathBorderMin || deathBorderMax <= aroundAliveCount) {
                        _cells[x, y, z].Disable();
                    }
                }
            }
        }
    }
    
    int CountAroundAlives(ref Cell[,,] currentCells, int x, int y, int z)
    {
        int aroundAliveCount = 0;
        for (int around_x = x - 1; around_x <= x + 1; around_x++) {
            for (int around_y = y - 1; around_y <= y + 1; around_y++) {
                for (int around_z = z - 1; around_z <= z + 1; around_z++) {
                    if(around_x == x && around_y == y && around_z == z) continue;
                    if(around_x < 0 || around_x >= numCellsPerSide) continue;
                    if(around_y < 0 || around_y >= numCellsPerSide) continue;
                    if(around_z < 0 || around_z >= numCellsPerSide) continue;

                    if (currentCells[around_x, around_y, around_z].IsAlive()) {
                        aroundAliveCount++;
                    }
                }
            }
        }

        return aroundAliveCount;
    }
}
