using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEditor;
using UnityEngine;

public class Main : MonoBehaviour
{
    [SerializeField] private GameObject _cellPrefab;
    private const int cellNum = 8;
    
    class Cell
    {
        private bool _isAlive;
        private GameObject _gameObject;
        private Renderer _renderer;
        public Cell(Vector3 position, GameObject prefab)
        {
            _gameObject = Instantiate(prefab, position, Quaternion.identity);
            _renderer = _gameObject.GetComponent<Renderer>();
            _isAlive = false;
            this.Disable();
        }

        public void Disable()
        {
            _isAlive = false;
            _renderer.material.color = new Color(1, 1, 1, 0.001f);
        }
        public void Enable()
        {
            _isAlive = true;
            _renderer.material.color = new Color(0, 0, 0, 0.6f);
        }
        public bool IsAlive()
        {
            return _isAlive;
        }
    }
    Cell[,,] _cells = new Cell[cellNum, cellNum, cellNum];

    // Start is called before the first frame update
    void Start()
    {
        for(int x = 0; x < cellNum; x++)
        {
            for(int y = 0; y < cellNum; y++)
            {
                for(int z = 0; z < cellNum; z++)
                {
                    _cells[x, y, z] = new Cell(new Vector3(x, y, z), _cellPrefab);
                }
            }
        }

        for (int i = 0; i <= 50; i++)
        {
            int x = Random.Range(0, cellNum-1);
            int y = Random.Range(0, cellNum-1);
            int z = Random.Range(0, cellNum-1);
            _cells[x, y, z].Enable();
        }
    }

    private float timeCurrent = 0.0f;
    private float timeInterval = 0.5f;
    // Update is called once per frame
    void Update()
    {
        timeCurrent += Time.deltaTime;
        if (timeCurrent <= timeInterval) return;
        timeCurrent = 0.0f;
        Debug.Log("Hello");
        
        Cell[,,] currentCells = _cells;
        for (int x = 0; x < cellNum; x++)
        {
            for (int y = 0; y < cellNum; y++)
            {
                for (int z = 0; z < cellNum; z++)
                {
                    int aroundAliveCount = 0;
                    for (int around_x = x - 1; around_x <= x + 1; around_x++)
                    {
                        for (int around_y = y - 1; around_y <= y + 1; around_y++)
                        {
                            for (int around_z = z - 1; around_z <= z + 1; around_z++)
                            {
                                if(around_x == x && around_y == y && around_z == z) continue;
                                if(around_x < 0 || around_x >= cellNum) continue;
                                if(around_y < 0 || around_y >= cellNum) continue;
                                if(around_z < 0 || around_z >= cellNum) continue;

                                if (currentCells[around_x, around_y, around_z].IsAlive())
                                {
                                    aroundAliveCount++;
                                }
                            }
                        }
                    }

                    if (aroundAliveCount >= 8 && aroundAliveCount <= 14)
                    {
                        _cells[x, y, z].Enable();
                    }
                    if (aroundAliveCount <= 4 || aroundAliveCount >= 16)
                    {
                        _cells[x, y, z].Disable();
                    }
                }
            }
        }
    }
}
