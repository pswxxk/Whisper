using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushObject : MonoBehaviour
{
    public Collider pushObjects;
    public Animator animator;

    public bool isPushing;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Push(); 
        }
    }

    void Push()
    {
        isPushing = true;
        animator.SetBool("isPushing", true);
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPushing = false;
            animator.SetBool("isPushing", false); 
        }
    }
}
