using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField] private GameObject foodPrefab;

    private Tomangochi tomangochi;   
    private GameObject currentFood;

    [SerializeField] private float speed = 2f;
    [SerializeField] private AudioClip soundeffect;


    void Start()
    {
        tomangochi = FindObjectOfType<Tomangochi>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && currentFood == null)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Physics2D.OverlapPoint(mousePosition) == null)
            {
                CreateFoodAtMousePosition();
            }
        }
    }

    private void CreateFoodAtMousePosition()
    {
        Vector2 mousePosition = Input.mousePosition;
        Vector2 worldPosition2D = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector3 worldPosition = new Vector3(worldPosition2D.x, worldPosition2D.y, foodPrefab.transform.position.z);

        currentFood = Instantiate(foodPrefab, worldPosition, Quaternion.identity);
        tomangochi.SetTargetFood(currentFood.transform);

        playSound(soundeffect);
    }

    public void ClearCurrentFood()
    {
        currentFood = null;
    }

    private void playSound(AudioClip clip)
    {
        AudioSource sound = gameObject.AddComponent<AudioSource>();

        sound.clip = clip;
        sound.Play();

        Destroy(sound, clip.length);
    }
}
