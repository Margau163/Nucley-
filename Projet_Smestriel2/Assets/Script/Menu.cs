using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public GameObject imageAccueil; // Glissez votre image d'accueil ici dans l'inspecteur
    public GameObject imageMenu; // Glissez votre image de menu ici dans l'inspecteur
    public GameObject buttonStart;
    public GameObject buttonQuit;
    public GameObject buttonContinu;

    void Start()
    {
        // Assurez-vous que l'image d'accueil est activée et que le menu est désactivé au début
        imageAccueil.SetActive(true);
        imageMenu.SetActive(false);
        buttonStart.SetActive(false);
        Scene scene = SceneManager.GetActiveScene();      
    }

    void Update()
    {
        // Si l'utilisateur clique avec la souris
       if (Input.GetMouseButtonDown(0)) // 0 est pour le bouton gauche de la souris
        {
            // Vérifiez si l'image d'accueil est actuellement activée
            if (imageAccueil.activeSelf)
            {
                // Activez l'image de menu et désactivez l'image d'accueil
                imageMenu.SetActive(true);
                buttonStart.SetActive(true);
                imageAccueil.SetActive(false);
               
            }
       }     
    }
    public void StartNewGame()
    {
        Time.timeScale = 1;
        Debug.Log("J'ai changé"); // Bon pour le débogage
        SceneManager.LoadScene("test setup");
        imageMenu.SetActive(false);
        buttonStart.SetActive(false);

    }
    public void ContinuGame()
    {
        Time.timeScale = 1;
        Debug.Log("J'ai continué"); // Bon pour le débogage
        SceneManager.LoadScene("test setup");
        imageMenu.SetActive(false);
        buttonContinu.SetActive(false);
    }
}
