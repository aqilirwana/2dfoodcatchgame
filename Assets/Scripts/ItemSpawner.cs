using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Transform[] foodList;

    private BoxCollider2D boxCollider;


    private float xValue1, xValue2;

    private bool isSpawning = true;

    private void Start()
    {
        
    }

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();

        float screenWidthInWorldUnits = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x * 2;

        xValue1 = SetBoxColliderSize(boxCollider, screenWidthInWorldUnits).Item1;
        xValue2 = SetBoxColliderSize(boxCollider, screenWidthInWorldUnits).Item2;

    }

    public IEnumerator SpawnFood(float time)
    {

        while (isSpawning)
        {
            yield return new WaitForSecondsRealtime(time);

            Vector3 spawnPos = transform.position;
            spawnPos.x = Random.Range(xValue1, xValue2);

            Instantiate(foodList[Random.Range(0, foodList.Length)], spawnPos, Quaternion.identity, this.gameObject.transform);

            time = Random.Range(1f, 2f); // Update the time with a new random value
        }
    }

    private (float, float) SetBoxColliderSize(BoxCollider2D col, float widthUnit)
    {
        Vector2 boxColliderSize = col.size;

        boxColliderSize.x = widthUnit - 0.6f;

        col.size = boxColliderSize;

        float x1 = transform.position.x - boxColliderSize.x / 2f;
        float x2 = transform.position.x + boxColliderSize.x / 2f;

        return (x1, x2);
    }
}
