using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ColorPickerController : MonoBehaviour
{
    [SerializeField] private FlexibleColorPicker squareBullet;
    [SerializeField] private FlexibleColorPicker circleBullet;
    [SerializeField] private FlexibleColorPicker ronboidBullet;
    [SerializeField] private FlexibleColorPicker triangleBullet;

    [SerializeField] private Image squareImage;
    [SerializeField] private Image circleImage;
    [SerializeField] private Image ronboidImage;
    [SerializeField] private Image triangleImage;

    private void OnEnable()
    {
        Debug.Log("Color picker enabled.");
        LoadSavedColors();
    }

    private void LoadSavedColors()
    {
        squareBullet.color = BulletColorManager.Instance.SquareColor;
        circleBullet.color = BulletColorManager.Instance.CircleColor;
        ronboidBullet.color = BulletColorManager.Instance.RonboidColor;
        triangleBullet.color = BulletColorManager.Instance.TriangleColor;

        squareImage.color = squareBullet.color;
        circleImage.color = circleBullet.color;
        ronboidImage.color = ronboidBullet.color;
        triangleImage.color = triangleBullet.color;
    }

    public void AcceptSquareChanges()
    {
        BulletColorManager.Instance.SetSquareColor(squareBullet.color);
        squareImage.color = squareBullet.color;
    }

    public void AcceptCircleChanges()
    {
        BulletColorManager.Instance.SetCircleColor(circleBullet.color);
        circleImage.color = circleBullet.color;
    }

    public void AcceptRonboidChanges()
    {
        BulletColorManager.Instance.SetRonboidColor(ronboidBullet.color);
        ronboidImage.color = ronboidBullet.color;
    }

    public void AcceptTriangleChanges()
    {
        BulletColorManager.Instance.SetTriangleColor(triangleBullet.color);
        triangleImage.color = triangleBullet.color;
    }

    public void ResetSquareColor()
    {
        squareBullet.color = BulletColorManager.Instance.SquareColorDefault;
        AcceptSquareChanges();
    }

    public void ResetCircleColor()
    {
        circleBullet.color = BulletColorManager.Instance.CircleColorDefault;
        AcceptCircleChanges();
    }

    public void ResetRonboidColor()
    {
        ronboidBullet.color = BulletColorManager.Instance.RonboidColorDefault;
        AcceptRonboidChanges();
    }

    public void ResetTriangleColor()
    {
        triangleBullet.color = BulletColorManager.Instance.TriangleColorDefault;
        AcceptTriangleChanges();
    }
}
