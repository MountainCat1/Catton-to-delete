﻿namespace Account.Application.Extensions;

public static class ConfigurationBuilderExtensions
{
    // public static TConfiguration GetConfiguration<TConfiguration>(this ConfigurationManager configurationManager, string path, string sectionName) 
    //     where TConfiguration : new()
    // {
    //     configurationManager.AddJsonFile(path);
    //     var configuration = new TConfiguration();
    //     configurationManager.GetSection(sectionName).Bind(configuration);
    //     return configuration;
    // }
    //
    // public static TConfiguration GetConfiguration<TConfiguration>(this ConfigurationManager configurationManager, string path) 
    //     where TConfiguration : new()
    // {
    //     return configurationManager.GetConfiguration<TConfiguration>(path, typeof(TConfiguration).Name);
    // }
    
    public static TConfiguration GetConfiguration<TConfiguration>(
        this ConfigurationManager configurationManager, string sectionName) 
        where TConfiguration : new()
    {
        var configuration = new TConfiguration();
        configurationManager.GetSection(sectionName).Bind(configuration);
        return configuration;
    }

    public static TConfiguration GetConfiguration<TConfiguration>(this ConfigurationManager configurationManager) 
        where TConfiguration : new()
    {
        return configurationManager.GetConfiguration<TConfiguration>(typeof(TConfiguration).Name);
    }
}