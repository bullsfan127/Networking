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
    /// <summary>
    /// The link represents a data line moving one direction.
    /// </summary>
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

       /// <summary>
       /// How busy is the line
       /// </summary>
        public int Magnitude;
        /// <summary>
        /// How long is the Link
        /// </summary>
        public int Distance;

        /// <summary>
        /// Link texture which is actually 1 by 1 texture
        /// </summary>
        Texture2D LinkTexture;
        /// <summary>
        /// Where does the link end
        /// </summary>
        public GraphNode endNode;

        /// <summary>
        /// Drawing vector for drawing the line
        /// </summary>
        public Vector2 startPosition;
        /// <summary>
        /// End vector for drawing the line
        /// </summary>
        public Vector2 endPosition;

        /// <summary>
        /// Link Color
        /// </summary>
        public Color particleColor = Color.Blue;
        /// <summary>
        /// Link Width
        /// </summary>
        public int width = 2;

        public Link(GraphicsDevice graphics, int Mag, int Dist)
        {
            Magnitude = Mag;
            Distance = Dist;

            LinkTexture = new Texture2D(graphics, 1, 1, false, SurfaceFormat.Color);
            LinkTexture.SetData(new[] { particleColor });
        }
        /// <summary>
        /// Actually draws the Link
        /// </summary>
        /// <param name="gameTime">the current gameTime</param>
        /// <param name="spritebatch">the spritebatch assumed unstarted</param>
        public void Draw(GameTime gameTime, SpriteBatch spritebatch)
        {
            
            float angle = (float)Math.Atan2(endPosition.Y - startPosition.Y, endPosition.X - startPosition.X);

            float length = Vector2.Distance(startPosition, endPosition);
            
            spritebatch.Begin();
            spritebatch.Draw(LinkTexture, startPosition, null, particleColor,
              angle, Vector2.Zero, new Vector2(length, width),
              SpriteEffects.None, 0);
            spritebatch.End();
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="gameTime"></param>
        public void Draw(GameTime gameTime){}

        public void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
