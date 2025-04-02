﻿using Microsoft.Extensions.DependencyInjection;

namespace PaletteFromImage.AppDomain.Setup;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPaletteFromImage(this IServiceCollection services)
    {
        services.AddSingleton<IFileUtils, FileUtils>();
        services.AddSingleton<IClusteringAlgorithm, KMeansClusteringAlgorithm>();
        services.AddSingleton<IPaletteGenerator, PaletteGenerator>();

        return services;
    }
}