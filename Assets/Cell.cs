using UnityEditor;
using UnityEngine;

namespace DefaultNamespace
{
  public class Cell{
    private bool _isAlive;
    private Renderer _renderer;
    public Cell(GameObject obj)
    {
      _renderer = obj.GetComponent<Renderer>();
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
      _renderer.material.color = new Color(1f, 0.44f, 0.64f, 0.3f);
    }
    public bool IsAlive()
    {
      return _isAlive;
    }
  }
}