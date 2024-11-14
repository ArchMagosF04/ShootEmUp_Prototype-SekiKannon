using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPickerController : MonoBehaviour
{
    [SerializeField] private FlexibleColorPicker squareBullet;
    [SerializeField] private FlexibleColorPicker circleBullet;
    [SerializeField] private FlexibleColorPicker ronboidBullet;
    [SerializeField] private FlexibleColorPicker triangleBullet;

    private void OnEnable()
    {
        LoadSavedColors();
    }

    private void LoadSavedColors()
    {
        squareBullet.color = BulletColorManager.Instance.SquareColor;
        circleBullet.color = BulletColorManager.Instance.CircleColor;
        ronboidBullet.color = BulletColorManager.Instance.RonboidColor;
        triangleBullet.color = BulletColorManager.Instance.TriangleColor;
    }

    public void AcceptSquareChanges()
    {
        BulletColorManager.Instance.SetSquareColor(squareBullet.color); 
    }

    public void AcceptCircleChanges()
    {
        BulletColorManager.Instance.SetCircleColor(circleBullet.color);
    }

    public void AcceptRonboidChanges()
    {
        BulletColorManager.Instance.SetRonboidColor(ronboidBullet.color);
    }

    public void AcceptTriangleChanges()
    {
        BulletColorManager.Instance.SetTriangleColor(triangleBullet.color);
    }

    public void ResetSquareColor()
    {
        squareBullet.color = BulletColorManager.Instance.HexToColor(BulletColorManager.Instance.DefaultSquare);
    }

    public void ResetCircleColor()
    {
        circleBullet.color = BulletColorManager.Instance.HexToColor(BulletColorManager.Instance.DefaultCircle);
    }

    public void ResetRonboidColor()
    {
        ronboidBullet.color = BulletColorManager.Instance.HexToColor(BulletColorManager.Instance.DefaultRonboid);
    }

    public void ResetTriangleColor()
    {
        triangleBullet.color = BulletColorManager.Instance.HexToColor(BulletColorManager.Instance.DefaultTriangle);
    }
}
