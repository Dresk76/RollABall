using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UI Button Style", menuName = "Scriptable Objects/UIButtonStyle")]
public class UIButtonStyle : ScriptableObject
{
    public Color NormalColor;
    public Color HoverColor;
    public Color PressedColor;
}
