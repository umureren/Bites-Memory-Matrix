using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldBlue : MonoBehaviour
{
    [SerializeField] private bool isBlue;

    private void Start()
    {
        isBlue = false;
    }
    // Start is called before the first frame update
    
    public void SetBlue()
    {
        isBlue = true;
        GetComponent<Renderer>().material.color = Color.blue;
    }

    public bool GetBlue()
    {
        return isBlue;
    }
}
