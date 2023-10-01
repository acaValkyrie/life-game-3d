using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayText
{
    private TextMeshProUGUI _textPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        //_textPrefab.text = "Hello World";
    }
    
    public void SetText(string text)
    {
        _textPrefab.text = text;
    }

    public DisplayText(TextMeshProUGUI prefab)
    {
        _textPrefab = prefab;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
