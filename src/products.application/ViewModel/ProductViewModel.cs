﻿namespace products.application.ViewModel
{
    public record ProductViewModel(string Id, string Name, decimal Price, int Stock);
    public record AddProductViewModel(string Name, decimal Price, int Stock);
}