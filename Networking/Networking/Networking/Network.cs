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
using GUI.Controls;
namespace Networking
{
   public class Network : IUpdateable, IDrawable
    {
       List<GraphNode> GraphList = new List<GraphNode>();
       Fields xnaStuff;
       SpriteBatch spriteBatch;
       GraphNode node;
       Label label;
       public int Count
       {
           get { return GraphList.Count; }
           
       }
       public Network(Fields x)
       {
           xnaStuff = x;
       
       
       }
       public void createLabel(SpriteFont font, SpriteBatch batch)
       {
           label = new Label(font, "", new Vector2(10, 200),Color.White, 1);
           spriteBatch = batch;
           node = GraphList[0];
       }
       public void addEdge(GraphNode Neighbor1, GraphNode Neighbor2, int Distance, int Magnitude)
       {
            Line newLink = new Line(findNode(Neighbor1), findNode(Neighbor2), xnaStuff);
               findNode(Neighbor1).Edges.Add(newLink);
               findNode(Neighbor2).Edges.Add(newLink);
          
       }
       public void addNode(GraphNode Neighbor, GraphNode node, int Distance, int Magnitude)
       {    

            if (Neighbor != null)
            {
                GraphList.Add(node);
             Line newLink = new Line(findNode(Neighbor), node, xnaStuff);
             newLink.setDistance(Distance);
             newLink.setMagnitude(Magnitude);
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
           bool update = false;
           
           foreach (GraphNode a in GraphList)
           {   
               if (node.Equals(a))
                   {
                       label.Text = a.ToString();

                   }
               if (Mouse.GetState().LeftButton == ButtonState.Pressed)
               {

                   if (new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 1, 1).Intersects(a.picturePosition))
                   {
                       label.Text = a.ToString();
                       update = true;
                       node = a;

                   
                   }
               }
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
           label.Draw(gameTime,spriteBatch);
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
