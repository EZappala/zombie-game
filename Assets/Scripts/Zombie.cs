using UnityEngine;

[RequireComponent(typeof(AiController), typeof(AudioSource))]
public sealed class Zombie : MonoBehaviour
{
    [SerializeField]
    AudioClip growl;

    [SerializeField]
    AudioClip idle;

    AudioSource source;
    Animator animator;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        source.loop = true;
        source.clip = idle;

        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        source.clip = animator.GetBool("Walking") ? growl : idle;
    }
}
