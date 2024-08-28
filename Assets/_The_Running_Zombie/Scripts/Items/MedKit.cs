using UnityEngine;

public class MedKit : Item
{
    [SerializeField] private int _healAmount = 50;

    // D�wi�k zbierania apteczki
    [SerializeField] private AudioClip medKitPickupSound;

    private AudioSource _audioSource;

    private void OnEnable()
    {
        _audioSource = GetComponent<AudioSource>();

        // Dodanie AudioSource, je�li brak
        if (_audioSource == null)
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    protected override void Collide(ZombieStateAndHealth zombieHealth)
    {
        base.Collide(zombieHealth);
        zombieHealth.Heal(_healAmount);

        // Odtwarzanie d�wi�ku po zebraniu apteczki
        PlayMedKitPickupSound();

        // Zniszczenie apteczki po zebraniu, po kr�tkim czasie, aby d�wi�k m�g� si� odtworzy�
        Destroy(gameObject, medKitPickupSound.length);
    }

    // Funkcja odtwarzaj�ca d�wi�k zebrania apteczki
    private void PlayMedKitPickupSound()
    {
        if (medKitPickupSound != null && _audioSource != null)
        {
            _audioSource.clip = medKitPickupSound;
            _audioSource.loop = false;  // D�wi�k nie jest odtwarzany w p�tli
            _audioSource.Play();
        }
    }
}
