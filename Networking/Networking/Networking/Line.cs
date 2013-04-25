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

        private GraphNode NeighborOne;
        private GraphNode NeighborTwo;

        public Line(GraphNode Neighbor1, GraphNode Neighbor2, SpriteBatch batch, GraphicsDevice graphics)
        {
            NeighborOne = Neighbor1;
            NeighborTwo = Neighbor2;

            spriteBatch = batch;

            double distance = Math.Sqrt(Math.Pow((Neighbor2.picturePosition.Center.X -
                                                Neighbor1.picturePosition.Center.X), 2) +
                                                Math.Pow((Neighbor2.picturePosition.Center.Y -
                                                Neighbor1.picturePosition.Center.Y), 2));

            outgoing = new Link(graphics, 0, (int)distance);
            outgoing.endNode = Neighbor2;
            ingoing = new Link(graphics, 0, (int)distance);
            ingoing.endNode = Neighbor1;

            if (Neighbor2.picturePosition.X > Neighbor1.picturePosition.X && Neighbor2.picturePosition.Y < Neighbor1.picturePosition.Y)
            {
                outgoing.endPosition = new Vector2(Neighbor2.picturePosition.Center.X - (Neighbor2.picturePosition.Width / 2), Neighbor2.picturePosition.Center.Y);
                outgoing.startPosition = new Vector2(Neighbor1.picturePosition.Center.X, Neighbor1.picturePosition.Center.Y - (Neighbor1.picturePosition.Width / 2));

                ingoing.endPosition = new Vector2(Neighbor2.picturePosition.Center.X, Neighbor2.picturePosition.Center.Y + (Neighbor2.picturePosition.Width / 2));
                ingoing.startPosition = new Vector2(Neighbor1.picturePosition.Center.X + (Neighbor1.picturePosition.Width / 2), Neighbor1.picturePosition.Center.Y);
            }
            else if (Neighbor2.picturePosition.X < Neighbor1.picturePosition.X && Neighbor2.picturePosition.Y < Neighbor1.picturePosition.Y)
            {
                outgoing.endPosition = new Vector2(Neighbor2.picturePosition.Center.X, Neighbor2.picturePosition.Center.Y + (Neighbor2.picturePosition.Width / 2));
                outgoing.startPosition = new Vector2(Neighbor1.picturePosition.Center.X - (Neighbor1.picturePosition.Width / 2), Neighbor1.picturePosition.Center.Y);

                ingoing.endPosition = new Vector2(Neighbor2.picturePosition.Center.X, Neighbor2.picturePosition.Center.Y - (Neighbor2.picturePosition.Width / 2));
                ingoing.startPosition = new Vector2(Neighbor1.picturePosition.Center.X + (Neighbor1.picturePosition.Width / 2), Neighbor1.picturePosition.Center.Y);
            }

            if (Neighbor2.picturePosition.X > Neighbor1.picturePosition.X && Neighbor2.picturePosition.Y > Neighbor1.picturePosition.Y)
            {
                outgoing.endPosition = new Vector2(Neighbor2.picturePosition.Center.X, Neighbor2.picturePosition.Center.Y - (Neighbor2.picturePosition.Width / 2));
                outgoing.startPosition = new Vector2(Neighbor1.picturePosition.Center.X + (Neighbor1.picturePosition.Width / 2), Neighbor1.picturePosition.Center.Y);

                ingoing.endPosition = new Vector2(Neighbor2.picturePosition.Center.X - (Neighbor2.picturePosition.Width / 2), Neighbor2.picturePosition.Center.Y);
                ingoing.startPosition = new Vector2(Neighbor1.picturePosition.Center.X, Neighbor1.picturePosition.Center.Y + (Neighbor1.picturePosition.Width / 2));
            }

            if (Neighbor2.picturePosition.X < Neighbor1.picturePosition.X && Neighbor2.picturePosition.Y > Neighbor1.picturePosition.Y)
            {
                outgoing.endPosition = new Vector2(Neighbor2.picturePosition.Center.X, Neighbor2.picturePosition.Center.Y - (Neighbor2.picturePosition.Width / 2));
                outgoing.startPosition = new Vector2(Neighbor1.picturePosition.Center.X - (Neighbor1.picturePosition.Width / 2), Neighbor1.picturePosition.Center.Y);

                ingoing.endPosition = new Vector2(Neighbor2.picturePosition.Center.X + (Neighbor2.picturePosition.Width / 2), Neighbor2.picturePosition.Center.Y);
                ingoing.startPosition = new Vector2(Neighbor1.picturePosition.Center.X, Neighbor1.picturePosition.Center.Y + (Neighbor1.picturePosition.Width / 2));
            }
            if (Neighbor2.picturePosition.X < Neighbor1.picturePosition.X && Neighbor2.picturePosition.Y < Neighbor1.picturePosition.Y)
            {
                outgoing.endPosition = new Vector2(Neighbor2.picturePosition.Center.X, Neighbor2.picturePosition.Center.Y + (Neighbor2.picturePosition.Width / 2));
                outgoing.startPosition = new Vector2(Neighbor1.picturePosition.Center.X - (Neighbor1.picturePosition.Width / 2), Neighbor1.picturePosition.Center.Y);

                ingoing.endPosition = new Vector2(Neighbor2.picturePosition.Center.X + (Neighbor2.picturePosition.Width / 2), Neighbor2.picturePosition.Center.Y);
                ingoing.startPosition = new Vector2(Neighbor1.picturePosition.Center.X, Neighbor1.picturePosition.Center.Y - (Neighbor1.picturePosition.Width / 2));
            }
            //outgoing.endPosition = new Vector2(endNode.picturePosition.Center.X, endNode.picturePosition.Center.Y);
            //outgoing.startPosition = new Vector2(startNode.picturePosition.Center.Y, startNode.picturePosition.Center.X);
        }

        public void Update(GameTime gameTime)
        {
            //update the lines
            outgoing.Update(gameTime);
            ingoing.Update(gameTime);

            //check to see if the packet has arrived.
            if (ingoing.Intransit != null)
            if (ingoing.finished == true)
            {
                ingoing.endNode.recieved.Enqueue((Packet)ingoing.Intransit.Clone());
                ingoing.Intransit.OnLine = false;
                ingoing.Intransit = null;
                ingoing.finished = false;
                ingoing.transmitting = false;
                
            }
            if(outgoing.Intransit != null)
            if (outgoing.finished == true)
            {
                outgoing.endNode.recieved.Enqueue((Packet)outgoing.Intransit.Clone());
                outgoing.Intransit.OnLine = false;
                outgoing.Intransit = null;
                outgoing.finished = false;
                outgoing.transmitting = false;

            }
           
                
        }

        public void Draw(GameTime gameTime)
        {
            outgoing.Draw(gameTime, spriteBatch);
            ingoing.Draw(gameTime, spriteBatch);
        }

        public void send(Packet p, GraphNode startNode)
        {
            if (startNode.Equals(NeighborOne))
            { 
                p.OnLine = true;
                outgoing.transmit(p);
                outgoing.finished = false;
               
            }
            else
            { 
                p.OnLine = true;
                ingoing.transmit(p);
                ingoing.finished = false;
            }
        }

        internal void setMagnitude(int Magnitude)
        {
            ingoing.Magnitude = Magnitude;
            outgoing.Magnitude = Magnitude;
        }

        internal void setDistance(int Distance)
        {
            ingoing.Distance = Distance;
            outgoing.Distance = Distance;
        }
    }
}