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
#endregion 
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

        List<Line> edges = new List<Line>();

        GraphicsDevice graphics;
        SpriteBatch spriteBatch;
        Color color;

        int[] ip = new int[3];

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
        public GraphNode(GraphicsDevice graphics, SpriteBatch spritebatch, int[] Ip)
        {
            this.graphics = graphics;
            this.spriteBatch = spritebatch;
            this.ip = Ip;
        
        }

        public void addLine(GraphNode endPoint)
        {
            edges.Add(new Line(this, endPoint, spriteBatch, graphics));
        
        }


        #region XNA
        public void Update(GameTime gameTime)
        {
            foreach(Line a in edges)
            {
                a.Update(gameTime);
            }

        }
        
        public void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(serverPicture, picturePosition, color);
            spriteBatch.End();

            foreach (Line a in edges)
            {
                a.Draw(gameTime);
            
            }
           
        }
        #endregion
    }
}
