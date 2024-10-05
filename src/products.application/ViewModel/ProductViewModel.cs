﻿namespace products.application.ViewModel
{
    public record ProductViewModel(string Id, string Name, decimal Price, int Stock);
    public record UpdateProductViewModel(string Name, decimal Price, int Stock);
}
