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
using Microsoft.Xna.Framework.Media;

namespace Networking
{
   public class Packet : IUpdateable, IDrawable
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
       private Color color;

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

       public void Draw(GameTime gameTime, SpriteBatch spritebatch)
       {
           spritebatch.Begin();
           spritebatch.Draw(packetParticle, position, this.color);

       }
       
       public void Draw(GameTime gameTime)
       {
           throw new NotImplementedException();
       }
       
       public void Update(GameTime gameTime)
       {
           throw new NotImplementedException();
       }

       public bool correctPlace(int[] ip)
       {

           return (destination == ip);
       }
    
   }
}
