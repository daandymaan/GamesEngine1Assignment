# GamesEngine1Assignment
This project is for the Games engine 1 assignment where I will create a train traversing nature.

# Details 
Name: Daniel Simons                    
Course: DT228                           
Student-number: C17371946


# Assignment 
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

### Terrain generation 
#### Railroad generation
#### Mountain + hill generation

## Parts from course + Innovative parts
The terrain generation comes mainly through this course, I found this quite useful, I didnt have quite enough time to generate trees in the environment but that was something I was looking into towards the end. 
The smoke generation and the model for the train I created myself, I created the script for the train controller myself also which allows different functionality to occur. 
I altered the shader file made in this course to better suit the environment and I couldn't find a way to create different textures *not colours* for different terrains using a shader file which I spent a little too much time doing. 

## Notewothy 
I struggled with a lot of the terrain generation, which I found quite difficult, but once I got it establish it was quite cool to explore and mess around with the different values. 
I am quite proud of the smoke generation from the chimney of the train and how I incorporated trains sounds to go with it. 
Overall I wish I had more time but fourth year really doesnt give you much :( .


## Instructions to build
There is an .exe file you can run to demostrate the key functions of this simulation. 
*TrainTerrain.exe* 

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


