# Bust-the-Ghost

### Team members

Kaoutar Benazzou | Aya Lyousfi | El Mehdi Ziate

## Introduction

Within this project, we will be combining between gaming and probabilistic reasoning in a project known as “Bust the Ghost” using Unity as a game development platform. This report outlines a comprehensive implementation of the third A.I. project that integrates the thrill of gaming with the sophistication of Bayesian probabilistic inference. This project will not only push us to create an entertaining gaming experience but also to dive into the interesting world of probabilistic reasoning.

## **Game Environment and Concept**

Bust the Ghost resembles to the game Minesweeper that we used to play a lot. Here players navigate a 7x13 grid in a quest to unveil the ghost hidden within. The game introduces a new concept having a sensor indicate a specific color once a tile is selected to direct the player to the ghost using proximity. A green hue signifies a safe distance, while red reveals the ghost's close presence. However, the sensor's noise adds an element of uncertainty, so we incorporate probabilistic reasoning methodologies to deduce the ghost's likely presence.

## Unity

Unity is a powerful and famous game development engine that enables developers to create interactive and visually appealing 2D, 3D, augmented reality (AR), and virtual reality (VR) experiences with it. Unity provides an extensive toolkit, features, and a user-friendly interface, making it intuitive to use for both beginners and experienced developers. Unity has a large and active community of developers providing forums, tutorials, and multiple resources for developers to easily pick up how it works. It also offers tools to create user interfaces including the grid and the buttons in this project seamlessly. 

## **Probabilistic Inferencing**

Probabilistic inference is an important topic in AI, more specifically in our project at hand. It refers to the process of using mathematics and probability theory to make predictions, draw new conclusions, or update beliefs based on available evidence/information. In our context as well, probabilistic inferencing helps in handling uncertainty and making decisions in situations where the outcome is not completely deterministic. Bayesian inference is one way to approach probabilistic reasoning by updating beliefs or probabilities based on new evidence found. It relies on Bayes' theorem that mathematically relates the likelihood of an event to the prior probability of the event and the probability of the evidence. 

