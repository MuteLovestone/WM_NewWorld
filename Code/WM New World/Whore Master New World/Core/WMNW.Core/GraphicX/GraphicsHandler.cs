using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Text;

namespace WMNW.Core.GraphicX
{
    public static class GraphicsHandler
    {
        #region Fields

        /// <summary>
        /// Grants access to our Graphics Device 
        /// </summary>
        private static GraphicsDevice _graphicsDevice = null;
        /// <summary>
        /// Grants acces to our content manager
        /// </summary>
        private static ContentManager _content = null;
        /// <summary>
        /// Grants access to the Sprite Batch
        /// </summary>
        private static SpriteBatch _spriteBatch = null;

        /// <summary>
        /// Helps determine if our Sprite Batch is Currently inbetween Begin and End
        /// </summary>
        private static bool _spriteBatchActive = false;

        /// <summary>
        /// Texture used in many places inside our graphics engine
        /// </summary>
        private static Texture2D _whiteTexture;

        #endregion

        #region Properties

        /// <summary>
        /// Shortcut to our Sprite Batch that can be accessed from anywhere
        /// </summary>
        public static SpriteBatch SpriteBatch
        {
            get
            {
                return _spriteBatch;
            }
        }

        /// <summary>
        /// Gets and Sets if we are using the Camera this draw
        /// </summary>
        private static bool Camera
        {
            get;
            set;
        }

        #endregion

        #region Initialization

        public static void Initialize( GraphicsDevice graphicsDevice, ContentManager content )
        {
            _graphicsDevice = graphicsDevice;
            _content = content;
            _spriteBatch = new SpriteBatch ( _graphicsDevice );

            //Create a single pixel white texture that can be used over and over
            _whiteTexture = new Texture2D ( _graphicsDevice, 1, 1 );
            _whiteTexture.SetData ( new[] { Color.White } );
        }

        #endregion

        #region Special Methods

        /// <summary>
        /// Clear the screen to the color passed
        /// </summary>
        /// <param name="color"></param>
        public static void Clear( Color color )
        {
            if ( _graphicsDevice == null )
                throw new Exception ( "Graphics Device cannot be null - Clear(Color)" );
            _graphicsDevice.Clear ( color );
        }

        /// <summary>
        /// Begins our Sprite Batch 
        /// </summary>
        public static void Begin()
        {
            if ( _spriteBatchActive )
                return; //Do not allow attempts at running Begin multiple times
            _spriteBatch.Begin ();
            _spriteBatchActive = true;
        }

        /// <summary>
        /// Begins our Sprite Batch
        /// </summary>
        /// <param name="spriteSortMode">Sprite Sort Mode</param>
        /// <param name="blendState">Blend State</param>
        /// <param name="samplerState">Sampler State</param>
        /// <param name="depthStencilState">Depth Stencil State</param>
        /// <param name="rasterizerState">Rasterizer State</param>
        public static void Begin( SpriteSortMode spriteSortMode, BlendState blendState, SamplerState samplerState,
                                  DepthStencilState depthStencilState, RasterizerState rasterizerState )
        {
            if ( _spriteBatchActive )
                return; //Do not allow attempts at running Begin multiple times
            _spriteBatch.Begin ( spriteSortMode, blendState, samplerState, depthStencilState, rasterizerState );
            _spriteBatchActive = true;
        }

        /*/// <summary>
        /// Start a SpriteBatch draw that allows moving from day to night and adding in if the camera should be included
        /// </summary>
        /// <param name="camera"></param>
        public static void Begin(bool camera)
        {
            if (_spriteBatchActive)
                return; //Do not allow attempts at running Begin multiple times
            if (camera)
                SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null,
                    SGame.Camera2D.Transform);
            else SpriteBatch.Begin();
            _spriteBatchActive = true;
        }*/

