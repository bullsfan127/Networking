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
        private Vector2 _startPosition;

        public Vector2 startPosition
        {
            get { return _startPosition; }
            set {
                PositionChanged = true;
                _startPosition = value; 
                }
        }
        /// <summary>
        /// End vector for drawing the line
        /// </summary>
        private Vector2 _endPosition;

        public Vector2 endPosition
        {
            get { return _endPosition; }
            set {
                PositionChanged = true;
                _endPosition = value; 
                }
        }

        /// <summary>
        /// Link Color
        /// </summary>
        public Color particleColor = Color.Blue;
        /// <summary>
        /// Link Width
        /// </summary>
        public int width = 2;

        Packet intransit;

        public bool PositionChanged = false;
        
        Vector2 pixelsPerMove;
        Vector2 direction;

        
        public Packet Intransit
        {
            get { return intransit; }
            set { intransit = value; }
        }
        public bool finished;
        public bool transmitting;
        float packetSpeed;
        public Link(Fields f, int Mag, int Dist)
        {
            Magnitude = Mag;
            Distance = Dist;
            
            LinkTexture = new Texture2D(f.graphics, 1, 1, false, SurfaceFormat.Color);
            LinkTexture.SetData(new[] { particleColor });
            direction = startPosition - endPosition;
            direction.Normalize();
            pixelsPerMove.X = MathHelper.Distance(startPosition.X, endPosition.X);
            pixelsPerMove.Y = MathHelper.Distance(startPosition.Y, endPosition.Y);

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


            if (intransit != null)
                intransit.Draw(gameTime, spritebatch);
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="gameTime"></param>
        public void Draw(GameTime gameTime) { }

        public void Update(GameTime gameTime)
        {
           
            if (PositionChanged)
            {
                direction = endPosition - startPosition;
                direction.Normalize();
                pixelsPerMove.X = MathHelper.Distance(startPosition.X, endPosition.X);
                pixelsPerMove.Y = MathHelper.Distance(startPosition.Y, endPosition.Y);
                packetSpeed = .075f;
                PositionChanged = false;
            }



            if (intransit != null)
            { 
                intransit.Update(gameTime);
                if ((!intransit.PositionRect.Intersects(endNode.picturePosition)) && !finished)
                {

                    intransit.position += (direction *  (float)gameTime.ElapsedGameTime.TotalMilliseconds * packetSpeed);



                }
                else
                {
                    finished = true;
                    intransit.position = endPosition;
                }

            }
        }

        public bool transmit(Packet packet)
        {
            if ((packet != null) && !transmitting)
            {
                intransit = packet;
                packet.position = startPosition;
                return true;
            }
            else
                return false;
        }
    }
}
