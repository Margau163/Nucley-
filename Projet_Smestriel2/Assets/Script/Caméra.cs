using System.Collections;
using UnityEngine;
using Cinemachine;

public class Caméra : MonoBehaviour
{

    //déclarer variable _defaultNoyaux envoyé par le script moovplayer
    public Connection _defaultNoyaux; //variable du noyaux que le joueur peut changer 
    public MoovPlayer _moovPlayer; //variable du noyaux sur lequel le joueur se trouve dans la liste
    public GameObject _currentSecondNoyauFocus; //variable du game object noyaux sur lequel le joueur se trouve dans la liste
    [SerializeField] private float zoomSpeed;
    [SerializeField] private float minFieldOfView;
    [SerializeField] private float maxFieldOfView;
    [SerializeField] private float sensitivity;
    [SerializeField] private float xRotation;
    [SerializeField] private float yRotation;
    [SerializeField] private float moovSpeed;
    [SerializeField] private Transform player; // Assignez cette variable dans l'éditeur Unity avec le Transform du joueur
    [SerializeField] private CinemachineVirtualCamera _Caméra; // Assignez cette variable dans l'éditeur Unity avec la caméra virtuelle
    //private Vector3 offset; // L'offset entre la caméra et le joueur


    void Start()
    {
        _currentSecondNoyauFocus = _defaultNoyaux.connections[_moovPlayer.currentIndex]; // on stocke le premier noyau dans la variable secondNoyauxFocus pour pouvoir le réutiliser plus tard
        //offset = transform.position - player.position;    // Calcul de l'offset initial basé sur les positions actuelles de la caméra et du joueur 
    }

    
    void Update()
    {
            float fov = Camera.main.fieldOfView;
            fov -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
            fov = Mathf.Clamp(fov, minFieldOfView, maxFieldOfView);
            Camera.main.fieldOfView = fov;
            //transform.position = player.position + offset;

            // Caméra regarde autour d'elle
            if (Input.GetMouseButton(1)) // Bouton du milieu de la souris
            {
                float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
                float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

                xRotation -= mouseY;
                yRotation += mouseX;

                xRotation = Mathf.Clamp(xRotation, -90f, 90f);

                transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
            }

            // Caméra se déplace
            if (Input.GetMouseButton(2)) // Bouton droite de la souris
            {
                float mouseX = Input.GetAxis("Mouse X") * moovSpeed * Time.deltaTime;
                float mouseY = Input.GetAxis("Mouse Y") * moovSpeed * Time.deltaTime;

                transform.Translate(-mouseX, -mouseY, 0);
            }

            // Recentrage de la caméra sur la player
           
            if (Input.GetKeyDown(KeyCode.Q)) // Remplacez 'R' par la touche que vous souhaitez utiliser pour recentrer la caméra
            {
                RecenterCameraOnPlayer();
            }
    }
    private void RecenterCameraOnPlayer()
    {
        // Calculez la direction de la caméra vers le joueur
        Vector3 directionToPlayer = player.position - transform.position;
        // Créez une rotation basée sur la direction vers le joueur, en s'alignant sur l'axe Y pour ne pas incliner la caméra
        Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer, Vector3.up);
        // Appliquez la rotation à la caméra
        transform.rotation = targetRotation;
    }

    public void ChangeSegondNoyaux(GameObject newSecondFocus)
    {
        _currentSecondNoyauFocus = newSecondFocus;
        //mettre à jour la variable _secondNoyauxFocus
    }

}
