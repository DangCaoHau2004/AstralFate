using UnityEngine;

public class DoorController : MonoBehaviour

{
    Animator animator;
    DoorTrigger doorTrigger;


    void Awake()
    {
        doorTrigger = transform.parent.Find("DoorTrigger").GetComponent<DoorTrigger>();
        animator = GetComponent<Animator>();
        OpenDoor();
    }


    void Update()
    {
        if (doorTrigger.GetIsSpawned() && GameObject.FindGameObjectWithTag("Enemy") == null)
        {
            OpenDoor();
        }

    }
    public void OpenDoor()
    {
        animator.SetBool("Open", true);
    }
    public void CloseDoor()
    {
        animator.SetBool("Open", false);
    }
}
