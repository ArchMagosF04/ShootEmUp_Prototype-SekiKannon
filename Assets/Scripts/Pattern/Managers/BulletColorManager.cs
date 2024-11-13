using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletColorManager : MonoBehaviour
{
    public static BulletColorManager Instance;

    public Color SquareColor { get; private set; }
    public Color CircleColor { get; private set; }
    public Color RonboidColor { get; private set; }
    public Color TriangleColor {  get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    public void SetSquareColor(Color squareColor)
    {
        PlayerPrefs.GetString("SquareColor", ColorToHex(squareColor));
    }

    public void SetCircleColor(Color circleColor)
    {
        PlayerPrefs.GetString("CircleColor", ColorToHex(circleColor));
    }

    public void SetRonboidColor(Color ronboidColor)
    {
        PlayerPrefs.GetString("RonboidColor", ColorToHex(ronboidColor));
    }

    public void SetTriangleColor(Color triangleColor)
    {
        PlayerPrefs.GetString("TriangleColor", ColorToHex(triangleColor));
    }

    void GetSavedColor()
    {
        Color GetedColor = HexToColor(PlayerPrefs.GetString("SavedColor"));
    }
    // Note that Color32 and Color implictly convert to each other. You may pass a Color object to this method without first casting it.
    string ColorToHex(Color32 color)
    {
        string hex = color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2");
        return hex;
    }

    Color HexToColor(string hex)
    {
        byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        return new Color32(r, g, b, 255);
    }
}