        /// <summary>
        /// Start a SpriteBatch draw that allows moving from day to night and adding in if the camera should be included
        /// </summary>
        /// <param name="camera"></param>
        /// <param name="cameraMatrix"></param>
        public static void Begin( Matrix cameraMatrix )
        {
            if ( _spriteBatchActive )
                return; //Do not allow attempts at running Begin multiple times

            SpriteBatch.Begin ( SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, cameraMatrix );

            _spriteBatchActive = true;
        }

        /// <summary>
        /// End our Sprite Batch and draw all images to screen
        /// </summary>
        public static void End()
        {
            if ( !_spriteBatchActive )
                return; //Only try and end if we were running our Sprite Batch to begin with
            _spriteBatch.End ();
            _spriteBatchActive = false;
        }

        #endregion

        #region Get Methods

        #region Texture

        /// <summary>
        /// Get a Tetxure from a name
        /// </summary>
        /// <param name="name">Textures name</param>
        /// <returns>Texture2D</returns>
        public static Texture2D GetTexture( string name )
        {
            //Return null if name passed was not there
            if ( String.IsNullOrEmpty ( name ) )
                return null;

            Texture2D texture;

            if ( ( texture = ( Texture2D )_content.Load<Texture2D> ( name ) ) != null )
                return texture;

            throw new Exception ( "Cannot find texture: " + name );
        }

        #endregion

        #region Font

        /// <summary>
        /// Get a Sprite Font from a name
        /// </summary>
        /// <param name="name">Name of Sprite Font</param>
        /// <returns>SpriteFont</returns>
        public static SpriteFont GetFont( string name )
        {
            //Return null if name passed was not there
            if ( String.IsNullOrEmpty ( name ) )
                return null;

            SpriteFont font;

            if ( ( font = ( SpriteFont )_content.Load<SpriteFont> ( "Fonts/" + name ) ) != null )
                return font;

            throw new Exception ( "Cannot find Font: " + name );
        }

        #endregion

        #endregion

        #region Draw Calls

        #region Real Draws

        ///// <summary>
        ///// Major Method that is used for every single draw call made
        ///// </summary>
        ///// <param name="texture">Texture being Drawn</param>
        ///// <param name="animation">Animation if needed it can be passed and will be used to Draw</param>
        ///// <param name="destination">Destination to draw to, if Width/Height are 0 than it only has X and Y values</param>
        ///// <param name="source">Source information on the texture we are drawing</param>
        ///// <param name="color">Color</param>
        ///// <param name="rotation">Rotation used in our drawing</param>
        ///// <param name="origin">Origin used in our drawing</param>
        ///// <param name="scalef">Scale value listed as a float</param>
        ///// <param name="scalev">Scale value listed as a Vector2</param>
        ///// <param name="spriteEffects">Sprite Effects</param>
        ///// <param name="layerDepth">Layer Depth</param>
        ///// <param name="gameTime">Generally used for Animation purposes</param>
        private static void RealDraw( Texture2D texture, Rectangle destination, Rectangle? source,
                                      Color color, float? rotation, Vector2? origin, float? scalef, Vector2? scalev, SpriteEffects? spriteEffects,
                                      float? layerDepth )
        {
            if ( !_spriteBatchActive )
                return;
            if ( texture == null )
                return; //If texture is null return
            //If our Destinations Width or Height is equal to 0 than we know we are using an entire rectangle
            if ( destination.Width == 0 || destination.Height == 0 )
            {
                if ( !source.HasValue )
                {
                    _spriteBatch.Draw ( texture, new Vector2 ( destination.X, destination.Y ), color );
                }
                else
                {
                    //If we have values for all things sent than use them
                    if ( rotation.HasValue && origin.HasValue && spriteEffects.HasValue && layerDepth.HasValue )
                    {
                        //Determine if we have Vector2 or float scale
                        if ( !scalev.HasValue && scalef.HasValue )
                        {
                            _spriteBatch.Draw ( texture, new Vector2 ( destination.X, destination.Y ), source, color,
                                ( float )rotation, ( Vector2 )origin, ( float )scalef,
                                ( SpriteEffects )spriteEffects, ( float )layerDepth );
                        }
                        else if ( scalev.HasValue && !scalef.HasValue )
                        {
                            _spriteBatch.Draw ( texture, new Vector2 ( destination.X, destination.Y ), source, color,
                                ( float )rotation, ( Vector2 )origin, ( Vector2 )scalev,
                                ( SpriteEffects )spriteEffects, ( float )layerDepth );
                        }
                    }
                    else //Else follow with the remaining path
                    {
                        _spriteBatch.Draw ( texture, new Vector2 ( destination.X, destination.Y ), source, color );
                    }
                }
            }
            else //Else we know that destination values where sent and must be used
            {
                if ( !source.HasValue )
                {
                    _spriteBatch.Draw ( texture, destination, color );
                }
                else
                {
                    //If we have values for everything sent than draw using them
                    if ( rotation.HasValue && origin.HasValue && spriteEffects.HasValue && layerDepth.HasValue )
                    {
                        _spriteBatch.Draw ( texture, destination, source, color, ( float )rotation, ( Vector2 )origin,
                            ( SpriteEffects )spriteEffects, ( float )layerDepth );
                    }
                    else //Else follow the remaining path
                    {
                        _spriteBatch.Draw ( texture, destination, source, color );
                    }
                }
            }
        }

