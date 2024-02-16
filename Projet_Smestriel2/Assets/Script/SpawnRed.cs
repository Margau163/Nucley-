using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRed : MonoBehaviour
{
    public List<Transform> spawnPoints; // Liste des points de spawn
    [SerializeField] private float spawnTime; // Temps entre chaque spawn
    [SerializeField] private float spawnDestroy; // Temps avant le premier spawn]
    [SerializeField] private GameObject Ennemi; // Objet � spawn

    // Start is called before the first frame update
    void Start()
    {
       
    }

    //les verres doivent arriver en random et disparaitre au bout d'un certain temps
    // Update is called once per frame
    void Update()
    {
        spawnTime -= Time.deltaTime; // D�cr�mente le temps entre chaque spawn
		if (spawnTime <= 0) // Si le temps entre chaque spawn est inf�rieur ou �gal � 0
		{
			spawnTime = 4f; // R�initialise le temps entre chaque spawn
			int spawnIndex = Random.Range(0, spawnPoints.Count); // Choisis un point de spawn al�atoire
			GameObject newEnnemi = Instantiate(Ennemi, spawnPoints[spawnIndex].position, Quaternion.identity); // Instancie un verre rouge � la position du point de spawn
            Destroy (newEnnemi, spawnDestroy); // D�truit le verre rouge au bout de x secondes
		}
    }
}
