namespace Raf.Utils.Shared.MimeTypes;

public class MimeTypeUtils
{
    private static IDictionary<string, string> _mappings = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase)
    {
        {".png", "image/png"},
        {".jpeg", "image/jpeg"}
    };

    public static string GetMimeType(string extension)
    {
        if (extension == null)
        {
            throw new ArgumentNullException("extension");
        }

        if (!extension.StartsWith("."))
        {
            extension = "." + extension;
        }
        
        string mime;
        
        return _mappings.TryGetValue(extension, out mime) ? mime : "";
    }
}