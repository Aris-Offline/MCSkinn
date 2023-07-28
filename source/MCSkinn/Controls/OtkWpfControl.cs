using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using OpenTK;
using OpenTK.Graphics;
using System.Windows.Threading;
using Inkore.UI.WPF.Modern.Controls;
using System.Windows.Input;
using MCSkinn.Scripts.Paril.OpenGL;

namespace OpenTK.WPF
{
	using FramebufferAttachment = OpenTK.Graphics.OpenGL.FramebufferAttachment;
	using FramebufferErrorCode = OpenTK.Graphics.OpenGL.FramebufferErrorCode;
	using FramebufferTarget = OpenTK.Graphics.OpenGL.FramebufferTarget;
	using GL = OpenTK.Graphics.OpenGL.GL;
	using PixelFormat = OpenTK.Graphics.OpenGL.PixelFormat;
	using PixelType = OpenTK.Graphics.OpenGL.PixelType;
	using RenderbufferStorage = OpenTK.Graphics.OpenGL.RenderbufferStorage;
	using RenderbufferTarget = OpenTK.Graphics.OpenGL.RenderbufferTarget;

	public class OtkWpfControl : UserControl
	{
		#region Dependency Properties

		#region DrawTime

		/// <summary>
		/// measured drawing time for a frame in milliseconds
		/// </summary>
		private static readonly DependencyProperty DrawTimeProperty = DependencyProperty.Register
		(
			"DrawTime",
			typeof(double),
			typeof(OtkWpfControl),
			new PropertyMetadata( 0.0 )
		);

		public double DrawTime
		{
			get { return (double)GetValue( DrawTimeProperty ); }
			set { SetValue( DrawTimeProperty , value ); }
		}

		#endregion DrawTime

		#region VersionMajor

		/// <summary>
		/// specify OpenGL version major part , default version is 2.0
		/// </summary>
		private static readonly DependencyProperty VersionMajorProperty = DependencyProperty.Register
		(
			"VersionMajor",
			typeof(int),
			typeof(OtkWpfControl),
			new PropertyMetadata( 2 )
		);

		public int VersionMajor
		{
			get { return (int)GetValue( VersionMajorProperty ); }
			set { SetValue( VersionMajorProperty , value ); }
		}

		#endregion VersionMajor

		#region VersionMinor

		/// <summary>
		/// specify OpenGL version minor part , default version is 2.0
		/// </summary>
		private static readonly DependencyProperty VersionMinorProperty = DependencyProperty.Register
		(
			"VersionMinor",
			typeof(int),
			typeof(OtkWpfControl),
			new PropertyMetadata( 0 )
		);

		public int VersionMinor
		{
			get { return (int)GetValue( VersionMinorProperty ); }
			set { SetValue( VersionMinorProperty , value ); }
		}

        #endregion VersionMinor

        #region IsRenderingPaused

        /// <summary>
        /// specify OpenGL version minor part , default version is 2.0
        /// </summary>
        private static readonly DependencyProperty IsRenderingPausedProperty = DependencyProperty.Register
        (
            "IsRenderingPaused",
            typeof(bool),
            typeof(OtkWpfControl),
            new PropertyMetadata(false)
        );

        public bool IsRenderingPaused
        {
            get { return (bool)GetValue(IsRenderingPausedProperty); }
            set { SetValue(IsRenderingPausedProperty, value); }
        }

        #endregion IsRenderingPaused

        #region Framerate

        /// <summary>
        /// specify OpenGL version minor part , default version is 2.0
        /// </summary>
        private static readonly DependencyPropertyKey FrameratePropertyKey = DependencyProperty.RegisterReadOnly
        (
            "Framerate",
            typeof(int),
            typeof(OtkWpfControl),
            new PropertyMetadata(0)
        );
        public static readonly DependencyProperty FramerateProperty = FrameratePropertyKey.DependencyProperty;

        public int Framerate
        {
            get { return (int)GetValue(FramerateProperty); }
            set { SetValue(FrameratePropertyKey, value); }
        }

        #endregion Framerate

        #region RenderScale

        private static readonly DependencyProperty RenderScaleProperty = DependencyProperty.Register
        (
            "RenderScale",
            typeof(double),
            typeof(OtkWpfControl),
            new PropertyMetadata(1.0)
        );