        /// <summary>
        /// Major method that is used for every single draw call drawing text
        /// </summary>
        /// <param name="font">Font texture used</param>
        /// <param name="text">Text to be displayed</param>
        /// <param name="position">Position of text placment</param>
        /// <param name="color">Color of text</param>
        /// <param name="rotation">Rotation used on text</param>
        /// <param name="origin">Origin of text</param>
        /// <param name="scalef">Scale value listed as a Float</param>
        /// <param name="scalev">Scale value listed as a Vector2</param>
        /// <param name="spriteEffects">Sprite Effects</param>
        /// <param name="layerDepth">Layer Depth</param>
        private static void RealSpriteFontDraw( SpriteFont font, StringBuilder text, Vector2 position, Color color,
                                                float? rotation, Vector2? origin, float? scalef, Vector2? scalev, SpriteEffects? spriteEffects,
                                                float? layerDepth )
        {
            if ( !_spriteBatchActive )
                return;
            if ( font == null )
                return; //If font is null return
            if ( String.IsNullOrEmpty ( text.ToString () ) )
                return; //Dont try drawing null text

            if ( rotation.HasValue && origin.HasValue && ( scalef.HasValue || scalev.HasValue ) && spriteEffects.HasValue &&
                 layerDepth.HasValue )
            {
                //If all our nullable values are present then check which scale was passed to see which method to use
                if ( scalef.HasValue )
                    _spriteBatch.DrawString ( font, text, position, color, ( float )rotation, ( Vector2 )origin,
                        ( float )scalef, ( SpriteEffects )spriteEffects, ( float )layerDepth );
                else
                    _spriteBatch.DrawString ( font, text, position, color, ( float )rotation, ( Vector2 )origin,
                        ( Vector2 )scalev, ( SpriteEffects )spriteEffects, ( float )layerDepth );
            }
            else
                _spriteBatch.DrawString ( font, text, position, color );
        }

        #endregion

        #region Regular Draws

        #region Calls Without Source

        /// <summary>
        /// Draw
        /// </summary>
        /// <param name="name">Texture name</param>
        public static void Draw( string name )
        {
            RealDraw ( GetTexture ( name ), new Rectangle ( 0, 0, 0, 0 ), GetTexture ( name ).Bounds, Color.White, null, null, null,
                null, null, null );
        }

        /// <summary>
        /// Draw
        /// </summary>
        /// <param name="name">Texture name</param>
        /// <param name="destination">Destination</param>
        /// <param name="color">Color</param>
        public static void Draw( string name, Vector2 destination, Color color )
        {
            RealDraw ( GetTexture ( name ), new Rectangle ( ( int )destination.X, ( int )destination.Y, 0, 0 ),
                GetTexture ( name ).Bounds, color, null, null, null, null, null, null );
        }

