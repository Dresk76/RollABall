using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Application Settings", menuName = "Scriptable Objects/Config/Application Settings")]
public class ApplicationSettings : ScriptableObject
{
    public int targetFrameRate = 60;
}
