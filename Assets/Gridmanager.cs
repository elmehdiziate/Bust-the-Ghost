using UnityEngine;
using UnityEngine.UI;

public class Gridmanager : MonoBehaviour
{
    [SerializeField]
    private int rows = 7;
    [SerializeField]
    private int cols = 13;
    [SerializeField]
    private float tileSize = 1;
    private GameObject ghost;
    [SerializeField]
    private int initialCredits = 10; // Initial credits for the player
    private int remainingCredits;

    [SerializeField]
    private int allowedBusts = 3; // Number of allowed busts
    private int remainingBusts;

    private Canvas canvas;
    public Text creditsText; // UI Text to display credits
    public Text bustsText; // UI Text to display remaining busts
    public GameObject winLosePanel; // Panel to display win/lose message
    public Text winLoseText; // Text to show win/lose message

    public GameObject[,] grid; // 2D array to store the tiles
    public double[,] probabilities;
    private Vector2 ghostPosition;
    private bool showProbabilities = false; // Variable to track the visibility of probabilities
    private Vector2 lastClickedCell = Vector2.zero;
    // Start is called before the first frame update
    void Start()
    {
        // Find the existing Canvas in the scene
        canvas = FindObjectOfType<Canvas>();

        // Create Credits Text
        creditsText = CreateUIText("CreditsText", "Credits: " + initialCredits, new Vector2(200, 50), new Vector2(10, -10));

        // Create Busts Text
        bustsText = CreateUIText("BustsText", "Busts: " + allowedBusts, new Vector2(200, 50), new Vector2(220, -10));

        // Position the Credits and Busts Text at the top
        creditsText.rectTransform.anchoredPosition = new Vector2(500, 10);  // Updated position
        bustsText.rectTransform.anchoredPosition = new Vector2(500, -20);  // Updated position
        // Create Win/Lose Panel
        CreateWinLosePanel();

        remainingCredits = initialCredits;
        remainingBusts = allowedBusts;
        GenerateGrid();
        PlaceNoisyPrint();
    }

