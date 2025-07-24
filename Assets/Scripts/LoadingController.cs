using UnityEngine;
using System.Collections;

public class LoadingController : MonoBehaviour
{
    [SerializeField] float delayBeforeLoad = 2f;
    Player player;

    AudioPlayer audioPlayer;

    void Awake()
    {
        audioPlayer = FindFirstObjectByType<AudioPlayer>();
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        player.SetDelayPlayerMove(delayBeforeLoad);
        StartCoroutine(DisableAfterDelay());
    }

    IEnumerator DisableAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeLoad);
        audioPlayer.PlaySpawnClip();
        gameObject.SetActive(false);
    }
}
