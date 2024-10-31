using UnityEngine;

public class Tomangochi : MonoBehaviour
{
    [SerializeField] private GameObject pet;
    private Animator animator;
    [SerializeField] private float speed = 2f;

    private Transform targetFood;
    private Food foodManager;

    [SerializeField] GameObject poopPrefab;
    [SerializeField] private float poopOffsetY = -3f;

    [Range(0, 60)] public float hunger = 60f;
    [Range(0, 60)] public float happiness = 60f;

    public int poopCount = 0;
    private int poopThreshold = 3;


    public float hungerDecayRate = 1f;
    public float happinessDecayRate = 0.5f;

    private bool isSatisfied = true;
    private float maxStatValue = 30f;


    void Start()
    {
        animator = pet.GetComponentInChildren<Animator>();
        foodManager = FindObjectOfType<Food>();

        hunger = maxStatValue;
        happiness = maxStatValue;
    }

    void Update()
    {
        hunger -= hungerDecayRate * Time.deltaTime;
        happiness -= happinessDecayRate * Time.deltaTime;

        hunger = Mathf.Clamp(hunger, 0, maxStatValue);
        happiness = Mathf.Clamp(happiness, 0, maxStatValue);

        if (hunger <= 0 || happiness <= 0)
        {
            if (isSatisfied)
            {
                NoSatisfeid();
                isSatisfied = false;
            }
        }
        else
        {
            if (!isSatisfied)
            {
                Normalize();
                isSatisfied = true;
            }
        }

        if (targetFood != null)
        {
            MoveToFood();
        }
        if (poopCount >= poopThreshold)
        {
            Poop();
        }
    }

    public void SetTargetFood(Transform foodTransform)
    {
        targetFood = foodTransform;
    }

    private void MoveToFood()
    {
        Vector2 targetPosition2D = new Vector2(targetFood.position.x, targetFood.position.y);
        Vector2 currentPosition2D = new Vector2(pet.transform.position.x, pet.transform.position.y);

        Vector2 direction = (targetPosition2D - currentPosition2D).normalized;
        pet.transform.position += (Vector3)direction * speed * Time.deltaTime;

        if (Vector2.Distance(currentPosition2D, targetPosition2D) < 0.1f)
        {
            EatFood();
        }
    }

    public void EatFood()
    {
        Feed();
        Destroy(targetFood.gameObject);
        targetFood = null;

        foodManager.ClearCurrentFood();
    }

    private void NoSatisfeid()
    {
        animator.SetBool("NoHappy", true);
    }

    private void Normalize()
    {
        animator.SetBool("NoHappy", false);
    }

    public void Feed()
    {
        hunger = Mathf.Clamp(hunger + 20, 0, maxStatValue);
        Debug.Log("Tomangochi has been fed!");
        poopCount++;
    }

    public void Play()
    {
        happiness = Mathf.Clamp(happiness + 15, 0, maxStatValue);
        Debug.Log("Tomangochi is happy!");
    }

    private void Poop()
    {
        Debug.Log("Tomangochi has pooped!");
        poopCount = 0;

        Vector2 poopPosition2D = new Vector2(pet.transform.position.x, pet.transform.position.y);
        Vector3 poopPosition = new Vector3(poopPosition2D.x, poopPosition2D.y, poopPrefab.transform.position.z);

        Instantiate(poopPrefab, poopPosition, Quaternion.identity);
    }
}