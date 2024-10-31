using UnityEngine;

public class Clean : MonoBehaviour
{
    private float targetY = -3f;
    private float fallSpeed = 1f;

    private void OnMouseUp()
    {
        Destroy(gameObject);
    }

    void Update()
    {
        if (transform.position.y > targetY)
        {
            Vector3 newPosition = transform.position;
            newPosition.y = Mathf.MoveTowards(transform.position.y, targetY, fallSpeed * Time.deltaTime);
            transform.position = newPosition;
        }
    }
}
