using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public InputManager inputManager;

    // Start is called before the first frame update
    void Start()
    {
        if (inputManager == null) inputManager = new InputManager();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
