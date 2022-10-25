using System;

namespace JayToolbox.Integrations.Jira.AspNetCore;

public class JiraOptions
{
    public const string SectionName = "Jira";

    public string? Server { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }

    public void Validate()
    {
        if (Server == null)
            throw new ArgumentException("Jira Server address must be specified.", nameof(Server));
    }
}