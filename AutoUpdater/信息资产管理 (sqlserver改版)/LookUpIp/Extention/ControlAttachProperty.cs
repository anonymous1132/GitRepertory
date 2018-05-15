using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using CheckBox = System.Windows.Controls.CheckBox;
using ComboBox = System.Windows.Controls.ComboBox;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using RadioButton = System.Windows.Controls.RadioButton;
using RichTextBox = System.Windows.Controls.RichTextBox;
using TextBox = System.Windows.Controls.TextBox;
namespace LookUpIp.Extention
{
  public static class ControlAttachProperty
    {
        /************************************ Attach Property **************************************/

        #region FocusBackground 获得焦点背景色，

        public static readonly DependencyProperty FocusBackgroundProperty = DependencyProperty.RegisterAttached(
            "FocusBackground", typeof(Brush), typeof(ControlAttachProperty), new FrameworkPropertyMetadata(null));

        public static void SetFocusBackground(DependencyObject element, Brush value)
        {
            element.SetValue(FocusBackgroundProperty, value);
        }

        public static Brush GetFocusBackground(DependencyObject element)
        {
            return (Brush)element.GetValue(FocusBackgroundProperty);
        }

        #endregion

        #region FocusForeground 获得焦点前景色，

        public static readonly DependencyProperty FocusForegroundProperty = DependencyProperty.RegisterAttached(
            "FocusForeground", typeof(Brush), typeof(ControlAttachProperty), new FrameworkPropertyMetadata(null));

        public static void SetFocusForeground(DependencyObject element, Brush value)
        {
            element.SetValue(FocusForegroundProperty, value);
        }

        public static Brush GetFocusForeground(DependencyObject element)
        {
            return (Brush)element.GetValue(FocusForegroundProperty);
        }

        #endregion

        #region MouseOverBackgroundProperty 鼠标悬浮背景色

        public static readonly DependencyProperty MouseOverBackgroundProperty = DependencyProperty.RegisterAttached(
            "MouseOverBackground", typeof(Brush), typeof(ControlAttachProperty), new FrameworkPropertyMetadata(null));

        public static void SetMouseOverBackground(DependencyObject element, Brush value)
        {
            element.SetValue(MouseOverBackgroundProperty, value);
        }

        public static Brush MouseOverBackground(DependencyObject element)
        {
            return (Brush)element.GetValue(FocusBackgroundProperty);
        }

        #endregion

        #region MouseOverForegroundProperty 鼠标悬浮前景色

        public static readonly DependencyProperty MouseOverForegroundProperty = DependencyProperty.RegisterAttached(
            "MouseOverForeground", typeof(Brush), typeof(ControlAttachProperty), new FrameworkPropertyMetadata(null));

        public static void SetMouseOverForeground(DependencyObject element, Brush value)
        {
            element.SetValue(MouseOverForegroundProperty, value);
        }

        public static Brush MouseOverForeground(DependencyObject element)
        {
            return (Brush)element.GetValue(FocusForegroundProperty);
        }

        #endregion

        #region FocusBorderBrush 焦点边框色，输入控件

        public static readonly DependencyProperty FocusBorderBrushProperty = DependencyProperty.RegisterAttached(
            "FocusBorderBrush", typeof(Brush), typeof(ControlAttachProperty), new FrameworkPropertyMetadata(null));
        public static void SetFocusBorderBrush(DependencyObject element, Brush value)
        {
            element.SetValue(FocusBorderBrushProperty, value);
        }
        public static Brush GetFocusBorderBrush(DependencyObject element)
        {
            return (Brush)element.GetValue(FocusBorderBrushProperty);
        }

        #endregion

        #region MouseOverBorderBrush 鼠标进入边框色，输入控件

