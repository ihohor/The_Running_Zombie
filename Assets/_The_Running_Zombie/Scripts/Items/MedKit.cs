using UnityEngine;

public class MedKit : Item
{
    [SerializeField] private int _healAmount = 50;

    // DŸwiêk pojawienia siê apteczki
    [SerializeField] private AudioClip medKitSound;

    private AudioSource _audioSource;

    private void OnEnable()
    {
        _audioSource = GetComponent<AudioSource>();

        // Dodanie AudioSource, jeœli brak
        if (_audioSource == null)
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Odtwarzanie dŸwiêku w pêtli, dopóki apteczka istnieje
        PlayMedKitSound();
    }

    protected override void Collide(ZombieStateAndHealth zombieHealth)
    {
        base.Collide(zombieHealth);
        zombieHealth.Heal(_healAmount);

        // Zatrzymaj dŸwiêk po zderzeniu
        StopMedKitSound();

        // Zniszcz apteczkê po u¿yciu
        Destroy(gameObject);
    }

    // Funkcja odtwarzaj¹ca dŸwiêk apteczki
    private void PlayMedKitSound()
    {
        if (medKitSound != null && _audioSource != null)
        {
            _audioSource.clip = medKitSound;
            _audioSource.loop = true;  // DŸwiêk bêdzie odtwarzany w pêtli
            _audioSource.Play();
        }
    }

    // Funkcja zatrzymuj¹ca dŸwiêk apteczki
    private void StopMedKitSound()
    {
        if (_audioSource != null && _audioSource.isPlaying)
        {
            _audioSource.Stop();
        }
    }
}