        public double RenderScale
        {
            get { return (double)GetValue(RenderScaleProperty); }
            set { SetValue(RenderScaleProperty, value); }
        }

        #endregion RenderScale

        #endregion Dependency Properties

        #region Events

        /// <summary>
        /// Occurs when OpenGL should be initialised.
        /// </summary>
        [Description("Called when OpenGL has been initialized."), Category("SharpGL")]
		public event EventHandler OpenGLInitialized;

		/// <summary>
		/// Occurs when the control is resized. This can be used to perform custom projections.
		/// </summary>
		[Description("Called when the control is resized - you can use this to do custom viewport projections."), Category("SharpGL")]
		public event EventHandler Resized;

		/// <summary>
		/// event args for draw timeming
		/// </summary>
		public class OpenGLDrawEventArgs : EventArgs
		{
			/// <summary>
			/// true if handler requires drawing (refresh image)
			/// initially false , means skip refreshing the image
			/// </summary>
			public bool Redrawn { get; set; }

			/// <summary>
			/// time from CompositionTarget_Rendering has attached
			/// this is not the interval
			/// </summary>
			public TimeSpan RenderingTime { get; set; }
		}

		/// <summary>
		/// Occurs when OpenGL drawing should occur.
		/// </summary>
		[Description("Called whenever OpenGL drawing can should occur."), Category("SharpGL")]
		public event EventHandler<OpenGLDrawEventArgs> OpenGLDraw;

		#endregion Events

		public bool RenderOnTimer { get; private set; }

        /// <summary>
        /// Initializes a new instance
        /// </summary>
        public OtkWpfControl(bool renderOnTimer = false, GraphicsMode gmode = null)
		{
			Content = mImage;
			mImage.Stretch = Stretch.Fill;
            //RenderOptions.SetBitmapScalingMode(mImage, BitmapScalingMode.HighQuality);

			RenderOnTimer = renderOnTimer;

            Unloaded += OpenGLControl_Unloaded;
			Loaded   += OpenGLControl_Loaded;

            // initialize framebufferhandler for OpeTK
            mLoaded = false;
            mSize = Size.Empty;
            mFramebufferId = -1;

			if (gmode == null)
			{
                gmode = new GraphicsMode();
				gmode = new GraphicsMode(gmode.ColorFormat, gmode.Depth, gmode.Stencil, 4, gmode.ColorFormat, 2, false);
            }

            mTkGlControl = new GLControl(gmode, VersionMajor, VersionMinor, GraphicsContextFlags.Default);
            mTkGlControl.MakeCurrent();

			if (RenderOnTimer)
			{
				renderTimer = new DispatcherTimer();
				renderTimer.Interval = TimeSpan.FromMilliseconds(5);
                renderTimer.Tick += RenderTimer_Tick;
			}
        }

		public double RenderWidth => ActualWidth * RenderScale;
        public double RenderHeight => ActualHeight * RenderScale;

		public TimeSpan? RenderTimerInterval
		{
			get { return renderTimer?.Interval; }
			set
			{
				if (value != null && renderTimer != null)
					renderTimer.Interval = value.Value;
			}
		}

        private void RenderTimer_Tick(object? sender, EventArgs e)
        {
			CompositionTarget_Rendering(sender, e);
        }

        public GLControl Renderer => mTkGlControl;

		/// <summary>
		/// When overridden in a derived class, is invoked whenever application code or 
		/// internal processes call <see cref="M:System.Windows.FrameworkElement.ApplyTemplate"/>.
		/// </summary>
		public override void OnApplyTemplate()
		{
			//  Call the base.
			base.OnApplyTemplate();

		

			//  Fire the OpenGL initialised event.
			OpenGLInitialized?.Invoke( this , EventArgs.Empty );
		}

		#region Implementation

		#region Event Handlers

		/// <summary>
		/// Handles the Loaded event of the OpenGLControl control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> Instance containing the event data.</param>
		private void OpenGLControl_Loaded( object sender , RoutedEventArgs routedEventArgs )
		{
			SizeChanged += OpenGLControl_SizeChanged;

			UpdateOpenGLControl( RenderSize );

            // start rendering to be on WPF redering timing
            if (!RenderOnTimer)
            {
                CompositionTarget.Rendering += CompositionTarget_Rendering;
            }
			else
			{
				renderTimer.Start();
			}
		}

