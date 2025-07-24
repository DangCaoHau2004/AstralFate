using System.Collections;
using UnityEngine;

public class DoorBossController : MonoBehaviour
{

    Animator animator;
    DoorTrigger doorTrigger;

    SceneLoader sceneLoader;

    void Awake()
    {
        doorTrigger = transform.parent.Find("DoorTrigger").GetComponent<DoorTrigger>();
        animator = GetComponent<Animator>();
        CloseDoor();
        sceneLoader = GameObject.Find("SceneLoader").GetComponent<SceneLoader>();
    }


    void Update()
    {
        if (doorTrigger.GetIsSpawned() && GameObject.FindGameObjectWithTag("Boss") == null)
        {
            StartCoroutine(ChangeMapDelay(2f));
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

    IEnumerator ChangeMapDelay(float delay)
    {
        OpenDoor();
        yield return new WaitForSeconds(delay);
        sceneLoader.LoadNextMap();
    }

}
