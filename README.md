# Procedural Hextile Map Generator

With this Unity project I wanted to learn more about procedural generation and get to know the engine even more. 

## The entire map is built exclusively by code. 

I created a hexagon class to draw the mesh of a single hexagon-tile. The size and height can be changed in the inspector. Even an inner radius can be changed, so that the hexagon is hollow to be able to create the effect of a hexagon grid.
Inside the GridGenerator class, an entire map of hex-tiles is drawn. The number of tiles can also be set in the inspector.
Two Perlin Noise functions are used to assign land type to each hex-tile. One is used to build the terrain of the world and the other is used to place some groups of forest randomly across the dry land.

Two buttons are added which will allow you to build a building on the grid map. These building will automatically snap to the nearest hex-tile to the mouse position. 
Last but not least, a simple "W,A,S,D" movement is added to the camera so that the map can be explored.

