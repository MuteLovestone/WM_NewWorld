using System;
using Microsoft.Xna.Framework.Input;

namespace WMNW.Core.Input.Classes
{
    /// <summary>Extended game pad state with additional buttons and axes</summary>
    public partial struct ExtendedGamePadState
    {

        /// <summary>Initializes a new extended game pas state to the provided values</summary>
        /// <param name="availableAxes">Bit mask of the axes made available in the state</param>
        /// <param name="axes">
        ///   Values of all 24 axes in the order they appear in the ExtendedAxes enumeration
        /// </param>
        /// <param name="availableSliders">Bit mask of the slider provided by the state</param>
        /// <param name="sliders">
        ///   Values of all 8 sliders in the order they appear in the ExtendedSliders enumeration
        /// </param>
        /// <param name="buttonCount">Number of buttons provided by the state</param>
        /// <param name="buttons">State of all 128 buttons in the state</param>
        /// <param name="povCount">Number of Point-of-View controllers in the state</param>
        /// <param name="povs">State of all 4 Point-of-View controllers</param>
        public ExtendedGamePadState (
            ExtendedAxis availableAxes, float[/*24*/] axes,
            ExtendedSliders availableSliders, float[/*8*/] sliders,
            int buttonCount, bool[/*128*/] buttons,
            int povCount, int[/*4*/] povs
        )
        {

            // Take over all axes
            this.AvailableAxes = availableAxes;
            this.X = axes [ 0 ];
            this.Y = axes [ 1 ];
            this.Z = axes [ 2 ];
            this.VelocityX = axes [ 3 ];
            this.VelocityY = axes [ 4 ];
            this.VelocityZ = axes [ 5 ];
            this.AccelerationX = axes [ 6 ];
            this.AccelerationY = axes [ 7 ];
            this.AccelerationZ = axes [ 8 ];
            this.ForceX = axes [ 9 ];
            this.ForceY = axes [ 10 ];
            this.ForceZ = axes [ 11 ];
            this.RotationX = axes [ 12 ];
            this.RotationY = axes [ 13 ];
            this.RotationZ = axes [ 14 ];
            this.AngularVelocityX = axes [ 15 ];
            this.AngularVelocityY = axes [ 16 ];
            this.AngularVelocityZ = axes [ 17 ];
            this.AngularAccelerationX = axes [ 18 ];
            this.AngularAccelerationY = axes [ 19 ];
            this.AngularAccelerationZ = axes [ 20 ];
            this.TorqueX = axes [ 21 ];
            this.TorqueY = axes [ 22 ];
            this.TorqueZ = axes [ 23 ];

            // Take over all sliders
            this.AvailableSliders = availableSliders;
            this.Slider1 = sliders [ 0 ];
            this.Slider2 = sliders [ 1 ];
            this.VelocitySlider1 = sliders [ 2 ];
            this.VelocitySlider2 = sliders [ 3 ];
            this.AccelerationSlider1 = sliders [ 4 ];
            this.AccelerationSlider2 = sliders [ 5 ];
            this.ForceSlider1 = sliders [ 6 ];
            this.ForceSlider2 = sliders [ 7 ];

            // Take over all buttons
            this.ButtonCount = buttonCount;
            this.buttonState1 = 0;
            for ( int index = 0; index < Math.Min ( 64, buttonCount ); ++index )
            {
                if ( buttons [ index ] )
                {
                    this.buttonState1 |= ( 1UL << index );
                }
            }
            this.buttonState2 = 0;
            for ( int index = 0; index < ( buttonCount - 64 ); ++index )
            {
                if ( buttons [ index + 64 ] )
                {
                    this.buttonState2 |= ( 1UL << index );
                }
            }

            // Take over all PoV controllers
            this.PovCount = povCount;
            this.Pov1 = povs [ 0 ];
            this.Pov2 = povs [ 1 ];
            this.Pov3 = povs [ 2 ];
            this.Pov4 = povs [ 3 ];
        }

        /// <summary>
        ///   Initializes a new extended game pad state from a standard game pad state
        /// </summary>
        /// <param name="gamePadState">
        ///   Standard game pad state the extended game pad state is initialized from
        /// </param>
        public ExtendedGamePadState ( ref GamePadState gamePadState )
        {
            // Axes
            {
                this.AvailableAxes =
                    ExtendedAxis.X |
                ExtendedAxis.Y |
                ExtendedAxis.RotationX |
                ExtendedAxis.RotationY;

                this.X = gamePadState.ThumbSticks.Left.X;
                this.Y = gamePadState.ThumbSticks.Left.Y;
                this.Z = 0.0f;
                this.VelocityX = this.VelocityY = this.VelocityZ = 0.0f;
                this.AccelerationX = this.AccelerationY = this.AccelerationZ = 0.0f;
                this.ForceX = this.ForceY = this.ForceZ = 0.0f;

                this.RotationX = gamePadState.ThumbSticks.Right.X;
                this.RotationY = gamePadState.ThumbSticks.Right.Y;
                this.RotationZ = 0.0f;
                this.AngularVelocityX = this.AngularVelocityY = this.AngularVelocityZ = 0.0f;
                this.AngularAccelerationX = 0.0f;
                this.AngularAccelerationY = 0.0f;
                this.AngularAccelerationZ = 0.0f;
                this.TorqueX = this.TorqueY = this.TorqueZ = 0.0f;
            }

            // Buttons
            {
                this.ButtonCount = 11;
                this.buttonState1 =
                    ( gamePadState.IsButtonDown ( Buttons.A ) ? 1UL : 0UL ) |
                ( gamePadState.IsButtonDown ( Buttons.B ) ? 2UL : 0UL ) |
                ( gamePadState.IsButtonDown ( Buttons.X ) ? 4UL : 0UL ) |
                ( gamePadState.IsButtonDown ( Buttons.Y ) ? 8UL : 0UL ) |
                ( gamePadState.IsButtonDown ( Buttons.LeftShoulder ) ? 16UL : 0UL ) |
                ( gamePadState.IsButtonDown ( Buttons.RightShoulder ) ? 32UL : 0UL ) |
                ( gamePadState.IsButtonDown ( Buttons.Back ) ? 64UL : 0UL ) |
                ( gamePadState.IsButtonDown ( Buttons.Start ) ? 128UL : 0UL ) |
                ( gamePadState.IsButtonDown ( Buttons.LeftStick ) ? 256UL : 0UL ) |
                ( gamePadState.IsButtonDown ( Buttons.RightStick ) ? 512UL : 0UL ) |
                ( gamePadState.IsButtonDown ( Buttons.BigButton ) ? 1024UL : 0UL );
                this.buttonState2 = 0;
            }

            // Sliders
            {
                this.AvailableSliders =
                    ExtendedSliders.Slider1 |
                ExtendedSliders.Slider2;

                this.Slider1 = gamePadState.Triggers.Left;
                this.Slider2 = gamePadState.Triggers.Right;
                this.VelocitySlider1 = this.VelocitySlider2 = 0.0f;
                this.AccelerationSlider1 = this.AccelerationSlider2 = 0.0f;
                this.ForceSlider1 = this.ForceSlider2 = 0.0f;
            }

            // PoVs
            {
                this.PovCount = 1;
                this.Pov1 = ExtendedGamePadState.PovFromDpad ( gamePadState.DPad );
                this.Pov2 = -1;
                this.Pov3 = -1;
                this.Pov4 = -1;
            }
        }
    }
}