    private void GenerateGrid()
    {
        grid = new GameObject[rows, cols];
        probabilities = new double[rows, cols];

        PlaceGhost();

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                GameObject tile = (GameObject)Instantiate(Resources.Load("white"), transform);
                float posX = col * tileSize;
                float posY = row * -tileSize;
                tile.transform.position = new Vector2(posX, posY);
                grid[row, col] = tile;

                tile.AddComponent<BoxCollider2D>(); // Add a collider


                // Add the tile script and setup
                TileScript tileScript = tile.AddComponent<TileScript>();
                tileScript.Setup(this);
                

                double probability = (double)1 / (rows * cols);
                probabilities[row, col] = probability;

                GameObject textMeshChild = new GameObject("TextMesh");
                textMeshChild.transform.parent = grid[row, col].transform;
                textMeshChild.transform.localPosition = Vector3.zero;
                TextMesh probabilityText = textMeshChild.AddComponent<TextMesh>();
                probabilityText.text = probabilities[row, col].ToString("F2");
                probabilityText.characterSize = 0.35f;
                probabilityText.anchor = TextAnchor.MiddleCenter;
                probabilityText.alignment = TextAlignment.Center;
                probabilityText.color = Color.black;
                textMeshChild.SetActive(showProbabilities);
            }
        }

        float gridW = cols * tileSize;
        float gridH = rows * tileSize;
        transform.position = new Vector2(-gridW / 2 + tileSize / 2, gridH / 2 - tileSize / 2);
        Button peepButton = GameObject.Find("Peep").GetComponent<Button>();
        peepButton.onClick.AddListener(ToggleProbabilities);
        Button bustButton = GameObject.Find("Bust").GetComponent<Button>();
        bustButton.onClick.AddListener(OnBustClicked);
    }

    // Method to create UI Text
    private Text CreateUIText(string name, string text, Vector2 size, Vector2 position)
    {
        GameObject textObject = new GameObject(name);
        textObject.transform.SetParent(canvas.transform, false);

        Text uiText = textObject.AddComponent<Text>();
        uiText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        uiText.text = text;
        uiText.fontSize = 30;
        uiText.color = Color.black;
        uiText.alignment = TextAnchor.MiddleCenter;

        RectTransform rectTransform = textObject.GetComponent<RectTransform>();
        rectTransform.sizeDelta = size;
        rectTransform.anchoredPosition = position;

        return uiText;
    }

    private void CreateWinLosePanel()
    {
        // Create Panel
        winLosePanel = new GameObject("WinLosePanel");
        winLosePanel.transform.SetParent(canvas.transform, false);
        Image panelImage = winLosePanel.AddComponent<Image>();
        panelImage.color = new Color(0, 0, 0, 0.7f); // Updated color (black with 70% opacity)

        RectTransform panelRect = winLosePanel.GetComponent<RectTransform>();
        panelRect.sizeDelta = new Vector2(300, 150);
        panelRect.anchoredPosition = Vector2.zero;

        // Create Text on Panel
        winLoseText = CreateUIText("WinLoseText", "", new Vector2(280, 130), Vector2.zero);
        winLoseText.transform.SetParent(winLosePanel.transform);
        winLoseText.color = Color.white; // White text color
        winLoseText.fontSize = 24; // Larger font size

        winLosePanel.SetActive(false); // Initially hide the win/lose panel
    }

    private void PlaceGhost()
    {
        int randomRow = UnityEngine.Random.Range(0, rows);
        int randomCol = UnityEngine.Random.Range(0, cols);
        float posX = randomCol * tileSize;
        float posY = randomRow * -tileSize;

        ghost = (GameObject)Instantiate(Resources.Load("ghost"), transform); // Store the reference
        SpriteRenderer ghostRenderer = ghost.GetComponent<SpriteRenderer>();
        ghostRenderer.sortingOrder = -1;
        ghost.transform.position = new Vector2(posX, posY);
        ghostPosition = ghost.transform.position;
        ghost.SetActive(false); // Initially hide the ghost
        Debug.Log(" The Ghost Position is: " + ghostPosition);
    }

    public void ToggleProbabilities()
    {
        Debug.Log("running");
        showProbabilities = !showProbabilities;
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                GameObject textMeshChild = grid[row, col].transform.Find("TextMesh").gameObject;
                if (textMeshChild != null)
                {
                    textMeshChild.SetActive(showProbabilities);
                }
            }
        }
    }

    public void OnCellClicked(Vector2 cellPosition)
    {
        // Adjust cellPosition based on the centered grid
        Vector2 adjustedPosition = new Vector2((cellPosition.x + (cols * tileSize) / 2) - 0.65f, (cellPosition.y - (rows * tileSize) / 2)+0.65f);
        int col = Mathf.RoundToInt((cellPosition.x - transform.position.x) / tileSize);
        int row = Mathf.RoundToInt((transform.position.y - cellPosition.y) / tileSize);
        Debug.Log("the col is: "+ col+ "the row is: "+ row);

        Debug.Log(adjustedPosition);
        lastClickedCell = adjustedPosition;
        if (row < 0 || row >= rows || col < 0 || col >= cols)
        {
            Debug.LogError("Clicked position is outside the grid.");
            return;
        }
        // Update only the clicked cell's color
        //int distance = CalculateManhattanDistance(adjustedPosition, ghostPosition);
        int distance = CalculateEuclideanDistance(adjustedPosition, ghostPosition);
        string colorBasedOnDistance = DetermineHighestProbabilityColor(row, col, distance);
        SetTileColor(row, col, colorBasedOnDistance);

        // Update and normalize probabilities for all cells
        UpdateAndNormalizeProbabilities(row, col, colorBasedOnDistance);


        // Deduct credit and update UI
        remainingCredits--;
        UpdateCreditsText();

        // Check if player ran out of credits
        if (remainingCredits <= 0)
        {
            GameOver(false); // Player loses
        }
    }


    private void UpdateAndNormalizeProbabilities(int clickedRow, int clickedCol, string clickedColor)
    {
        double totalProbability = 0;

        // Update probabilities
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                //int distance = CalculateManhattanDistance(new Vector2(clickedCol, clickedRow), new Vector2(col, row));
                int distance = CalculateEuclideanDistance(new Vector2(clickedCol, clickedRow), new Vector2(col, row));
                double probabilityOfColorGivenGhost = JointTableProbability(clickedColor, distance);
                probabilities[row, col] *= probabilityOfColorGivenGhost;
                totalProbability += probabilities[row, col];
            }
        }

        // Normalize probabilities
        if (totalProbability > 0)
        {
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    probabilities[row, col] /= totalProbability;
                    UpdateProbabilityDisplay(row, col); // Update the displayed probability
                }
            }
        }
    }

    private void UpdateProbabilityDisplay(int row, int col)
    {
        GameObject textMeshChild = grid[row, col].transform.Find("TextMesh").gameObject;
        if (textMeshChild != null)
        {
            TextMesh probabilityText = textMeshChild.GetComponent<TextMesh>();
            probabilityText.text = probabilities[row, col].ToString("F2");
        }
    }




    private void UpdateGridColorsAndProbabilities(Vector2 adjustedPosition)
    {
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                //int distance = CalculateManhattanDistance(adjustedPosition, ghostPosition);
                int distance = CalculateEuclideanDistance(adjustedPosition, ghostPosition);
                UpdateTileProbabilityAndColor(row, col, distance);
            }
        }
    }



    private void UpdateTileProbabilityAndColor(int row, int col, int distance)
    {
        string highestProbColor = DetermineHighestProbabilityColor(row, col, distance);
        SetTileColor(row, col, highestProbColor);
    }

    private string DetermineHighestProbabilityColor(int row, int col, int distance)
    {
        // Convert double probabilities to float
        float redProb = (float)JointTableProbability("red", distance);
        float yellowProb = (float)JointTableProbability("yellow", distance);
        float greenProb = (float)JointTableProbability("green", distance);
        float orangeProb = (float)JointTableProbability("orange", distance);

        // Use Mathf.Max to determine the highest probability
        float maxProb = Mathf.Max(new float[] { redProb, yellowProb, greenProb, orangeProb });

        // Determine the color based on the highest probability
        if (maxProb == redProb) return "red";
        if (maxProb == yellowProb) return "yellow";
        if (maxProb == greenProb) return "green";
        return "orange";
    }


    private void SetTileColor(int row, int col, string color)
    {
        Color tileColor = Color.white;
        switch (color)
        {
            case "red": tileColor = Color.red; break;
            case "yellow": tileColor = Color.yellow; break;
            case "green": tileColor = Color.green; break;
            case "orange": tileColor = new Color(1f, 0.5f, 0f); break;
        }
        grid[row, col].GetComponent<SpriteRenderer>().color = tileColor;
    }


    public double JointTableProbability(string color, int DistanceFromGhost)
    {
        //Table 1
        if (color.Equals("yellow") && (DistanceFromGhost == 3 || DistanceFromGhost == 4)) return 0.5;
        if (color.Equals("red") && (DistanceFromGhost == 3 || DistanceFromGhost == 4)) return 0.05;
        if (color.Equals("green") && (DistanceFromGhost == 3 || DistanceFromGhost == 4)) return 0.3;
        if (color.Equals("orange") && (DistanceFromGhost == 3 || DistanceFromGhost == 4)) return 0.15;

        //Table2
        if (color.Equals("yellow") && (DistanceFromGhost == 1 || DistanceFromGhost == 2)) return 0.15;
        if (color.Equals("red") && (DistanceFromGhost == 1 || DistanceFromGhost == 2)) return 0.3;
        if (color.Equals("green") && (DistanceFromGhost == 1 || DistanceFromGhost == 2)) return 0.05;
        if (color.Equals("orange") && (DistanceFromGhost == 1 || DistanceFromGhost == 2)) return 0.5;

        //Table3
        if (color.Equals("yellow") && DistanceFromGhost >= 5) return 0.3;
        if (color.Equals("red") && DistanceFromGhost >= 5) return 0.05;
        if (color.Equals("green") && DistanceFromGhost >= 5) return 0.5;
        if (color.Equals("orange") && DistanceFromGhost >= 5) return 0.15;

        //Table4
        if (color.Equals("red") && DistanceFromGhost == 0) return 0.7;
        if (color.Equals("yellow") && DistanceFromGhost == 0) return 0.05;
        if (color.Equals("green") && DistanceFromGhost == 0) return 0.05;
        if (color.Equals("orange") && DistanceFromGhost == 0) return 0.2;

        return 0;
    }



    private int CalculateManhattanDistance(Vector2 pos1, Vector2 pos2)
    {
        return Mathf.Abs((int)(pos1.x - pos2.x)) + Mathf.Abs((int)(pos1.y - pos2.y));
    }
    private int CalculateEuclideanDistance(Vector2 pos1, Vector2 pos2)
    {
        return (int)Mathf.Sqrt(Mathf.Pow(pos2.x - pos1.x, 2) + Mathf.Pow(pos2.y - pos1.y, 2));
    }

    public void OnBustClicked()
    {
        if (lastClickedCell == Vector2.zero)
        {
            Debug.LogError("No cell has been clicked yet.");
            return;
        }

        // Deduct a bust and update UI
        remainingBusts--;
        UpdateBustsText();

        // Check if player ran out of busts
        if (remainingBusts <= 0 && lastClickedCell != ghostPosition)
        {
            GameOver(false); // Player loses
        }
        else if (lastClickedCell == ghostPosition)
        {
            GameOver(true); // Player wins
        }
    }

    private void UpdateCreditsText()
    {
        creditsText.text = "Credits: " + remainingCredits;
    }

    private void UpdateBustsText()
    {
        bustsText.text = "Busts: " + remainingBusts;
    }

    private void GameOver(bool won)
    {
        winLosePanel.SetActive(true);
        winLoseText.text = won ? "You Won!" : "You Lost!";
        // Optionally, disable further interactions or restart the game
        if (won)
        {
            int ghostRow = Mathf.RoundToInt((transform.position.y - ghost.transform.position.y) / tileSize);
            int ghostCol = Mathf.RoundToInt((ghost.transform.position.x - transform.position.x) / tileSize);
            grid[ghostRow, ghostCol].SetActive(false);
            ghost.SetActive(true); // Reveal the ghost if the player wins
        }
    }

    public void PlaceNoisyPrint()
    {
        int attempts = 0;
        int maxAttempts = rows * cols; // Prevent infinite loop

        while (attempts < maxAttempts)
        {
            int x = UnityEngine.Random.Range(0, cols);
            int y = UnityEngine.Random.Range(0, rows);

            // Check if the selected tile is not the ghost and not already a noisy print
            if (!grid[y, x].GetComponent<SpriteRenderer>().color.Equals(Color.red) && !(new Vector2(x, y) == ghostPosition))
            {
                grid[y, x].GetComponent<SpriteRenderer>().color = Color.red;
                break;
            }

            attempts++;
        }

        if (attempts >= maxAttempts)
        {
            Debug.LogWarning("No suitable spot found for placing a noisy print.");
        }
    }


    // Update is called once per frame
    void Update()
    {
        // Add any update logic here if needed
    }
}