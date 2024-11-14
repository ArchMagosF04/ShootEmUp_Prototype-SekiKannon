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

    public string DefaultSquare { get; private set; } = "0050FF";
    public string DefaultCircle { get; private set; } = "00FF00";
    public string DefaultRonboid { get; private set; } = "FF7800";
    public string DefaultTriangle { get; private set; } = "FF0000";

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

    public void GetSquareColor()
    {
        SquareColor = HexToColor(PlayerPrefs.GetString("SquareColor", "0050FF"));
    }

    public void GetCircleColor()
    {
        CircleColor = HexToColor(PlayerPrefs.GetString("CircleColor", "00FF00"));
    }

    public void GetRonboidColor()
    {
        RonboidColor = HexToColor(PlayerPrefs.GetString("RonboidColor", "FF7800"));
    }

    public void GetTriangleColor()
    {
        TriangleColor = HexToColor(PlayerPrefs.GetString("TriangleColor", "FF0000"));
    }

    public void SetSquareColor(Color color)
    {
        PlayerPrefs.SetString("SquareColor", ColorToHex(color));
        GetSquareColor();
    }

    public void SetCircleColor(Color color)
    {
        PlayerPrefs.SetString("CircleColor", ColorToHex(color));
        GetCircleColor();
    }

    public void SetRonboidColor(Color color)
    {
        PlayerPrefs.SetString("RonboidColor", ColorToHex(color));
        GetRonboidColor();
    }

    public void SetTriangleColor(Color color)
    {
        PlayerPrefs.SetString("TriangleColor", ColorToHex(color));
        GetTriangleColor();
    }

    public string ColorToHex(Color32 color)
    {
        string hex = color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2");
        return hex;
    }

    public Color HexToColor(string hex)
    {
        byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        return new Color32(r, g, b, 255);
    }
}
