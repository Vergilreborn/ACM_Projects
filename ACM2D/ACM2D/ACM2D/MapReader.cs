using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Media;

namespace ACM2D
{
    class MapReader
    {



        //String path
        String path = "";

        //Have storage for the Tiles to be filled
        public List<Doors> doors;
     

        //Map Size Variable (will be used for camera)
        int xMapSize = 0;
        int yMapSize = 0;

        //This will be used to read the file
        StreamReader reader;

        //String level
        public int currentLvl;

        //The tiles texture pack
        Texture2D mapTiles;

        //Debugging box
        bool normal = true;

        //A constuctor that loads the textures
        public MapReader(Texture2D mapTiles)
        {
            this.mapTiles = mapTiles;
        }
        //Initialize all the lists so we can add data to them
        //the data will be in forms of tiles and sorted by collision
        public void init()
        {
            this.doors = new List<Doors>();
       
        }

        //setting the debug on and off
        public void debugOnOff(int offOn){
            if (offOn == 0)
                normal = true;
            else
                normal = false;
          
        }

        //This returns the status of the debugging, testing
        //if it is on or off. 
        public bool getDebugStatus(){
            return normal;
        }

        //This will construct the map according to the given
        //path of the file name
        public void buildMap(String path, int lvl)
        {
            //connect the streamreader to the file
            currentLvl = lvl;
            this.path = path;
            reader = new StreamReader(path + "" + currentLvl + ".cubes");

            //Grab the line and make sure its for the "Map Tiles"
            String line = reader.ReadLine();

            if (line.Equals("[MapTiles]"))
            {
                //get the map sizes
                setSize(reader.ReadLine());
                fillTileLists();
            }
        }

        //Load the new map
        public void loadNext(int level)
        {
            currentLvl = level;
            reader = new StreamReader(path + "" + currentLvl + ".cubes");

            init();
            //Grab the line and make sure its for the "Map Tiles"
            String line = reader.ReadLine();

            if (line.Equals("[MapTiles]"))
            {
                //get the map sizes
                setSize(reader.ReadLine());
                fillTileLists();
            }

        }


        //Reset the tiles


        //sets the map sizes according to the given file
        public void setSize(String sizeLine)
        {

            //Divide the info into an array and parse values
            String[] data = new String[2];
            data = sizeLine.Split(',');
            xMapSize = int.Parse(data[0]);
            yMapSize = int.Parse(data[1]);
        }

        //Fills the Lists with correct data
        public void fillTileLists()
        {
            //Checks to see if we are not at the end of the file
            while (reader.Peek() != -1)
            {

                //Grab the line we peeked at
                String line = reader.ReadLine();
                //create array and store the data
                String[] data = new String[5];
                data = line.Split(',');
                //use the data according to the collision type and sort
                switch (line.ToCharArray()[line.Length - 1])
                {
                    case 'r': doors.Add(new Doors(data, Color.Red)); break;
                    case 'b': doors.Add(new Doors(data, Color.Blue)); break;
                    case 'y': doors.Add(new Doors(data, Color.Yellow)); break;
                    case 'g': doors.Add(new Doors(data, Color.Green)); break;
                    case 'p': doors.Add(new Doors(data, Color.Purple)); break;
                    case 'w': doors.Add(new Doors(data, Color.Cyan)); break;
                    case 'n': doors.Add(new Doors(data, Color.White)); break;
                    
                }

            }
            //closes the file when we are finished
            reader.Close();
        }

        //This will draw the tiles on the screen. Collision detection will
        //be done in the enemies and the players own .cs files
        public void Draw(SpriteBatch spriteBatch)
        {
            if (!normal)
            {
                foreach (Doors n in doors)
                {
                    spriteBatch.Draw(mapTiles, n.destRect, n.sourceRect, n.getColor());
                }
           
            }
            else
            {
                foreach (Doors n in doors)
                {
                    spriteBatch.Draw(mapTiles, n.destRect, n.sourceRect, Color.White);
                }

            }

        }
    }
}