![image](https://github.com/elmehdiziate/Bust-the-Ghost/assets/109172506/7bf27ae3-3004-4ccb-a060-99d3aa1a78ce)


- *P*(*A\B*) represents the probability of event A occurring given that event B has occurred. This is called the posterior probability.
- *P*(*B\A*) represents the probability of event B occurring given that event A has occurred. This is called the likelihood.
- *P*(*A*) represents the prior probability of event A, i.e., the probability of A occurring before taking into account the new evidence.
- *P*(*B*) represents the probability of event B occurring, i.e., the total probability.

Knowing that P(A|B) is given by the following conditional probability formula:

![image](https://github.com/elmehdiziate/Bust-the-Ghost/assets/109172506/b39d8b32-1f87-4a70-bf5e-3cc87c887639)


## Game Description

“Bust the Ghost” game is a 7x13 grid where a ghost is randomly placed at some tile. The player can click on “Play” to start the game. 

![image](https://github.com/elmehdiziate/Bust-the-Ghost/assets/109172506/10b41e9f-a1c3-4909-a1bd-38a7b8ec6aef)


The player is taken to a screen where the grid is displayed for the user to start playing. We have added a random red tile to add noise to the game and mislead the player thinking that the ghost might be around that red tile, this way they can lose one point form their initial credit. 

![image](https://github.com/elmehdiziate/Bust-the-Ghost/assets/109172506/714d3b68-dd42-403c-ac43-84369d5c2152)


The game allows the user to take a look at the possible probabilities P(Ghost|Color) in each cell by clicking the **peep** button. The game uses a uniform distribution of probabilities at first as shown bellow. 

![image](https://github.com/elmehdiziate/Bust-the-Ghost/assets/109172506/67753dc2-3d8b-45f5-b35b-12b97a1fa854)

The player clicks on the tiles and a color pops up to give a hint about how close the user is to the ghost, simultaneously, after each click the probability is updated based on how far the ghost is from that cell. The colors displayed as sensors are: 

- Red it means that the cell is where the ghost is hidden
- Orange indicated that the ghost is close and it is 1 or 2 cells away
- Yellow shows that the ghost is placed 3 or 4 cells away
- Green is an indication that the ghost is more than 5 cells away

Each click costs the player one point from their initial credit and the player can keep playing as long as the credit does not run out or they have not busted the ghost yet. When the user locates the cell where the ghost is placed, they can click on **Bust the Ghost** button. The player then either wins if they guess the location of the ghost correctly there:

![image](https://github.com/elmehdiziate/Bust-the-Ghost/assets/109172506/837367c1-14e3-48c5-ab2a-dfc1f2ded0df)


Or loses if they miss where the ghost is actually placed:

![image](https://github.com/elmehdiziate/Bust-the-Ghost/assets/109172506/35ee0f6c-2bee-47e5-9d5d-0dcf4b0e2624)


## Bayesian Inference in Bust the Ghost

As seen from the previous screenshots we keep changing the probabilities each time a tile is clicked depending on the distance form the ghost. One of the four colors is displayed depending on the distance from the ghost. To decide on the color to display we need to include mathematics especially probabilities based on Bayesian inference. This is done by finding the probability P(Color|Distance) defining the likelihood of sensor readings given the distance from the ghost. This is key to mapping sensor inputs to probabilistic outputs. Updating the posterior probability of the ghost’s location can be done using this formula: 

`P(ghostN)=P(ghostN/colorN)=P(ghostN-1) * P(colorN/Distance)`

`P(ghost/color)`is the probability of a tile being a ghost given  information about its color.

Where:

- **P(ghostN)** is the probability to be updated, it is the posterior probability of where tho ghost is located after clicking N times.
- **P(ghostN-1)** is the prior probability of the location of the ghost before clicking. At the beginning, the prior probability P(ghost0) = P(ghost).
- **P(colorN|Distance)** is the conditional probability of observing a specific color given the distance from the ghost. We need to determine these probabilities based on the color scheme mentioned earlier.

⇒ With **Distance** being the distance from the ghost.

### The probabilities for each color:

To do the joint probabilities we will use the following 4 tables:

**Table 1:**

| Distance from Ghost | Yellow | Red | Green | Orange |
| --- | --- | --- | --- | --- |
| 1 | 0.15 | 0.3 | 0.05 | 0.5 |
| 2 | 0.15 | 0.3 | 0.05 | 0.5 |

**Table 2:**

| Distance from Ghost | Yellow | Red | Green | Orange |
| --- | --- | --- | --- | --- |
| 3 | 0.5 | 0.05 | 0.3 | 0.15 |
| 4 | 0.5 | 0.05 | 0.3 | 0.15 |

**Table 3:**

| Distance from Ghost | Yellow | Red | Green | Orange |
| --- | --- | --- | --- | --- |
| >=5 | 0.3 | 0.05 | 0.5 | 0.15 |

**Table 4:**

| Distance from Ghost | Yellow | Red | Green | Orange |
| --- | --- | --- | --- | --- |
| 0 | 0.05 | 0.7 | 0.05 | 0.2 |

We can modify the posterior probability from the color probabilities calculated previously using:
`P(ghostN)=P(ghostN/colorN)=P(ghostN-1) * P(colorN/Distance)`

Then we normalize the probabilities so as there is consistency and they actually add up to 1.   After updating the posterior probability, we divide each updated value by the sum of all updated values.

## Code Description

### StartMenu.cs

```csharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;  // Make sure to include this for UI elements

public class StartMenu : MonoBehaviour
{
    public void StartGame()
    {
        Debug.Log("Starting game...");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
```

- The script, named "StartMenu," contains a method called "StartGame," which is invoked when the button “Play” in the start menu is clicked. Within this method, a debug message is logged to the console indicating that the game is starting. The key functionality is implemented using Unity's SceneManager class, where the active scene's index (Starting Menu scene) is obtained, and one is added to it so we can get to the bust game scene. This operation effectively loads the next scene in the build order, facilitating a smooth transition from the start menu to the gameplay scene. The script is attached to the Play button, and serves as a simple mechanism to progress from the start menu to the subsequent scene in the game's flow. (This is was done through the building setting that allowed us to load the Starting menu scene before the game).

### Create the grid

**Generate the grid:**

```csharp
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
```

- creating a grid of game objects within a Unity game. It initializes two arrays: **`grid`** to store the GameObject instances representing the tiles of the grid, and **`probabilities`** to store a numerical value for each tile. The method places a "ghost" within the grid using the **`PlaceGhost`** method (not provided in the snippet), then iterates through each grid position defined by **`rows`** and **`cols`**. At each position, it instantiates a new tile from a prefab named "white", sets its position based on the current row and column indices, and attaches a **`BoxCollider2D`** and a custom **`TileScript`** to it.
- It also calculates an initial probability for the tile, creates a text object to display this probability, and sets the text properties. This initial probability is a uniform distribution to start with by assigning the same probability to guess the ghost to each cell in the grid by sampling over the the 1 over the number of cells. You an refer to the previous screenshot.
- After the grid is created, the method adjusts the entire grid's position to be centered, and sets up two buttons, "Peep" and "Bust", with event listeners that call **`ToggleProbabilities`** and **`OnBustClicked`** methods when clicked that we will discuss later on.

**Place the ghost:**

```csharp
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
```

- The **`PlaceGhost`** method is designed to randomly position a "ghost" within a grid in a Unity game. It starts by determining a random row and column where the ghost will be placed, using Unity's **`Random.Range`** function to ensure that the chosen row and column are within the bounds of the grid defined by **`rows`** and **`cols`** variables. With these indices, it calculates the ghost's position on the screen (**`posX`** and **`posY`**) by multiplying the column and row by the **`tileSize`**. The sign of **`posY`** is inverted to align with Unity's coordinate system where the y-axis is positive upwards.
- A new GameObject is instantiated from a prefab named "ghost" which is expected to be a resource in the project. The instantiated ghost GameObject is then stored in the variable **`ghost`**. The method obtains the **`SpriteRenderer`** component from the ghost and sets its **`sortingOrder`** to -1, which affects the rendering order of the sprite, making sure it appears behind certain other elements in the scene.
- The position of the ghost GameObject is set using the calculated **`posX`** and **`posY`**, and this position is stored in the **`ghostPosition`** variable for future reference. Finally, the ghost is initially set to inactive with **`SetActive(false)`**; this means that the ghost is not visible or interactable within the game scene until the player click on the bust button and guess the ghost position.

### Peep Button

The peep button allows the user to either see the probabilities or hide them, the function **`ToggleProbabilities`**  is linked to it:

```csharp
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
```

- The **`ToggleProbabilities`** method is a public function in a Unity script that toggles the visibility of probability values on a grid. It then changes the state of a boolean variable named **`showProbabilities`** to its opposite value (if it was **`true`**, it becomes **`false`**, and vice versa).
- The method proceeds with a nested loop that goes through each cell of the grid defined by **`rows`** and **`cols`**. For each cell, it attempts to find a child GameObject named "TextMesh". If such a GameObject is found (i.e., **`textMeshChild`** is not null), it sets the active state of this GameObject to the current value of **`showProbabilities`**. This effectively shows or hides the probability text on each tile in the grid, depending on whether **`showProbabilities`** is true or false.
- In essence, this function is meant to toggle the visibility of text elements on the grid, presumably to show or hide the probability of certain game events occurring at each grid position to the player.

### Bust Button

The Bust button allows the user to confirm his guess about the position of the ghost. when clicked, it triggers the **`OnBustClicked`** function: 

```csharp
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
```

- This code defines a method named **`OnBustClicked`**, presumably within a game-related script. The method first checks if a game cell has been previously clicked by examining the variable **`lastClickedCell`**. If no cell has been clicked (i.e., **`lastClickedCell`** is Vector2.zero), an error is logged, and the method exits. If a cell has been clicked, the code proceeds to decrement a variable named **`remainingBusts`** and updates the user interface to reflect the change. Subsequently, it checks whether the player has run out of busts (**`remainingBusts <= 0`**). If so, and the last clicked cell is not the position of a ghost (**`ghostPosition`**), the game over method is called with a parameter indicating that the player has lost. On the other hand, if the last clicked cell matches the ghost's position, the game over method is called with a parameter indicating that the player has won.

### Probabilistic inferencing and coloring the grid:

**OnCellClicked:**

```csharp
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
```

- This code defines a method named **`OnCellClicked`**. The method is triggered when a game cell is clicked, with the cell's position represented by the parameter **`cellPosition`**. The code adjusts the position based on a centered grid and calculates the corresponding grid indices (row and column) using the cell position and grid parameters. It then logs the calculated row and column values. The adjusted position is stored in the variable **`lastClickedCell`**. If the clicked position is outside the grid boundaries, an error is logged, and the method exits. The code calculates the Euclidean distance between the clicked cell and a ghost position, determines the color for the clicked cell based on this distance, updates the cell's color, and normalizes probabilities for neighboring cells. The player's remaining credits are then decremented, and the UI is updated accordingly. If the player runs out of credits, the **`GameOver`** method is called with a parameter indicating that the player has lost the game.

**JointTableProbability:**

```csharp
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
```

- The method **`JointTableProbability`** calculates a joint probability based on two factors: the color of a cell and its distance from a ghost. The method takes two parameters, **`color`** representing the color of the cell and **`DistanceFromGhost`** representing the distance of the cell from the ghost. It then evaluates various conditions using if statements to determine the probability associated with the given color and distance. The probabilities are specified in four tables: Table 1, Table 2, Table 3, and Table 4. Each table corresponds to a specific range of distances and assigns probabilities to different colors. For example, if the cell color is "yellow" and the distance from the ghost is either 3 or 4 (according to Table 1), the probability is set to 0.5. The method returns the calculated probability based on the provided color and distance. If no matching condition is found, the default return is 0. This implementation allows for a flexible and configurable way to assign probabilities to different color-distance combinations, providing a mechanism for probabilistic reasoning in the context of a grid-based game.

**UpdateAndNormalizeProbabilities:**

```csharp
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
```

- The private method **`UpdateAndNormalizeProbabilities`** is responsible for updating and normalizing probability values in a 2D array based on the selected cell's color, position, and their distances from other cells. The method iterates through each cell in the grid, calculating the Euclidean distance between the clicked cell and the current cell. Using a joint probability function (**`JointTableProbability`**), it updates the probability of the current cell having the selected color given the distance from the clicked cell. The total probability across all cells is then computed, and the probabilities are normalized by dividing each cell's probability by the total probability. If the total probability is greater than zero, indicating a valid probability distribution, the displayed probabilities are updated for each cell. This process ensures that the probabilities maintain a consistent and normalized distribution across the grid based on the selected cell's color and its spatial relationship with other cells.

**DetermineHighestProbabilityColor:**

```csharp
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
```

- The method **`DetermineHighestProbabilityColor`** is responsible for determining the color with the highest probability for a specific cell in a grid, considering the given row, column, and distance from a ghost. The method starts by converting the double probabilities obtained from the **`JointTableProbability`** method into float variables for each color (red, yellow, green, and orange) at the given distance. It then uses **`Mathf.Max`** to find the maximum probability among these four colors. The result, **`maxProb`**, represents the highest probability among the color options. Finally, the method compares **`maxProb`** with each individual color's probability and returns the corresponding color string with the highest probability. If multiple colors share the maximum probability, the method returns the first color encountered in the conditional checks. This implementation effectively selects the color that is deemed most likely based on the highest calculated probability for the given cell's parameters.

**PlaceNoisyPrint:**

```csharp
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
```

- The **`PlaceNoisyPrint`** method randomly selects cells on a grid to place a "noisy print" represented by the color red. It uses a while loop with a limited number of attempts to prevent an infinite loop. In each iteration, random coordinates are generated, and the code checks if the selected cell is not the ghost position and is not already marked with a noisy print. If the conditions are met, the cell's color is set to red, indicating the successful placement of a noisy print. If, after the maximum number of attempts, a suitable spot is not found, a warning is logged. This ensures that the method terminates gracefully and provides feedback if it cannot find an appropriate location for the noisy print within the specified number of attempts.

## Conclusion

This game was a fun project to implement. It allowed us to have some experience with Unity, learn C#, and experiment with game frontend. The game frontend, being a crucial component of the user experience, allowed us to experiment with different design elements and user interface components. This hands-on experience with the game frontend not only improved our technical skills but also provided insights into the importance of user interaction and visual appeal in game development. At first, the project challenged us as we navigated the learning curve of Unity and C#. However, as we progressed, these challenges turned into appreciation to the amount of work game developers put into each part of the game. Beyond the technical aspects, this project also helped us practice probability. Understanding probabilities became essential, especially in the context of the game mechanics (building a probabilistic agent). A notable aspect of our learning journey was the deeper exploration of Bayes’ theorem. This fundamental concept in probability theory played a crucial role in our decision-making processes within the game. The application of Bayes’ theorem allowed us to model and interpret uncertainties, contributing to a more sophisticated and nuanced game design.

## Video demonstration on Youtube

The link to the demonstration can be found at: 

[Bust the Ghost](https://youtu.be/ZqzJHBSkjjU)


## References

[https://medium.com/mlearning-ai/what-is-the-bayes-theorem-545a2ef0b91c](https://medium.com/mlearning-ai/what-is-the-bayes-theorem-545a2ef0b91c)
