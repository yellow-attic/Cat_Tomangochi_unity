using UnityEngine;

public class EatFood : MonoBehaviour
{
    private Tomangochi tomangochi;

    void Start()
    {
        tomangochi = FindObjectOfType<Tomangochi>();
    }

    public void Eat()
    {
        tomangochi.EatFood();
    }
}