        /// <summary>
        /// Draw
        /// </summary>
        /// <param name="name">Texture name</param>
        /// <param name="destination">Destination</param>
        /// <param name="color">Color</param>
        public static void Draw( string name, Rectangle destination, Color color )
        {
            RealDraw ( GetTexture ( name ), destination, GetTexture ( name ).Bounds, color, null, null, null, null, null, null );
        }

        /// <summary>
        /// Draw
        /// </summary>
        /// <param name="texture">Texture</param>
        /// <param name="destination">Destination to draw to, if Width/Height are 0 than it only has X and Y values</param>
        /// <param name="source">Source information on the texture we are drawing</param>
        /// <param name="color">Color</param>
        /// <param name="rotation">Rotation used in our drawing</param>
        /// <param name="origin"></param>
        /// <param name="scale">Scale of object we are drawing</param>
        /// <param name="spriteEffects">Sprite Effects</param>
        /// <param name="layerDepth">Layer Depth</param>
        public static void Draw( Texture2D texture, Vector2 destination, Rectangle source, Color color, float rotation,
                                 Vector2 origin, Vector2 scale, SpriteEffects spriteEffects, float layerDepth )
        {
            RealDraw ( texture, new Rectangle ( ( int )destination.X, ( int )destination.Y, 0, 0 ), source, color, rotation,
                origin, null, scale, spriteEffects, layerDepth );
        }

        #endregion

        #region Calls With Source

        /// <summary>
        /// Draw
        /// </summary>
        /// <param name="name">Texture name</param>
        /// <param name="destination">Destination</param>
        /// <param name="source">Source</param>
        /// <param name="color">Color</param>
        public static void Draw( string name, Vector2 destination, Rectangle source, Color color )
        {
            RealDraw ( GetTexture ( name ), new Rectangle ( ( int )destination.X, ( int )destination.Y, 0, 0 ), source, color,
                null, null, null, null, null, null );
        }

        #region Draw Calls Without GameTime

        /// <summary>
        /// Draw
        /// </summary>
        /// <param name="name">Texture name</param>
        /// <param name="destination">Destination</param>
        /// <param name="source">Source</param>
        /// <param name="color">Color</param>
        public static void Draw( string name, Rectangle destination, Rectangle source, Color color )
        {
            RealDraw ( GetTexture ( name ), destination, source, color, null, null, null, null, null, null );
        }

        /// <summary>
        /// Draw
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="destination"></param>
        /// <param name="color"></param>
        /// <param name="rotation"></param>
        /// <param name="origin"></param>
        /// <param name="scale"></param>
        /// <param name="effects"></param>
        /// <param name="layer"></param>
        public static void Draw( Texture2D texture, Vector2 destination, Color color, float rotation, Vector2 origin,
                                 Vector2 scale, SpriteEffects effects, float layer )
        {
            RealDraw ( texture, new Rectangle ( ( int )destination.X, ( int )destination.Y, 0, 0 ), null, color, rotation,
                origin, null, scale, effects, layer );
        }


        #endregion

        #endregion

        #endregion

        #region Calls With Source

        #region Calls With Actual Textures

        /// <summary>
        /// Draw
        /// </summary>
        /// <param name="texture">Texture</param>
        /// <param name="destination">Destination</param>
        /// <param name="color">Color</param>
        public static void Draw( Texture2D texture, Rectangle destination, Color color )
        {
            _spriteBatch.Draw ( texture, destination, color );
        }

        /// <summary>
        /// Draw
        /// </summary>
        /// <param name="texture">Texture</param>
        /// <param name="destination">Destination</param>
        /// <param name="color">Color</param>
        public static void Draw( Texture2D texture, Vector2 destination, Color color )
        {
            _spriteBatch.Draw ( texture, destination, color );
        }

