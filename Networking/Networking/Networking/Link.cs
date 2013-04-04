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
    public class Link : IUpdateable, IDrawable
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

        int data;

        public int Data
        {
            get { return data; }
            set { data = value; }
        }

        public int[] ip = new int[3];
        public Texture2D packetParticle; 
        public int Magnitude;
        public int Distance;
        
        public GraphNode endNode;

        public Vector2 startPosition;
        public Vector2 endPosition;

        public Color particleColor = Color.Azure;
        public int width = 2;

        public Link(GraphicsDevice graphics, int Mag, int Dist)
        {
            Magnitude = Mag;
            Distance = Dist;
        
            packetParticle= new Texture2D(graphics, 1, 1, false, SurfaceFormat.Color);
            packetParticle.SetData(new[]{Color.White});
        }
        public void Draw(GameTime gameTime, SpriteBatch spritebatch)
        {
            float angle = (float)Math.Atan2(endPosition.Y - startPosition.Y, endPosition.X - startPosition.X);
            float length = Vector2.Distance(startPosition, endPosition);
            spritebatch.Begin();
            spritebatch.Draw(packetParticle, startPosition, null, particleColor,
              angle, Vector2.Zero, new Vector2(length, width),
              SpriteEffects.None, 0);
            spritebatch.End();
        }

        public void Draw(GameTime gameTime){}

        public void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
