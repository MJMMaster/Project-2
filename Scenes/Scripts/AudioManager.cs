using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Music")]
    public AudioSource musicSource;
    public AudioClip backgroundMusic;

    [Header("SFX")]
    public AudioClip shootClip;
    public AudioClip damageClip;
    public AudioClip deathClip;
    public AudioClip pickupClip;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        PlayMusic(backgroundMusic);
    }

    // ----------------------
    // MUSIC
    // ----------------------
    public void PlayMusic(AudioClip clip)
    {
        if (musicSource == null || clip == null) return;
        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    // ----------------------
    // SOUND EFFECTS
    // ----------------------
    public void PlaySFX(AudioClip clip, Vector3 position, float volume = 1f)
    {
        if (clip == null) return;
        AudioSource.PlayClipAtPoint(clip, position, volume);
    }

    public void PlayShoot(Vector3 position) => PlaySFX(shootClip, position);
    public void PlayDamage(Vector3 position) => PlaySFX(damageClip, position);
    public void PlayDeath(Vector3 position) => PlaySFX(deathClip, position);
    public void PlayPickup(Vector3 position) => PlaySFX(pickupClip, position);
}