        public static readonly DependencyProperty MouseOverBorderBrushProperty =
            DependencyProperty.RegisterAttached("MouseOverBorderBrush", typeof(Brush), typeof(ControlAttachProperty),
                new FrameworkPropertyMetadata(Brushes.Transparent,
                    FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// Sets the brush used to draw the mouse over brush.
        /// </summary>
        public static void SetMouseOverBorderBrush(DependencyObject obj, Brush value)
        {
            obj.SetValue(MouseOverBorderBrushProperty, value);
        }

        /// <summary>
        /// Gets the brush used to draw the mouse over brush.
        /// </summary>
        [AttachedPropertyBrowsableForType(typeof(TextBox))]
        [AttachedPropertyBrowsableForType(typeof(CheckBox))]
        [AttachedPropertyBrowsableForType(typeof(RadioButton))]
        [AttachedPropertyBrowsableForType(typeof(DatePicker))]
        [AttachedPropertyBrowsableForType(typeof(ComboBox))]
        [AttachedPropertyBrowsableForType(typeof(RichTextBox))]
        public static Brush GetMouseOverBorderBrush(DependencyObject obj)
        {
            return (Brush)obj.GetValue(MouseOverBorderBrushProperty);
        }

        #endregion

        #region AttachContentProperty 附加组件模板
        /// <summary>
        /// 附加组件模板
        /// </summary>
        public static readonly DependencyProperty AttachContentProperty = DependencyProperty.RegisterAttached(
            "AttachContent", typeof(ControlTemplate), typeof(ControlAttachProperty), new FrameworkPropertyMetadata(null));

        public static ControlTemplate GetAttachContent(DependencyObject d)
        {
            return (ControlTemplate)d.GetValue(AttachContentProperty);
        }

        public static void SetAttachContent(DependencyObject obj, ControlTemplate value)
        {
            obj.SetValue(AttachContentProperty, value);
        }
        #endregion

        #region WatermarkProperty 水印
        /// <summary>
        /// 水印
        /// </summary>
        public static readonly DependencyProperty WatermarkProperty = DependencyProperty.RegisterAttached(
            "Watermark", typeof(string), typeof(ControlAttachProperty), new FrameworkPropertyMetadata(""));

        public static string GetWatermark(DependencyObject d)
        {
            return (string)d.GetValue(WatermarkProperty);
        }

        public static void SetWatermark(DependencyObject obj, string value)
        {
            obj.SetValue(WatermarkProperty, value);
        }
        #endregion

        #region FIconProperty 字体图标
        /// <summary>
        /// 字体图标
        /// </summary>
        public static readonly DependencyProperty FIconProperty = DependencyProperty.RegisterAttached(
            "FIcon", typeof(string), typeof(ControlAttachProperty), new FrameworkPropertyMetadata(""));

        public static string GetFIcon(DependencyObject d)
        {
            return (string)d.GetValue(FIconProperty);
        }

        public static void SetFIcon(DependencyObject obj, string value)
        {
            obj.SetValue(FIconProperty, value);
        }
        #endregion

        #region FIconSizeProperty 字体图标大小
        /// <summary>
        /// 字体图标
        /// </summary>
        public static readonly DependencyProperty FIconSizeProperty = DependencyProperty.RegisterAttached(
            "FIconSize", typeof(double), typeof(ControlAttachProperty), new FrameworkPropertyMetadata(12D));

        public static double GetFIconSize(DependencyObject d)
        {
            return (double)d.GetValue(FIconSizeProperty);
        }

        public static void SetFIconSize(DependencyObject obj, double value)
        {
            obj.SetValue(FIconSizeProperty, value);
        }
        #endregion

        #region FIconMarginProperty 字体图标边距
        /// <summary>
        /// 字体图标
        /// </summary>
        public static readonly DependencyProperty FIconMarginProperty = DependencyProperty.RegisterAttached(
            "FIconMargin", typeof(Thickness), typeof(ControlAttachProperty), new FrameworkPropertyMetadata(null));

        public static Thickness GetFIconMargin(DependencyObject d)
        {
            return (Thickness)d.GetValue(FIconMarginProperty);
        }

        public static void SetFIconMargin(DependencyObject obj, Thickness value)
        {
            obj.SetValue(FIconMarginProperty, value);
        }
        #endregion

    


        /// <summary>
        /// 静态构造函数
        /// </summary>
        static ControlAttachProperty()
        {
            //ClearTextCommand

        }
    }
}
