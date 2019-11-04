using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScriptButton : MonoBehaviour
{
    public Button Coward;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Button Coward1
    {
        get => Coward;
        set => Coward = value;
    }
}
