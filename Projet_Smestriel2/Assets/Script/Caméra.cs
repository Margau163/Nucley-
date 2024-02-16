using System.Collections;
using UnityEngine;
using Cinemachine;

public class Cam�ra : MonoBehaviour
{

    //d�clarer variable _defaultNoyaux envoy� par le script moovplayer
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
    [SerializeField] private Transform player; // Assignez cette variable dans l'�diteur Unity avec le Transform du joueur
    [SerializeField] private CinemachineVirtualCamera _Cam�ra; // Assignez cette variable dans l'�diteur Unity avec la cam�ra virtuelle
    //private Vector3 offset; // L'offset entre la cam�ra et le joueur


    void Start()
    {
        _currentSecondNoyauFocus = _defaultNoyaux.connections[_moovPlayer.currentIndex]; // on stocke le premier noyau dans la variable secondNoyauxFocus pour pouvoir le r�utiliser plus tard
        //offset = transform.position - player.position;    // Calcul de l'offset initial bas� sur les positions actuelles de la cam�ra et du joueur 
    }

    
    void Update()
    {
            float fov = Camera.main.fieldOfView;
            fov -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
            fov = Mathf.Clamp(fov, minFieldOfView, maxFieldOfView);
            Camera.main.fieldOfView = fov;
            //transform.position = player.position + offset;

            // Cam�ra regarde autour d'elle
            if (Input.GetMouseButton(1)) // Bouton du milieu de la souris
            {
                float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
                float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

                xRotation -= mouseY;
                yRotation += mouseX;

                xRotation = Mathf.Clamp(xRotation, -90f, 90f);

                transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
            }

            // Cam�ra se d�place
            if (Input.GetMouseButton(2)) // Bouton droite de la souris
            {
                float mouseX = Input.GetAxis("Mouse X") * moovSpeed * Time.deltaTime;
                float mouseY = Input.GetAxis("Mouse Y") * moovSpeed * Time.deltaTime;

                transform.Translate(-mouseX, -mouseY, 0);
            }

            // Recentrage de la cam�ra sur la player
           
            if (Input.GetKeyDown(KeyCode.Q)) // Remplacez 'R' par la touche que vous souhaitez utiliser pour recentrer la cam�ra
            {
                RecenterCameraOnPlayer();
            }
    }
    private void RecenterCameraOnPlayer()
    {
        // Calculez la direction de la cam�ra vers le joueur
        Vector3 directionToPlayer = player.position - transform.position;
        // Cr�ez une rotation bas�e sur la direction vers le joueur, en s'alignant sur l'axe Y pour ne pas incliner la cam�ra
        Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer, Vector3.up);
        // Appliquez la rotation � la cam�ra
        transform.rotation = targetRotation;
    }

    public void ChangeSegondNoyaux(GameObject newSecondFocus)
    {
        _currentSecondNoyauFocus = newSecondFocus;
        //mettre � jour la variable _secondNoyauxFocus
    }

}
