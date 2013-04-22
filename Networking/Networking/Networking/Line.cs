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
    public class Line : IDrawable, IUpdateable
    {
        #region notused

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

        #endregion notused

        /// <summary>
        /// The outgoing link
        /// </summary>
        public Link outgoing;

        /// <summary>
        /// The ingoing link
        /// </summary>
        public Link ingoing;

        private SpriteBatch spriteBatch;

        public Line(GraphNode startNode, GraphNode endNode, SpriteBatch batch, GraphicsDevice graphics)
        {
            spriteBatch = batch;

            double distance = Math.Sqrt(Math.Pow((endNode.picturePosition.Center.X -
                                                startNode.picturePosition.Center.X), 2) +
                                                Math.Pow((endNode.picturePosition.Center.Y -
                                                startNode.picturePosition.Center.Y), 2));

            outgoing = new Link(graphics, 0, (int)distance);
            outgoing.endNode = endNode;

            outgoing.endPosition = new Vector2(endNode.picturePosition.Center.X + (endNode.picturePosition.Width / 2), endNode.picturePosition.Center.Y);
            outgoing.startPosition = new Vector2(startNode.picturePosition.Center.X + (endNode.picturePosition.Width / 2), startNode.picturePosition.Center.Y);

            ingoing = new Link(graphics, 0, (int)distance);
            ingoing.endNode = startNode;

            ingoing.endPosition = new Vector2(endNode.picturePosition.Center.X - (endNode.picturePosition.Width / 2), endNode.picturePosition.Center.Y);
            ingoing.startPosition = new Vector2(startNode.picturePosition.Center.X - (endNode.picturePosition.Width / 2), startNode.picturePosition.Center.Y);
        }

        public void Update(GameTime gameTime)
        {
            //update the lines
            outgoing.Update(gameTime);
            ingoing.Update(gameTime);

            //check to see if the packet has arrived.
            if (ingoing.finished == true)
            {
                ingoing.endNode.recieved.Enqueue(ingoing.Intransit);
                ingoing.Intransit = null;
                ingoing.finished = false;
                ingoing.transmitting = false;
            }
        }

        public void Draw(GameTime gameTime)
        {
            outgoing.Draw(gameTime, spriteBatch);
            ingoing.Draw(gameTime, spriteBatch);
        }

        public void send(Packet p)
        {
            outgoing.transmit(p);
        }
    }
}