using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : Unit, IDamageable
{
    [SerializeField] private List<Sprite> sprites = new List<Sprite>();
    [SerializeField] private SpriteRenderer model;
    [SerializeField] private GameObject asteroidPrefab; // Prefab do asteroide menor
    [SerializeField] private int maxDivisions = 2; // Número máximo de divisões que o asteroide pode fazer
    [SerializeField] private float fragmentSpeed = 2f; // Velocidade inicial dos fragmentos
    private int currentDivision = 0; // Contador de divisões para cada asteroide
    private Rigidbody2D rb;

    void Start()
    {
        AttributeUtils.InitializeAllAttributes(attributes);
        RandomSprite();
        rb = GetComponent<Rigidbody2D>();
    }

    public void RandomSprite()
    {
        model.sprite = sprites[Random.Range(0, sprites.Count)];
    }

    public void TakeDamage(float damage)
    {
        Attribute life = AttributeUtils.ReturnAttribute("Life", attributes);
        if (life != null)
        {
            life.CurrentValue -= damage;
            if (life.CurrentValue <= 0)
            {
                Divide();
                Destroy(gameObject); // Destroi o asteroide atual
            }
        }
    }

    private void Divide()
    {
        if (currentDivision >= maxDivisions) return;

        int fragmentsToCreate = CalculateFragments();

        for (int i = 0; i < fragmentsToCreate; i++)
        {
            CreateFragment();
        }
    }

    private int CalculateFragments()
    {
        // Calcula o número de fragmentos com base no nível de divisão
        return Mathf.Max(1, 2 - currentDivision); // Diminui o número de fragmentos em cada divisão
    }

    private void CreateFragment()
    {
        // Define uma posição de spawn com um pequeno deslocamento aleatório
        Vector3 spawnPosition = transform.position + (Vector3)Random.insideUnitCircle * 0.3f;

        // Instancia o fragmento de asteroide
        GameObject newAsteroid = Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);

        // Configura o fragmento como um nível de divisão superior e diminui maxDivisions
        Obstacle obstacleComponent = newAsteroid.GetComponent<Obstacle>();
        obstacleComponent.currentDivision = currentDivision + 1;
        obstacleComponent.maxDivisions = maxDivisions - 1; // Diminui o número de divisões restantes

        // Ajusta o tamanho do fragmento para diminuir de forma controlada
        newAsteroid.transform.localScale = transform.localScale * 0.75f; // Diminui 25% ao invés de 50%

        // Direção e força aleatória para o fragmento
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        Rigidbody2D fragmentRb = newAsteroid.GetComponent<Rigidbody2D>();
        fragmentRb.AddForce(randomDirection * fragmentSpeed, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<IDamageable>() != null && other.tag == "Player")
        {
            other.GetComponent<IDamageable>().TakeDamage(AttributeUtils.ReturnAttribute("damage", attributes).CurrentValue);
            Destroy(this.gameObject);
        }
    }
}
