﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Networking
{
   public class Packet : IUpdateable, IDrawable, ICloneable
   {
       #region autoImplement
       public bool Enabled
       {
           get { throw new NotImplementedException(); }
       }

       public event EventHandler<EventArgs> EnabledChanged;


       public int UpdateOrder
       {
           get { throw new NotImplementedException(); }
       }

       public event EventHandler<EventArgs> UpdateOrderChanged;
       public int DrawOrder
       {
           get { throw new NotImplementedException(); }
       }

       public event EventHandler<EventArgs> DrawOrderChanged;

       public bool Visible
       {
           get { throw new NotImplementedException(); }
       }

       public event EventHandler<EventArgs> VisibleChanged;
       #endregion

       /// <summary>
       /// What the packet contains
       /// </summary>
       int data;

       Rectangle destinationRect;

       public Rectangle PositionRect
       {
           get { return destinationRect; }
           set { destinationRect = value; }
       }

       public int Data
       {
           get { return data; }
           set { data = value; }
       }

       /// <summary>
       /// Start ip
       /// </summary>
       public int[] start = new int[3];
       /// <summary>
       /// end ip
       /// </summary>
       public int[] destination = new int[3];

       /// <summary>
       /// Image that will move along the link
       /// </summary>
       public Texture2D packetParticle;
       /// <summary>
       /// position that the packet image will be rendered
       /// </summary>
       public Vector2 position;

       bool onLine;
       private Color color = Color.Orange;

       public Color Color
       {
           get { return color; }
           set { color = value; }
       }

       public bool OnLine
       {
           get { return onLine; }
           set { onLine = value; }
       }

       public Packet(int data, int[] ipDestAddress, int[] ipSrcAddress)
       {
           start = ipSrcAddress;
           destination = ipDestAddress;
           Data = data;

       }
       public Packet(int data, int[] ipDestAddress, int[] ipSrcAddress, Texture2D packet)
       {
           start = ipSrcAddress;
           destination = ipDestAddress;
           Data = data;
           packetParticle = packet;

       }
       public void Draw(GameTime gameTime, SpriteBatch spritebatch)
       {
           spritebatch.Begin();
            if(OnLine)
           spritebatch.Draw(packetParticle, position, this.color);
           spritebatch.End();

       }
       
       public void Draw(GameTime gameTime)
       {
           throw new NotImplementedException();
       }
       
       public void Update(GameTime gameTime)
       {
           PositionRect = new Rectangle((int)position.X, (int)position.Y, packetParticle.Width, packetParticle.Height);
       }

       public bool correctPlace(int[] ip)
       {

           return (destination == ip);
       }


       public object Clone()
       {
           Packet output = new Packet(this.data, this.destination, this.start);
          output.packetParticle = this.packetParticle;
           output.position = this.position;
           output.color = this.color;
          return output;
       }
       public string toString()
       {
           string output;
           output = "" + data + " Pos: " + position.ToString() + " " + OnLine.ToString()+ " ";
           return output;
       
       }
   
   }
}
