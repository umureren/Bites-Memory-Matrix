using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClick : MonoBehaviour
{
    public  int[] array = new int[25];
    private System.Random _random = new System.Random();
    void Start()
    {
        for (int i = 0; i < 25; i++)
        {
            array[i] = i;
        }

        //int[] array = { 1, 2, 3, 4, 5, 6, 7, 8 };
        Shuffle(array);
        foreach (int value in array)
        {
            Debug.Log(value);
        }
    }
    void Shuffle(int[] array)
    {
        int p = array.Length;
        for (int n = p - 1; n > 0; n--)
        {
            int r = _random.Next(1, n);
            int t = array[r];
            array[r] = array[n];
            array[n] = t;
        }
    }

}

