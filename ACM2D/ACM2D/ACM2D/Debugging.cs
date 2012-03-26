using System;
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
    class Debugging
    {

        Viewport viewport;
        List<Player> players;
        String information;
        List<String> terminalDataShown;
        
        
        
        public Debugging(List<Player> players, Viewport viewport){
            this.players = players;
            this.viewport = viewport;
            terminalDataShown = new List<String>();

        }

        public void update(GameTime gametime, KeyboardState current, KeyboardState previous){

            if (current.IsKeyDown(Keys.Enter) && previous.IsKeyUp(Keys.Enter))
            {
                evaluate(information);
                information = "";
                return;
            }


            if (!previous.Equals(current))
            {
                for (int i = 0; i < previous.GetPressedKeys().Length; i++)
                {
                    bool notPressed = false;
                    for (int j = 0; j < current.GetPressedKeys().Length; j++)
                    {
                        if (current.GetPressedKeys()[j].Equals(previous.GetPressedKeys()[i]))
                            notPressed = true;
                    }


                    if (!notPressed)
                    {
                        if (previous.GetPressedKeys()[i].Equals(Keys.Space))
                            information += " ";
                        else if (previous.GetPressedKeys()[i].Equals(Keys.RightShift) ||
                            previous.GetPressedKeys()[i].Equals(Keys.LeftShift) ||
                            previous.GetPressedKeys()[i].Equals(Keys.RightAlt) ||
                            previous.GetPressedKeys()[i].Equals(Keys.LeftAlt) ||
                            previous.GetPressedKeys()[i].Equals(Keys.RightControl) ||
                            previous.GetPressedKeys()[i].Equals(Keys.LeftControl))
                            information += "";
                        else if (previous.GetPressedKeys()[i].Equals(Keys.OemComma))
                            information += ",";
                        else if (previous.GetPressedKeys()[i].Equals(Keys.OemPeriod))
                            information += ".";
                        else if (previous.GetPressedKeys()[i].Equals(Keys.OemQuestion))
                            information += "?";
                        else if (previous.GetPressedKeys()[i].Equals(Keys.OemMinus))
                            information += "-";
                        else if (previous.GetPressedKeys()[i].Equals(Keys.Back))
                        {
                            if (information.Length > 0)
                                information = information.Substring(0, information.Length - 1);
                        }
                        else if (previous.GetPressedKeys()[i].CompareTo(Keys.A) >= 0 && previous.GetPressedKeys()[i].CompareTo(Keys.Z) <= 0)
                        {
                            information += previous.GetPressedKeys()[i];
                        }
                        else if ((previous.GetPressedKeys()[i].CompareTo(Keys.D9) <= 0 && previous.GetPressedKeys()[i].CompareTo(Keys.D0) >= 0))
                        {
                            String parseD = (previous.GetPressedKeys()[i] + "");
                            information += parseD.Substring(1, parseD.Length - 1); ;
                        }
                    }
                }

            }
        }

        public void evaluate(String information)
        {
            if (terminalDataShown.Count > 14)
            {
                terminalDataShown.RemoveAt(0);
                terminalDataShown.RemoveAt(0);
            }
           
            String printData = "";
            String[] parsedInfo = information.Split(' ');

            switch(parsedInfo[0]){
                case "-HELP": printData = "Player <playerNumber> <DataInfo> <new change>"; break;
                case "PLAYER":
                    if (isNum(parsedInfo[1]) && int.Parse(parsedInfo[1])<2 && int.Parse(parsedInfo[1])>-1)
                    {
                        players[int.Parse(parsedInfo[1])].current =
                               (players[int.Parse(parsedInfo[1])].current + 1) % 2;
                        printData = "Player " +parsedInfo[1] + " weapon change";
                    }
                    break;

            }
            if(printData.Equals(""))
                printData = "\""+ information + "\" is not a command. Type \'-HELP\' for help";
            terminalDataShown.Add("Terminal -> " + information);
            terminalDataShown.Add(printData);
          
            
                
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont font){

            for (int i = 0; i < terminalDataShown.Count; i++)
            {
                spriteBatch.DrawString(font, terminalDataShown[terminalDataShown.Count -1- i], new Vector2(10, viewport.Height - 30 - (15 * i)), Color.White); ;
            }

            spriteBatch.DrawString(font, "Terminal -> " + information, new Vector2(10, viewport.Height - 15), Color.White);

        }

        public bool isNum(String num){
            char [] numArray = num.ToCharArray();
            for (int i = 0; i < numArray.Length; i++)
                if (numArray[i] < '0' || numArray[i] > '9')
                    return false;
            return true;
        }
    }
}
