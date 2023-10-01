using UnityEditor;
using UnityEngine;

namespace DefaultNamespace
{
  public class Cell{
    private bool _isAlive;
    private GameObject _gameObject;
    private Renderer _renderer;
    public Cell(GameObject obj)
    {
      _gameObject = obj;
      _renderer = _gameObject.GetComponent<Renderer>();
      this.Disable();
    }

    public Cell(Cell oldCell)
    {
      _gameObject = oldCell.GetObject();
      _renderer = oldCell.GetRenderer();
      _isAlive = oldCell.IsAlive();
    }

    public GameObject GetObject()
    {
      return _gameObject;
    }
    
    public Renderer GetRenderer()
    {
      return _renderer;
    }

    public void Disable()
    {
      _isAlive = false;
      _renderer.material.color = new Color(1, 1, 1, 0.001f);
    }
    public void Enable()
    {
      _isAlive = true;
      _renderer.material.color = new Color(0.44f, 0.64f, 1f, 0.3f);
    }
    public bool IsAlive()
    {
      return _isAlive;
    }
  }
}