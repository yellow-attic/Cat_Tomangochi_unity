using UnityEngine;

public class Touch : MonoBehaviour
{
    private Animator animator;
    private Tomangochi tomangochi;

    [SerializeField] private AudioClip soundeffect;

    void Start()
    {
        animator = GetComponent<Animator>();
        tomangochi = FindObjectOfType<Tomangochi>();
    }

    private void OnMouseDown()
    {
        animator.SetTrigger("Happy");
        tomangochi.Play();


        playSound(soundeffect);
    }

    private void playSound(AudioClip clip)
    {
        AudioSource sound = gameObject.AddComponent<AudioSource>();

        sound.clip = clip;
        sound.Play();

        Destroy(sound, clip.length);
    }
}