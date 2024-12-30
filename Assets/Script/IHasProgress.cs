using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public interface IHasProgress 
{
    public event EventHandler<OnProgressChangedArgs> OnProgressChanged;
    public class OnProgressChangedArgs : EventArgs
    {
        public float ProgressNormalized;
    }
}
