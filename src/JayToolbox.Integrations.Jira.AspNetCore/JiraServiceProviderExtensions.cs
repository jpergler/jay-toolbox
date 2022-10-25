using System;
using Microsoft.Extensions.DependencyInjection;

namespace JayToolbox.Integrations.Jira.AspNetCore;

public static class JiraServiceCollectionExtensions
{
    public static IServiceCollection AddJira(
        this IServiceCollection services,
        string baseUri,
        string? username = null,
        string? password = null)
    {
        var options = new JiraOptions
        {
            Server = baseUri,
            Username = username,
            Password = password
        };

        return AddJiraWithBasicAuth(services, options);
    }

    public static IServiceCollection AddJiraWithBasicAuth(
        this IServiceCollection services, Action<JiraOptions> configureOptions)
    {
        var options = new JiraOptions();
        configureOptions(options);

        return AddJiraWithBasicAuth(services, options);
    }

    public static IServiceCollection AddJiraWithBasicAuth(
        this IServiceCollection services, JiraOptions options)
    {
        options.Validate();

        services.AddScoped(p =>
            Atlassian.Jira.Jira.CreateRestClient
            (
                url: options.Server!,
                username: options.Username,
                password: options.Password
            ));

        AddJiraServices(services);

        return services;
    }

    static void AddJiraServices(IServiceCollection services)
    {
        services.AddScoped(p => p.GetRequiredService<Atlassian.Jira.Jira>().Versions);
        services.AddScoped(p => p.GetRequiredService<Atlassian.Jira.Jira>().Components);
        services.AddScoped(p => p.GetRequiredService<Atlassian.Jira.Jira>().Priorities);
        services.AddScoped(p => p.GetRequiredService<Atlassian.Jira.Jira>().Resolutions);
        services.AddScoped(p => p.GetRequiredService<Atlassian.Jira.Jira>().Statuses);
        services.AddScoped(p => p.GetRequiredService<Atlassian.Jira.Jira>().Links);
        services.AddScoped(p => p.GetRequiredService<Atlassian.Jira.Jira>().RemoteLinks);
        services.AddScoped(p => p.GetRequiredService<Atlassian.Jira.Jira>().IssueTypes);
        services.AddScoped(p => p.GetRequiredService<Atlassian.Jira.Jira>().Fields);
        services.AddScoped(p => p.GetRequiredService<Atlassian.Jira.Jira>().Issues);
        services.AddScoped(p => p.GetRequiredService<Atlassian.Jira.Jira>().Users);
        services.AddScoped(p => p.GetRequiredService<Atlassian.Jira.Jira>().Groups);
        services.AddScoped(p => p.GetRequiredService<Atlassian.Jira.Jira>().Projects);
        services.AddScoped(p => p.GetRequiredService<Atlassian.Jira.Jira>().Screens);
        services.AddScoped(p => p.GetRequiredService<Atlassian.Jira.Jira>().ServerInfo);
        services.AddScoped(p => p.GetRequiredService<Atlassian.Jira.Jira>().Filters);
        services.AddScoped(p => p.GetRequiredService<Atlassian.Jira.Jira>().RestClient);
    }
}