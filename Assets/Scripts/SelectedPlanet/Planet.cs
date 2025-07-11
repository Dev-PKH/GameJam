using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Planets
{
    Monalpterra,
    Dibetund,
    Trigmaer,
    None
}

public class Planet : MonoBehaviour
{
    //public // tmp

    public Planets planetStatus;
    public int distance;

    private void OnEnable()
    {
        
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        InGameManager.Instance.SetPlanet(this);
    }
}
