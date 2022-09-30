using System;
using UnityEngine;

public class IntValueChangeEventArgs : EventArgs
{
    private int i_min;
    private int i_max;
    private int i_value;
    public IntValueChangeEventArgs(int min, int max, int value)
    {

    }

    public int Min 
    {
        get { return this.i_min; }
    }

    public int Max
    {
        get { return this.i_max; }
    }

    public int Value 
    {
        get { return this.i_value; }
    }
}