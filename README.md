# GamesEngine1Assignment
This project is for the Games engine 1 assignment where I will create a train traversing nature.

# Details 
Name: Daniel Simons                    
Course: DT228                           
Student-number: C17371946


# Description of the project
This assignment took a different course to what was orignially planned due to time constraints. 
This assignment presents a procedurely generated world using Perlin noise and a train that traverses it.
The railroad the mountains the lakes and the bumps are all procedurally generated. 

## Information about functionality
The train has also some functionality.
Button | Function 
-------|---------
w      | Allows the train to start moving down the track and also starts the train noise
s      | Smoke/Steam can be enabled or disabled
shift  | Boosts the train down the track increasing its speed by 300%

There is also the quit button to exit out of the program safely 
These instructions appear on the screen in the GUI box 

# Instructions to build
There is an .exe file you can run to demostrate the key functions of this simulation. 

*TrainTerrain.exe* 

# How it works
## Terrain generation 
There are 3 main components for the terrain generation; 
1.The railroad generation which generates the rail route in a straight line down the z axis.
2.The Hill or bumpy terrain generation 
3.The mountain generation and lake generation. 

  ### Railroad generation
  The railroad generation is comprised of two classes, one class is the RailMesh which generates the terrain or the type of landscape for the other class
  the RailRouteGenerator which generates a rail route through the valley. 
  The railMesh generates a mesh using a defined amount of quadsPerTile which in other words means how many quads there should be for each tile. It sets the the y values of each   of the vertices to 0 so that it is a flat surface for the train to travel on. This mesh is then combined with the railroad texture I created and then used for the               RailRouteGenerator. 
  ```C#
  for (int row = 0; row < quadsPerTile; row++)
        {
            for (int col = 0; col < quadsPerTile; col++)
            {
                Vector3 bl = bottomLeft + new Vector3(col, 0, row);
                Vector3 tl = bottomLeft + new Vector3(col, 0, row + 1);
                Vector3 tr = bottomLeft + new Vector3(col + 1, 0, row + 1);
                Vector3 br = bottomLeft + new Vector3(col + 1, 0, row);
           
```
  The railRouteGenerator takes this mesh and creates a railroad infront of the train always on the z axis. It does this by keeping the x axis the same and the producing more       railroad infront of the train on the z axis.
```C#
  for (int z = -halfTile; z < halfTile; z++)
  {
      // The position of the new tile
      Vector3 pos = new Vector3((playerX),
          0,
          (z * quadsPerTile + playerZ));

      string tilename = "Tile_" + ((int)(pos.x)).ToString() + "_" + ((int)(pos.z)).ToString();
      if (!tiles.ContainsKey(tilename))
      {
          newTiles.Add(pos);
      }
      else
      {
          (tiles[tilename] as Tile).creationTime = updateTime;
      }
  }
```
  
  ### Mountain + hill generation
  The mountain and hilly terrains use the same tile, I had quite a few issues with the heights of these terrains set to over 150 x value higher than the train for some reason so   I included these terrains as the same tile but with different functions called for the different terrains
  ```C#
   public int terrainOption;
  ```
  This option determines which terrain will be generated using this conditional statement
```C#
 if(terrainOption == 0)
                {
                    bl = bottomLeft + new Vector3(col, generateBmps(transform.position.x + col, transform.position.z + row), row);
                    tl = bottomLeft + new Vector3(col, generateBmps(transform.position.x + col, transform.position.z + row + 1), row + 1);
                    tr = bottomLeft + new Vector3(col + 1, generateBmps(transform.position.x + col + 1, transform.position.z + row + 1), row + 1);
                    br = bottomLeft + new Vector3(col + 1, generateBmps(transform.position.x + col + 1, transform.position.z + row), row);
                } else if(terrainOption == 1)
                {
                    bl = bottomLeft + new Vector3(col, generateMtn(transform.position.x + col, transform.position.z + row), row);
                    tl = bottomLeft + new Vector3(col, generateMtn(transform.position.x + col, transform.position.z + row + 1), row + 1);
                    tr = bottomLeft + new Vector3(col + 1, generateMtn(transform.position.x + col + 1, transform.position.z + row + 1), row + 1);
                    br = bottomLeft + new Vector3(col + 1, generateMtn(transform.position.x + col + 1, transform.position.z + row), row);
                } else 
                {
                    bl = bottomLeft + new Vector3(col, 0, row);
                    tl = bottomLeft + new Vector3(col, 0, row + 1);
                    tr = bottomLeft + new Vector3(col + 1, 0, row + 1);
                    br = bottomLeft + new Vector3(col + 1, 0, row);
                }
```
  This generation is very similar to the RailMesh only there was the issue of the height being astronomically higher then the rest of the terrain which is why it has its own       tile class. There is two methods as seen in the abstract of code used for generating the terrains.Both these methods use PerlinNoise to give a randomised terrain landscape. 
  The first method generates hilly terrain or bumpy terrain.
  ```C#
  public static float generateBmps(float x, float y)
    {
        float flatness = 0.2f;
        float noise = Mathf.PerlinNoise(10000 + x , 10000 + y );
        if (noise > 0.5f + flatness)
        {
            noise = noise - flatness;
        }
        else if (noise < 0.5f - flatness)
        {
            noise = noise + flatness;
        }
        else
        {
            noise = 0.5f;
        }
        
        return (noise * 300) + (Mathf.PerlinNoise(1000 + x / 5, 100 + y / 5) * 2);
    }
  ```
 
 The second method generates the moutnainous and valley terrain the values that the perlin noise is divided by was changed to 500 to give the landscape a more realistic feel.
  ```C#
  public static float generateMtn(float x, float y)
    {
        float flatness = 0.2f;
        float noise = Mathf.PerlinNoise(10000 + x / 500, 10000 + y / 350);
        if (noise > 0.5f + flatness)
        {
            noise = noise - flatness;
        }
        else if (noise < 0.5f - flatness)
        {
            noise = noise + flatness;
        }
        else
        {
            noise = 0.5f;
        }
        
        return (noise * 300) + (Mathf.PerlinNoise(1000 + x / 5, 100 + y / 5) * 2);
    }
  ```
 These are how the tiles are created, these tiles are then used by the MtnGenerator and the terrain generator to plot out the landscape.
 Two generators were used as a sytlistic approach to the landscape was chosen as so mountains would not appear in front of the train.
 Hilly terrain is placed nearest the train while moutnainous terrain is placed further away. 
 Hilly terrain code
   ```C#
 for (int z = -halfTile; z < halfTile; z++)
      {
          // The position of the new tile
          Vector3 pos1 = new Vector3((playerX + 120),
              -151.5f,
              (z * quadsPerTile + playerZ));
          Vector3 pos2 = new Vector3((playerX + 20),
              -151.5f,
              (z * quadsPerTile + playerZ));
  ```
