using UnityEngine;

public class EnnemyUITracker : MonoBehaviour
{
    [SerializeField] private Ennemy ennemy;           // Ennemi à suivre
    [SerializeField] private RectTransform mapImage;  // Image fixe représentant le chemin
    [SerializeField] private RectTransform icon;      // Icône qui bouge sur l'image
    [SerializeField] private Vector2 mapWorldSize;    // Taille réelle du terrain représenté sur l'image UI

    private Vector3 previousWorldPos;

    void Start()
    {
        if (ennemy != null)
            previousWorldPos = ennemy.transform.position; // Mémorise la position initiale
    }

    void Update()
    {
        if (ennemy == null || icon == null || mapImage == null)
            return;

        // Calcul du delta de mouvement de l'ennemi
        Vector3 currentWorldPos = ennemy.transform.position;
        Vector3 deltaWorld = currentWorldPos - previousWorldPos;

        // Conversion du delta en UI
        Vector2 deltaUI = new Vector2(
            deltaWorld.x / mapWorldSize.x * mapImage.rect.width,
            deltaWorld.z / mapWorldSize.y * mapImage.rect.height // z si top-down
        );

        // Ajout du delta à l'icône
        icon.anchoredPosition += deltaUI;

        // Mise à jour de la position précédente
        previousWorldPos = currentWorldPos;
    }
}