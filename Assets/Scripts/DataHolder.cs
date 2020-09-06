using System.Collections.Generic;
using UnityEngine;

public class DataHolder : MonoBehaviour
{
    [Multiline (5)]
    public string description;
    public List<ScriptableObject> data;
}
