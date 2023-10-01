using System.Collections;
using System.Collections.Generic;
using System.Net;
using DefaultNamespace;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class Main : MonoBehaviour
{
    [SerializeField] private GameObject _cellPrefab;
    private const int NumCellsPerSide = 30;
    private const int NumCellsPerSideX = NumCellsPerSide;
    private const int NumCellsPerSideY = NumCellsPerSide;
    private const int NumCellsPerSideZ = NumCellsPerSide;

    [SerializeField][Range(0, NumCellsPerSideX*NumCellsPerSideY*NumCellsPerSideZ)] private int initialAliveCellNum = 100;
    
    [SerializeField][Range(0, 3*3*3 -1)] private int deathBorderMin = 4;
    [SerializeField][Range(0, 3*3*3 -1)] private int birthBorderMin = 9;
    [SerializeField][Range(0, 3*3*3 -1)] private int birthBorderMax = 9;
    [SerializeField][Range(0, 3*3*3 -1)] private int deathBorderMax = 10;
    
    
    Cell[,,] _cells = new Cell[NumCellsPerSideX, NumCellsPerSideY, NumCellsPerSideZ];
    
    DisplayText _displayText;
    [SerializeField] private TextMeshProUGUI _textPrefab;

    // Start is called before the first frame update
    void Start()
    {
        for(int x = 0; x < NumCellsPerSideX; x++) {
            for(int y = 0; y < NumCellsPerSideY; y++) {
                for(int z = 0; z < NumCellsPerSideZ; z++) {
                    _cells[x, y, z] = new Cell(
                        Instantiate(_cellPrefab, new Vector3(x, y, z), Quaternion.identity));
                }
            }
        }
        
        // 初期状態のセルの誕生
        for (int i = 0; i <= initialAliveCellNum; i++) {
            int x = Random.Range(0, (int)(NumCellsPerSideX*0.2f));
            int y = Random.Range(0, (int)(NumCellsPerSideY*0.2f));
            int z = Random.Range(0, (int)(NumCellsPerSideZ*0.2f));
            _cells[x, y, z].Enable();
        }
        
        _displayText = new DisplayText(_textPrefab);
        _displayText.SetText("Generation: 0");
    }

    [SerializeField] private float timeInterval = 0.5f;
    private float _timeCurrent = 0f;
    private int _generation = 0;
    // Update is called once per frame
    void Update() {
        _timeCurrent += Time.deltaTime;
        if (timeInterval > _timeCurrent) return;
        _timeCurrent = 0.0f;
        _generation++;
        _displayText.SetText("Generation: " + _generation.ToString());

        Cell[,,] currentCells = new Cell[NumCellsPerSideX, NumCellsPerSideY, NumCellsPerSideZ];
        for (int x = 0; x < NumCellsPerSideX; x++) {
            for (int y = 0; y < NumCellsPerSideY; y++) {
                for (int z = 0; z < NumCellsPerSideZ; z++)
                {
                    currentCells[x, y, z] = new Cell(_cells[x, y, z]);
                }
            }
        }
        
        for (int x = 0; x < NumCellsPerSideX; x++) {
            for (int y = 0; y < NumCellsPerSideY; y++) {
                for (int z = 0; z < NumCellsPerSideZ; z++) {
                    // 周囲の生きているセルを数える
                    int aroundAliveCount = CountAroundAlives(ref currentCells, x, y, z);
                    if (currentCells[x, y, z].IsAlive())
                    {
                        //Debug.Log($"around alive: {aroundAliveCount}, xyz: ({x}, {y}, {z})");
                    }
                    
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
        // Debug.Log("CountAroundAlives");
        // Debug.Log($"xyz: ({x}, {y}, {z})");
        // Debug.Log("around:");
        for (int aroundX = x-1; aroundX <= x+1; aroundX++) {
            for (int aroundY = y-1; aroundY <= y+1; aroundY++) {
                for (int aroundZ = z-1; aroundZ <= z+1; aroundZ++) {
                    // Debug.Log($"({aroundX}, {aroundY}, {aroundZ})");
                    if(aroundX == x && aroundY == y && aroundZ == z) continue;
                    if(aroundX < 0 || aroundX >= NumCellsPerSideX) continue;
                    if(aroundY < 0 || aroundY >= NumCellsPerSideY) continue;
                    if(aroundZ < 0 || aroundZ >= NumCellsPerSideZ) continue;
                    if (currentCells[aroundX, aroundY, aroundZ].IsAlive()) {
                        // Debug.Log($"({x}, {y}, {z})'s around ({aroundX}, {aroundY}, {aroundZ}) is alive.");
                        aroundAliveCount++;
                    }
                }
            }
        }
        
        return aroundAliveCount;
    }
}
