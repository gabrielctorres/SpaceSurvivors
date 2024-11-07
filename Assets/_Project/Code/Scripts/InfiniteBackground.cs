using UnityEngine;
using System.Collections.Generic;

public class InfiniteBackground : MonoBehaviour
{
    public GameObject backgroundPrefab1; // Prefab do primeiro background (ex: fundo com degradê)
    public GameObject backgroundPrefab2; // Prefab do segundo background (ex: estrelas)
    public Transform player;             // Referência ao jogador
    public int gridSize = 2;             // Quantidade de imagens em cada direção (ex: 2x2 grid)
    public Transform parentObject;       // Objeto pai onde as instâncias serão organizadas

    private float backgroundWidth;       // Largura do background (1920)
    private float backgroundHeight;      // Altura do background (1080)
    private Vector2 lastPlayerPosition;  // Guarda a última posição do jogador em que houve atualização
    private Dictionary<Vector2, GameObject> backgroundTiles1 = new Dictionary<Vector2, GameObject>();
    private Dictionary<Vector2, GameObject> backgroundTiles2 = new Dictionary<Vector2, GameObject>();

    void Start()
    {
        // Obtem o tamanho do background a partir do primeiro prefab
        backgroundWidth = backgroundPrefab1.GetComponent<SpriteRenderer>().bounds.size.x;
        backgroundHeight = backgroundPrefab1.GetComponent<SpriteRenderer>().bounds.size.y;

        // Inicializa os tiles ao redor do jogador
        lastPlayerPosition = new Vector2(
            Mathf.Floor(player.position.x / backgroundWidth),
            Mathf.Floor(player.position.y / backgroundHeight)
        );

        UpdateBackgrounds(true); // Atualiza o cenário no início
    }

    void Update()
    {
        // Checa se o jogador mudou de posição o suficiente para atualizar os backgrounds
        Vector2 playerGridPosition = new Vector2(
            Mathf.Floor(player.position.x / backgroundWidth),
            Mathf.Floor(player.position.y / backgroundHeight)
        );

        if (playerGridPosition != lastPlayerPosition)
        {
            lastPlayerPosition = playerGridPosition;
            UpdateBackgrounds(false); // Atualiza o cenário com base na nova posição
        }
    }

    void UpdateBackgrounds(bool initialSetup)
    {
        Vector2 playerGridPosition = lastPlayerPosition;

        // Remove backgrounds que estão longe demais
        List<Vector2> positionsToRemove = new List<Vector2>();
        foreach (var pos in backgroundTiles1.Keys)
        {
            if (Vector2.Distance(pos, playerGridPosition) > gridSize)
            {
                Destroy(backgroundTiles1[pos]);
                Destroy(backgroundTiles2[pos]);
                positionsToRemove.Add(pos);
            }
        }
        foreach (var pos in positionsToRemove)
        {
            backgroundTiles1.Remove(pos);
            backgroundTiles2.Remove(pos);
        }

        // Calcula a área do cenário que deve ser preenchida com imagens (ao redor do jogador)
        for (int x = -gridSize; x <= gridSize; x++)
        {
            for (int y = -gridSize; y <= gridSize; y++)
            {
                Vector2 tilePosition = new Vector2(playerGridPosition.x + x, playerGridPosition.y + y);
                Vector2 worldPosition = new Vector2(tilePosition.x * backgroundWidth, tilePosition.y * backgroundHeight);

                // Verifica se o tile ainda não foi criado
                if (!backgroundTiles1.ContainsKey(tilePosition))
                {
                    // Instancia o primeiro background e define o objeto pai
                    GameObject newBackground1 = Instantiate(backgroundPrefab1, worldPosition, Quaternion.identity, parentObject);
                    backgroundTiles1[tilePosition] = newBackground1;

                    // Instancia o segundo background e define o objeto pai
                    GameObject newBackground2 = Instantiate(backgroundPrefab2, worldPosition, Quaternion.identity, parentObject);
                    backgroundTiles2[tilePosition] = newBackground2;
                }
            }
        }
    }
}
