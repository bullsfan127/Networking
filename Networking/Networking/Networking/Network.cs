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
   public class Network : IUpdateable, IDrawable
    {
       List<GraphNode> GraphList = new List<GraphNode>();
       XNAFields xnaStuff;

       public int Count
       {
           get { return GraphList.Count; }
           
       }
       public Network(XNAFields x)
       {
           xnaStuff = x;
       
       
       }
       public void addEdge(GraphNode Neighbor1, GraphNode Neighbor2)
       {
            Line newLink = new Line(findNode(Neighbor1), findNode(Neighbor2), xnaStuff.spriteBatch, xnaStuff.graphics);
               findNode(Neighbor1).Edges.Add(newLink);
               findNode(Neighbor2).Edges.Add(newLink);
          
       }
       public void addNode(GraphNode Neighbor, GraphNode node)
       {    

            if (Neighbor != null)
            {
                GraphList.Add(node);
             Line newLink = new Line(findNode(Neighbor), node, xnaStuff.spriteBatch, xnaStuff.graphics);
           findNode(Neighbor).Edges.Add(newLink);
           findNode(node).Edges.Add(newLink);

            }
           else if (Count == 0)
           {
               GraphList.Add(node);
           }
       
       }

       public GraphNode findNode(GraphNode Neighbor)
       {
           return GraphList.Find(x => x.IP == Neighbor.IP);
          
       }


       public bool Enabled
       {
           get { throw new NotImplementedException(); }
       }

       public event EventHandler<EventArgs> EnabledChanged;

       public void Update(GameTime gameTime)
       {
           foreach (GraphNode a in GraphList)
           {
               a.Update(gameTime);
           }
       }

       public int UpdateOrder
       {
           get { throw new NotImplementedException(); }
       }

       public event EventHandler<EventArgs> UpdateOrderChanged;

       public void Draw(GameTime gameTime)
       {
           foreach (GraphNode a in GraphList)
           {

               a.Draw(gameTime);
           }
       }

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
    }
}
