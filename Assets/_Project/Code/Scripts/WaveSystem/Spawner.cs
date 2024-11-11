using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab; // Prefab do inimigo
    [SerializeField] private float spawnInterval = 1.5f; // Tempo entre spawns
    [SerializeField] private int maxEnemies = 10; // Número máximo de inimigos por wave

    private List<GameObject> activeEnemies = new List<GameObject>(); // Lista de inimigos ativos
    private bool canSpawn = true; // Controle de spawn

    public void StartSpawning()
    {
        canSpawn = true;
        StartCoroutine(SpawnEnemies());
    }

    public void StopSpawning()
    {
        canSpawn = false;
        StopCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        // Obtém o alvo (jogador) na cena
        GameObject target = GameObject.FindWithTag("Player");
        if (target == null)
        {
            Debug.LogWarning("Player not found!");
            yield break; // Sai da coroutine se o alvo não estiver disponível
        }

        while (canSpawn && activeEnemies.Count < maxEnemies)
        {
            Vector3 spawnPosition = GetSpawnPosition();
            GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

            // Calcula a direção do spawn para o alvo
            Vector2 directionToTarget = (target.transform.position - spawnPosition).normalized;

            // Adiciona força na direção do alvo
            float forceMagnitude = 300f; // Ajuste a magnitude da força conforme necessário
            enemy.GetComponent<Rigidbody2D>().AddForce(directionToTarget * forceMagnitude);

            activeEnemies.Add(enemy);

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private Vector3 GetSpawnPosition()
    {
        // Obtém a câmera principal
        Camera mainCamera = Camera.main;

        // Define os limites da câmera em coordenadas de mundo
        Vector3 cameraBottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        Vector3 cameraTopRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));

        // Define margens fora da tela para garantir que o spawn seja fora dos limites
        float spawnOffset = 2f;

        // Gera posição aleatória na borda da tela
        float x, y;

        // Decide aleatoriamente se o spawn ocorrerá no eixo vertical ou horizontal da borda
        if (Random.value > 0.5f)
        {
            // Spawn no eixo vertical (topo ou base da câmera)
            x = Random.Range(cameraBottomLeft.x - spawnOffset, cameraTopRight.x + spawnOffset);
            y = Random.value > 0.5f ? cameraTopRight.y + spawnOffset : cameraBottomLeft.y - spawnOffset;
        }
        else
        {
            // Spawn no eixo horizontal (esquerda ou direita da câmera)
            x = Random.value > 0.5f ? cameraTopRight.x + spawnOffset : cameraBottomLeft.x - spawnOffset;
            y = Random.Range(cameraBottomLeft.y - spawnOffset, cameraTopRight.y + spawnOffset);
        }

        return new Vector3(x, y, 0);
    }

    private void RemoveEnemy(GameObject enemy)
    {
        if (activeEnemies.Contains(enemy))
        {
            activeEnemies.Remove(enemy);
            Destroy(enemy);
        }
    }

    public void DestroyAllEnemies()
    {
        foreach (GameObject enemy in activeEnemies)
        {
            if (enemy != null)
            {
                Destroy(enemy);
            }
        }
        activeEnemies.Clear();
    }

    public bool AllEnemiesDestroyed()
    {
        return activeEnemies.Count == 0;
    }
}
