using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace APSA.Portable.Views.Controls.Slideout
{
    public class SlideoutControl : RelativeLayout
    {
        #region Constants

        /// <summary>
        /// Default animation length
        /// </summary>
        private const uint DefaultAnimationLength = 250;

        #endregion

        #region BindableProperty

        /// <summary>
        /// Bindable property for the <c>SlideOutDirection</c>
        /// </summary>
        public static readonly BindableProperty SlideOutDirectionProperty =
                                    BindableProperty.Create(
                                        declaringType: typeof(SlideoutControl),
                                        returnType: typeof(SlideoutDirection),
                                        propertyName: "SlideOutDirection",
                                        defaultValue: SlideoutDirection.Left,
                                        defaultBindingMode: BindingMode.TwoWay,
                                        validateValue: null,
                                        propertyChanged: SlideOutDirectionChanged
                                        );

        /// <summary>
        /// Bindable property for the <c>WidthScale</c>
        /// </summary>
        public static readonly BindableProperty SizeScaleProperty =
                                        BindableProperty.Create(
                                        declaringType: typeof(SlideoutControl),
                                        returnType: typeof(float),
                                        propertyName: "SizeScale",
                                        defaultValue: 0.5f,
                                        defaultBindingMode: BindingMode.TwoWay,
                                        validateValue: null,
                                        propertyChanged: SizeScaleChanged
                                        );
        /// <summary>
        /// Bindable property for the <c>FixedWidth</c>
        /// </summary>
        //public static readonly BindableProperty FixedSizeProperty =
        //                                BindableProperty.Create(
        //                                declaringType: typeof(SlideoutControl),
        //                                returnType: typeof(double),
        //                                propertyName: "FixedSize",
        //                                defaultValue: -1,
        //                                defaultBindingMode: BindingMode.OneWay,
        //                                validateValue: null,
        //                                propertyChanged: FixedSizeChanged
        //                                );

        /// <summary>
        /// Bindable property for the <c>Content</c>
        /// </summary>
        public static readonly BindableProperty ContentProperty =
                                        BindableProperty.Create(
                                        declaringType: typeof(SlideoutControl),
                                        returnType: typeof(View),
                                        propertyName: "Content",
                                        defaultValue: default(View),
                                        defaultBindingMode: BindingMode.TwoWay,
                                        validateValue: null,
                                        propertyChanged: ContentChanged
                                        );
        /// <summary>
        /// Bindable property for <c>IsPresented</c>
        /// </summary>
        public static readonly BindableProperty IsPresentedProperty =
                                        BindableProperty.Create(
                                        declaringType: typeof(SlideoutControl),
                                        returnType: typeof(bool),
                                        propertyName: "IsPresented",
                                        defaultValue: default(bool),
                                        defaultBindingMode: BindingMode.TwoWay,
                                        validateValue: null,
                                        propertyChanged: IsPresentedChanged
                                        );

        #endregion

        #region Main Properties

        /// <summary>
        /// The sliding direction of the <c>SlideoutView</c> 
        /// </summary>
        public SlideoutDirection SlideOutDirection
        {
            get { return (SlideoutDirection)this.GetValue(SlideOutDirectionProperty); }
            set { this.SetValue(SlideOutDirectionProperty, value); }
        }
        /// <summary>
        /// Gets/Sets the width of the <c>SlideoutView</c> in relation to the parent width.
        /// Default value is <c>0.5</c>
        /// </summary>
        public float SizeScale
        {
            get { return (float)this.GetValue(SizeScaleProperty); }
            set { this.SetValue(SizeScaleProperty, value); }
        }
        /// <summary>
        /// Gets/Sets a fixed width of the <c>Slideoutview</c>. If this is set, it will override the <c>WidthScale</c>. 
        /// Default value is <value>-1</value>
        /// </summary>
        //public double FixedSize
        //{
        //    get { return (double)this.GetValue(FixedSizeProperty); }
        //    set { this.SetValue(FixedSizeProperty, value); }
        //}
        /// <summary>
        /// Gets/Sets the content which is displayed inside the <c>Slideoutview</c>
        /// </summary>
        public View Content
        {
            get { return (View)this.GetValue(ContentProperty); }
            set { this.SetValue(ContentProperty, value); }
        }
        /// <summary>
        /// Gets/Sets if the <c>SlideoutView</c> is currently presented.
        /// If this changes, it will toggle the animation
        /// </summary>
        public bool IsPresented
        {
            get { return (bool)this.GetValue(IsPresentedProperty); }
            set { this.SetValue(IsPresentedProperty, value); }
        }

        /// <summary>
        /// Gets the current size of the <c>SlideoutView</c>
        /// </summary>
        public double Size
        {
            get
            {
                if (this.Parent == null) return 0;

                if (!this.IsPresented) return 0;
                //if (this.FixedSize > 0) return this.FixedSize;

                if (this.SlideOutDirection == SlideoutDirection.Left ||
                    this.SlideOutDirection == SlideoutDirection.Right)
                    return ((View)this.Parent).Width * this.SizeScale;
                else
                    return ((View)this.Parent).Height * this.SizeScale;
            }
        }

        /// <summary>
        /// The width used to layout the <c>SlideoutView</c> 
        /// </summary>
        private double LayoutToWidth
        {
            get
            {
                if (this.Parent == null) return 0;

                return
                    (this.SlideOutDirection == SlideoutDirection.Left ||
                    this.SlideOutDirection == SlideoutDirection.Right) ?
                        this.Size :
                        ((View)this.Parent).Width;
            }
        }

        /// <summary>
        /// The height used to layout the <c>SlideoutView</c> 
        /// </summary>
        private double LayoutToHeight
        {
            get
            {
                if (this.Parent == null) return 0;

                return
                    (this.SlideOutDirection == SlideoutDirection.Top ||
                    this.SlideOutDirection == SlideoutDirection.Bottom) ?
                        this.Size :
                        ((View)this.Parent).Height;
            }
        }

        #endregion
       
        #region Property change Handlers

        /// <summary>
        /// Handle property changed of <c>SlideOutDirection</c>
        /// </summary>
        /// <param name="obj">The <c>SlideoutView</c></param>
        /// <param name="oldValue">Old value</param>
        /// <param name="newValue">New value</param>
        private static void SlideOutDirectionChanged(BindableObject obj, object oldValue, object newValue)
        {
            var view = obj as SlideoutControl;
            if (view == null) return;

            if (view.IsPresented)
                throw new InvalidOperationException("Can't change SlideOutDirection when SlideoutView is currently presented");

            view.ForceLayout();
        }
        /// <summary>
        /// Handle property changed of <c>SizeScale</c>
        /// </summary>
        /// <param name="obj">The <c>SlideoutView</c></param>
        /// <param name="oldValue">Old value</param>
        /// <param name="newValue">New value</param>
        private static void SizeScaleChanged(BindableObject obj, object oldValue, object newValue)
        {
            var view = obj as SlideoutControl;
            if (view == null) return;

            if ((float)newValue < 0.1f || (float)newValue > 1)
                throw new ArgumentException("SizeScale must be between 0 and 1");

            view.ForceLayout();
        }
        /// <summary>
        /// Handle property changed of <c>FixedSize</c>
        /// </summary>
        /// <param name="obj">The <c>SlideoutView</c></param>
        /// <param name="oldValue">Old value</param>
        /// <param name="newValue">New value</param>
        private static void FixedSizeChanged(BindableObject obj, object oldValue, object newValue)
        {
            var view = obj as SlideoutControl;
            if (view == null) return;

            //view.ForceLayout();
        }
        /// <summary>
        /// Handle property changed of <c>Content</c>
        /// </summary>
        /// <param name="obj">The <c>SlideoutView</c></param>
        /// <param name="oldValue">Old value</param>
        /// <param name="newValue">New value</param>
        private static void ContentChanged(BindableObject obj, object oldValue, object newValue)
        {
            var view = obj as SlideoutControl;
            if (view == null) return;

            if ((View)oldValue != null)
                view.Children.Remove((View)oldValue);

            if (newValue != null)
            {
                view.Children.Add(
                    (View)newValue,
                    Constraint.RelativeToParent(
                        view.GetX),
                    Constraint.RelativeToParent(
                        view.GetY),
                    Constraint.RelativeToParent((p) => view.LayoutToWidth),
                    Constraint.RelativeToParent((p) => view.LayoutToHeight));
            }
        }
        /// <summary>
        /// Handle property changed of <c>IsPresented</c>
        /// </summary>
        /// <param name="obj">The <c>SlideoutView</c></param>
        /// <param name="oldValue">Old value</param>
        /// <param name="newValue">New value</param>
        private static void IsPresentedChanged(BindableObject obj, object oldValue, object newValue)
        {
            var view = obj as SlideoutControl;
            if (view == null) return;

            if ((bool)newValue)
                view.Expand();
            else
                view.Hide();
        }
        #endregion

        #region Methods

        /// <summary>
        /// Returns the horizontal position based on the parent
        /// </summary>
        /// <param name="layout">The parent</param>
        /// <returns>The horizontal position</returns>
        private double GetX(RelativeLayout layout)
        {
            double x = 0;
            if (this.SlideOutDirection == SlideoutDirection.Right)
                x = layout.Width;
            return x;
        }
        /// <summary>
        /// Returns the vertical position based on the parent
        /// </summary>
        /// <param name="layout">The parent</param>
        /// <returns>The vertical position</returns>
        private double GetY(RelativeLayout layout)
        {
            double y = 0;
            if (this.SlideOutDirection == SlideoutDirection.Bottom)
                y = layout.Height;
            return y;
        }
        /// <summary>
        /// Expand the <c>SlideoutView</c>
        /// </summary>
        private void Expand()
        {
            double x = this.Content.X;
            double y = this.Content.Y;

            if (this.SlideOutDirection == SlideoutDirection.Right)
            {
                //if (this.FixedSize > 0)
                //    x = ((View)this.Parent).Width - this.FixedSize;
                //else
                    x = ((View)this.Parent).Width - ((View)this.Parent).Width * this.SizeScale;
            }
            if (this.SlideOutDirection == SlideoutDirection.Bottom)
            {
                //if (this.FixedSize > 0)
                //    y = ((View)this.Parent).Height - this.FixedSize;
                //else
                    y = ((View)this.Parent).Height - ((View)this.Parent).Height * this.SizeScale;
            }

            this.Content.LayoutTo(
                new Rectangle(
                    x,
                    y,
                    this.LayoutToWidth,
                    this.LayoutToHeight),
                DefaultAnimationLength);

        }
        /// <summary>
        /// Hides the <c>SlideOutView</c>
        /// </summary>
        private void Hide()
        {
            var x = this.GetX(this);
            var y = this.GetY(this);

            this.Content.LayoutTo(
                new Rectangle(
                    x,
                    y,
                    this.LayoutToWidth,
                    this.LayoutToHeight),
                DefaultAnimationLength);
        }

        #endregion
    }
}