		public DispatcherTimer renderTimer;

		/// <summary>
		/// Handles the Unloaded event of the OpenGLControl control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> Instance containing the event data.</param>
		private void OpenGLControl_Unloaded( object sender , RoutedEventArgs routedEventArgs )
		{
			SizeChanged -= OpenGLControl_SizeChanged;
            if (!RenderOnTimer)
			{
                CompositionTarget.Rendering -= CompositionTarget_Rendering;
            }
			else
			{
				renderTimer.Stop();
			}
        }

		/// <summary>
		/// Handles the SizeChanged event of the OpenGLControl control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.SizeChangedEventArgs"/> Instance containing the event data.</param>
		private void OpenGLControl_SizeChanged( object sender , SizeChangedEventArgs e )
		{
			UpdateOpenGLControl( new Size(RenderWidth, RenderHeight) );
		}

		/// <summary>
		/// Handles the WPF redering timming
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">cast RenderingEventArgs to know RenderginTime</param>
		private void CompositionTarget_Rendering( object sender , EventArgs e )
		{
			try
			{
				if (IsRenderingPaused)
					return;

                // https://evanl.wordpress.com/2009/12/06/efficient-optimal-per-frame-eventing-in-wpf/
                if(e is RenderingEventArgs)
				{
                    var args = (RenderingEventArgs)e;
                    if (args.RenderingTime == mLast)
                    {
                        return;
                    }
                    mLast = args.RenderingTime;
                }

                //  Start the stopwatch so that we can time the rendering.
                mStopwatch.Restart();

                // import from FrameBufferHandler
                if (GraphicsContext.CurrentContext != mTkGlControl.Context)
                {
                    mTkGlControl.MakeCurrent();
                }

                var framebuffersize = new Size(RenderWidth, RenderHeight);
                if (framebuffersize != mSize || mLoaded == false)
                {
                    mSize = framebuffersize;
                    CreateFramebuffer();
                    GL.Viewport(0, 0, (int)RenderWidth, (int)RenderHeight);
                }

                // all of drawing commands will be performed onto the FBO
                GL.BindFramebuffer(FramebufferTarget.Framebuffer, mFramebufferId);

                //	If there is a draw handler, then call it.
                var handler = OpenGLDraw;
                if (handler != null)
                {
                    var ev = new OpenGLDrawEventArgs() { Redrawn = false, RenderingTime = e is RenderingEventArgs ? (e as RenderingEventArgs).RenderingTime :mLast };
                    handler(this, ev);
                    if (!ev.Redrawn)
                    {
                        // handler doesn't draw anything then skip refresh the image
                        mStopwatch.Stop();
                        return;
                    }
                }
                else
                {
                    GL.Clear(Graphics.OpenGL.ClearBufferMask.ColorBufferBit);
                }

                // wait FBO has completed drawing
                GL.Finish();

                if (mDrawnImage == null || mDrawnImage.Width != mSize.Width || mDrawnImage.Height != mSize.Height)
                {
                    // create bitmap for imagesource to be displayed
                    mDrawnImage = new WriteableBitmap((int)mSize.Width, (int)mSize.Height, 96, 96, PixelFormats.Pbgra32, BitmapPalettes.WebPalette);

                    // (re)assign read buffer
                    mBackbuffer = new byte[(int)mSize.Width * (int)mSize.Height * 4];
                }

                // to avoid image upside down, read to another memory
                GL.ReadPixels(0, 0, (int)mSize.Width, (int)mSize.Height, PixelFormat.Bgra, PixelType.UnsignedByte, mBackbuffer);

                // WriteableBitmap should be locked as short as possible
                mDrawnImage.Lock();

                // copy pixels upside down
                var src = new Int32Rect(0, 0, (int)mDrawnImage.Width, 1);
                for (int y = 0; y < (int)mDrawnImage.Height; y++)
                {
                    src.Y = (int)mDrawnImage.Height - y - 1;
                    mDrawnImage.WritePixels(src, mBackbuffer, mDrawnImage.BackBufferStride, 0, y);
                }
                mDrawnImage.AddDirtyRect(new Int32Rect(0, 0, (int)mDrawnImage.Width, (int)mDrawnImage.Height));

                mDrawnImage.Unlock();

                if (mBackbuffer != null)
                {
                    // refresh displayed image
                    mImage.Source = mDrawnImage;
                }

                //  Stop the stopwatch.
                mStopwatch.Stop();

                //  Store the frame drawing time.
                DrawTime = mStopwatch.Elapsed.TotalMilliseconds;

                _frames++;

            }
			catch
			{
				if (Debugger.IsAttached)
					throw;
			}
			finally
			{
                if (DateTime.Now.Subtract(_lastMeasureTime) > TimeSpan.FromSeconds(1))
                {
                    Framerate = _frames;
                    _frames = 0;
                    _lastMeasureTime = DateTime.Now;
                }
            }

        }

