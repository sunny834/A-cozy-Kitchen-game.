using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu()]
public class CuttingRecipeSo : ScriptableObject
{
    public KitchenObjectSo input;
    public KitchenObjectSo output;
    public int cuttingProgressMax;
}
