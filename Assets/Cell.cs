using UnityEditor;
using UnityEngine;

namespace DefaultNamespace
{
  public class Cell{
    private bool _isAlive;
    private bool _isAlived;
    private GameObject _gameObject;
    private Renderer _renderer;
    private Color _initialColor;
    public Cell(GameObject obj)
    {
      _gameObject = obj;
      _renderer = _gameObject.GetComponent<Renderer>();
      _initialColor = _renderer.material.color;
      this.Disable();
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
      _renderer.material.color = new Color(1, 1, 1, 0.0f);
    }
    public void Enable()
    {
      _isAlive = true;
      _renderer.material.color = _initialColor;
    }
    public bool IsAlived()
    {
      return _isAlived;
    }

    public void ShiftIsAlive()
    {
      _isAlived = _isAlive;
    }
  }
}