using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class TextureCamera : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject cameratexture;
    public Button fireButton;
    public Transform player;
    public float countdown = 60.0f;
    public GameObject TextKalah;
    public GameObject TextMenang;
    public Text Waktu;
    public Text Sisa;


    void Start()
    {
        if (Application.isMobilePlatform)
        {
            GameObject cameraParent = new GameObject("camParent");
            cameraParent.transform.parent = cameraParent.transform;
            this.transform.parent = cameraParent.transform;
            cameraParent.transform.Rotate(Vector3.right, 90);
        }
        Input.gyro.enabled = true;


        fireButton.onClick.AddListener(OnButtonDown);


        WebCamTexture webCamTexture = new WebCamTexture();
        cameratexture.GetComponent<MeshRenderer>().material.mainTexture = webCamTexture;
        webCamTexture.Play();
    }

    void OnButtonDown()
    {
        GetComponent<AudioSource>().Play();
        GameObject bullet = Instantiate(Resources.Load("bullet", typeof(GameObject))) as GameObject;
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        bullet.transform.rotation = player.transform.rotation;
        bullet.transform.position = player.transform.position;
        rb.AddForce(player.transform.forward * 500f);
        Destroy(bullet, 1.5f);

        GetComponent<AudioSource>().Play();
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion cameraRotation = new Quaternion(Input.gyro.attitude.x, Input.gyro.attitude.y, -Input.gyro.attitude.z, -Input.gyro.attitude.w);
        this.transform.localRotation = cameraRotation;

        Waktu.text = (countdown -= Time.deltaTime).ToString("Waktu 0.0");
        Sisa.text = GameObject.FindGameObjectsWithTag("Player").Length.ToString("Sisa 0");

        if (countdown <= 0.1f && (GameObject.FindGameObjectsWithTag("Player").Length > 0))
        {
            countdown = 0.0f;
            StartCoroutine("Endgame");
            
        }
        if((countdown >= -0.1f) && (GameObject.FindGameObjectsWithTag("Player").Length == 0))
        {
            countdown = 0.0f;
            StartCoroutine("Wingame");
        }
        
    }
    IEnumerator Endgame()
    {
        TextKalah.SetActive(true);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("MainMenu");
    }
    IEnumerator Wingame()
    {
        TextMenang.SetActive(true);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("MainMenu");
    }



}
