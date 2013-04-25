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
    public class GraphNode : IDrawable, IUpdateable
    {
        #region XNAautoimplement

        public bool Enabled
        {
            get { throw new NotImplementedException(); }
        }

        public int DrawOrder
        {
            get { throw new NotImplementedException(); }
        }

        public event EventHandler<EventArgs> DrawOrderChanged;

        public event EventHandler<EventArgs> VisibleChanged;

        public event EventHandler<EventArgs> EnabledChanged;

        public event EventHandler<EventArgs> UpdateOrderChanged;

        public bool Visible
        {
            get { throw new NotImplementedException(); }
        }

        public int UpdateOrder
        {
            get { throw new NotImplementedException(); }
        }

        #endregion XNAautoimplement

        /// <summary>
        /// the recieved queue
        /// </summary>
        public Queue<Packet> recieved = new Queue<Packet>();

        /// <summary>
        /// outgoing packet queue
        /// </summary>
        public Queue<Packet> outgoing = new Queue<Packet>();

        public Texture2D serverPicture;
        public Rectangle picturePosition;
        SpriteFont font;
        string num;
        List<Line> edges = new List<Line>();

        public List<Line> Edges
        {
            get { return edges; }
            set { edges = value; }
        }

        GraphicsDevice graphics;
        SpriteBatch spriteBatch;
        Color color;

        int[] ip = new int[3];
        private Fields fields;

        public int[] IP
        {
            get { return ip; }
            set { ip = value; }
        }

        public Color Color
        {
            get { return color; }
            set { color = value; }
        }

        public GraphNode(Fields f, int[] Ip)
        {
            this.graphics = f.graphics;
            this.spriteBatch = f.spriteBatch;
            this.ip = Ip;
            fields = f;
        }

        public void addLine(GraphNode endPoint)
        {
            Line line = new Line(this, endPoint, fields);
            edges.Add(line);
            endPoint.edges.Add(line);
        }

        public void loadImage(Texture2D image, Rectangle position)
        {
            serverPicture = image;
            picturePosition = position;
        }

        public void addText(SpriteFont font, string num)
        {
            this.font = font;
            this.num = num;
        }

        #region XNA

        public void Update(GameTime gameTime)
        {

            foreach (Line a in edges)
            {
                if ((outgoing.Count > 0))
                    if ((!a.outgoing.transmitting))
                    {
                        a.send(outgoing.Dequeue(), this);
                    }
                if ((recieved.Count > 0))
                    if ((!a.outgoing.transmitting))
                        a.send(recieved.Dequeue(), this);


                a.Update(gameTime);
            }
        }

        public void Draw(GameTime gameTime)
        {
            foreach (Line a in edges)
            {
                a.Draw(gameTime);
            }
            spriteBatch.Begin();

            spriteBatch.Draw(serverPicture, picturePosition, Color.Black);
            if (font != null)
                spriteBatch.DrawString(font, num, new Vector2(this.picturePosition.Center.X, this.picturePosition.Center.Y), Color.White);
            spriteBatch.End();
        }

        #endregion XNA
    }
}