        /// <summary>
        /// Draw
        /// </summary>
        /// <param name="texture">Texture</param>
        /// <param name="destination">Destination</param>
        /// <param name="source">Source to copy from</param>
        /// <param name="color">Color</param>
        public static void Draw( Texture2D texture, Vector2 destination, Rectangle source, Color color )
        {
            _spriteBatch.Draw ( texture, destination, source, color );
        }

        #endregion

        #endregion

        #region SpriteFont Draws

        /// <summary>
        /// Draw String
        /// </summary>
        /// <param name="name">Sprite Font name</param>
        /// <param name="text">Text to draw</param>
        /// <param name="position">Destination</param>
        /// <param name="color">Color</param>
        public static void DrawString( string name, string text, Vector2 position, Color color )
        {
            RealSpriteFontDraw ( GetFont ( name ), new StringBuilder ( text ), position, color, null, null, null, null, null,
                null );
        }

        /// <summary>
        /// Messures the Width and Height of a string passed
        /// </summary>
        /// <param name="font">Font to messure against</param>
        /// <param name="text">String to messure</param>
        /// <returns></returns>
        public static Vector2 MesureString( string font, string text )
        {
            return string.IsNullOrEmpty ( text ) ? Vector2.Zero : GetFont ( font ).MeasureString ( text );
        }

        #endregion

        #region SpriteFont With Source

        // <summary>
        /// Draw String
        /// <param name="font"></param>
        /// <param name="text">Text to draw</param>
        /// <param name="position">Destination</param>
        /// <param name="color">Color</param>
        public static void DrawString( SpriteFont font, string text, Vector2 position, Color color )
        {
            RealSpriteFontDraw ( font, new StringBuilder ( text ), position, color, null, null, null, null, null,
                null );
        }

        /// <summary>
        /// Messures the Width and Height of a string passed
        /// </summary>
        /// <param name="font">Font to messure against</param>
        /// <param name="text">String to messure</param>
        /// <returns></returns>
        public static Vector2 MesureString( SpriteFont font, string text )
        {
            return string.IsNullOrEmpty ( text ) ? Vector2.Zero : font.MeasureString ( text );
        }

        #endregion

        #region Other Useful Draw Calls

        public static void DrawLine( Point start, Point end, int border, Color color, float alpha )
        {
            Vector2 edge = new Vector2 ( end.X, end.Y ) - new Vector2 ( start.X, start.Y );
            var angle = ( float )Math.Atan2 ( edge.Y, edge.X );

            _spriteBatch.Draw ( _whiteTexture, new Rectangle ( start.X, start.Y, ( int )edge.Length (), border ), null, color * alpha, angle, Vector2.Zero, SpriteEffects.None, 0 );
        }

        /// <summary>
        /// Draws a Horizontal line.
        /// </summary>
        /// <param name="start">Start Point</param>
        /// <param name="end">End Point</param>
        /// <param name="border">Border Width</param>
        /// <param name="alpha">Alpha</param>
        public static void DrawHLine( Point start, int wid, int height, float alpha )
        {
            _spriteBatch.Draw ( _whiteTexture, new Rectangle ( start.X, start.Y, wid, height ), Color.White * alpha );
        }

        /// <summary>
        /// Draws a Horizontal line.
        /// </summary>
        /// <param name="start">Start Point</param>
        /// <param name="end">End Point</param>
        /// <param name="border">Border Width</param>
        /// <param name="alpha">Alpha</param>
        public static void DrawHLine( Point start, int width, int height, Color color )
        {
            _spriteBatch.Draw ( _whiteTexture, new Rectangle ( start.X, start.Y, width, height ), color );
        }

        /// <summary>
        /// Draws a Verticle line.
        /// </summary>
        /// <param name="start">Start Point</param>
        /// <param name="end">End Point</param>
        /// <param name="border">Border Width</param>
        /// <param name="alpha">Alpha</param>
        public static void DrawVLine( Point start, int width, int height, float alpha )
        {
            _spriteBatch.Draw ( _whiteTexture, new Rectangle ( start.X, start.Y, width, height ), Color.White * alpha );
        }

