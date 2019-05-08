using UnityEngine;

public class Pointer : MonoBehaviour
{
    private void Awake()
    {
        // Make default cursor invisible
        Cursor.visible = false;
    }

    private void Update()
    {
        // Move the pointer to the mouse position
        transform.position = (Vector2)Reference.Instance.mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }
}
