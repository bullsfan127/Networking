using System;
using System.Collections.Generic;
using System.Linq;
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
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Packet ap;
        public Link link;
        Texture2D nodeImage;
        Link link2;
        GraphNode node;
        GraphNode node2;
        GraphNode node3;
        GraphNode node4;
        GraphNode node5;
        SpriteFont font;
        List<GraphNode> nodes = new List<GraphNode>();
        Network graphNetwork;
        Fields fields;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            font = this.Content.Load<SpriteFont>("Arial");
            fields = new Fields(spriteBatch, graphics);
            int[] a = new int[] { 1, 2, 3 };
            int[] b = new int[] { 4, 5, 6 };
            int[] c = new int[] { 7, 8, 9 };
            int[] d = new int[] { 10, 11, 12 };
            int[] e = new int[] { 13, 14, 15 };
            int[] f = new int[] { 16, 17, 18 };
            node = new GraphNode(fields, a);
            node2 = new GraphNode(fields, b);
            node3 = new GraphNode(fields, c);
            node4 = new GraphNode(fields, d);
            node5 = new GraphNode(fields, f);
            nodeImage = this.Content.Load<Texture2D>("Node");
            node.addText(font, "1");
            node.loadImage(nodeImage, new Rectangle(100, 100, 50, 50));

            node2.loadImage(nodeImage, new Rectangle(640, 200, 50, 50));
            node2.addText(font, "2");
            node3.loadImage(nodeImage, new Rectangle(500, 101, 50, 50));
            node3.addText(font, "3");
            node4.loadImage(nodeImage, new Rectangle(60, 300, 50, 50));
            node4.addText(font, "4");
            node5.loadImage(nodeImage, new Rectangle(501, 301, 50, 50));
            node5.addText(font, "5");

            
            // TODO: use this.Content to load your game content here
            
            graphNetwork = new Network(fields);
            graphNetwork.addNode(null, node, 200, 150);
            graphNetwork.addNode(node, node3, 200, 150);
            graphNetwork.addNode(node3, node5, 200, 150);
            graphNetwork.addNode(node, node4, 200, 150);

            graphNetwork.addNode(node3, node2, 200, 150);
            
            graphNetwork.addEdge(node5, node2, 200, 150); 
            graphNetwork.addEdge(node5, node4, 200, 150);
            graphNetwork.addEdge(node5, node, 200, 150);
            for (int i = 0; i < 19; i++)
            {
                graphNetwork.findNode(node5).outgoing.Enqueue(new Packet(123, node.IP, node4.IP, this.Content.Load<Texture2D>("Packet"))

                {
                });


            }    
            ap = new Packet(123, node.IP, node4.IP, this.Content.Load<Texture2D>("Packet"));
             ap.packetParticle = this.Content.Load<Texture2D>("Packet");
             graphNetwork.findNode(node).outgoing.Enqueue(ap);

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
           
            ap.Color = Color.BlueViolet;

            if (Keyboard.GetState().IsKeyDown(Keys.Tab))
            {
                for (int i = 0; i < 19; i++)
                {
                    graphNetwork.findNode(node5).outgoing.Enqueue(new Packet(123, node.IP, node4.IP, this.Content.Load<Texture2D>("Packet"))

                    {
                    });


                }
                graphNetwork.findNode(node).outgoing.Enqueue(ap);
            }
        

            // TODO: Add your update logic here
            graphNetwork.Update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            //node.Draw(gameTime);
            //node2.Draw(gameTime);
            //node3.Draw(gameTime);
            //node4.Draw(gameTime);
            //node5.Draw(gameTime);
            graphNetwork.Draw(gameTime);
            spriteBatch.Begin();
                spriteBatch.DrawString(font, ap.toString(), Vector2.Zero, Color.Red);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }

    public struct Fields
    {
        public SpriteBatch spriteBatch;
        public GraphicsDevice graphics;
        public Viewport viewPort;
        public enum Direction { NORTH, SOUTH, EAST, WEST };
        public Fields(SpriteBatch _spriteBatch, GraphicsDeviceManager graphicDeviceManager)
        {
            this.spriteBatch = _spriteBatch;
            this.graphics = graphicDeviceManager.GraphicsDevice;
            this.viewPort = graphicDeviceManager.GraphicsDevice.Viewport;
        
        
        }
    
    
    }


}