		#endregion Event Handlers

		/// <summary>
		/// tell handler control being resized
		/// </summary>
		/// <param name="width">The width of the OpenGL drawing area.</param>
		/// <param name="height">The height of the OpenGL drawing area.</param>
		private void UpdateOpenGLControl( Size framebuffersize )
		{
			Resized?.Invoke( this , EventArgs.Empty );
		}

		/// <summary>
		/// create FBO for offscreen derndering using a render buffer
		/// </summary>
		private void CreateFramebuffer()
		{
			mTkGlControl.MakeCurrent();

			if ( mFramebufferId > 0 )
			{
				GL.DeleteFramebuffer( mFramebufferId );
			}

			if ( mColorbufferId > 0 )
			{
				GL.DeleteRenderbuffer( mColorbufferId );
			}

			if ( mDepthbufferId > 0 )
			{
				GL.DeleteRenderbuffer( mDepthbufferId );
			}

			mFramebufferId = GL.GenFramebuffer();
			GL.BindFramebuffer( FramebufferTarget.Framebuffer , mFramebufferId );

			mColorbufferId = GL.GenRenderbuffer();
			GL.BindRenderbuffer( RenderbufferTarget.Renderbuffer , mColorbufferId );
			GL.RenderbufferStorage( RenderbufferTarget.Renderbuffer , RenderbufferStorage.Rgba8 , (int)mSize.Width , (int)mSize.Height );

			mDepthbufferId = GL.GenRenderbuffer();
			GL.BindRenderbuffer( RenderbufferTarget.Renderbuffer , mDepthbufferId );
			GL.RenderbufferStorage( RenderbufferTarget.Renderbuffer , RenderbufferStorage.DepthComponent24 , (int)mSize.Width , (int)mSize.Height );

			GL.FramebufferRenderbuffer( FramebufferTarget.Framebuffer , FramebufferAttachment.ColorAttachment0 , RenderbufferTarget.Renderbuffer , mColorbufferId );
			GL.FramebufferRenderbuffer( FramebufferTarget.Framebuffer , FramebufferAttachment.DepthAttachment , RenderbufferTarget.Renderbuffer , mDepthbufferId );

			var error = GL.CheckFramebufferStatus( FramebufferTarget.Framebuffer );
			if ( error != FramebufferErrorCode.FramebufferComplete )
			{
				throw new Exception( "Failed to create FrameBuffer for OpenGLControl, "+error.ToString() );
			}

			mLoaded = true;
		}

		public WriteableBitmap BufferImage
		{
			get { return mImage.Source as WriteableBitmap; }
		}

		public Point GetMousePointAtRenderer()
		{
			Point p = Mouse.GetPosition(this);
			return new Point(p.X * RenderScale, p.Y * RenderScale);
		}

		#endregion Implementation

		#region Fields

		private Image mImage = new Image();

		/// <summary>
		/// A stopwatch used for timing rendering.
		/// </summary>
		protected Stopwatch mStopwatch = new Stopwatch();
		private TimeSpan mLast = TimeSpan.Zero;

		private GLControl mTkGlControl;         // hidden WinForms control for offscreen rendering

		private int _frames = 0;
        private DateTime _lastMeasureTime;

        private byte[] mBackbuffer;				// FBO pixels read buffer , create statically to avoid GC
		private WriteableBitmap mDrawnImage;	// displaying bitmap
		private int mFramebufferId;				// FBO
		private int mColorbufferId;				// FBO pixel buffer
		private int mDepthbufferId;             // FBO depth buffer
		private Size mSize;						// FBO (drawing) size
		private bool mLoaded;

		#endregion Fields
	}
}
