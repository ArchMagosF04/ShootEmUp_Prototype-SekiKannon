using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public Color squareColor;
    public Color circleColor;
    public Color ronboidColor;
    public Color triangleColor;

    public GameData(Color square, Color circle, Color ronboid, Color triangle)
    {
        squareColor = square;
        circleColor = circle;
        ronboidColor = ronboid;
        triangleColor = triangle;
    }
}
