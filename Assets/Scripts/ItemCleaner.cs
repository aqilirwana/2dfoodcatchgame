using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCleaner : MonoBehaviour
{
    private BoxCollider2D boxCollider;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();

        float screenWidthInWorldUnits = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x * 2;

        float bottomOfScreen = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).y;

        SetColliderSizeBottomPos(boxCollider, screenWidthInWorldUnits, bottomOfScreen);
    }

    private void SetColliderSizeBottomPos(BoxCollider2D col, float widthUnit, float bottomScreenUnit)
    {
        Vector2 boxColliderSize = col.size;

        boxColliderSize.x = widthUnit;

        col.size = boxColliderSize;

        // Get the current position of the GameObject
        Vector3 newPosition = transform.position;

        // Set the Y position to the bottom of the screen
        newPosition.y = bottomScreenUnit;

        // Apply the new position
        transform.position = newPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
    }
}
