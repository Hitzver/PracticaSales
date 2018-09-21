using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Reflection;
using System.Resources;
using PracticaSales.Interfaces;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PracticaSales.Helpers
{
    [ContentProperty("Text")]
    public class TranslateExtension : IMarkupExtension
    {
        readonly CultureInfo ci;
        const string ResourceId = "PracticaSales.Resources.Resource";

        static readonly Lazy<ResourceManager> ResMgr =
            new Lazy<ResourceManager>(() => new ResourceManager(
                ResourceId,
                typeof(TranslateExtension).GetTypeInfo().Assembly));

        public TranslateExtension()
        {
            ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
        }

        public string Text { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Text == null)
            {
                return "";
            }

            var translation = ResMgr.Value.GetString(Text, ci);

            if (translation == null)
            {
#if DEBUG
                throw new ArgumentException(
                    String.Format(
                        "Key '{0}' was not found in resources '{1}' for culture '{2}'.",
                        Text, ResourceId, ci.Name), "Text");
#else
                translation = Text; // returns the key, which GETS DISPLAYED TO THE USER
#endif
            }

            return translation;
        }
    }

}
