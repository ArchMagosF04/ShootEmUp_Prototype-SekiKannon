using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletColorManager : MonoBehaviour, IDataPersistance
{
    public static BulletColorManager Instance;

    [field: SerializeField] public Color SquareColor { get; private set; }
    [field: SerializeField] public Color CircleColor { get; private set; }
    [field: SerializeField] public Color RonboidColor { get; private set; }
    [field: SerializeField] public Color TriangleColor {  get; private set; }

    [field: SerializeField] public Color SquareColorDefault { get; private set; }
    [field: SerializeField] public Color CircleColorDefault { get; private set; }
    [field: SerializeField] public Color RonboidColorDefault { get; private set; }
    [field: SerializeField] public Color TriangleColorDefault { get; private set; }

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

    public void SetSquareColor(Color color)
    {
        SquareColor = color;
    }

    public void SetCircleColor(Color color)
    {
        CircleColor = color;
    }

    public void SetRonboidColor(Color color)
    {
        RonboidColor = color;
    }

    public void SetTriangleColor(Color color)
    {
        TriangleColor = color;
    }

    public void LoadData(GameData data)
    {
        this.SquareColor = data.squareColor;
        this.CircleColor = data.circleColor;
        this.RonboidColor = data.ronboidColor;
        this.TriangleColor = data.triangleColor;
    }

    public void SaveData(ref GameData data)
    {
        data.squareColor = this.SquareColor;
        data.circleColor = this.CircleColor;
        data.ronboidColor = this.RonboidColor;
        data.triangleColor = this.TriangleColor;
    }
}
