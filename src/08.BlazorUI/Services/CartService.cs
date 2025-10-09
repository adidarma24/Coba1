using System.Collections.Generic;

namespace MyApp.BlazorUI.Services
{
    // Minimal cart service used by the Blazor UI. Kept intentionally small to avoid
    // coupling with other projects; expand later if the app needs shared cart state.
    public class CartService
    {
        public List<object> Items { get; } = new();

        public void Add(object item)
        {
            Items.Add(item);
        }

        public void Remove(object item)
        {
            Items.Remove(item);
        }

        public void Clear()
        {
            Items.Clear();
        }
    }
}