        /// <summary>
        /// Draws a Horizontal line.
        /// </summary>
        /// <param name="start">Start Point</param>
        /// <param name="end">End Point</param>
        /// <param name="border">Border Width</param>
        /// <param name="alpha">Alpha</param>
        public static void DrawVLine( Point start, int width, int height, Color color )
        {
            _spriteBatch.Draw ( _whiteTexture, new Rectangle ( start.X, start.Y, width, height ), color );
        }

        /// <summary>
        /// Draw a rectangle around the rectangle passed
        /// </summary>
        /// <param name="rectangle">The rectangle to draw.</param>
        /// <param name="color">The draw color.</param>
        /// <param name="borderWidth">Width of Rectangle Border</param>
        public static void DrawRectangleAround( Rectangle rectangle, Color color, int borderWidth )
        {
            _spriteBatch.Draw ( _whiteTexture, new Rectangle ( rectangle.X - borderWidth, rectangle.Y - borderWidth, borderWidth, rectangle.Height + borderWidth * 2 ), color ); //Left
            _spriteBatch.Draw ( _whiteTexture, new Rectangle ( rectangle.X, rectangle.Y - borderWidth, rectangle.Width + borderWidth, borderWidth ), color );
            _spriteBatch.Draw ( _whiteTexture, new Rectangle ( rectangle.X + rectangle.Width, rectangle.Y, borderWidth, rectangle.Height + borderWidth ), color );
            _spriteBatch.Draw ( _whiteTexture, new Rectangle ( rectangle.X, rectangle.Y + rectangle.Height, rectangle.Width + borderWidth, borderWidth ), color );
        }

        /// <summary>
        /// Draw a rectangle.
        /// </summary>
        /// <param name="rectangle">The rectangle to draw.</param>
        /// <param name="color">The draw color.</param>
        /// <param name="borderWidth">Width of Rectangle Border</param>
        public static void DrawRectangle( Rectangle rectangle, Color color, int borderWidth )
        {
            _spriteBatch.Draw ( _whiteTexture, new Rectangle ( rectangle.X, rectangle.Y, borderWidth, rectangle.Height ), color ); //Left
            _spriteBatch.Draw ( _whiteTexture, new Rectangle ( rectangle.X, rectangle.Y, rectangle.Width + borderWidth, borderWidth ), color );
            _spriteBatch.Draw ( _whiteTexture, new Rectangle ( rectangle.X + rectangle.Width, rectangle.Y, borderWidth, rectangle.Height ), color );
            _spriteBatch.Draw ( _whiteTexture, new Rectangle ( rectangle.X, rectangle.Y + rectangle.Height - borderWidth, rectangle.Width + borderWidth, borderWidth ), color );
        }


        /// <summary>
        /// Fill a rectangle
        /// </summary>
        /// <param name="rectangle">The rectangle to fill</param>
        /// <param name="color">Color</param>
        public static void DrawFillRectangle( Rectangle rectangle, Color color )
        {
            _spriteBatch.Draw ( _whiteTexture, rectangle, color );
        }



        #endregion

        #region Scissors

        public static readonly RasterizerState RasterizerState = new RasterizerState () { ScissorTestEnable = true };

        public static bool Scissor = false;

        /// <summary>
        /// Starts up the Scissors
        /// </summary>
        public static void StartScissor( Rectangle rectangle )
        {
            Scissor = true;

            Begin ( SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, RasterizerState );

            //Set the current scissor rectangle
            SpriteBatch.GraphicsDevice.ScissorRectangle = rectangle;
        }

        /// <summary>
        /// Ends Scissor Mode
        /// </summary>
        public static void EndScissor()
        {
            Scissor = false;
            RasterizerState.GraphicsDevice.Clear ( Color.Transparent );
        }

        #endregion

        #endregion

    }
}

