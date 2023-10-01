﻿using UnityEditor;
using UnityEngine;

namespace DefaultNamespace
{
  public class Cell : MonoBehaviour{
    private bool _isAlive;
    private Renderer _renderer;
    public Cell(Vector3 position, GameObject prefab)
    {
      GameObject obj = Instantiate(prefab, position, Quaternion.identity);
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
      _renderer.material.color = new Color(0.1f, 0.2f, 0.1f, 0.8f);
    }
    public bool IsAlive()
    {
      return _isAlive;
    }
  }
}