using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 offset;
    public GameObject Player_user;

    // 1. Variabila nouă cerută de tutorial pentru viteza de rotație
    public float camRotationSpeed = 30f;

    void Start()
    {
        // ⚠️ CORECȚIE IMPORTANTĂ:
        // Offset-ul trebuie să fie diferența dintre Cameră și Jucător.
        // În codul tău vechi era doar "transform.position", ceea ce e greșit dacă jucătorul nu e la 0.
        if (Player_user != null)
        {
            offset = transform.position - Player_user.transform.position;
        }
    }

    void LateUpdate()
    {
        if (Player_user != null)
        {
            // 2. AICI ESTE PARTEA NOUĂ DIN IMAGINE (QUATERNIONS):

            // A. Luăm mișcarea mouse-ului pe orizontală
            float horizontalInput = Input.GetAxis("Mouse X");

            // B. Calculăm unghiul (mișcarea * viteză)
            // Am pus Time.deltaTime ca să se miște lin, altfel o ia razna camera.
            float angle = horizontalInput * camRotationSpeed * Time.deltaTime;

            // C. Creăm rotația folosind Quaternion.AngleAxis
            // Folosim Vector3.up (Axa Y) pentru a ne roti stânga-dreapta (panoramic).
            // (Deși textul zice "Z-axis", în Unity axa verticală e Y. Dacă pui Z, camera se dă peste cap).
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.up);

            // D. Aplicăm rotația asupra offset-ului (vectorul de distanță)
            offset = rotation * offset;

            // E. Actualizăm poziția camerei
            transform.position = Player_user.transform.position + offset;

            // F. Comanda finală cerută de text: LookAt
            transform.LookAt(Player_user.transform);
        }
    }
}