Mountainous terrain code
   ```C#
for (int z = -halfTile; z < halfTile; z++)
      {
          // The position of the new tile
          Vector3 pos1 = new Vector3((x * quadsPerTile + playerX),
              -152,
              (z * quadsPerTile + playerZ));
          Vector3 pos2 = new Vector3((-x * quadsPerTile - playerX),
              -152,
              (z * quadsPerTile + playerZ));
  ```
## Train mechanics 
There is a few train mechanisms such as boosting the train forward with increased speed, the sound clips the train produces and the smoke it also produces.
  ### Train controller
  The speed of the train and how the train starts is from user input on the keyboard. This is handled in the train controller script.
  When the w key is pressed the train starts moving and produces a sound of a train on the traintracks. 
   ```C#
 if(Input.GetKeyDown(KeyCode.W))
        {
            playAudio();
            startTrain = true;  
            trainTrack.loop = true;
        }
  
  if(startTrain)
        {
            Vector3 targetPos = transform.position;
            targetPos += (Vector3.forward * speed * Time.deltaTime);
            transform.position = targetPos;
        } 
  ```
  If the shift key is pressed the train initializes a boost. This starts a CoRoutine which stats and stops smoke coming from the chimney twice to simulate the sound it is making.
  ```C#
     if(Input.GetKeyDown(KeyCode.LeftShift))
      {
          StartCoroutine(SmokeControl());
      }
    public IEnumerator SmokeControl()
    {
        speed += 20;
        if(!trainChime.isPlaying)
        {
            trainChime.Play();
        }
        SmokeRelease();
        yield return new WaitForSeconds(1f);
        SmokeStop();
        yield return new WaitForSeconds(1f);
        SmokeRelease();
        yield return new WaitForSeconds(1f);
        SmokeStop();
        speed -= 20;
    }

    void SmokeRelease()
    {
        var emission = chimney.emission;
        emission.enabled = true;
    }

    void SmokeStop()
    {
        var emission = chimney.emission;
        emission.enabled = false;
    }
  ``` 
  You can also control the smoke manually using the s key which enables or disables the smoke
   ```C#
  if(Input.GetKeyDown(KeyCode.S))
    {
        var emission = chimney.emission;
        if(emission.enabled==true)
        {
            SmokeStop();
        }
        else
        {
            SmokeRelease();
        }
    } 
  ```
  ### Game manager
  The game manager is a class which allows information to be displayed the screen it also provides the user with a quit button. 
 ```C#
public void OnGUI()
    {
        GUI.color = Color.black;
        GUI.Label(new Rect(0, 0, Screen.width, Screen.height), "" + message);
        if (Event.current.type == EventType.Repaint)
        {
            message.Length = 0;
        }

        bool quitGame = GUI.Button(new Rect(Screen.width - 100, Screen.height - 100, 100, 20), "Quit Game");
      
        if(quitGame) {
      
        Application.Quit(); 
      
        }
    }

    public static void Log(string text)
    {
        message.Append(text + "\n");
    }
  ```
  ### Camera script
  The camera script is a simple script which the camera follows the train as it goes through the terrain. 
  ```C#
public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.transform.position, Time.deltaTime);
        transform.LookAt(target.parent);
    }
  ``` 

