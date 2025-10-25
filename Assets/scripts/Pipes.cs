using UnityEngine;

public class Pipes : MonoBehaviour
{
    public static float speed = 5f; // Vitesse globale des pipes

    private void Update()
    {
        // Déplacement des pipes selon la vitesse globale
        transform.position += Vector3.left * speed * Time.deltaTime;

        // Détruire le pipe s'il sort de l'écran (optionnel)
        if (transform.position.x < -20f)
            Destroy(gameObject);
    }

    // Méthode pour augmenter la vitesse (optionnel)
    public static void IncreaseSpeed(float amount)
    {
        speed += amount;
    }
}