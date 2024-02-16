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
        // Assurez-vous que l'image d'accueil est activ�e et que le menu est d�sactiv� au d�but
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
            // V�rifiez si l'image d'accueil est actuellement activ�e
            if (imageAccueil.activeSelf)
            {
                // Activez l'image de menu et d�sactivez l'image d'accueil
                imageMenu.SetActive(true);
                buttonStart.SetActive(true);
                imageAccueil.SetActive(false);
               
            }
       }     
    }
    public void StartNewGame()
    {
        Time.timeScale = 1;
        Debug.Log("J'ai chang�"); // Bon pour le d�bogage
        SceneManager.LoadScene("test setup");
        imageMenu.SetActive(false);
        buttonStart.SetActive(false);

    }
    public void ContinuGame()
    {
        Time.timeScale = 1;
        Debug.Log("J'ai continu�"); // Bon pour le d�bogage
        SceneManager.LoadScene("test setup");
        imageMenu.SetActive(false);
        buttonContinu.SetActive(false);
    }
}
