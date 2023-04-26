using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour
{
    RaycastHit2D actioner;
    Animator anim;

    public ManagerScript manager;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        //if (Input.GetKeyUp(KeyCode.E) && actioner.collider != null)
        //{
        //    anim.Play("Active");
        //    manager.KeyManager(transform.name);
        //}

        if (actioner.collider != null)
        {
            anim.Play("Active");
            manager.KeyManager(transform.name);
        }

        actioner = Physics2D.Raycast(transform.position, Vector2.down, 0.8f, 1 << 3);
        Debug.DrawRay(transform.position, Vector2.down * 0.8f, Color.green);
    }
}
