using UnityEngine;
using Cinemachine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CameraZoomOrthoOnLastWaypoint : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public CinemachineDollyCart dollyCart;
    public float targetOrthoSize = 3f; // Docelowy Ortho Size po powiêkszeniu
    public float zoomSpeed = 2f;       // Szybkoœæ zmiany Ortho Size
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

        // Jeœli powinniœmy zmieniæ Ortho Size kamery
        if (shouldZoomIn)
        {
            float currentOrthoSize = virtualCamera.m_Lens.OrthographicSize;
            virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(currentOrthoSize, targetOrthoSize, Time.deltaTime * zoomSpeed);

            // Zatrzymaj zmianê, gdy osi¹gnie docelowy Ortho Size
            if (Mathf.Abs(currentOrthoSize - targetOrthoSize) < 0.1f && !hasTransitionStarted)
            {
                virtualCamera.m_Lens.OrthographicSize = targetOrthoSize;
                shouldZoomIn = false;

                // Rozpocznij przejœcie do nastêpnej sceny
                hasTransitionStarted = true;
                StartCoroutine(TransitionToNextScene());
            }
        }
    }

    IEnumerator TransitionToNextScene()
    {
        // Poczekaj 6 sekund
        yield return new WaitForSeconds(6f);

        // Przejœcie do nastêpnej sceny
        SceneManager.LoadScene(nextSceneName);
    }
}
