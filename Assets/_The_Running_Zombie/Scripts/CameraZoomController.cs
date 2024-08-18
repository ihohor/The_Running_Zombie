using UnityEngine;
using Cinemachine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CameraZoomOrthoOnLastWaypoint : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public CinemachineDollyCart dollyCart;
    public float targetOrthoSize = 3f; // Docelowy Ortho Size po powi�kszeniu
    public float zoomSpeed = 2f;       // Szybko�� zmiany Ortho Size
    public string nextSceneName = "GameScene";

    private bool shouldZoomIn = false;
    private bool hasTransitionStarted = false;

    void Update()
    {
        // Sprawdzenie, czy DollyCart jest blisko ostatniego waypointa
        if (dollyCart.m_Position >= dollyCart.m_Path.PathLength && !shouldZoomIn)
        {
            shouldZoomIn = true;
        }

        // Je�li powinni�my zmieni� Ortho Size kamery
        if (shouldZoomIn)
        {
            float currentOrthoSize = virtualCamera.m_Lens.OrthographicSize;
            virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(currentOrthoSize, targetOrthoSize, Time.deltaTime * zoomSpeed);

            // Zatrzymaj zmian�, gdy osi�gnie docelowy Ortho Size
            if (Mathf.Abs(currentOrthoSize - targetOrthoSize) < 0.1f && !hasTransitionStarted)
            {
                virtualCamera.m_Lens.OrthographicSize = targetOrthoSize;
                shouldZoomIn = false;

                // Rozpocznij przej�cie do nast�pnej sceny
                hasTransitionStarted = true;
                StartCoroutine(TransitionToNextScene());
            }
        }
    }

    IEnumerator TransitionToNextScene()
    {
        // Poczekaj 6 sekund
        yield return new WaitForSeconds(6f);

        // Przej�cie do nast�pnej sceny
        SceneManager.LoadScene(nextSceneName);
    }
}
