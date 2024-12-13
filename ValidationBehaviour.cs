using Microsoft.Maui.Controls; // Or Xamarin.Forms if you're using Xamarin
using System;
namespace IancauMariaLab7
{
    public class ValidationBehaviour : Behavior<Editor>
    {
        protected override void OnAttachedTo(Editor editor)
        {
            base.OnAttachedTo(editor);
            editor.TextChanged += OnEntryTextChanged;
        }

        protected override void OnDetachingFrom(Editor editor)
        {
            base.OnDetachingFrom(editor);
            editor.TextChanged -= OnEntryTextChanged;
        }

        // The TextChanged event handler
        private void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            if (sender is Editor editor)
            {
                // Change background color based on text content
                editor.BackgroundColor = string.IsNullOrEmpty(args.NewTextValue)
                    ? Color.FromRgba("#AA4A44")  // Red if empty
                    : Color.FromRgba("#FFFFFF"); // White if there's text
            }
        }
    }
}
