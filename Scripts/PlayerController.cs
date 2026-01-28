using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    Rigidbody playerRigidbody;
    public float m_Thrust = 20f;

    public TMP_Text textAfisat;
    private string litereColectate;

    public Button BtnMeniu;
    public Button BtnReload;

    // --- 1. VARIABILE NOI PENTRU AUDIO ---
    [Header("Setari Audio")] // Asta face un titlu frumos in Inspector
    public AudioSource sursaAudio;   // Componenta care reda sunetul
    public AudioClip sunetSaritura;  // Fisierul audio pt saritura
    public AudioClip sunetColectare; // Fisierul audio pt litera
    // -------------------------------------

    void Start()
    {
        textAfisat.text = "Ai colectat: ";
        litereColectate = "";

        BtnMeniu.gameObject.SetActive(false);
        BtnReload.gameObject.SetActive(false);

        playerRigidbody = GetComponent<Rigidbody>();
    }

    // Folosim Update pentru Input (e mai precis pentru sunete decat FixedUpdate)
    void Update()
    {
        // --- 2. LOGICA PENTRU SUNET SARITURA ---
        // Folosim GetButtonDown ca sa se auda o singura data cand apesi, nu continuu
        if (Input.GetButtonDown("Jump"))
        {
            RedaSunet(sunetSaritura);
        }
    }

    private void FixedUpdate()
    {
        float movementHorizontal = Input.GetAxis("Horizontal");
        float movementVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(movementHorizontal, 0.0f, movementVertical);
        playerRigidbody.AddForce(movement * 5*speed * Time.deltaTime);

        // Aici ramane logica fizica de saritura (GetButton e ok pt forta continua)
        if (Input.GetButton("Jump"))
        {
            playerRigidbody.AddForce(transform.up * m_Thrust);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "LiteraColectabila")
        {
            // --- 3. AICI REDAM SUNETUL DE COLECTARE ---
            RedaSunet(sunetColectare);
            // ------------------------------------------

            other.gameObject.SetActive(false);

            litereColectate = litereColectate + other.gameObject.GetComponent<TextMeshPro>().text;
            textAfisat.text = "Ai colectat: " + litereColectate;
            Debug.Log("Am colectat litera: " + litereColectate);
        }

        Debug.Log("nr litere= " + litereColectate.Length);

        if (litereColectate.Equals("ETTI"))
        {
            BtnMeniu.gameObject.SetActive(true);
            BtnMeniu.GetComponentInChildren<Text>().text = "Go to menu!";
        }
        else
        {
            if (litereColectate.Length >= 4)
            {
                BtnReload.gameObject.SetActive(true);
                BtnReload.GetComponentInChildren<Text>().text = "Reload Scene!";
            }
        }
    }

    // --- 4. FUNCTIA SPECIALA CARE SCHIMBA PITCH-UL ---
    void RedaSunet(AudioClip clip)
    {
        // Verificam daca avem un sunet asignat ca sa nu primim eroare
        if (clip != null && sursaAudio != null)
        {
            // Schimbam tonalitatea putin (intre 0.9 si 1.1)
            sursaAudio.pitch = Random.Range(0.9f, 1.1f);
            // PlayOneShot permite suprapunerea sunetelor (daca iei 2 litere rapid)
            sursaAudio.PlayOneShot(clip);
        }
    }
}