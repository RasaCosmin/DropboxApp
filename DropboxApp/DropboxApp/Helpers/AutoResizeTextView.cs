using System;
using Android.Widget;
using Java.Lang;
using Android.Util;
using Android.Runtime;
using Android.Graphics;
using Android.Text;
using Android.Content;

namespace DropboxApp
{
	public class AutoResizeTextView : TextView
	{
		private const int MinTextSize = 10;
		private const bool ShrinkTextSize = true;
		private const char Ellipsis = '\u2026';
		private const float LineSpacingMultiplierMultiline = 0.8f;
		private const float LineSpacingMultiplierSingleline = 1f;
		private const float LineSpacingExtra = 0.0f;

		private static ICharSequence _originalText;
		private float _maxTextSize;
		private float _minTextSize;
		private bool _inMeasure = false;
		private bool _shrinkTextSize;

		private static Context _context;

		public AutoResizeTextView (Context context) : base (context)
		{
			Init (context, null);
		}

		public AutoResizeTextView (Context context, IAttributeSet attrs) : base (context, attrs)
		{
			Init (context, attrs);
		}

		public AutoResizeTextView (Context context, IAttributeSet attrs, int defStyle) : base (context, attrs, defStyle)
		{
			Init (context, attrs);
		}

		public AutoResizeTextView (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer)
		{
			Init (null, null);
		}

		private void Init (Context context, IAttributeSet attrs)
		{
			_maxTextSize = TextSize;

			if (context != null && _context == null)
				_context = context;

			if (attrs != null) {
				var a = context.ObtainStyledAttributes (attrs, Resource.Styleable.AutoScaleTextView);
				_minTextSize = a.GetFloat (Resource.Styleable.AutoScaleTextView_minTextSize, MinTextSize);
				_shrinkTextSize = a.GetBoolean (Resource.Styleable.AutoScaleTextView_shrinkTextSize, ShrinkTextSize);
				a.Recycle ();
			}
		}

		public override void SetTextSize (ComplexUnitType unit, float size)
		{
			_maxTextSize = size;
			base.SetTextSize (unit, size);
		}

		public ICharSequence GetOriginalText ()
		{
			//if (_originalText == null)
			return new Java.Lang.String (Text);

			//return _originalText;
		}

		public override void SetText (ICharSequence text, BufferType type)
		{
			if (!_inMeasure)
				_originalText = text;
			base.SetText (text, type);
		}

		protected override void OnDraw (Canvas canvas)
		{
			base.OnDraw (canvas);
			ShrinkTextToFit ();
		}

		protected void ShrinkTextToFit ()
		{
			int h = Height;
			int l = LineCount;

			Rect r = new Rect ();
			var y1 = GetLineBounds (0, r);
			var y2 = GetLineBounds (l - 1, r);

			var size = TextSize;

			if ((y1 > Height || MeasureTextWidth (Paint, Text) > Width) && size >= 8.0f) {
				var value = 2;
				if (_context != null) {
					size = DpToPixels (size);
					value = (int)DpToPixels (value);
				} else
					return;

				SetTextSize (ComplexUnitType.Sp, size - value);
				ShrinkTextToFit ();
			}
		}

