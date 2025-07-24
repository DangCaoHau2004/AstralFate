using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] AudioSource shootingSource;

    [Header("Damage")]
    [SerializeField] AudioSource damageSource;

    [Header("Spawn")]
    [SerializeField] AudioSource spawnSource;

    public void PlayShootingClip()
    {
        PlayClip(shootingSource);
    }

    public void PlayDamageClip()
    {
        PlayClip(damageSource);
    }

    public void PlaySpawnClip()
    {
        PlayClip(spawnSource);
    }

    void PlayClip(AudioSource source)
    {
        if (source != null && source.clip != null)
        {
            source.Play();
        }
    }
}