# Parts from course + Innovative parts
The terrain generation comes mainly through this course, I found this quite useful, I didnt have quite enough time to generate trees in the environment but that was something I was looking into towards the end. 
The smoke generation and the model for the train I created myself, I created the script for the train controller myself also which allows different functionality to occur. 
I altered the shader file made in this course to better suit the environment and I couldn't find a way to create different textures *not colours* for different terrains using a shader file which I spent a little too much time doing. 

# Notewothy 
I struggled with a lot of the terrain generation, which I found quite difficult, but once I got it establish it was quite cool to explore and mess around with the different values. 
I am quite proud of the smoke generation from the chimney of the train and how I incorporated trains sounds to go with it. 
Overall I wish I had more time but fourth year really doesnt give you much :( .


## Embedded video of assignment 
[![YouTube](http://img.youtube.com/vi/t2vKYxqbJf0/0.jpg)](https://www.youtube.com/watch?v=t2vKYxqbJf0)


PROPOSAL SUBMISSION
-------------------
# Introduction
This proposal will detail my idea for the games engine 1 assignment. My proposal is to create a procedurally generated railroad in which the setting and railroad alter and transform with synchronized music. The railroad generation and the world that is generated for the train to travel through will be dependent on the different characteristics found in music. Some of these characteristics include; rhythm, the element of time within music and the speed of the beat, dynamics of music such as the loudness or quietness of music (Crescendo and descrescendo), melody,  which is the presentation of pitch or in other words the highness or the lowness of a note or sound and finally harmony the combination of pitches to create chords. 

Each of these aspects will be implemented within this railroad music visualization and I will go through each proposed idea. Firstly I will go over the proposed models for this application.

# Models:
Models:
Steam train:

![Steam Train](https://i.pinimg.com/originals/62/27/40/622740f31fe66bcd449b254cfc43d48d.jpg)

The steam train will be traversing the environment created by the music in this procedurally generated world.  

Railroad track:

![RailRoad](https://exit.al/en/wp-content/uploads/sites/3/2020/09/TUDD_Snapshot_Jordanian__Railway_Network-_ENPI.jpg)

This will be how the steam train will traverse through the generated world. This railroad will change with the characteristics of the music playing.

Tree:

![RailRoad](https://free3d.com/imgd/s69/5b50eb8826be8bdf518b4567/5781-low-poly-tree.png)

These will populate the terrain along and be influenced by the music also.


# Musical Characteristics
## Rhythm 
Rhythm is the pattern in music with regard to time, rhythm is normally compared with the beat of the music. I hope to use rhythm to control the speed of the train, the beats per minute (BPM) of music would be a unique attribute to use for speed and I think it would work the best for the control of the speed of the train. Another use for the BPM of the music would be to control the movement of the trees. I feel to have the trees bounce or make some irregular movement would be a nice visual representation of the beat in the music. 

## Dynamics 
Dynamics refers to the variation of loudness in music, so how soft or how loud music can be in certain parts of a song. I will use this to change the elevation of the railroad, as the music starts to get louder the railroad will start to elevate slightly and as the music gets quieter the railroad will slope down slightly giving this 3d effect to an audio visualizer. 

![VolumeUp](Images/increaseVolume.PNG)
![VolumeDown](Images/volumeDecrease.PNG)

## Melody 
Melody is the combination of pitch and rhythm, the pitch is the highness and the lowness of a note. I hope to use melody to guide the direction of the train. Higher notes on the music scale will make the railroad slightly turn left while lower notes on the scale will make the railroad slightly turn right.

![melodyTrack](Images/melody.PNG)

# Design 
This is a simple class diagram, I envision 5 main classes in the development of this program. The first class is the world generator, this will handle the creation of the railroad and world for the train to traverse. The audio generator will process the music selected to give calculations to how the world should be created with reference to the music selected. These calculations will then be used to layout the terrain and railroad for the train. There will also be a method to initialize the steam train.

![melodyTrack](Images/UML.PNG)

# Research and Inspirations
My main inspiration for this project is this video which captures a train going through the snowy mountains of Norway.
[![YouTube](http://img.youtube.com/vi/ZHgXfhiDIIM/0.jpg)](https://www.youtube.com/watch?v=ZHgXfhiDIIM&ab_channel=RailCowGirl)


Another inspiration is an audio visualizer I found that traversed through anatural setting full of beautiful colours.
[![YouTube](http://img.youtube.com/vi/pr6uq6F8L58/0.jpg)](https://www.youtube.com/watch?v=pr6uq6F8L58&ab_channel=TrapNation)


I had a little research into how music would operate and how I could use it with world generation, this document helped me find out the different characteristics of music 

https://wmich.edu/mus-gened/mus150/Ch1-elements.pdf

A tutorial I looked at for this project was in this youtube video I found it quite helpful.
[![YouTube](http://img.youtube.com/vi/PzVbaaxgPco/0.jpg)](https://www.youtube.com/watch?v=PzVbaaxgPco&ab_channel=RenaissanceCoders)