		protected override void OnMeasure (int widthMeasureSpec, int heightMeasureSpec)
		{
			base.OnMeasure (widthMeasureSpec, heightMeasureSpec);
			return;

			_inMeasure = true;
			//base.OnMeasure (widthMeasureSpec, heightMeasureSpec);
			try {
				var availableWidth = MeasureSpec.GetSize (widthMeasureSpec) - CompoundPaddingLeft - CompoundPaddingRight;
				var availableHeight = MeasureSpec.GetSize (heightMeasureSpec) - CompoundPaddingTop - CompoundPaddingBottom;

				_originalText = GetOriginalText ();

				if (_originalText == null || _originalText.Length () == 0 || availableWidth <= 0) {
					SetMeasuredDimension (widthMeasureSpec, heightMeasureSpec);
					return;
				}

				var textPaint = Paint;
				var targetTextSize = _maxTextSize;
				var originalText = new Java.Lang.String (_originalText.ToString ());
				var finalText = originalText;

				var textSize = GetTextSize (originalText, textPaint, targetTextSize);
				var textExcedsBounds = textSize.Height () > availableHeight || textSize.Width () > availableWidth;

				if (_shrinkTextSize && textExcedsBounds) {
					var heightMultiplier = availableHeight / (float)textSize.Height ();
					var widthMultiplier = availableWidth / (float)textSize.Width ();
					var multiplier = System.Math.Min (heightMultiplier, widthMultiplier);

					targetTextSize = System.Math.Max (targetTextSize * multiplier, _minTextSize);

					textSize = GetTextSize (finalText, textPaint, targetTextSize);
				}
				if (textSize.Width () > availableWidth) {
					var modifiedText = new StringBuilder ();
					var lines = originalText.Split (Java.Lang.JavaSystem.GetProperty ("line.separator"));
					for (var i = 0; i < lines.Length; i++) {
						modifiedText.Append (ResizeLine (textPaint, lines [i], availableWidth));
						if (i != lines.Length - 1)
							modifiedText.Append (Java.Lang.JavaSystem.GetProperty ("line.separator"));
					}

					finalText = new Java.Lang.String (modifiedText);
					textSize = GetTextSize (finalText, textPaint, targetTextSize);
				}

				textPaint.TextSize = targetTextSize;

				var isMultiline = finalText.IndexOf ('\n') > -1;
				if (isMultiline) {
					SetLineSpacing (LineSpacingExtra, LineSpacingMultiplierMultiline);
					SetIncludeFontPadding (false);
				} else {
					SetLineSpacing (LineSpacingExtra, LineSpacingMultiplierSingleline);
					SetIncludeFontPadding (true);
				}

				Text = finalText + "\u200B";

				var measuredWidth = textSize.Width () + CompoundPaddingLeft + CompoundPaddingRight;
				var measureHeight = textSize.Height () + CompoundPaddingTop + CompoundPaddingBottom;

				measureHeight = System.Math.Max (measureHeight, MeasureSpec.GetSize (heightMeasureSpec));
				SetMeasuredDimension (measuredWidth, measureHeight);
			} finally {
				_inMeasure = false;

			}
		}

		private Rect GetTextSize (Java.Lang.String text, TextPaint textPaint, float textSize)
		{
			textPaint.TextSize = textSize;
			var layout = new StaticLayout (text, textPaint, Integer.MaxValue, Layout.Alignment.AlignNormal, 1f, 0f, true);
			var textWidth = 0;
			var lines = text.Split (Java.Lang.JavaSystem.GetProperty ("line.separator"));
			for (var i = 0; i < lines.Length; ++i)
				textWidth = System.Math.Max (textWidth, MeasureTextWidth (textPaint, lines [i]));

			return new Rect (0, 0, textWidth, layout.Height);
		}

		protected int MeasureTextWidth (TextPaint textPaint, string line)
		{
			return Java.Lang.Math.Round (textPaint.MeasureText (line));
		}

		private System.String ResizeLine (TextPaint textPaint, string line, int availableWidth)
		{
			var texteWidth = MeasureTextWidth (textPaint, line);
			var lastDeletePos = -1;

			var builder = new StringBuilder (line);
			while (texteWidth > availableWidth && builder.Length () > 0) {
				lastDeletePos = builder.Length () / 2;
				builder.DeleteCharAt (builder.Length () / 2);
				var textToMeasure = builder.ToString () + Ellipsis;
				texteWidth = MeasureTextWidth (textPaint, textToMeasure);
			}

			if (lastDeletePos > -1)
				builder.Insert (lastDeletePos, Ellipsis);
			return builder.ToString ();
		}

		protected float DpToPixels (float dp)
		{
			float val;
			if (dp >= Resources.DisplayMetrics.Density)
				val = (dp / Resources.DisplayMetrics.Density);
			else
				val = dp;
			return val;
		}

		protected int PixelsToDp (int pixels)
		{
			return (int)TypedValue.ApplyDimension (ComplexUnitType.Dip, pixels, _context.Resources.DisplayMetrics);
		}
	}
}

