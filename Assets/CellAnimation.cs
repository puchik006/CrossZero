using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CellAnimation : MonoBehaviour
{
    private Button _btnCell;
    private Image _image;
    private const string LEFT_CROSS = "_LeftCrossLineLength";
    private const string RIGHT_CROSS = "_RightCrossLineLength";
    private float time = 0f;

    private Material _originalMaterial; // Store the original material of the Image

    private void OnValidate()
    {
        _btnCell = GetComponent<Button>();
        _image = GetComponent<Image>();
    }

    private void Awake()
    {
        _btnCell.onClick.AddListener(ButtonAction);

        // Store the original material of the Image
        _originalMaterial = _image.material;
    }

    private void ButtonAction()
    {
        StartCoroutine(ShowCrossAnimation());
    }

    private IEnumerator ShowCrossAnimation()
    {
        // Create a new material instance for this Image
        Material materialInstance = new Material(_originalMaterial);
        _image.material = materialInstance;

        // Left cross animation
        while (time < 4f)
        {
            time += Time.deltaTime;
            SetMaterialFloat(LEFT_CROSS, time);
            yield return null;
        }

        // Reset time for the right cross animation
        time = 0f;

        // Right cross animation
        while (time < 4f)
        {
            time += Time.deltaTime;
            SetMaterialFloat(RIGHT_CROSS, time);
            yield return null;
        }

        // Reset the material to the original after the animation
        _image.material = _originalMaterial;
    }

    private void SetMaterialFloat(string propertyName, float value)
    {
        _image.material.SetFloat(propertyName, value);
    }
}
