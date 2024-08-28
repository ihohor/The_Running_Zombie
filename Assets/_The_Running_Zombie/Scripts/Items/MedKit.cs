using UnityEngine;

public class MedKit : Item
{
    [SerializeField] private int _healAmount = 50;

    // DŸwiêk zbierania apteczki
    [SerializeField] private AudioClip medKitPickupSound;

    private AudioSource _audioSource;

    private void OnEnable()
    {
        _audioSource = GetComponent<AudioSource>();

        // Dodanie AudioSource, jeœli brak
        if (_audioSource == null)
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    protected override void Collide(ZombieStateAndHealth zombieHealth)
    {
        base.Collide(zombieHealth);
        zombieHealth.Heal(_healAmount);

        // Odtwarzanie dŸwiêku po zebraniu apteczki
        PlayMedKitPickupSound();

        // Zniszczenie apteczki po zebraniu, po krótkim czasie, aby dŸwiêk móg³ siê odtworzyæ
        Destroy(gameObject, medKitPickupSound.length);
    }

    // Funkcja odtwarzaj¹ca dŸwiêk zebrania apteczki
    private void PlayMedKitPickupSound()
    {
        if (medKitPickupSound != null && _audioSource != null)
        {
            _audioSource.clip = medKitPickupSound;
            _audioSource.loop = false;  // DŸwiêk nie jest odtwarzany w pêtli
            _audioSource.Play();
        }
    }
}
